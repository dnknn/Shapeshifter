﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Shapeshifter.UserInterface.WindowsDesktop.Actions.Interfaces;
using NSubstitute;
using System.Threading.Tasks;
using Shapeshifter.UserInterface.WindowsDesktop.Services.Clipboard.Interfaces;
using Shapeshifter.UserInterface.WindowsDesktop.Data.Interfaces;

namespace Shapeshifter.Tests.Actions
{
    [TestClass]
    public class PasteActionTest : ActionTestBase
    {
        [TestMethod]
        public async Task CanAlwaysPerformIfDataIsGiven()
        {
            var container = CreateContainer();

            var fakeData = Substitute.For<IClipboardDataPackage>();

            var action = container.Resolve<IPasteAction>();
            Assert.IsTrue(await action.CanPerformAsync(fakeData));
        }

        [TestMethod]
        public void CanGetTitle()
        {
            var container = CreateContainer();

            var action = container.Resolve<IPasteAction>();
            Assert.IsNotNull(action.Title);
        }

        [TestMethod]
        public void CanGetDescription()
        {
            var container = CreateContainer();

            var action = container.Resolve<IPasteAction>();
            Assert.IsNotNull(action.Description);
        }

        [TestMethod]
        public async Task PerformTriggersPaste()
        {
            var container = CreateContainer(c =>
            {
                c.RegisterFake<IClipboardInjectionService>();
                c.RegisterFake<IClipboardPasteService>();
            });

            var fakeData = GetPackageContaining<IClipboardData>();

            var action = container.Resolve<IPasteAction>();
            await action.PerformAsync(fakeData);

            var fakeClipboardInjectionService = container.Resolve<IClipboardInjectionService>();
            fakeClipboardInjectionService
                .Received()
                .InjectData(fakeData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ThrowsExceptionIfNoDataGiven()
        {
            var container = CreateContainer();
            
            var action = container.Resolve<IPasteAction>();
            await action.CanPerformAsync(null);
        }
    }
}
