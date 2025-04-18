﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CalculatorApp.ViewModel;

namespace CalculatorApp.View
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindowClosing;
        }
        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainVM mainVM)
            {
                Properties.Settings.Default.IsDigitGroupingEnabled = mainVM.Calculator.IsDigitGroupingEnabled;
                Properties.Settings.Default.IsProgrammerMode = mainVM.Calculator.IsProgrammerMode;
                Properties.Settings.Default.SelectedBase = mainVM.Calculator.SelectedBase;

                
                Properties.Settings.Default.Save();
            }
        }
    }
}
