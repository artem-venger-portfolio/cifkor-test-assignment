using System;

namespace WebClient
{
    public interface IWebRequest : IDisposable
    {
        void Send();
        bool IsDone();
    }
}