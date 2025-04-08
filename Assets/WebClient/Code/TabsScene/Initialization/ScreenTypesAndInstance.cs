using System;
using UnityEngine;

namespace WebClient
{
    public readonly struct ScreenTypesAndInstance
    {
        public ScreenTypesAndInstance(Type model, Type viewType, Type presenter, Component viewInstance)
        {
            Model = model;
            ViewType = viewType;
            Presenter = presenter;
            ViewInstance = viewInstance;
        }

        public Type Model { get; }
        public Type ViewType { get; }
        public Type Presenter { get; }
        public Component ViewInstance { get; }
    }
}