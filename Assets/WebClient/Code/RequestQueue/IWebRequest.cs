namespace WebClient
{
    public interface IWebRequest
    {
        public RequestType Type { get; }
        void Send();
        bool IsDone();
        void Finish();
    }
}