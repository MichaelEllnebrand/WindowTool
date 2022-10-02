using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowTool
{
    internal static class Window
    {
        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;

        private const int GA_PARENT = 1; // Retrieves the parent window.This does not include the owner, as it does with the GetParent function.
        private const int GA_ROOT = 2; // Retrieves the root window by walking the chain of parent windows.
        private const int GA_ROOTOWNER = 3; // Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.

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


    }
}
