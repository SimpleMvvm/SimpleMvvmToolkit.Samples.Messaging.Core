using System.Threading;
using SimpleMvvmSamples.Messaging.Core.Models;
using SimpleMvvmSamples.Messaging.Core.Views;
using SimpleMvvmToolkit.Express;

namespace SimpleMvvmSamples.Messaging.Core.ViewModels
{
    /// <summary>
    /// This class extends ViewModelDetailBase which implements IEditableDataObject.
    /// <para>
    /// Specify type being edited <strong>DetailType</strong> as the second type argument
    /// and as a parameter to the seccond ctor.
    /// </para>
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class CustomerDetailViewModel : ViewModelDetailBase<CustomerDetailViewModel, Customer>
    {
        private readonly SynchronizationContext _syncRoot;

        public CustomerDetailViewModel()
        {
            _syncRoot = SynchronizationContext.Current;

            // STEP 2: Register to get notified on order increase
            RegisterToReceiveMessages<IncreaseInfo, ApprovalInfo>
                (MessageTokens.IncreaseOrders, OnOrdersIncrease);
        }

        // STEP 3: Handle notification of total orders increase
        void OnOrdersIncrease(object sender, NotificationEventArgs
            <IncreaseInfo, ApprovalInfo> e)
        {
            // Exit if increase requested for another customer
            if (e.Data.CustomerName != Customer.CustomerName) return;
            _syncRoot.Post(s => GetApproval(e), null);
        }

        private void GetApproval(NotificationEventArgs<IncreaseInfo, ApprovalInfo> e)
        {
            // Prompt user for approval
            ApproveIncreaseView appoveView = new ApproveIncreaseView(e.Data);
            appoveView.Closed += (s, ea) =>
            {
                // Callback notifier with result
                ApprovalInfo approveInfo = new ApprovalInfo
                    (Customer.CustomerName, e.Data.Amount,
                        appoveView.DialogResult != null && (bool) appoveView.DialogResult);
                e.Completed(approveInfo);
            };
            appoveView.ShowDialog();
        }

        // Expose the model for data binding

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                NotifyPropertyChanged(m => m.Customer);
            }
        }
        //public Customer Customer
        //{
        //    get { return Model; }
        //    set { Model = value; }
        //}
    }
}