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

        private UnityUINavigationPanelTabViewBase _tab;

        public void SetPageName(string pageName)
        {
            _pageNameField.text = pageName;
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggle.group = toggleGroup;
        }

        public void SetTab(UnityUINavigationPanelTabViewBase tab)
        {
            _tab = tab;
        }
    }
}