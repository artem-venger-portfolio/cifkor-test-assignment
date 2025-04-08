using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public class WeatherEntry : MonoBehaviour
    {
        [SerializeField]
        private RawImage _image;

        [SerializeField]
        private TMP_Text _temperatureField;

        public void SetTexture(Texture2D texture)
        {
            _image.texture = texture;
        }

        public void SetTemperature(string periodName, float value, string unit)
        {
            _temperatureField.text = $"{periodName} - {value}{unit}";
        }
    }
}