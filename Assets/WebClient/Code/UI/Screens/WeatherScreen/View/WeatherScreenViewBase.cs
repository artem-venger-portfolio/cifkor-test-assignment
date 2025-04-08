using System.Collections.Generic;

namespace WebClient
{
    public abstract class WeatherScreenViewBase : ViewBase
    {
        public abstract void Open();
        public abstract void Close();
        public abstract void DisplayPeriods(List<WeatherPeriod> periods);
    }
}