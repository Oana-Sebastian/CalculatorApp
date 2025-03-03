using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using CalculatorApp.ViewModel.Commands;

namespace CalculatorApp.ViewModel
{
    public class MemoryVM : INotifyPropertyChanged
    {
        private double _memoryValue;
        public double MemoryValue
        {
            get => _memoryValue;
            set
            {
                if (_memoryValue != value)
                {
                    _memoryValue = value;
                    OnPropertyChanged(nameof(MemoryValue));
                }
            }
        }

        // Memory commands
        public ICommand MemoryClearCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand MemoryStoreCommand { get; }
        public ICommand MemoryAddCommand { get; }
        public ICommand MemorySubtractCommand { get; }
        public ICommand MemoryViewCommand { get; }

        public MemoryVM()
        {
            MemoryClearCommand = new RelayCommand(_ => MemoryClear());
            MemoryRecallCommand = new RelayCommand(_ => MemoryRecall());
            MemoryStoreCommand = new RelayCommand(param => MemoryStore(param));
            MemoryAddCommand = new RelayCommand(param => MemoryAdd(param));
            MemorySubtractCommand = new RelayCommand(param => MemorySubtract(param));
            MemoryViewCommand = new RelayCommand(_ => MemoryView());
        }

        /// <summary>
        /// Clears the stored memory value (MC).
        /// </summary>
        private void MemoryClear()
        {
            MemoryValue = 0;
        }

        /// <summary>
        /// Recalls the stored memory value (MR).
        /// This method may be used to update a display in a higher-level view model.
        /// </summary>
        private void MemoryRecall()
        {
            // In a composite design, the higher-level view model (or the view)
            // would bind to the MemoryValue property.
            // This method is provided if you want to add additional logic.
        }

        /// <summary>
        /// Stores the passed value into memory (MS).
        /// Parameter should be the current value (as string or double) from the calculator.
        /// </summary>
        private void MemoryStore(object parameter)
        {
            if (double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double current))
            {
                MemoryValue = current;
            }
        }

        /// <summary>
        /// Adds the passed value to the stored memory (M+).
        /// </summary>
        private void MemoryAdd(object parameter)
        {
            if (double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double current))
            {
                MemoryValue += current;
            }
        }

        /// <summary>
        /// Subtracts the passed value from the stored memory (M–).
        /// </summary>
        private void MemorySubtract(object parameter)
        {
            if (double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double current))
            {
                MemoryValue -= current;
            }
        }

        /// <summary>
        /// Memory View (Mv). In this simple implementation, the view simply binds to MemoryValue.
        /// This method is provided if additional logic is desired.
        /// </summary>
        private void MemoryView()
        {
            // No additional logic required here.
            // The MemoryValue property is available for binding.
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
