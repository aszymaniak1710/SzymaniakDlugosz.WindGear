using System.Windows.Input;
using SzymaniakDlugosz.WindGear.Interfaces;
using SzymaniakDlugosz.WindGear.UI.MVVM;
using SzymaniakDlugosz.WindGear.UI.ViewModels;

namespace SzymaniakDlugosz.WindGear.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IBLService _bl;
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand ShowManufacturersCommand { get; }
        public ICommand ShowProductsCommand { get; }

        public MainViewModel(IBLService bl)
        {
            _bl = bl;
            ShowManufacturersCommand = new RelayCommand(o => ShowManufacturers());
            ShowProductsCommand = new RelayCommand(o => ShowProducts());
            
            // Domyœlny widok
            ShowManufacturers();
        }

        private void Navigate(ViewModelBase viewModel)
        {
            CurrentView = viewModel;
        }

        private void ShowManufacturers()
        {
            CurrentView = new ManufacturerListViewModel(_bl, Navigate);
        }

        private void ShowProducts()
        {
            CurrentView = new ProductListViewModel(_bl, Navigate);
        }
    }
}
