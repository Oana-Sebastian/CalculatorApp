using System;
using System.Collections.Generic;
using System.Windows;
using CalculatorApp.ViewModel;
using CalculatorApp.Properties;
using System.Configuration;

namespace CalculatorApp
{
    public partial class App : Application

    {

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);


            if (Current.MainWindow?.DataContext is MainVM mainVM)
            {
                Settings.Default.IsDigitGroupingEnabled = mainVM.Calculator.IsDigitGroupingEnabled;
                Settings.Default.IsProgrammerMode = mainVM.Calculator.IsProgrammerMode;
                Settings.Default.SelectedBase = mainVM.Calculator.SelectedBase;

                Settings.Default.Save();
            }
        }
    }

}