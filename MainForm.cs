using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using TranslationTool.SentenceProcessors;

namespace TranslationTool
{
    public partial class MainForm : Form
    {
        private OpenFileDialog _script;
        private readonly ISentenceProcessor[] _processors;
        private const int Count = 10;

        public MainForm()
        {
            InitializeComponent();
            FormatButton.Enabled = false;
            ExtractButton.Enabled = false;
            InsertButton.Enabled = false;

            _processors = new ISentenceProcessor[]
            {
                new NameProcessor(),
                new NamesProcessor(),
                new MessageProcessor(),
            };
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            _script = new OpenFileDialog();
            _script.Filter = "All JSON Files|*.json";

            if (_script.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FilePathLabel.Text = $"Loaded script: {_script.FileName}";
            FormatButton.Enabled = true;
            ExtractButton.Enabled = true;
            InsertButton.Enabled = true;
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
        }

        private void OnInsertClick(object sender, EventArgs e)
        {
            var text = Clipboard.GetText().Replace("\"\"", "\"");
            var translatedSentences = ReadSentences(text);
            var sentences = ReadSentences(File.ReadAllText(_script.FileName));

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

                    var validated = true;

                    foreach (var processor in _processors)
                    {
                        if (processor.Validate(ref translated, ref target))
                        {
                            continue;
                        }

                        MessageBox.Show($"Sentence {translated.Id} failed at {processor.GetType()}", "Error", MessageBoxButtons.OK);
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
                    break;
                }
            }

            WriteSentences(sentences);
        }

        private Sentence[] ReadSentences(string text)
        {
            return JsonConvert.DeserializeObject<Sentence[]>(text);
        }

        private void WriteSentences(Sentence[] sentences)
        {
            File.WriteAllText(_script.FileName, JsonConvert.SerializeObject(sentences, Formatting.Indented));
        }
    }
}