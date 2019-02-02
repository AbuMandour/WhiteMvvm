using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WhiteSolution.Utils.WeakEvent
{
    internal struct Subscription
    {
        public Subscription(WeakReference subscriber, MethodInfo handler)
        {
            Subscriber = subscriber;
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public WeakReference Subscriber { get; }
        public MethodInfo Handler { get; }
    }
}
