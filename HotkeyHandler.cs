using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Text;
using GlobalLowLevelHooks;

namespace WindowTool
{
    internal class HotkeyHandler
    {
        private MouseHook mouseHook;
        private KeyboardHook keyboardHook;

        private bool isMovingWindow;
        private bool isResizingWindow;

        private bool isLeftControlDown;
        private bool isLeftWinDown;
        private bool isLeftAltDown;




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
        }


        private void KeyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            if (key == KeyboardHook.VKeys.LCONTROL) isLeftControlDown = true;
            if (key == KeyboardHook.VKeys.LWIN) isLeftWinDown = true;
            if (key == KeyboardHook.VKeys.LMENU) isLeftAltDown = true;
            CheckHotKeyDown();
        }

        private void KeyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            if (key == KeyboardHook.VKeys.LCONTROL) isLeftControlDown = false;
            if (key == KeyboardHook.VKeys.LWIN) isLeftWinDown = false;
            if (key == KeyboardHook.VKeys.LMENU) isLeftAltDown = false;
            CheckHotKeyDown();
        }

        private void CheckHotKeyDown()
        {
            if (isLeftControlDown == true && isLeftWinDown == true)
            {
                // save window

                if (isLeftAltDown == false && isMovingWindow == false) StartMovingWindow();
                if (isLeftAltDown == true && isResizingWindow == false) StartResizingWindow();
            }
            else
            {
                if (isMovingWindow) StopMovingWindow();
                if (isResizingWindow) StopResizingWindow();
            }

            Debug.WriteLine($"isMovingWindow {isMovingWindow} isResizingWindow {isResizingWindow}");
        }


        private void StartMovingWindow()
        {
            isMovingWindow = true;
            isResizingWindow = false;
        }

        private void StopMovingWindow()
        {
            isMovingWindow = false;

        }


        private void StartResizingWindow()
        {
            isMovingWindow = false;
            isResizingWindow = true;
        }

        private void StopResizingWindow()
        {
            isResizingWindow = false;
        }
    }
}
