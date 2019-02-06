using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WhiteMvvm.Behaviors.Base
{
    /// <summary>
    /// base class to wire behavior to any view
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseBehavior<T> :Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }
        /// <summary>
        /// override attached method to assign visual element to associated object and register OnBindingContextChanged event
        /// </summary>
        /// <param name="visualElement"></param>
        protected override void OnAttachedTo(T visualElement)
        {
            base.OnAttachedTo(visualElement);

            AssociatedObject = visualElement;

            if (visualElement.BindingContext != null)
                BindingContext = visualElement.BindingContext;

            visualElement.BindingContextChanged += OnBindingContextChanged;
        }
        /// <summary>
        /// OnBindingContextChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
        /// <summary>
        /// override OnDetaching to un register from view
        /// </summary>
        /// <param name="view"></param>
        protected override void OnDetachingFrom(T view)
        {
            view.BindingContextChanged -= OnBindingContextChanged;
        }
        /// <summary>
        /// override OnBindingContextChanged to assign binding context of associated object to binding context of view
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
