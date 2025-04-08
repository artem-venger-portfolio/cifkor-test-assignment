namespace WebClient
{
    public interface IWebRequest
    {
        void Send();
        bool IsDone();
        void Finish();
    }
}