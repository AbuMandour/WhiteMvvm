using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhiteMvvm.Behaviors;

namespace WhiteMvvm.UnitTest.Mocks
{
    [TestClass]
    public class MockEventToCommandBehavior : EventToCommandBehavior
    {
        public void RaiseEvent(params object[] args)
        {
            _handler.DynamicInvoke(args);
        }
    }
}
