using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Interactivity;
using lab_9.ViewModels;
using System;

namespace lab_9.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SelectedPath_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = (MainWindowViewModel)DataContext!;
                vm.SelectedPath = ((TextBox)sender!).Text;
            }
        }
        private void NextItem(object? sender, RoutedEventArgs e)
        {
            var keyboardInstance = KeyboardDevice.Instance;
            var inputManagerInstance = InputManager.Instance;
            this.FindControl<TreeDataGrid>("fileViewer").Focus();
            inputManagerInstance.ProcessInput(new RawKeyEventArgs(keyboardInstance, (ulong)DateTime.Now.Ticks, this, RawKeyEventType.KeyDown, Key.Down, RawInputModifiers.None));
        }
        private void PreviousItem(object? sender, RoutedEventArgs e)
        {
            string t = e.ToString();
            var keyboardInstance = KeyboardDevice.Instance;
            var inputManagerInstance = InputManager.Instance;
            this.FindControl<TreeDataGrid>("fileViewer").Focus();
            inputManagerInstance.ProcessInput(new RawKeyEventArgs(keyboardInstance, (ulong)DateTime.Now.Ticks, this, RawKeyEventType.KeyDown, Key.Up, RawInputModifiers.None));
        }
    }
}
