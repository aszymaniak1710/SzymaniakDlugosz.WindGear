using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SzymaniakDlugosz.WindGear.Interfaces;
using SzymaniakDlugosz.WindGear.UI.MVVM;

namespace SzymaniakDlugosz.WindGear.UI.ViewModels
{
    public class ManufacturerListViewModel : ViewModelBase
    {
        private readonly IBLService _bl;
        private readonly Action<ViewModelBase> _navigate;
        
        public ObservableCollection<IManufacturer> Manufacturers { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public ManufacturerListViewModel(IBLService bl, Action<ViewModelBase> navigate = null)
        {
            _bl = bl;
            _navigate = navigate;
            Manufacturers = new ObservableCollection<IManufacturer>(_bl.GetManufacturers());

            AddCommand = new RelayCommand(o => Add());
            EditCommand = new RelayCommand(o => Edit(o as IManufacturer), o => o is IManufacturer);
            DeleteCommand = new RelayCommand(o => Delete(o as IManufacturer), o => o is IManufacturer);
        }

        private void Add()
        {
            var newMan = _bl.CreateManufacturer();
            _navigate?.Invoke(new ManufacturerEditViewModel(newMan, _bl, _navigate));
        }

        private void Edit(IManufacturer manufacturer)
        {
            _navigate?.Invoke(new ManufacturerEditViewModel(manufacturer, _bl, _navigate));
        }

        private void Delete(IManufacturer manufacturer)
        {
            if (MessageBox.Show($"Delete {manufacturer.Name}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _bl.DeleteManufacturer(manufacturer.Id);
                Manufacturers.Remove(manufacturer);
            }
        }
    }

    public class ManufacturerEditViewModel : ViewModelBase
    {
        private readonly IBLService _bl;
        private readonly Action<ViewModelBase> _navigate;
        private readonly IManufacturer _model;
        private readonly bool _isNew;

        public string Name
        {
            get => _model.Name;
            set { _model.Name = value; OnPropertyChanged(); }
        }

        public string Country
        {
            get => _model.Country;
            set { _model.Country = value; OnPropertyChanged(); }
        }

        public int FoundedYear
        {
            get => _model.FoundedYear;
            set { _model.FoundedYear = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ManufacturerEditViewModel(IManufacturer model, IBLService bl, Action<ViewModelBase> navigate)
        {
            _model = model;
            _bl = bl;
            _navigate = navigate;
            _isNew = model.Id == 0; // 0 to domyœlne Id

            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        private void Save()
        {
            try
            {
                if (_isNew) _bl.AddManufacturer(_model);
                else _bl.UpdateManufacturer(_model);
                _navigate?.Invoke(new ManufacturerListViewModel(_bl, _navigate));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Cancel()
        {
            _navigate?.Invoke(new ManufacturerListViewModel(_bl, _navigate));
        }
    }
}
