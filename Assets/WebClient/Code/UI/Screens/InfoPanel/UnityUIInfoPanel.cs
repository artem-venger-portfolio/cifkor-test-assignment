using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public class UnityUIInfoPanel : MonoBehaviour, IInfoPanel
    {
        [SerializeField]
        private TMP_Text _titleField;

        [SerializeField]
        private TMP_Text _contentField;

        [SerializeField]
        private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(Close);
        }

        public void Open(string title, string content)
        {
            _titleField.text = title;
            _contentField.text = content;
            SetActive(isActive: true);
        }

        private void Close()
        {
            SetActive(isActive: false);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}