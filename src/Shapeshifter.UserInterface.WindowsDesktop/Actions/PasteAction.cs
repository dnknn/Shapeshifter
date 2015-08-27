﻿using Shapeshifter.UserInterface.WindowsDesktop.Actions.Interfaces;
using Shapeshifter.UserInterface.WindowsDesktop.Data.Interfaces;
using Shapeshifter.UserInterface.WindowsDesktop.Infrastructure.Logging.Interfaces;
using Shapeshifter.UserInterface.WindowsDesktop.Services.Clipboard.Interfaces;
using System;
using System.Threading.Tasks;

namespace Shapeshifter.UserInterface.WindowsDesktop.Actions
{
    class PasteAction : IPasteAction
    {
        readonly IClipboardInjectionService clipboardInjectionService;
        readonly IClipboardPasteService clipboardPasteService;
        readonly ILogger logger;

        public PasteAction(
            IClipboardInjectionService clipboardInjectionService,
            IClipboardPasteService clipboardPasteService,
            ILogger logger)
        {
            this.clipboardInjectionService = clipboardInjectionService;
            this.clipboardPasteService = clipboardPasteService;
            this.logger = logger;
        }

        public string Description
        {
            get
            {
                return "Pastes clipboard contents as-is.";
            }
        }

        public byte Order
        {
            get
            {
                return 0;
            }
        }

        public string Title
        {
            get
            {
                return "Paste";
            }
        }

        public async Task<bool> CanPerformAsync(
            IClipboardDataPackage package)
        {
            if(package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            return true;
        }

        public async Task PerformAsync(
            IClipboardDataPackage package)
        {
            clipboardInjectionService.InjectData(package);
            clipboardPasteService.PasteClipboardContents();

            logger.Information("Paste action performed.", 1);
        }
    }
}
