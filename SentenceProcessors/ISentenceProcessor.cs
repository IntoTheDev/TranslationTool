namespace TranslationTool.SentenceProcessors
{
    public interface ISentenceProcessor
    {
        bool Validate(ref Sentence translated, ref Sentence target);
        void Process(ref Sentence translated, ref Sentence target);
    }
}