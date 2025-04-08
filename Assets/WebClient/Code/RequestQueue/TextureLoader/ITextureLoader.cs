namespace WebClient
{
    public interface ITextureLoader
    {
        void Start();
        void Stop();
        void Load(string url);
        bool IsLoading();
        bool IsLoading(string url);
        void Finish();
    }
}