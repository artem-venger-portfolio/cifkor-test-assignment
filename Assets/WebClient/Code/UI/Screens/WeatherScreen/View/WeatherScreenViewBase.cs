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

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}