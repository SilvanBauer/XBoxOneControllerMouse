using SharpDX.XInput;
using InputActions;
using System;
using System.Windows;
using System.Windows.Threading;

namespace XBoxOneControllerMouse {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private XInputController _controller;
        
        private bool _connectedMessage;
        private bool _batteryMessage;
        private bool _aWasDown;
        private bool _bWasDown;
        private bool _xWasDown;
        private bool _yWasDown;

        public MainWindow() {
            InitializeComponent();

            Hide();

            _controller = new XInputController();

            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, Properties.Settings.Default.TimeBetweenUpdates);
            timer.Tick += (sender, e) => {
                _controller.Update();

                if (!_connectedMessage && _controller.Connected) {
                    MessageBox.Show("Controller connected!", "Controller connected", MessageBoxButton.OK, MessageBoxImage.Information);

                    _connectedMessage = true;
                } else if (_connectedMessage && !_controller.Connected) {
                    MessageBox.Show("Controller disconnected!", "Controller disconnected", MessageBoxButton.OK, MessageBoxImage.Information);

                    _connectedMessage = false;
                    _batteryMessage = false;
                }

                if (_controller.Connected && _controller.BatteryInfo.BatteryLevel == BatteryLevel.Low && !_batteryMessage) {
                    MessageBox.Show("The battery of the controller is low!", "Battery low", MessageBoxButton.OK, MessageBoxImage.Information);

                    _batteryMessage = true;
                }

                if (_controller.Connected) {
                    var currentCursorPos = MouseActions.GetMousePosition();
                    var xMove = (Int32) _controller.LeftThumb.X / (20 - Properties.Settings.Default.Sensitivity);
                    var yMove = (Int32) _controller.LeftThumb.Y / (20 - Properties.Settings.Default.Sensitivity);

                    MouseActions.SetCursorPos(currentCursorPos.X + xMove, currentCursorPos.Y + yMove);

                    currentCursorPos = MouseActions.GetMousePosition();
                    var hMove = (Int32) _controller.RightThumb.X / (20 - Properties.Settings.Default.Sensitivity);
                    var vMove = (Int32) _controller.RightThumb.Y / (20 - Properties.Settings.Default.Sensitivity);

                    MouseActions.ScrollVerticalWheel(currentCursorPos.X, currentCursorPos.Y, vMove);
                    MouseActions.ScrollHorizonalWheel(currentCursorPos.X, currentCursorPos.Y, hMove);

                    if (_controller.ADown && !_aWasDown) {
                        MouseActions.LeftDown(currentCursorPos.X, currentCursorPos.Y);
                    } else if (!_controller.ADown && _aWasDown) {
                        MouseActions.LeftUp(currentCursorPos.X, currentCursorPos.Y);
                    }

                    _aWasDown = _controller.ADown;

                    if (_controller.BDown && !_bWasDown) {
                        MouseActions.RightDown(currentCursorPos.X, currentCursorPos.Y);
                    } else if (!_controller.BDown && _bWasDown) {
                        MouseActions.RightUp(currentCursorPos.X, currentCursorPos.Y);
                    }

                    _bWasDown = _controller.BDown;

                    if (_controller.XDown && !_xWasDown) {
                        MouseActions.ScrollVerticalWheel(currentCursorPos.X, currentCursorPos.Y, -100 / (20 - Properties.Settings.Default.Sensitivity));
                    }

                    _xWasDown = _controller.XDown;

                    if (_controller.YDown && !_yWasDown) {
                        MouseActions.ScrollVerticalWheel(currentCursorPos.X, currentCursorPos.Y, 100 / (20 - Properties.Settings.Default.Sensitivity));
                    }

                    _yWasDown = _controller.YDown;
                }
            };
            timer.Start();
        }
    }
}
