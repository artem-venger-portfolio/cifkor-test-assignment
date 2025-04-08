using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public class NavigationPanelToggle : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _pageNameField;

        [SerializeField]
        private Toggle _toggle;

        public void SetPageName(string pageName)
        {
            _pageNameField.text = pageName;
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggle.group = toggleGroup;
        }
    }
}