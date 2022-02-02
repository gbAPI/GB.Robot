using System.Windows;

namespace GB.Robot.WPF_UI_MVVM.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        public OperatorWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window audit = new OperatorAuditWindow();
            audit.Visibility = Visibility.Visible;
            audit.Show();
        }
    }
}
