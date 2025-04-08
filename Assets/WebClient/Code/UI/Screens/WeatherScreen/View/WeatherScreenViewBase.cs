using System.Collections.Generic;

namespace WebClient
{
    public abstract class WeatherScreenViewBase : ViewBase
    {
        public void Open()
        {
            SetActive(isActive: true);
        }

        public void Close()
        {
            SetActive(isActive: false);
        }

        public abstract void DisplayPeriods(List<WeatherPeriod> periods);

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}