using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorApp.ViewModel.Commands;

namespace CalculatorApp.ViewModel
{
    public class ProgrammerVM : INotifyPropertyChanged
    {
        private string _display = "0";
        public string Display
        {
            get => _display;
            set
            {
                if (_display != value)
                {
                    _display = value;
                    OnPropertyChanged(nameof(Display));
                }
            }
        }

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
                    OnPropertyChanged(nameof(OperationsAllowed));
                    OnPropertyChanged(nameof(ModeText));
                    Display = FormatNumber(_lastValue);

                }
            }
        }

        public bool OperationsAllowed => !IsProgrammerMode || (IsProgrammerMode && SelectedBase == 10);

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
                    Display = FormatNumber(_lastValue);

                }
            }
        }

        public ICommand DigitCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand MemoryChangeCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand FileManagementCommand { get; }
        public ICommand ToggleProgrammerModeCommand { get; }
        public ICommand SetBaseCommand { get; }


        private double _lastValue;
        private string _currentOperator = "";
        private double _lastOperand;
        private string _lastOperator = "";
        private bool _isNewEntry = true;
        private bool _errorState = false;
        private bool _memoryRecalled = false;
        public bool OperatorsEnabled => !_errorState;
        private double _copiedValue = 0;


        public ProgrammerVM()
        {
            IsProgrammerMode = Properties.Settings.Default.IsProgrammerMode;
            SelectedBase = Properties.Settings.Default.SelectedBase;
            DigitCommand = new RelayCommand(param => AppendDigit(param.ToString()));
            OperatorCommand = new RelayCommand(execute: param => ProcessOperator(param.ToString()), canExecute: param => OperatorsEnabled);
            EqualCommand = new RelayCommand(param => Evaluate());
            ClearCommand = new RelayCommand(param => Clear(param));
            BackspaceCommand = new RelayCommand(param => Backspace());
            MemoryRecallCommand = new RelayCommand(param => RecallMemory((double)param));
            MemoryChangeCommand = new RelayCommand(param => MemoryChange());
            FileManagementCommand = new RelayCommand(param => FileManagement(param.ToString()));
            ToggleProgrammerModeCommand = new RelayCommand(_ => IsProgrammerMode = !IsProgrammerMode);
            SetBaseCommand = new RelayCommand(param =>
            {
                if (int.TryParse(param.ToString(), out int baseValue))
                {
                    SelectedBase = baseValue;
                }
            });
        }

        private void RecallMemory(double recalledMemory)
        {
            Display = FormatNumber(recalledMemory);
            _lastValue = recalledMemory;
            _memoryRecalled = true;
        }

        private void MemoryChange()
        {
            _memoryRecalled = true;
        }

        private void AppendDigit(string digit)
        {
            if (_memoryRecalled)
            {
                Display = "";
                _memoryRecalled = false;
                _isNewEntry = true;
            }

            if (_errorState)
            {
                _errorState = false;
                OnPropertyChanged(nameof(OperatorsEnabled));
                ClearEverything();
                _isNewEntry = true;
                Display = "";
            }

            if (_isNewEntry || Display == "0")
            {
                Display = digit;
                _isNewEntry = false;
            }
            else
            {
                Display += digit;
            }
            
        }

        private void FileManagement(string command)
        {
            if (_errorState)
                return;


           
                try
                {
                    int value = Convert.ToInt32(Display, SelectedBase);
                    _copiedValue = value; 
                }
                catch
                {
                    Display = "Error";
                    _errorState = true;
                    _isNewEntry = true;
                    return;
                }
            switch (command)
            {
                case "C":
                    // Copy: valoarea a fost deja salvată mai sus
                    break;
                case "X":
                    
                        try
                        {
                            int cut = Convert.ToInt32(Display, SelectedBase);
                            _copiedValue = cut;
                            ClearEverything();
                        }
                        catch
                        {
                            Display = "Error";
                            _errorState = true;
                            _isNewEntry = true;
                        }                                  
                    break;
                case "V":
                    Display = "";
                    if (IsProgrammerMode)
                    {
                        int val = (int)_copiedValue;
                        Display = Convert.ToString(val, SelectedBase).ToUpper();
                    }                  
                    _isNewEntry = false;
                    break;
            }
        }

        private void ProcessOperator(string op)
        {
            if (_memoryRecalled)
            {
                _memoryRecalled = false;
            }

            if (_errorState)
                return;

            if (op == "√(x)" || op == "1/x" || op == "x²" || op == "%")
            {
                ProcessImmediateOperation(op);
                return;
            }

            if (op == "+/-")
            {
                
                    try
                    {
                        int current = Convert.ToInt32(Display, SelectedBase);
                        if (current == 0)
                            return;
                        int result = -current;
                        Display = Convert.ToString(result, SelectedBase).ToUpper();
                        _lastValue = result;
                        _isNewEntry = true;
                    }
                    catch
                    {
                        Display = "Error";
                        _errorState = true;
                        _isNewEntry = true;
                    }
                
                return;
            }

            if (!_isNewEntry)
            {
                if (!string.IsNullOrEmpty(_currentOperator))
                    Evaluate();
                else
                {
                        _lastValue = Convert.ToInt32(Display, SelectedBase);
                    }
            }

            _currentOperator = op;
            _lastOperator = "";
            _lastOperand = 0;
            _isNewEntry = true;
        }

        private void ProcessImmediateOperation(string op)
        {
            if (_errorState)
                return;

            
                int currentValue = 0;
                try
                {
                    currentValue = Convert.ToInt32(Display, SelectedBase);
                }
                catch
                {
                    Display = "Error";
                    _errorState = true;
                    _isNewEntry = true;
                    return;
                }
                int result = 0;
                switch (op)
                {
                    case "√(x)":
                        if (currentValue < 0)
                        {
                            _errorState = true;
                            OnPropertyChanged(nameof(OperatorsEnabled));
                        }
                        result = (int)Math.Sqrt(currentValue);
                        break;
                    case "1/x":
                        if (currentValue == 0)
                        {
                            Display = "Cannot divide by zero";
                            _isNewEntry = true;
                            _errorState = true;
                            OnPropertyChanged(nameof(OperatorsEnabled));
                            return;
                        }
                        result = 1 / currentValue;
                        break;
                    case "x²":
                        result = currentValue * currentValue;
                        break;
                    case "%":
                        result = currentValue / 100;
                        break;
                }
                Display = Convert.ToString(result, SelectedBase).ToUpper();
                _lastValue = result;
                _isNewEntry = true;
            
        }

        private void Evaluate()
        {
            if (_errorState)
            {
                ClearEverything();
                return;
            }

            if (SelectedBase != 10)
            {
                EvaluateProgrammer();
            }
            else
            {
                EvaluateStandard();
            }
        }

        private void EvaluateStandard()
        {
            if (_errorState)
                ClearEverything();

            if (!double.TryParse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double currentValue))
            {
                Display = "Error";
                _errorState = true;
                _isNewEntry = true;
                OnPropertyChanged(nameof(OperatorsEnabled));
                return;
            }

            if (!string.IsNullOrEmpty(_currentOperator))
            {
                double result = Compute(_lastValue, currentValue, _currentOperator);
                if (_errorState)
                {
                    _isNewEntry = true;
                    _currentOperator = "";
                    return;
                }
                Display = FormatNumber(result);
                _lastOperand = currentValue;
                _lastOperator = _currentOperator;
                _lastValue = result;
                _currentOperator = "";
                _isNewEntry = true;
            }
            else if (!string.IsNullOrEmpty(_lastOperator))
            {
                double result = Compute(currentValue, _lastOperand, _lastOperator);
                if (_errorState)
                {
                    _isNewEntry = true;
                    return;
                }
                Display = FormatNumber(result);
                _lastValue = result;
                _isNewEntry = true;
            }
        }

        private void EvaluateProgrammer()
        {
            int currentValue = 0;
            try
            {
                currentValue = Convert.ToInt32(Display, SelectedBase);
            }
            catch
            {
                Display = "Error";
                _errorState = true;
                _isNewEntry = true;
                return;
            }

            if (!string.IsNullOrEmpty(_currentOperator))
            {
                int lastVal = (int)Math.Round(_lastValue);
                int result = 0;
                switch (_currentOperator)
                {
                    case "+":
                        result = lastVal + currentValue;
                        break;
                    case "-":
                        result = lastVal - currentValue;
                        break;
                    case "×":
                    case "*":
                        result = lastVal * currentValue;
                        break;
                    case "/":
                        if (currentValue == 0)
                        {
                            Display = "Cannot divide by zero";
                            _errorState = true;
                            _isNewEntry = true;
                            OnPropertyChanged(nameof(OperatorsEnabled));
                            return;
                        }
                        result = lastVal / currentValue;
                        break;
                    default:
                        result = currentValue;
                        break;
                }
                _lastOperand = currentValue;
                _lastOperator = _currentOperator;
                _lastValue = result;
                _currentOperator = "";
                _isNewEntry = true;
                Display = Convert.ToString(result, SelectedBase).ToUpper();
            }
            else if (!string.IsNullOrEmpty(_lastOperator))
            {
                int currentInt = currentValue;
                int result = 0;
                switch (_lastOperator)
                {
                    case "+":
                        result = currentInt + (int)_lastOperand;
                        break;
                    case "-":
                        result = currentInt - (int)_lastOperand;
                        break;
                    case "×":
                    case "*":
                        result = currentInt * (int)_lastOperand;
                        break;
                    case "/":
                        if (_lastOperand == 0)
                        {
                            Display = "Cannot divide by zero";
                            _errorState = true;
                            _isNewEntry = true;
                            return;
                        }
                        result = currentInt / (int)_lastOperand;
                        break;
                    default:
                        result = currentInt;
                        break;
                }
                _lastValue = result;
                _isNewEntry = true;
                Display = Convert.ToString(result, SelectedBase).ToUpper();
            }
        }

        private double Compute(double a, double b, string op)
        {
            switch (op)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "×":
                case "*":
                    return a * b;
                case "/":
                    if (b == 0)
                    {
                        Display = "Cannot divide by zero";
                        _errorState = true;
                        _isNewEntry = true;
                        OnPropertyChanged(nameof(OperatorsEnabled));
                        return 0;
                    }
                    return a / b;
                default:
                    return 0;
            }
        }

        private string FormatNumber(double value)
        {
            
                long intVal = (long)Math.Round(value);
                switch (SelectedBase)
                {
                    case 2:
                        return Convert.ToString(intVal, 2);
                    case 8:
                        return Convert.ToString(intVal, 8);
                    case 10:
                        return intVal.ToString();
                    case 16:
                        return intVal.ToString("X");
                    default:
                        return intVal.ToString();
                }
           
        }

        private void Clear(object parameter)
        {
            string param = parameter?.ToString();
            if (param == "CE")
            {
                if (!string.IsNullOrEmpty(_currentOperator))
                {
                    Display = "0";
                    _isNewEntry = true;
                }
                else
                {

                    ClearEverything();
                }
            }
            else if (param == "C")
            {
                ClearEverything();
            }
        }

        private void ClearEverything()
        {
            Display = "0";
            _lastValue = 0;
            _currentOperator = "";
            _lastOperator = "";
            _lastOperand = 0;
            _isNewEntry = true;
            _errorState = false;
            _memoryRecalled = false;
            OnPropertyChanged(nameof(OperatorsEnabled));
        }

        private void Backspace()
        {
            if (_errorState)
                ClearEverything();
            if (!_isNewEntry && Display.Length > 0)
            {
                Display = Display.Remove(Display.Length - 1);
                if (string.IsNullOrEmpty(Display))
                    Display = "0";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
