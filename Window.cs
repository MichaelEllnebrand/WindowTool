using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowTool
{
    internal static class Window
    {
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_SHOWWINDOW = 0x0040;
        
        const int SW_RESTORE = 9;

        const int GA_PARENT = 1; // Retrieves the parent window.This does not include the owner, as it does with the GetParent function.
        const int GA_ROOT = 2; // Retrieves the root window by walking the chain of parent windows.
        const int GA_ROOTOWNER = 3; // Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll")]
        private static extern IntPtr GetAncestor(IntPtr hWnd, int gaFlags);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int wFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        public static IntPtr GetWindow(Point point)
        {
            IntPtr intPtr = WindowFromPoint(point);
            intPtr = GetAncestor(intPtr, GA_ROOT);
            return intPtr;
        }

        public static void SetWindowPosition(IntPtr hWnd, int x, int y)
        {
            SetForegroundWindow(hWnd);
            SetWindowPos(hWnd, 0, x, y, 0, 0, SWP_NOSIZE | SWP_SHOWWINDOW);
        }

        public static void SetWindowSize(IntPtr hWnd, int width, int height)
        {
            SetForegroundWindow(hWnd);
            SetWindowPos(hWnd, 0, 0, 0, width, height, SWP_NOMOVE | SWP_SHOWWINDOW);
        }

        public static Rectangle GetWindowPosition(IntPtr hWnd)
        {
            GetWindowRect(hWnd, out Rectangle rect);
            return rect;
        }

        public static void HandleMaximizedWindow(IntPtr hWnd)
        {
            WINDOWPLACEMENT windowPlacement = new();
            GetWindowPlacement(hWnd, ref windowPlacement);
            if (windowPlacement.showCmd == 3)
            {
                Rectangle windowDimentions = GetWindowPosition(hWnd);
                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);
                SetWindowPos(hWnd, 0, windowDimentions.Top, windowDimentions.Right, windowDimentions.Width, windowDimentions.Height, SWP_SHOWWINDOW);
            }
        }
    }
}
