// <copyright file="WindowsMemoryCleanupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Logic;
    using System.Runtime.Versioning;

    /// <summary>
    /// Class that cleans up memory for Windows.
    /// </summary>
    /// <seealso cref="Logic.IMemoryCleanupService" />
    [SupportedOSPlatform("windows")]
    public class WindowsMemoryCleanupService : IMemoryCleanupService
    {
        /// <summary>
        /// Does the cleanup.
        /// </summary>
        public void Cleanup()
        {
            // Clean .NET Memory
            // Link: https://stackoverflow.com/questions/1852929/can-i-force-memory-cleanup-in-c
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }

        // [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        // private static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);
    }
}
