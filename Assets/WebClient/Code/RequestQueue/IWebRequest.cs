namespace WebClient
{
    public interface IWebRequest
    {
        public RequestType Type { get; }
        void Send();
        bool IsInProgress();
        void Interrupt();
        bool IsDone();
        void Finish();
    }
}