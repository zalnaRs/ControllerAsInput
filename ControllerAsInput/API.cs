using System.Diagnostics;
using WindowsInput;
using SharpDX.XInput;
using System.Threading;
using System;
using System.Xml;

namespace ControllerAsInput
{
    public class ControllerAsInput
    {
        public static int MovementDivider = 1000;
        public static int ScrollDivider = 20000;
        public const int RefreshRate = 60;

        public Timer _timer;
        public Controller _controller;
        public IMouseSimulator _mouseSimulator;
        public IKeyboardSimulator _keyboardSimulator;

        public bool _wasADown;
        public bool _wasBDown;
        public bool _wasXDown;
        public bool _wasYDown;
        public bool _wasBackDown;
        public bool _wasStartDown;
        public bool _wasLeftDown;
        public bool _wasRightDown;
        public bool _wasUpDown;
        public bool _wasDownDown;
        public bool _wasLeftThumbDown;
        public bool _wasRightShoulderDown;
        public ControllerAsInput()
        {
            _controller = new Controller(UserIndex.One);
            _mouseSimulator = new InputSimulator().Mouse;
            _keyboardSimulator = new InputSimulator().Keyboard;
            _timer = new Timer(obj => Update());
        }

        private void Update()
        {
            _controller.GetState(out var state);
            Movement(state);
            Scroll(state);
            LeftButton(state);
            RightButton(state);
            KeyBoardButton(state);
            KeyBoardCloseButton(state);
            BackSpaceButton(state);
            EnterButton(state);
            ArrowButtons(state);
            CapsLockButton(state);
            SpaceButton(state);
        }
        private void Movement(State state)
        {
            var x = state.Gamepad.LeftThumbX / MovementDivider;
            var y = state.Gamepad.LeftThumbY / MovementDivider;
            try
            {
                _mouseSimulator.MoveMouseBy(x, -y);
            } catch { Console.Clear(); }
            
        }
        private void Scroll(State state)
        {
            var x = state.Gamepad.RightThumbX / ScrollDivider;
            var y = state.Gamepad.RightThumbY / ScrollDivider;
            _mouseSimulator.HorizontalScroll(x);
            _mouseSimulator.VerticalScroll(y);
        }
        private void LeftButton(State state)
        {
            var isADown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);
            if (isADown && !_wasADown) _mouseSimulator.LeftButtonDown();
            if (!isADown && _wasADown) _mouseSimulator.LeftButtonUp();
            _wasADown = isADown;
        }

        private void RightButton(State state)
        {
            var isBDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);
            if (isBDown && !_wasBDown) _mouseSimulator.RightButtonDown();
            if (!isBDown && _wasBDown) _mouseSimulator.RightButtonUp();
            _wasBDown = isBDown;
        }
        private void KeyBoardButton(State state)
        {

            var isXDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X);
            if (isXDown && !_wasXDown)
            {
                try
                {
                    Process.Start(@"osk.exe");
                }
                catch
                {
                    Console.WriteLine("Failed to open on screen keyboard!");
                }
            }

            _wasXDown = isXDown;
        }
        private void KeyBoardCloseButton(State state)
        {

            var isYDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y);
            if (isYDown && !_wasYDown)
            {
                try
                {
                    Process[] runingProcess = Process.GetProcesses();
                    int i;
                    for (i = 0; i < runingProcess.Length; i++)
                    {
                        if (runingProcess[i].ProcessName == "osk")
                        {
                            runingProcess[i].Kill();
                        }

                    }
                }
                catch
                {
                    Console.WriteLine("Failed to close on screen keyboard!");
                }
            }

            isYDown = _wasYDown;
        }
        private void BackSpaceButton(State state)
        {

            var isBackDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back);
            if (isBackDown && !_wasBackDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.BACK);
            if (!isBackDown && _wasBackDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.BACK);
            _wasBackDown = isBackDown;
        }
        private void EnterButton(State state)
        {
            
            var isStartDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start);
            if (isStartDown && !_wasStartDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
            if (!_wasStartDown && isStartDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.RETURN);
            _wasStartDown = isStartDown;
        }
        private void ArrowButtons(State state)
        {

            var isLeftDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft);
            if (isLeftDown && !_wasLeftDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.LEFT);
            if (!_wasLeftDown && isLeftDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.LEFT);
            _wasLeftDown = isLeftDown;
            var isRightDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight);
            if (isRightDown && !_wasRightDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.RIGHT);
            if (!_wasRightDown && isRightDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.RIGHT);
            _wasRightDown = isRightDown;
            var isUpDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp);
            if (isUpDown && !_wasUpDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.UP);
            if (!_wasUpDown && isUpDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.UP);
            _wasUpDown = isUpDown;
            var isDownDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown);
            if (isDownDown && !_wasDownDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.DOWN);
            if (!_wasDownDown && isDownDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.DOWN);
            _wasDownDown = isDownDown;
        }
        private void CapsLockButton(State state)
        {
            var isLeftThumbDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftThumb);
            if (isLeftThumbDown && !_wasLeftThumbDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.CAPITAL);
            if (!_wasLeftThumbDown && isLeftThumbDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.CAPITAL);
            _wasLeftThumbDown = isLeftThumbDown;
        }
        private void SpaceButton(State state)
        {
            var isRightShoulderDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder);
            if (isRightShoulderDown && !_wasRightShoulderDown) _keyboardSimulator.KeyDown(WindowsInput.Native.VirtualKeyCode.SPACE);
            if (!_wasRightShoulderDown && isRightShoulderDown) _keyboardSimulator.KeyUp(WindowsInput.Native.VirtualKeyCode.SPACE);
            _wasRightShoulderDown = isRightShoulderDown;
        }
        public void Start()
        {
            _timer.Change(0, 1000 / RefreshRate);
        }
    }
}
