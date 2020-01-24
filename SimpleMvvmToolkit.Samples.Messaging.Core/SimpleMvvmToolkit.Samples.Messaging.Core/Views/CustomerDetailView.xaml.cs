using System.Windows.Controls;
using SimpleMvvmSamples.Messaging.Core.Models;
using SimpleMvvmSamples.Messaging.Core.ViewModels;

namespace SimpleMvvmSamples.Messaging.Core.Views
{
    /// <summary>
    /// Interaction logic for CustomerDetailView.xaml
    /// </summary>
    public partial class CustomerDetailView : UserControl
    {
        private readonly CustomerDetailViewModel _vm;
        public CustomerDetailView()
        {
            InitializeComponent();

            // Get a reference to the view-model
            _vm = (CustomerDetailViewModel)DataContext;
        }

        private int _customerIndex;
        public int CustomerIndex
        {
            get { return _customerIndex; }
            set
            {
                _customerIndex = value;
                _vm.Customer = Customer.CustomersList[value];
            }
        }

    }
}
