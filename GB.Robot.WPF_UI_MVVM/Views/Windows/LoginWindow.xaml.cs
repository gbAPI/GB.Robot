using System;
using System.Windows;
using System.Windows.Input;

namespace GB.Robot.WPF_UI_MVVM.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

        }

        private void RowDefinition_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void OperatorShow(object sender, RoutedEventArgs e)
        {
            OperatorWindow window = new();
            window.Visibility = Visibility.Visible;
            Visibility = Visibility.Hidden;
            window.ShowDialog();
            Visibility = Visibility.Visible;
            window = null;
        }
    }
}
