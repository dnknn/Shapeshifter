﻿namespace Shapeshifter.WindowsDesktop.Infrastructure.Handles.Interfaces
{
    using System;

    public interface IMemoryHandle: IHandle
    {
        IntPtr Pointer { get; }
    }
}