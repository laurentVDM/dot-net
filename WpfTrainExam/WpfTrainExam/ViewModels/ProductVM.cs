using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WpfApplication1.ViewModels;
using WpfTrainExam.Models;

namespace WpfTrainExam.ViewModels
{
    internal class ProductVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
        private ProductModel _selectedProduct;
        private ObservableCollection<ProductModel> _productsList;
        private ObservableCollection<CountryProductCountModel> _countryProductCounts;
        private DelegateCommand _discontinueCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<ProductModel> ProductsList
        {
            get
            {
                if (_productsList == null)
                {
                    _productsList = LoadProducts();
                }

                return _productsList;
            }
        }

        public ObservableCollection<CountryProductCountModel> CountryProductCounts
        {
            get
            {
                if (_countryProductCounts == null)
                {
                    _countryProductCounts = LoadCountryProductCounts();
                }

                return _countryProductCounts;
            }
        }

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }

        public DelegateCommand DiscontinueCommand
        {
            get { return _discontinueCommand = _discontinueCommand ?? new DelegateCommand(DiscontinueProduct); }
        }

        private void DiscontinueProduct()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.Discontinued = true;
                ProductsList.Remove(SelectedProduct);
            }
        }

        private ObservableCollection<ProductModel> LoadProducts()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach (var item in dc.Products.Where(p => !p.Discontinued))
            {
                localCollection.Add(new ProductModel(item));
            }

            return localCollection;
        }

        private ObservableCollection<CountryProductCountModel> LoadCountryProductCounts()
        {
            var countryProductCounts = dc.Orders
                .Where(o => o.OrderDetails.Any())
                .GroupBy(o => o.ShipCountry)
                .Select(group => new CountryProductCountModel
                {
                    Country = group.Key,
                    ProductCount = group.SelectMany(o => o.OrderDetails).Count()
                })
                .OrderByDescending(c => c.ProductCount)
                .ToList();

            return new ObservableCollection<CountryProductCountModel>(countryProductCounts);
        }
    }
}
