namespace WebClient
{
    public interface ITextureLoader
    {
        void Start();
        void Stop();
        void Load(string url);
        bool IsLoading();
        void Finish();
        bool IsLoading(string url);
    }
}