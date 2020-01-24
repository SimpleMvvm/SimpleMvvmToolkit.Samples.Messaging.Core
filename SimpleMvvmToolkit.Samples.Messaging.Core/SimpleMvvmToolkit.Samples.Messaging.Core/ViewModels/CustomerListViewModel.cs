using System.Collections.ObjectModel;
using System.Windows;
using SimpleMvvmSamples.Messaging.Core.Models;
using SimpleMvvmToolkit.Express;

namespace SimpleMvvmSamples.Messaging.Core.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class CustomerListViewModel : ViewModelBase<CustomerListViewModel>
    {
        public CustomerListViewModel()
        {
            // Init customers
            this.Customers = Customer.CustomersList;
            this.SelectedCustomer = this.Customers[0];
        }

        private ObservableCollection<Customer> _customers =
            new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                NotifyPropertyChanged(m => m.Customers);
            }
        }

        private int _totalOrders;
        public int TotalOrders
        {
            get { return _totalOrders; }
            set
            {
                _totalOrders = value;
                NotifyPropertyChanged(m => m.TotalOrders);
            }
        }

        public int OrdersLimit => Customer.TotalOrdersLimit;

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                NotifyPropertyChanged(m => m.SelectedCustomer);
            }
        }

        private string _messageText;
        public string MessageText
        {
            get
            {
                return _messageText;
            }
            set
            {
                _messageText = value;
                NotifyPropertyChanged(m => m.MessageText);
            }
        }

        private Visibility _messageVisibility = Visibility.Collapsed;
        public Visibility MessageVisibility
        {
            get { return _messageVisibility; }
            set
            {
                _messageVisibility = value;
                NotifyPropertyChanged(m => m.MessageVisibility);
            }
        }

        // Increase selected customer orders by 1000
        public void IncreaseOrders()
        {
            if (SelectedCustomer == null) return;

            // Hide message
            MessageVisibility = Visibility.Collapsed;

            // Increase orders
            SelectedCustomer.Orders += 1000;
            var increaseInfo = new IncreaseInfo(SelectedCustomer.CustomerName, 1000);

            // STEP 4: Broadcast increase message using the MessageBus,
            // specifying a callback that the subscriber can invoke
            SendMessage(MessageTokens.IncreaseOrders, new NotificationEventArgs
                <IncreaseInfo, ApprovalInfo>(null, increaseInfo, OnIncreaseResponse));
        }

        // STEP 5: Handle callback from notification subscriber
        void OnIncreaseResponse(ApprovalInfo approve)
        {
            // Set message text
            string resultText = approve.Result ? "approved" : "rejected";
            MessageText = $"Order quantity of {approve.Amount} {resultText} for {approve.CustomerName}";
            MessageVisibility = Visibility.Visible;

            // Reverse increase if rejected
            if (!approve.Result)
            {
                SelectedCustomer.Orders -= 1000;
            }
        }
    }
}