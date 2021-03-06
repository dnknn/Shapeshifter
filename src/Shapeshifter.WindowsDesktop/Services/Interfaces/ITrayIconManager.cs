﻿namespace Shapeshifter.WindowsDesktop.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Infrastructure.Events;

    public interface ITrayIconManager: IDisposable
    {
        event EventHandler<TrayIconDoubleClickedEventArgument> IconDoubleClicked;

		void UpdateHoverText(string text);

		void DisplayInformation(string title, string text);
		void DisplayWarning(string title, string text);
		void DisplayError(string title, string text);

		void UpdateMenuItems(
            string boldMenuItemTitle,
            IReadOnlyCollection<MenuItem> contextMenuItems);
    }
}