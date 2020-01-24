using System.Collections.ObjectModel;
using SimpleMvvmToolkit.Express;

namespace SimpleMvvmSamples.Messaging.Core.Models
{
    public class Customer : ModelBase<Customer>
    {
        // Manufacture a list of customers
        private static ObservableCollection<Customer> _customersList;
        public static ObservableCollection<Customer> CustomersList
        {
            get
            {
                if (_customersList == null)
                {
                    _customersList = new ObservableCollection<Customer>
                    {
                        new Customer { CustomerName = "Bill Gates", Orders = 1000 },
                        new Customer { CustomerName = "Steve Jobs", Orders = 2000 },
                        new Customer { CustomerName = "Mark Zuckerberg", Orders = 3000 }
                    };
                }
                return _customersList;
            }
        }

        // Total orders limit
        public static int TotalOrdersLimit => 10000;

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                NotifyPropertyChanged(m => m.CustomerName);
            }
        }

        private int _orders;
        public int Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                NotifyPropertyChanged(m => m.Orders);
            }
        }
    }
}
