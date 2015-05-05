﻿using System;
using System.Windows;
using Shapeshifter.Core.Data;
using Shapeshifter.UserInterface.WindowsDesktop.Factories.Interfaces;

namespace Shapeshifter.UserInterface.WindowsDesktop.Factories
{
    class EnhancedMetafileClipboardDataControlFactory : IClipboardDataControlFactory
    {
        public UIElement BuildControl(IClipboardData clipboardData)
        {
            throw new NotImplementedException();
        }

        public IClipboardData BuildData(string format, object data)
        {
            throw new NotImplementedException();
        }

        public bool CanBuildControl(IClipboardData data)
        {
            return false;
        }

        public bool CanBuildData(string format)
        {
            return format == DataFormats.EnhancedMetafile;
        }
    }
}