using System;
using System.Drawing;
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
        private Point moveWindowMouseOffset;
        private Point resizeWindowsMouseOffset;

        private int resizeStartWidth;
        private int resizeStartHeight;

        private bool clampToScreen;
        private Point currentWindowPosition;
        private int currentWindowWidth;
        private int currentWindowHeight;

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

        internal void SetClampToScreen(bool clamp)
        {
            clampToScreen = clamp;

        }

        private void MouseHook_MouseMove(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            if (isMovingWindow == false && isResizingWindow == false) return;
            if (currentWindowHandle == IntPtr.Zero) return;

            Point mousePosition = Control.MousePosition;

            if (isMovingWindow)
            {
                currentWindowPosition.X = mousePosition.X - moveWindowMouseOffset.X;
                currentWindowPosition.Y = mousePosition.Y - moveWindowMouseOffset.Y;
                if (clampToScreen)
                {
                    Screen currentScreen = Screen.FromHandle(currentWindowHandle);
                    int remainingAreaX = currentScreen.WorkingArea.X + currentScreen.WorkingArea.Width - currentWindowWidth;
                    if (remainingAreaX < currentScreen.WorkingArea.X)
                    {
                        remainingAreaX = currentScreen.WorkingArea.X;
                    }
                    int remainingAreaY = currentScreen.WorkingArea.Y + currentScreen.WorkingArea.Height - currentWindowHeight;
                    if (remainingAreaY < currentScreen.WorkingArea.Y) 
                    {
                        remainingAreaY = currentScreen.WorkingArea.Y;
                    }
                    currentWindowPosition.X = Math.Clamp(currentWindowPosition.X, currentScreen.WorkingArea.X, remainingAreaX);
                    currentWindowPosition.Y = Math.Clamp(currentWindowPosition.Y, currentScreen.WorkingArea.Y, remainingAreaY);
                }

                Window.SetWindowPosition(currentWindowHandle, currentWindowPosition.X, currentWindowPosition.Y);
            }

            if (isResizingWindow)
            {
                currentWindowWidth = resizeStartWidth + mousePosition.X - resizeWindowsMouseOffset.X;
                currentWindowHeight = resizeStartHeight + mousePosition.Y - resizeWindowsMouseOffset.Y;

                if (clampToScreen)
                {
                    Screen currentScreen = Screen.FromHandle(currentWindowHandle);
                    currentWindowWidth = Math.Clamp(currentWindowWidth, 1, currentScreen.WorkingArea.Width - (currentWindowPosition.X - currentScreen.Bounds.X));
                    currentWindowHeight = Math.Clamp(currentWindowHeight, 1, currentScreen.WorkingArea.Height - (currentWindowPosition.Y - currentScreen.Bounds.Y));
                }

                Window.SetWindowSize(currentWindowHandle, currentWindowWidth, currentWindowHeight);
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
            SetStartingOffsets();
        }

        private void StopMovingWindow()
        {
            isMovingWindow = false;
        }

        private void StartResizingWindow()
        {
            isMovingWindow = false;
            isResizingWindow = true;
            SetStartingOffsets();
        }

        private void StopResizingWindow()
        {
            isResizingWindow = false;
        }

        private void SetStartingOffsets()
        {
            Point mousePostion = Control.MousePosition;
            currentWindowRectangle = Window.GetWindowPosition(currentWindowHandle);

            moveWindowMouseOffset.X = mousePostion.X - currentWindowRectangle.X;
            moveWindowMouseOffset.Y = mousePostion.Y - currentWindowRectangle.Y;
            resizeWindowsMouseOffset.X = mousePostion.X;
            resizeWindowsMouseOffset.Y = mousePostion.Y;


            resizeStartWidth = currentWindowRectangle.Width - currentWindowRectangle.X;
            resizeStartHeight = currentWindowRectangle.Height - currentWindowRectangle.Y;

            currentWindowWidth = resizeStartWidth;
            currentWindowHeight = resizeStartHeight;
        }
    }
}