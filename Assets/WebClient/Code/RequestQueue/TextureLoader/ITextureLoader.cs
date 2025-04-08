namespace WebClient
{
    public interface ITextureLoader
    {
        void StartNew(string url);
        bool IsAllLoaded();
        void Finish();
        bool IsLoading(string url);
    }
}