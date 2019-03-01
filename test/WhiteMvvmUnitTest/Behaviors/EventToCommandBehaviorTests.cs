using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvmUnitTest.Mocks;
using Xamarin.Forms;

namespace WhiteMvvmUnitTest.Behaviors
{
    [TestClass]
    public class EventToCommandBehaviorTests 
    {
        [TestMethod]
        public void InvalidEventNameShouldThrowArgumentExceptionText()
        {
            var behavior = new MockEventToCommandBehavior
            {
                EventName = "OnItemTapped"
            };
            var listView = new ListView();

            Assert.ThrowsException<ArgumentException>(() => listView.Behaviors.Add(behavior));
        }

        [TestMethod]
        public void CommandExecutedWhenEventFiresText()
        {
            bool executedCommand = false;
            var behavior = new MockEventToCommandBehavior
            {
                EventName = "ItemTapped",
                Command = new Command(() =>
                {
                    executedCommand = true;
                })
            };
            var listView = new ListView();
            listView.Behaviors.Add(behavior);

            behavior.RaiseEvent(listView, null);

            Assert.IsTrue(executedCommand);
        }

        [TestMethod]
        public void CommandCanExecuteTest()
        {
            var behavior = new MockEventToCommandBehavior
            {
                EventName = "ItemTapped",
                Command = new Command(() => Assert.IsTrue(false), () => false)
            };
            var listView = new ListView();
            listView.Behaviors.Add(behavior);

            behavior.RaiseEvent(listView, null);
        }

        [TestMethod]
        public void CommandCanExecuteWithParameterShouldNotExecuteTest()
        {
            bool shouldExecute = false;
            var behavior = new MockEventToCommandBehavior
            {
                EventName = "ItemTapped",
                CommandParameter = shouldExecute,
                Command = new Command<string>(o => Assert.IsTrue(false), o => o.Equals(true))
            };
            var listView = new ListView();
            listView.Behaviors.Add(behavior);

            behavior.RaiseEvent(listView, null);
        }

        [TestMethod]
        public void CommandWithConverterTest()
        {
            const string item = "ItemProperty";
            bool executedCommand = false;
            var behavior = new MockEventToCommandBehavior
            {
                EventName = "ItemTapped",
                EventArgsConverter = new ItemTappedEventArgsConverter(false),
                Command = new Command<string>(o =>
                {
                    executedCommand = true;
                    Assert.IsNotNull(o);
                    Assert.AreEqual(item, o);
                })
            };
            var listView = new ListView();
            listView.Behaviors.Add(behavior);

            behavior.RaiseEvent(listView, new ItemTappedEventArgs(listView, item));

            Assert.IsTrue(executedCommand);
        }
    }
}
