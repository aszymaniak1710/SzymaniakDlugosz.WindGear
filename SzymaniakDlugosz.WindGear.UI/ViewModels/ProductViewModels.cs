using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SzymaniakDlugosz.WindGear.Core;
using SzymaniakDlugosz.WindGear.Interfaces;
using SzymaniakDlugosz.WindGear.UI.MVVM;

namespace SzymaniakDlugosz.WindGear.UI.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        private readonly IBLService _bl;
        private readonly Action<ViewModelBase> _navigate;
        private ObservableCollection<IProduct> _allProducts;
        private string _searchText;

        public ObservableCollection<IProduct> Products { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    Filter();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public ProductListViewModel(IBLService bl, Action<ViewModelBase> navigate = null)
        {
            _bl = bl;
            _navigate = navigate;
            _allProducts = new ObservableCollection<IProduct>(_bl.GetProducts());
            Products = new ObservableCollection<IProduct>(_allProducts);

            AddCommand = new RelayCommand(o => Add());
            EditCommand = new RelayCommand(o => Edit(o as IProduct), o => o is IProduct);
            DeleteCommand = new RelayCommand(o => Delete(o as IProduct), o => o is IProduct);
        }

        private void Filter()
        {
            if (string.IsNullOrWhiteSpace(_searchText))
            {
                Products.Clear();
                foreach (var p in _allProducts) Products.Add(p);
            }
            else
            {
                var filtered = _allProducts.Where(p => 
                    (p.Model != null && p.Model.Contains(_searchText, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Manufacturer != null && p.Manufacturer.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                ).ToList();
                
                Products.Clear();
                foreach (var p in filtered) Products.Add(p);
            }
        }

        private void Add()
        {
            var newProd = _bl.CreateProduct();
            _navigate?.Invoke(new ProductEditViewModel(newProd, _bl, _navigate));
        }

        private void Edit(IProduct product)
        {
            _navigate?.Invoke(new ProductEditViewModel(product, _bl, _navigate));
        }

        private void Delete(IProduct product)
        {
             if (MessageBox.Show($"Delete {product.Model}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _bl.DeleteProduct(product.Id);
                _allProducts.Remove(product);
                Products.Remove(product);
            }
        }
    }

    public class ProductEditViewModel : ViewModelBase
    {
        private readonly IBLService _bl;
        private readonly Action<ViewModelBase> _navigate;
        private readonly IProduct _model;
        private readonly bool _isNew;

        public ObservableCollection<IManufacturer> AvailableManufacturers { get; }
        public ObservableCollection<SailType> SailTypes { get; }

        public string Model
        {
            get => _model.Model;
            set { _model.Model = value; OnPropertyChanged(); }
        }

        public double Area
        {
            get => _model.Area;
            set { _model.Area = value; OnPropertyChanged(); }
        }

        public string Material
        {
            get => _model.Material;
            set { _model.Material = value; OnPropertyChanged(); }
        }

        public SailType Type
        {
            get => _model.Type;
            set { _model.Type = value; OnPropertyChanged(); }
        }

        public bool IsCamber
        {
            get => _model.IsCamber;
            set { _model.IsCamber = value; OnPropertyChanged(); }
        }

        public int ManufacturerId
        {
            get => _model.ManufacturerId;
            set { _model.ManufacturerId = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ProductEditViewModel(IProduct model, IBLService bl, Action<ViewModelBase> navigate)
        {
            _model = model;
            _bl = bl;
            _navigate = navigate;
            _isNew = model.Id == 0;

            AvailableManufacturers = new ObservableCollection<IManufacturer>(_bl.GetManufacturers());
            SailTypes = new ObservableCollection<SailType>(Enum.GetValues(typeof(SailType)).Cast<SailType>());

            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        private void Save()
        {
            try
            {
                if (_isNew) _bl.AddProduct(_model);
                else _bl.UpdateProduct(_model);
                _navigate?.Invoke(new ProductListViewModel(_bl, _navigate));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Cancel()
        {
            _navigate?.Invoke(new ProductListViewModel(_bl, _navigate));
        }
    }
}
