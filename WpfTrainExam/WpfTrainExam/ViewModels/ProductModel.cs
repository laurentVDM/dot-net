using System;
using System.ComponentModel;
using WpfTrainExam.Models;

namespace WpfTrainExam.ViewModels
{
    public class ProductModel : INotifyPropertyChanged
    {
        private readonly Product _myProduct;

        public ProductModel(Product current)
        {
            this._myProduct = current;
        }

        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ProductId
        {
            get { return _myProduct.ProductId; }
            set
            {
                _myProduct.ProductId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        public string ProductName
        {
            get { return _myProduct.ProductName; }
            set
            {
                _myProduct.ProductName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public string Category
        {
            get { return _myProduct.Category?.CategoryName; }
            set
            {
                if (_myProduct.Category == null)
                {
                    _myProduct.Category = new Category { CategoryName = value };
                }
                else
                {
                    _myProduct.Category.CategoryName = value;
                }

                OnPropertyChanged(nameof(Category));
            }
        }

        public string ContactName
        {
            get { return _myProduct.Supplier?.ContactName; }
            set
            {
                // Assuming you want to set the contact name
                if (_myProduct.Supplier == null)
                {
                    _myProduct.Supplier = new Supplier { ContactName = value };
                }
                else
                {
                    _myProduct.Supplier.ContactName = value;
                }

                OnPropertyChanged(nameof(ContactName));
            }
        }

        public bool Discontinued
        {
            get { return _myProduct.Discontinued; }
            set
            {
                _myProduct.Discontinued = value;
                OnPropertyChanged(nameof(Discontinued));
            }
        }

    }
}
