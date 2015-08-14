﻿using System;
using Shapeshifter.Core.Data;
using Shapeshifter.UserInterface.WindowsDesktop.Actions.Interfaces;
using Shapeshifter.Core.Data.Interfaces;
using System.Threading.Tasks;
using Shapeshifter.UserInterface.WindowsDesktop.Services.Clipboard.Interfaces;

namespace Shapeshifter.UserInterface.WindowsDesktop.Actions
{
    class PasteAsPlainTextAction : IPasteAsPlainTextAction
    {
        private readonly IClipboardInjectionService clipboardInjectionService;

        public PasteAsPlainTextAction(IClipboardInjectionService clipboardInjectionService)
        {
            this.clipboardInjectionService = clipboardInjectionService;
        }

        public string Description
        {
            get
            {
                return "Pastes clipboard contents as plain text.";
            }
        }

        public string Title
        {
            get
            {
                return "Paste as plain text";
            }
        }

        public bool CanPerform(IClipboardData clipboardData)
        {
            return clipboardData is IClipboardTextData;
        }

        public async Task PerformAsync(IClipboardData clipboardData)
        {
            var textData = (IClipboardTextData)clipboardData;
            clipboardInjectionService.InjectText(textData.Text);
        }
    }
}
