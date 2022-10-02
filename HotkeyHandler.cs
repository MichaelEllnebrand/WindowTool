using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        }

        private void KeyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
        }
    }
}
