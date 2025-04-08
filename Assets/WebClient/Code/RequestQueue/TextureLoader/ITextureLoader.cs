namespace WebClient
{
    public interface ITextureLoader
    {
        void Start();
        void Stop();
        void Load(string url);
        bool IsLoading();
        bool IsAllLoaded();
        void Finish();
        bool IsLoading(string url);
    }
}