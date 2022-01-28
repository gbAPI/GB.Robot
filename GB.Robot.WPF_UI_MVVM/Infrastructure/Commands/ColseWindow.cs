﻿using GB.Robot.WPF_UI_MVVM.Infrastructure.Commands.Base;
using System;
using System.Windows;

namespace GB.Robot.WPF_UI_MVVM.Infrastructure.Commands
{
    class ColseWindow : Command
    {
        private static Window GetWindow(object p) => p as Window ?? App.FocusedWindow ?? App.ActivedWindow;

        protected override bool CanExecute(object p) => GetWindow(p) != null;

        protected override void Execute(object p) => GetWindow(p)?.Close();
    }
}
