using System;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Input;
using CalculatorApp.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CalculatorApp.View;
using System.Reflection.Metadata;

namespace CalculatorApp.ViewModel
{
    public class CalculatorVM : INotifyPropertyChanged
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
                    FormatAndSetDisplay();
                    
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

        public string ModeText => IsProgrammerMode ? "Programmer Mode: Base "+ SelectedBase : "Standard Mode";

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
                    OnPropertyChanged(nameof(isBinary));
                    OnPropertyChanged(nameof(isOctal));
                    OnPropertyChanged(nameof(isDecimal));
                    OnPropertyChanged(nameof(isHex));
                    ((RelayCommand)DigitCommand)?.RaiseCanExecuteChanged();
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
        public ICommand ToggleDigitGroupingCommand { get; }
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
        private double _copiedValue=0;
        public string decimalSeparator => CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        public bool isBinary => _selectedBase>=2 && IsProgrammerMode;
        public bool isOctal => _selectedBase>=8 && IsProgrammerMode;
        public bool isDecimal => _selectedBase>=10;
        public bool isHex => _selectedBase>=16 && IsProgrammerMode;
      
        public CalculatorVM()
        {
            IsDigitGroupingEnabled = Properties.Settings.Default.IsDigitGroupingEnabled;
            IsProgrammerMode = Properties.Settings.Default.IsProgrammerMode;
            SelectedBase = Properties.Settings.Default.SelectedBase;
            DigitCommand = new RelayCommand(param => AppendDigit(param.ToString()),CanExecuteDigit);
            OperatorCommand = new RelayCommand(execute: param => ProcessOperator(param.ToString()), canExecute: param => OperatorsEnabled);
            EqualCommand = new RelayCommand(param => Evaluate());
            ClearCommand = new RelayCommand(param => Clear(param));
            BackspaceCommand = new RelayCommand(param => Backspace());         
            MemoryRecallCommand = new RelayCommand(param => RecallMemory((double)param));
            MemoryChangeCommand = new RelayCommand(param => MemoryChange());
            FileManagementCommand = new RelayCommand(param => FileManagement(param.ToString()));
            ToggleDigitGroupingCommand = new RelayCommand(_ =>
            {
                if (!IsProgrammerMode)
                    IsDigitGroupingEnabled = !IsDigitGroupingEnabled;
                else IsDigitGroupingEnabled = false;
            });
            ToggleProgrammerModeCommand = new RelayCommand( _ => IsProgrammerMode = !IsProgrammerMode );
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

        private bool CanExecuteDigit(object parameter)
        {
            if (!IsProgrammerMode)
                return true;
            if (parameter is string digit)
            {
                switch (_selectedBase)
                {
                    case 2:
                        return "01".Contains(digit);
                    case 8:
                        return "01234567".Contains(digit);
                    case 10:
                        return "0123456789".Contains(digit);
                    case 16:
                        return "0123456789ABCDEF".Contains(digit.ToUpper());
                }
            }
            return false;
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

            if (digit == decimalSeparator)
            {
                if (!IsProgrammerMode)
                {
                    if (_isNewEntry)
                    {
                        Display = "0" + decimalSeparator;
                        _isNewEntry = false;
                        return;
                    }
                    if (Display.Contains(decimalSeparator))
                    {
                        return;
                    }
                }
                else
                {
                    return; 
                }
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
            if (!IsProgrammerMode)
                FormatAndSetDisplay();
        }

        void FileManagement(string command)
        {
            if (_errorState)
                return;
            switch (command)
            {
                case ("C"):
                    if (double.TryParse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double copy))
                    {
                        _copiedValue = copy;
                    }
                    break;
                case ("X"):
                    if (double.TryParse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double cut))
                    {
                        _copiedValue = cut;
                        ClearEverything();
                    }
                    break;
                case ("V"):
                    Display = "";
                    Display = FormatNumber(_copiedValue);
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
                if (IsProgrammerMode)
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
                }
                else
                {
                    if (double.TryParse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double current))
                    {
                        if (current == 0)
                            return;
                        double result = -current;
                        Display = FormatNumber(result);
                        _lastValue = result;
                        _isNewEntry = true;
                    }
                }
                return;
            }

            if (!_isNewEntry)
            {
                if (!string.IsNullOrEmpty(_currentOperator))
                    Evaluate();
                else
                {
                    if (IsProgrammerMode)
                        _lastValue = Convert.ToInt32(Display, SelectedBase);
                    else
                        _lastValue = double.Parse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture);
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

            if (IsProgrammerMode)
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
            else
            {
                if (!double.TryParse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double current))
                {
                    Display = "Error";
                    _errorState = true;
                    _isNewEntry = true;
                    return;
                }
                double result = 0;
                switch (op)
                {
                    case "√(x)":
                        if (current < 0)
                        {
                            _errorState = true;
                            OnPropertyChanged(nameof(OperatorsEnabled));
                        }
                        result = Math.Sqrt(current);
                        break;
                    case "1/x":
                        if (current == 0)
                        {
                            Display = "Cannot divide by zero";
                            _isNewEntry = true;
                            _errorState = true;
                            OnPropertyChanged(nameof(OperatorsEnabled));
                            return;
                        }
                        result = 1 / current;
                        break;
                    case "x²":
                        result = current * current;
                        break;
                    case "%":
                        result = current / 100;
                        break;
                }
                if (!string.IsNullOrEmpty(_currentOperator))
                {
                    Display = FormatNumber(result);
                }
                else
                {
                    _lastValue = result;
                    Display = FormatNumber(result);
                }
                _isNewEntry = true;
            }
        }

        private void Evaluate()
        {
            if (_errorState)
            {
                ClearEverything();
                return;
            }

            if (IsProgrammerMode && SelectedBase != 10)
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
            if (IsProgrammerMode)
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
            else if (!IsDigitGroupingEnabled)
                return value.ToString("0.############################", CultureInfo.CurrentCulture);
            else
                return value.ToString("#,0.############################", CultureInfo.CurrentCulture);
        }

        private void FormatAndSetDisplay()
        {
            var nfi = CultureInfo.CurrentCulture.NumberFormat;
            string raw = Display.Replace(nfi.NumberGroupSeparator, "");
            string formatted = string.Empty;

            if (raw.Contains(decimalSeparator))
            {
                var parts = raw.Split(new string[] { decimalSeparator }, StringSplitOptions.None);
                string integerPart = parts[0];
                string fractionalPart = parts.Length > 1 ? parts[1] : "";

                if (IsDigitGroupingEnabled)
                {
                    if (long.TryParse(integerPart, NumberStyles.Integer, CultureInfo.CurrentCulture, out long intPart))
                    {
                        formatted = intPart.ToString("#,0", CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        formatted = integerPart;
                    }
                }
                else
                {
                    formatted = integerPart;
                }
                formatted += decimalSeparator + fractionalPart;
            }
            else
            {
                if (IsDigitGroupingEnabled)
                {
                    if (long.TryParse(raw, NumberStyles.Integer, CultureInfo.CurrentCulture, out long intPart))
                    {
                        formatted = intPart.ToString("#,0", CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        formatted = raw;
                    }
                }
                else
                {
                    formatted = raw;
                }
            }
            Display = formatted;
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
