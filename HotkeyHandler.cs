using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using GlobalLowLevelHooks;

namespace WindowTool
{
    internal class HotkeyHandler
    {
        private MouseHook mouseHook;
        private KeyboardHook keyboardHook;

        private bool isLeftControlDown;
        private bool isLeftWinDown;
        private bool isLeftAltDown;

        private bool isMovingWindow;
        private bool isResizingWindow;

        private IntPtr currentWindowHandle;
        private Rectangle currentWindowRectangle;
        private Point currentMouseOffset;

        private int resizeStartWidth;
        private int resizeStartHeight;
        

        internal void Start()
        {
            mouseHook = new MouseHook();
            mouseHook.MouseMove += MouseHook_MouseMove;
            mouseHook.Install();
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyDown += KeyboardHook_KeyDown;
            keyboardHook.KeyUp += KeyboardHook_KeyUp;
            keyboardHook.Install();
        }

        internal void Stop()
        {
            mouseHook.MouseMove -= MouseHook_MouseMove;
            mouseHook.Uninstall();
            keyboardHook.KeyDown -= KeyboardHook_KeyDown;
            keyboardHook.KeyUp -= KeyboardHook_KeyUp;
            keyboardHook.Uninstall();
        }


        private void MouseHook_MouseMove(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            if (isMovingWindow == false && isResizingWindow == false) return;
            if (currentWindowHandle == IntPtr.Zero) return;

            Point mousePosition = Control.MousePosition;

            if (isMovingWindow)
            {
                int x = mousePosition.X - currentMouseOffset.X;
                int y = mousePosition.Y - currentMouseOffset.Y;
                // TODO: Option to clamp to current screen

                Window.SetWindowPosition(currentWindowHandle, x, y);
            }

            if (isResizingWindow)
            {
                int width = resizeStartWidth + mousePosition.X - currentMouseOffset.X;
                int height = resizeStartHeight + mousePosition.Y - currentMouseOffset.Y;

                Window.SetWindowSize(currentWindowHandle, width, height);

            }
        }


        private void KeyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            if (key == KeyboardHook.VKeys.LCONTROL) isLeftControlDown = true;
            if (key == KeyboardHook.VKeys.LWIN) isLeftWinDown = true;
            if (key == KeyboardHook.VKeys.LMENU) isLeftAltDown = true;
            CheckHotKey();
        }

        private void KeyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            if (key == KeyboardHook.VKeys.LCONTROL) isLeftControlDown = false;
            if (key == KeyboardHook.VKeys.LWIN) isLeftWinDown = false;
            if (key == KeyboardHook.VKeys.LMENU) isLeftAltDown = false;
            CheckHotKey();
        }

        private void CheckHotKey()
        {
            if (isLeftControlDown == true && isLeftWinDown == true)
            {
                if (currentWindowHandle == IntPtr.Zero) SaveWindowAtPointer();

                if (isLeftAltDown == false && isMovingWindow == false) StartMovingWindow();
                if (isLeftAltDown == true && isResizingWindow == false) StartResizingWindow();
            }
            else
            {
                if (isMovingWindow) StopMovingWindow();
                if (isResizingWindow) StopResizingWindow();
                currentWindowHandle = IntPtr.Zero;
            }
        }

        private void SaveWindowAtPointer()
        {
            Point mousePosition = Control.MousePosition;
            currentWindowHandle = Window.GetWindow(mousePosition);
        }

        private void StartMovingWindow()
        {
            isMovingWindow = true;
            isResizingWindow = false;

            Point mousePostion = Control.MousePosition;
            currentWindowRectangle = Window.GetWindowPosition(currentWindowHandle);
            currentMouseOffset.X = mousePostion.X - currentWindowRectangle.X;
            currentMouseOffset.Y = mousePostion.Y - currentWindowRectangle.Y;
        }

        private void StopMovingWindow()
        {
            isMovingWindow = false;
        }

        private void StartResizingWindow()
        {
            isMovingWindow = false;
            isResizingWindow = true;

            Point mousePostion = Control.MousePosition;
            currentWindowRectangle = Window.GetWindowPosition(currentWindowHandle);
            resizeStartWidth = currentWindowRectangle.Width - currentWindowRectangle.X;
            resizeStartHeight = currentWindowRectangle.Height - currentWindowRectangle.Y;

            currentMouseOffset.X = mousePostion.X;
            currentMouseOffset.Y = mousePostion.Y;
        }

        private void StopResizingWindow()
        {
            isResizingWindow = false;
        }
    }
}