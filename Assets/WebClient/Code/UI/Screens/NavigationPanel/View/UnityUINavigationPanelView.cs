using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public sealed class UnityUINavigationPanelView : NavigationPanelViewBase
    {
        [SerializeField]
        private Toggle _weatherToggle;

        [SerializeField]
        private Toggle _dogBreedsToggle;
    }
}