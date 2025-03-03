using System;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Input;
using CalculatorApp.ViewModel.Commands;

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


        public ICommand DigitCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }

        private double _lastValue;
        private string _currentOperator = "";
        private double _lastOperand;
        private string _lastOperator = "";
        private bool _isNewEntry = true;
        private bool _errorState = false;
        public bool OperatorsEnabled => !_errorState;

        public CalculatorVM()
        {
            DigitCommand = new RelayCommand(param => AppendDigit(param.ToString()));
            OperatorCommand = new RelayCommand(param => ProcessOperator(param.ToString()));
            EqualCommand = new RelayCommand(param => Evaluate());
            ClearCommand = new RelayCommand(param => Clear(param));
            BackspaceCommand = new RelayCommand(param => Backspace());
        }

        private void FormatAndSetDisplay()
        {
            var nfi = CultureInfo.CurrentCulture.NumberFormat;
          
            string raw = Display.Replace(nfi.NumberGroupSeparator, "");
            if (raw.Contains(nfi.NumberDecimalSeparator))
            {
                var parts = raw.Split(new string[] { nfi.NumberDecimalSeparator }, StringSplitOptions.None);
                string integerPart = parts[0];
                string fractionalPart = parts.Length > 1 ? parts[1] : "";
                string formattedInteger = "";
                if (string.IsNullOrEmpty(integerPart) || integerPart == "-")
                {
                    formattedInteger = integerPart + "0";
                }
                else if (long.TryParse(integerPart, NumberStyles.Integer, CultureInfo.CurrentCulture, out long intPart))
                {
                    formattedInteger = intPart.ToString("#,0", CultureInfo.CurrentCulture);
                }
                else
                {
                    formattedInteger = integerPart; 
                }
                Display = formattedInteger + nfi.NumberDecimalSeparator + fractionalPart;
            }
            else
            {
                if (long.TryParse(raw, NumberStyles.Integer, CultureInfo.CurrentCulture, out long intPart))
                {
                    Display = intPart.ToString("#,0", CultureInfo.CurrentCulture);
                }
                else
                {
                    Display = raw;
                }
            }
        }
        private string FormatNumber(double value)
        {
            return value.ToString("#,0.############################", CultureInfo.CurrentCulture);
        }

        private void AppendDigit(string digit)
        {
            if (_errorState)
            {
                _errorState = false;
                OnPropertyChanged(nameof(OperatorsEnabled));
                ClearEverything();
                _isNewEntry = true;
                Display = "";
            }

            if (digit == ".")
            {
                
                if (_isNewEntry)
                {
                    Display = "0.";
                    _isNewEntry = false;
                    return;
                }
               
                if (Display.Contains("."))
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
            FormatAndSetDisplay();
        }


        private void ProcessOperator(string op)
        {

            if (_errorState)
                return;

            if (op == "√(x)" || op == "1/x" || op == "x²" || op == "%")
            {
                ProcessImmediateOperation(op);
                return;
            }
            
            if (op == "+/-")
            {
                if (double.TryParse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double current))
                {
                    if (current == 0)
                        return;
                    double result = -current;
                    Display = result.ToString();
                    _lastValue = result;  
                    _isNewEntry = true;
                }
                return;
            }

            
            if (!_isNewEntry)
            {
                
                if (!string.IsNullOrEmpty(_currentOperator))
                    Evaluate();
                else
                    _lastValue = double.Parse(Display, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture); ;
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
            Display = FormatNumber(result);
            if (string.IsNullOrEmpty(_currentOperator))
            {
                _lastValue = result;
            }
                _isNewEntry = true;
        }

        private void Evaluate()
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
