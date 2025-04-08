using System;

namespace WebClient
{
    public class MVPTypesGroup
    {
        private MVPTypesGroup(Type model, Type view, Type presenter)
        {
            Model = model;
            View = view;
            Presenter = presenter;
        }

        public Type Model { get; }
        public Type View { get; }
        public Type Presenter { get; }

        public static MVPTypesGroup Create<TModel, TView, TPresenter>()
        {
            return new MVPTypesGroup(typeof(TModel), typeof(TView), typeof(TPresenter));
        }
    }
}