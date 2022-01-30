using System;
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
            this.MouseLeftButtonDown += DragMoves;
        }

        private void DragMoves(object sender, MouseButtonEventArgs e)
        {
            Window wind = App.FocusedWindow ?? App.ActivedWindow;
            if (e.ButtonState == MouseButtonState.Pressed)
                wind.DragMove();
        }
    }
}
