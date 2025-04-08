namespace WebClient
{
    public interface ITextureLoader
    {
        void Start();
        void Stop();
        void Load(string url);
        bool IsLoading();
        void StartNew(string url);
        bool IsAllLoaded();
        void Finish();
        bool IsLoading(string url);
    }
}