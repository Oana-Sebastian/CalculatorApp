using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CalculatorApp.View;
using CalculatorApp.ViewModel.Commands;

namespace CalculatorApp.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public ICommand CompositeMemoryAddCommand { get; }
        public ICommand CompositeMemorySubtractCommand { get; }
        public ICommand CompositeMemoryClearCommand { get; }
        public ICommand CompositeMemoryStoreCommand { get; }
        public ICommand MemoryViewCommand { get; }
        public ICommand AboutCommand { get; }

        public CalculatorVM Calculator { get; }
        public MemoryVM Memory { get; }
        public MainVM()
        {
            Memory = new MemoryVM();
            Calculator = new CalculatorVM();

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
