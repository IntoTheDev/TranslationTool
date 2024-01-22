﻿using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TranslationTool.Log
{
    public sealed class Logger
    {
        private readonly RichTextBox _textBox;
        private readonly StringBuilder _builder;
        private readonly Color[] _colors;
        private readonly Color _defaultColor;

        public Logger(RichTextBox textBox)
        {
            _textBox = textBox;
            _builder = new StringBuilder();
            _colors = new[]
            {
                Color.Black,
                Color.Red,
            };
            _defaultColor = textBox.SelectionColor;
        }

        public void Log(string value, LogType type = LogType.Default)
        {
            _builder.Append("[").Append(DateTime.Now.ToShortTimeString()).Append("] ").Append(value).AppendLine();
            _textBox.SelectionColor = _colors[(int)type];
            _textBox.AppendText(_builder.ToString());
            _textBox.SelectionColor = _defaultColor;
            _builder.Clear();
        }

        public void Clear()
        {
            _textBox.Clear();
        }
    }
}