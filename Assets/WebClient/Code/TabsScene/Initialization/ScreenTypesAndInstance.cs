using System;

namespace WebClient
{
    public readonly struct ScreenTypesAndInstance
    {
        private ScreenTypesAndInstance(Type model, Type viewType, Type presenter, ViewBase viewInstance)
        {
            Model = model;
            ViewType = viewType;
            Presenter = presenter;
            ViewInstance = viewInstance;
        }

        public Type Model { get; }
        public Type ViewType { get; }
        public Type Presenter { get; }
        public ViewBase ViewInstance { get; }

        public static ScreenTypesAndInstance Create<TModel, TView, TPresenter>(TView viewInstance) where TView: ViewBase
        {
            return new ScreenTypesAndInstance(typeof(TModel), typeof(TView), typeof(TPresenter), viewInstance);
        }
    }
}