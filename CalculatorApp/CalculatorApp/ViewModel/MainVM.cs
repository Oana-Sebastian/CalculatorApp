using System.ComponentModel;

namespace CalculatorApp.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public CalculatorVM Calculator { get; }
        public MemoryVM Memory { get; }

        public MainVM()
        {
            Calculator = new CalculatorVM();
            Memory = new MemoryVM();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
