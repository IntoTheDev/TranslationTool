namespace TranslationTool.SentenceProcessors
{
    public sealed class NamesProcessor : ISentenceProcessor
    {
        public bool Validate(ref Sentence translated, ref Sentence target)
        {
            return (target.Names == null && translated.Names == null) || (target.Names != null && translated.Names.Length == target.Names.Length);
        }
        
        public void Process(ref Sentence translated, ref Sentence target)
        {
            target.Names = translated.Names;
        }
    }
}