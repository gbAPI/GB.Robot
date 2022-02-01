using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GB.Robot.WPF_UI_MVVM.Views.Elements
{
    /// <summary>
    /// Логика взаимодействия для CaptionElement.xaml
    /// </summary>
    public partial class CaptionElement : UserControl
    {

        public static readonly DependencyProperty MaxWindowOnProperty = DependencyProperty.Register(nameof(MaxWindowOn),
            typeof(bool), typeof(CaptionElement),
            new FrameworkPropertyMetadata { DefaultValue = true, BindsTwoWayByDefault = true });

        public static readonly DependencyProperty MinWindowOnProperty = DependencyProperty.Register(nameof(MinWindowOn),
            typeof(bool), typeof(CaptionElement),
            new FrameworkPropertyMetadata { DefaultValue = true, BindsTwoWayByDefault = true });

        public static readonly DependencyProperty CaptionTitleProperty = DependencyProperty.Register(nameof(CaptionTitle),
            typeof(string), typeof(CaptionElement),
            new FrameworkPropertyMetadata { DefaultValue = "CaptionTitle", BindsTwoWayByDefault = true });

        public static readonly DependencyProperty DefaultHeightProperty = DependencyProperty.Register(nameof(DefaultHeight),
            typeof(int), typeof(CaptionElement),
            new FrameworkPropertyMetadata { DefaultValue = 450, BindsTwoWayByDefault = true });

        public static readonly DependencyProperty DefaultWidthProperty = DependencyProperty.Register(nameof(DefaultWidth),
            typeof(int), typeof(CaptionElement),
            new FrameworkPropertyMetadata { DefaultValue = 800, BindsTwoWayByDefault = true });

        public int DefaultWidth
        {
            get => (int)GetValue(DefaultWidthProperty);
            set => SetValue(DefaultWidthProperty, value);
        }
        public int DefaultHeight
        {
            get => (int)GetValue(DefaultHeightProperty);
            set => SetValue(DefaultHeightProperty, value);
        }
        public bool MaxWindowOn
        {
            get => (bool)GetValue(MaxWindowOnProperty);
            set => SetValue(MaxWindowOnProperty, value);
        }
        public bool MinWindowOn
        {
            get => (bool)GetValue(MinWindowOnProperty);
            set => SetValue(MinWindowOnProperty, value);
        }
        public string CaptionTitle
        {
            get => (string)GetValue(CaptionTitleProperty);
            set => SetValue(CaptionTitleProperty, value);
        }

        public CaptionElement()
        {
            InitializeComponent();
            MouseLeftButtonDown += DragMoves;
        }

        private void DragMoves(object sender, MouseButtonEventArgs e)
        {
            Window wind = App.FocusedWindow ?? App.ActivedWindow;
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                wind.DragMove();
            }
        }

        private void MaxiButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = App.FocusedWindow ?? App.ActivedWindow;
            if (w != null)
            {
                if (w.WindowState != WindowState.Maximized)
                {
                    w.WindowState = WindowState.Maximized;
                }
                else
                {
                    w.WindowState = WindowState.Normal;
                    w.Height = DefaultHeight;
                    w.Width = DefaultWidth;
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = App.FocusedWindow ?? App.ActivedWindow;
            w?.Close();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = App.FocusedWindow ?? App.ActivedWindow;

            if (w != null)
            {
                w.WindowState = WindowState.Minimized;
            }
        }
    }
}
