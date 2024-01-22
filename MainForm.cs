using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using TranslationTool.Log;
using TranslationTool.SentenceProcessors;

namespace TranslationTool
{
    public partial class MainForm : Form
    {
        private OpenFileDialog _script;
        private readonly ISentenceProcessor[] _processors;
        private readonly Logger _logger;
        private const int Count = 10;

        public MainForm()
        {
            InitializeComponent();

            _processors = new ISentenceProcessor[]
            {
                new NameProcessor(),
                new NamesProcessor(),
                new MessageProcessor(),
            };

            _logger = new Logger(RichTextBox);
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            _script = new OpenFileDialog();
            _script.Filter = "All JSON Files|*.json";

            if (_script.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var text = $"Loaded script: {_script.FileName}";

            FilePathLabel.Text = text;
            FormatButton.Enabled = true;
            ExtractButton.Enabled = true;
            InsertButton.Enabled = true;

            UpdateProgress();
            _logger.Log(text);
        }

        private void OnFormatClick(object sender, EventArgs e)
        {
            var sentences = ReadSentences(File.ReadAllText(_script.FileName));

            for (var i = 0; i < sentences.Length; i++)
            {
                ref var sentence = ref sentences[i];

                sentence.Id = i;
            }

            WriteSentences(sentences);
            _logger.Log("Formatted script.");
        }

        private void OnExtractClick(object sender, EventArgs e)
        {
            var sentences = ReadSentences(File.ReadAllText(_script.FileName));
            var list = new List<Sentence>();

            foreach (var sentence in sentences)
            {
                if (sentence.Translated)
                {
                    continue;
                }

                list.Add(sentence);

                if (list.Count == Count)
                {
                    break;
                }
            }

            Clipboard.SetText(JsonConvert.SerializeObject(list, Formatting.Indented));
            _logger.Log($"Extracted {list.Count.ToString()} sentences.");
        }

        private void OnInsertClick(object sender, EventArgs e)
        {
            Sentence[] translatedSentences;

            try
            {
                translatedSentences = ReadSentences(Clipboard.GetText());
            }
            catch (Exception exception)
            {
                _logger.Log("Failed to deserialize inserted sentences.", LogType.Error);
                return;
            }
            
            var sentences = ReadSentences(File.ReadAllText(_script.FileName));
            var count = 0;

            for (var i = 0; i < translatedSentences.Length; i++)
            {
                ref var translated = ref translatedSentences[i];

                for (var j = 0; j < sentences.Length; j++)
                {
                    ref var target = ref sentences[j];

                    if (target.Id != translated.Id)
                    {
                        continue;
                    }

                    if (target.Translated)
                    {
                        break;
                    }

                    var validated = true;

                    foreach (var processor in _processors)
                    {
                        if (processor.Validate(ref translated, ref target))
                        {
                            continue;
                        }

                        _logger.Log($"Sentence {translated.Id.ToString()} failed at {processor.GetType()}.", LogType.Error);
                        validated = false;
                        break;
                    }

                    if (!validated)
                    {
                        break;
                    }

                    foreach (var processor in _processors)
                    {
                        processor.Process(ref translated, ref target);
                    }

                    target.Translated = true;
                    count++;
                    break;
                }
            }

            WriteSentences(sentences);
            _logger.Log($"Inserted {count.ToString()} sentences.");
            OnExtractClick(default, EventArgs.Empty);
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            _logger.Clear();
        }

        private void UpdateProgress()
        {
            var sentences = ReadSentences(File.ReadAllText(_script.FileName));

            ProgressLabel.Text = $"{sentences.Count(sentence => sentence.Translated).ToString()}/{sentences.Length.ToString()}";
        }

        private Sentence[] ReadSentences(string text)
        {
            return JsonConvert.DeserializeObject<Sentence[]>(text);
        }

        private void WriteSentences(Sentence[] sentences)
        {
            File.WriteAllText(_script.FileName, JsonConvert.SerializeObject(sentences, Formatting.Indented));
            UpdateProgress();
        }

        private void OnKeyDown(object sender, KeyEventArgs key)
        {
            if (_script == null)
            {
                return;
            }
            
            switch (key.KeyData)
            {
                case Keys.C:
                    OnExtractClick(sender, EventArgs.Empty);
                    break;
                case Keys.V:
                    OnInsertClick(sender, EventArgs.Empty);
                    break;
            }
        }
    }
}