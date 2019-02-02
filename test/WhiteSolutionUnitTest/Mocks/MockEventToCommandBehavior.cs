using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSolution;
using WhiteSolution.Behaviors;

namespace WhiteSolutionUnitTest.Mocks
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
