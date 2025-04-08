using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace WebClient
{
    [UsedImplicitly]
    public class NavigationPanelModel
    {
        private readonly List<NavigationPanelTabModelBase> _tabs = new();

        public event Action<NavigationPanelTabModelBase> TabAdded;

        public void AddTab(NavigationPanelTabModelBase model)
        {
            _tabs.Add(model);
            TabAdded?.Invoke(model);
        }
    }
}