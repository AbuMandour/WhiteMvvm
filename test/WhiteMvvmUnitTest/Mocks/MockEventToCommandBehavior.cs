using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm;
using WhiteMvvm.Behaviors;

namespace WhiteMvvmUnitTest.Mocks
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
