using System.Collections.ObjectModel;
using System.Windows.Controls;
using SimpleMvvmSamples.Messaging.Core.Models;
using SimpleMvvmSamples.Messaging.Core.ViewModels;

namespace SimpleMvvmSamples.Messaging.Core.Views
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        private readonly CustomerListViewModel _vm;
        public CustomerListView()
        {
            InitializeComponent();

            // Get a reference to the view-model
            _vm = (CustomerListViewModel)DataContext;

        }

        public ObservableCollection<Customer> Customers
        {
            get { return _vm.Customers; }
            set { _vm.Customers = value; }
        }

    }
}
