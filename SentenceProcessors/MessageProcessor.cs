namespace TranslationTool.SentenceProcessors
{
    public sealed class MessageProcessor : ISentenceProcessor
    {
        public bool Validate(ref Sentence translated, ref Sentence target)
        {
            return string.IsNullOrEmpty(target.Message) == string.IsNullOrEmpty(translated.Message);
        }

        public void Process(ref Sentence translated, ref Sentence target)
        {
            target.Message = translated.Message;
        }
    }
}