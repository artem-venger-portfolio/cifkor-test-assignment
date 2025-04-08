using System;
using System.Collections.Generic;

namespace WebClient
{
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