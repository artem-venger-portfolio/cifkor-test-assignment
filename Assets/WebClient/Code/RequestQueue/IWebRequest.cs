using System.Collections.Generic;

namespace WebClient
{
    public interface IWebRequest
    {
        public RequestType Type { get; }
        public List<WeatherPeriod> Result { get; }
        void Send();
        bool IsInProgress();
        void Interrupt();
    }
}