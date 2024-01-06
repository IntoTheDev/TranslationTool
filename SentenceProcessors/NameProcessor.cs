namespace TranslationTool.SentenceProcessors
{
    public sealed class NameProcessor : ISentenceProcessor
    {
        public bool Validate(ref Sentence translated, ref Sentence target)
        {
            return string.IsNullOrEmpty(target.Name) == string.IsNullOrEmpty(translated.Name);
        }

        public void Process(ref Sentence translated, ref Sentence target)
        {
            target.Name = translated.Name;
        }
    }
}