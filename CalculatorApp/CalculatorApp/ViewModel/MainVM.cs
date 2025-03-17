using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CalculatorApp.View;
using CalculatorApp.ViewModel.Commands;

namespace CalculatorApp.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {

        private string _display = "0";
        public string ActiveDisplay
        {
            get => IsProgrammerMode ? Programmer.Display : Calculator.Display;
        }

        public ICommand CompositeMemoryAddCommand { get; }
        public ICommand CompositeMemorySubtractCommand { get; }
        public ICommand CompositeMemoryClearCommand { get; }
        public ICommand CompositeMemoryStoreCommand { get; }
        public ICommand MemoryViewCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand EqualCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand DigitCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand MemoryChangeCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand FileManagementCommand { get; }
        public ICommand ToggleDigitGroupingCommand { get; }
        public ICommand ToggleProgrammerModeCommand { get; }
        public ICommand SetBaseCommand { get; }

        private bool _isProgrammerMode;
        public bool IsProgrammerMode
        {
            get => _isProgrammerMode;
            set
            {
                if (_isProgrammerMode != value)
                {
                    _isProgrammerMode = value;
                    OnPropertyChanged(nameof(IsProgrammerMode));
                }
            }
        }
        public string ModeText => IsProgrammerMode ? "Programmer Mode: Base " + SelectedBase : "Standard Mode";

        private int _selectedBase;
        public int SelectedBase
        {
            get => _selectedBase;
            set
            {
                if (_selectedBase != value)
                {
                    _selectedBase = value;
                    OnPropertyChanged(nameof(SelectedBase));
                    OnPropertyChanged(nameof(ModeText));
                }
            }
        }

        private bool _isDigitGroupingEnabled;
        public bool IsDigitGroupingEnabled
        {
            get => _isDigitGroupingEnabled;
            set
            {
                if (_isDigitGroupingEnabled != value)
                {
                    _isDigitGroupingEnabled = value;
                    OnPropertyChanged(nameof(IsDigitGroupingEnabled));
                }
            }
        }


        public CalculatorVM Calculator { get; }
        public MemoryVM Memory { get; }
        public ProgrammerVM Programmer { get; }
        public MainVM()
        {
            Memory = new MemoryVM();
            Calculator = new CalculatorVM();
            Programmer = new ProgrammerVM();

            IsDigitGroupingEnabled = Properties.Settings.Default.IsDigitGroupingEnabled;
            IsProgrammerMode = Properties.Settings.Default.IsProgrammerMode;
            SelectedBase = Properties.Settings.Default.SelectedBase;

            ToggleProgrammerModeCommand = new RelayCommand(_ => IsProgrammerMode = !IsProgrammerMode);

            SetBaseCommand = new RelayCommand(param =>
            {
                if (int.TryParse(param.ToString(), out int baseValue))
                {
                    SelectedBase = baseValue;
                }
            });

            CompositeMemoryAddCommand = new RelayCommand(param =>
            {
                if (Memory.MemoryAddCommand.CanExecute(param))
                    Memory.MemoryAddCommand.Execute(param);

                if (Calculator.MemoryChangeCommand != null &&
                    Calculator.MemoryChangeCommand.CanExecute(param))
                {
                    Calculator.MemoryChangeCommand.Execute(param);
                }
            });

            CompositeMemorySubtractCommand = new RelayCommand(param =>
            {
                if (Memory.MemorySubtractCommand.CanExecute(param))
                    Memory.MemorySubtractCommand.Execute(param);

                if (Calculator.MemoryChangeCommand != null &&
                    Calculator.MemoryChangeCommand.CanExecute(param))
                {
                    Calculator.MemoryChangeCommand.Execute(param);
                }
            });

            CompositeMemoryClearCommand = new RelayCommand(param =>
            {
                if (Memory.MemoryClearAllCommand.CanExecute(param))
                    Memory.MemoryClearAllCommand.Execute(param);

                if (Calculator.MemoryChangeCommand != null &&
                    Calculator.MemoryChangeCommand.CanExecute(param))
                {
                    Calculator.MemoryChangeCommand.Execute(param);
                }
            });
            
            CompositeMemoryStoreCommand = new RelayCommand(param =>
            {
                if (Memory.MemoryStoreCommand.CanExecute(param))
                    Memory.MemoryStoreCommand.Execute(param);

                if (Calculator.MemoryChangeCommand != null &&
                    Calculator.MemoryChangeCommand.CanExecute(param))
                {
                    Calculator.MemoryChangeCommand.Execute(param);
                }
            });

            DigitCommand = new RelayCommand(param =>
            {
                if (IsProgrammerMode && Programmer.DigitCommand.CanExecute(param))
                    Programmer.DigitCommand.Execute(param);
                else if(Calculator.DigitCommand.CanExecute(param))
                    Calculator.DigitCommand.Execute(param);

            });

            OperatorCommand = new RelayCommand(param =>
            {
                if (IsProgrammerMode && Programmer.OperatorCommand.CanExecute(param))
                    Programmer.OperatorCommand.Execute(param);
                else if (Calculator.OperatorCommand.CanExecute(param))
                    Calculator.OperatorCommand.Execute(param);

            });

            EqualCommand = new RelayCommand(param =>
            {
                if (IsProgrammerMode && Programmer.EqualCommand.CanExecute(param))
                    Programmer.EqualCommand.Execute(param);
                else if (Calculator.EqualCommand.CanExecute(param))
                    Calculator.EqualCommand.Execute(param);

            });

            ClearCommand = new RelayCommand(param =>
            {
                if (IsProgrammerMode && Programmer.ClearCommand.CanExecute(param))
                    Programmer.ClearCommand.Execute(param);
                else if (Calculator.ClearCommand.CanExecute(param))
                    Calculator.ClearCommand.Execute(param);

            });

            BackspaceCommand = new RelayCommand(param =>
            {
                if (IsProgrammerMode && Programmer.BackspaceCommand.CanExecute(param))
                    Programmer.BackspaceCommand.Execute(param);
                else if (Calculator.BackspaceCommand.CanExecute(param))
                    Calculator.BackspaceCommand.Execute(param);

            });
            MemoryRecallCommand = new RelayCommand(param =>
            {
                
                if (Calculator.MemoryRecallCommand.CanExecute(param))
                    Calculator.MemoryRecallCommand.Execute(param);

            });

            MemoryChangeCommand = new RelayCommand(param =>
            {
               
                if (Calculator.MemoryChangeCommand.CanExecute(param))
                    Calculator.MemoryChangeCommand.Execute(param);

            });
            
            FileManagementCommand = new RelayCommand(param =>
            {
                if (IsProgrammerMode && Programmer.FileManagementCommand.CanExecute(param))
                    Programmer.FileManagementCommand.Execute(param);
                else if (Calculator.FileManagementCommand.CanExecute(param))
                    Calculator.FileManagementCommand.Execute(param);
            });

            ToggleDigitGroupingCommand = new RelayCommand(param =>
            {
                if (Calculator.ToggleDigitGroupingCommand.CanExecute(param))
                    Calculator.ToggleDigitGroupingCommand.Execute(param);
            });

            



            MemoryViewCommand = new RelayCommand(_ => OpenMemoryWindow());
            AboutCommand = new RelayCommand(_ => ShowAbout());

        }

        private void OpenMemoryWindow()
        {
            var memoryWindow = new MemoryWindow(this);
            memoryWindow.ShowDialog();
        }

        private void ShowAbout()
        {
            MessageBox.Show("Calculator Application\nDeveloped by Oană Sebastian\nGroup: 10LF233", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
