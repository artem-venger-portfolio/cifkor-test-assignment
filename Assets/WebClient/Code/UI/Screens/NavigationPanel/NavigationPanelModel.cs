using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace WebClient
{
    [UsedImplicitly]
    public class NavigationPanelModel
    {
        private readonly List<string> _tabs = new();

        public event Action<string> TabAdded;

        public void AddTab(string name)
        {
            _tabs.Add(name);
            TabAdded?.Invoke(name);
        }
    }
}