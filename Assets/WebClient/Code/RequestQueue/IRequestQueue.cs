namespace WebClient
{
    public interface IRequestQueue
    {
        public void Add(IWebRequest request);
        public void Start();
    }
}