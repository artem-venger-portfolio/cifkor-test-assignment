namespace WebClient
{
    public interface IWebRequest
    {
        public RequestType Type { get; }
        void Send();
        bool IsInProgress();
        bool IsDone();
        void Finish();
    }
}