using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using CalculatorApp.View;
using CalculatorApp.ViewModel.Commands;

namespace CalculatorApp.ViewModel
{
    public class MemoryVM : INotifyPropertyChanged
    {
        public event Action<double> MemoryChanged;
        public ObservableCollection<double> MemoryStack { get; private set; } = new ObservableCollection<double>();

        public bool HasMemory => MemoryStack.Count > 0;

        private double? _recalledMemory;
        public double? RecalledMemory
        {
            get => _recalledMemory;
            set
            {
                if (_recalledMemory != value)
                {
                    _recalledMemory = value;
                    OnPropertyChanged(nameof(RecalledMemory));
                }
            }
        }

        private double? _selectedMemory;
        public double? SelectedMemory
        {
            get => _selectedMemory;
            set
            {
                if (_selectedMemory != value)
                {
                    _selectedMemory = value;
                    OnPropertyChanged(nameof(SelectedMemory));
                    ((RelayCommand)ClearSelectedCommand).RaiseCanExecuteChanged();
                    if (_selectedMemory.HasValue)
                    {
                        SelectedIndex = MemoryStack.IndexOf(_selectedMemory.Value);
                    }
                    else
                    {
                        SelectedIndex = null;
                    }
                }
            }
        }

        private int? _selectedIndex;
        public int? SelectedIndex
        {
            get => _selectedIndex;
            private set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
                }
            }
        }

        public ICommand ClearSelectedCommand { get; }

        public ICommand MemoryStoreCommand { get; }
        public ICommand MemoryAddCommand { get; }
        public ICommand MemoryAddSelectedCommand { get; }
        public ICommand MemorySubtractCommand { get; }
        public ICommand MemorySubtractSelectedCommand { get; }
        public ICommand MemoryClearAllCommand { get; } 
        

        public MemoryVM()
        {
            MemoryStack.CollectionChanged += MemoryStack_CollectionChanged;
            MemoryStoreCommand = new RelayCommand(param => MemoryStore(param));
            MemoryAddCommand = new RelayCommand(param => MemoryAdd(param));
            MemoryAddSelectedCommand = new RelayCommand(param => MemoryAddSelected(param));
            MemorySubtractCommand = new RelayCommand(param => MemorySubtract(param));
            MemorySubtractSelectedCommand = new RelayCommand(param => MemorySubtractSelected(param));
            MemoryClearAllCommand = new RelayCommand(_ => ClearAllMemory(), _ => HasMemory);
            ClearSelectedCommand = new RelayCommand(param => ClearSelected(), param => SelectedMemory.HasValue);
            RecalledMemory = 0;
        }


        private void ClearSelected()
        {
            if (SelectedMemory.HasValue)
            {
                int index = MemoryStack.IndexOf(SelectedMemory.Value);
                if (index >= 0)
                {
                    MemoryStack.RemoveAt(index);
                    SelectedMemory = null;
                    if (MemoryStack.Count==0)
                        RecalledMemory = 0;
                    else
                    RecalledMemory = MemoryStack[MemoryStack.Count - 1];
                }
            }

        }
        private void MemoryStore(object parameter)
        {
            if (double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double value))
            {
                MemoryStack.Add(value);
                RecalledMemory = value;
            }
        }

        private void MemoryStack_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasMemory));
            ((RelayCommand)MemoryClearAllCommand).RaiseCanExecuteChanged();
        }


        private void MemoryAdd(object parameter)
        {
            if (!double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double value))
                return;


            if (MemoryStack.Count == 0)
            {
                MemoryStack.Add(0);
                RecalledMemory = 0;
            }
            int index = MemoryStack.Count - 1;
            MemoryStack[index] = MemoryStack[index] + value;
            RecalledMemory=MemoryStack[index];
        }

        private void MemoryAddSelected(object parameter)
        {
            if (!double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double value))
                return;

            int index = SelectedIndex.HasValue ? SelectedIndex.Value : MemoryStack.Count - 1;
            if (index < 0)
            {
                MemoryStack.Add(0);
                index = 0;
            }
            MemoryStack[index] = MemoryStack[index] + value;
            SelectedMemory = MemoryStack[index];
            RecalledMemory = MemoryStack[MemoryStack.Count-1];
        }

        private void MemorySubtract(object parameter)
        {
            if (!double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double value))
                return;

            if (MemoryStack.Count == 0)
            {
                MemoryStack.Add(0);
                RecalledMemory = 0;
            }
            int index = MemoryStack.Count - 1;
            MemoryStack[index] = MemoryStack[index] - value;
            RecalledMemory= MemoryStack[index];
        }
        
        private void MemorySubtractSelected(object parameter)
        {
            if (!double.TryParse(parameter?.ToString(),
                                  NumberStyles.Float | NumberStyles.AllowThousands,
                                  CultureInfo.CurrentCulture,
                                  out double value))
                return;

            int index = SelectedIndex.HasValue ? SelectedIndex.Value : MemoryStack.Count - 1;
            if (index < 0)
            {
                MemoryStack.Add(0);
                index = 0;
            }
            MemoryStack[index] = MemoryStack[index] - value;
            SelectedMemory = MemoryStack[index];
            RecalledMemory = MemoryStack[MemoryStack.Count-1];
        }

        private void ClearAllMemory()
        {
            MemoryStack.Clear();
            RecalledMemory = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
