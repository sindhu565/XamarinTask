using DemoAPP.DBFolder;
using DemoAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        RegisterPageViewModel _viewModel = RegisterPageViewModel.GetInstance();
        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }



        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {

                var details = (UserDetailsTable)BindingContext;
                details.userName = _viewModel.UserName;
                details.MobileNumber = _viewModel.MobileNumber;
                details.Email = _viewModel.Email;
                details.designation = _viewModel.Designation;
                await App.Database.SaveNoteAsync(details);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {

            }
        }



        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.IsVisibleUserNameErrorMessage = false;
        }

        
        private void Desigination_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.IsVisibileDesignationErrmsg = false;
        }

       
      


        private void MobileNunber(object sender, TextChangedEventArgs e)
        {
            _viewModel.IsMobileNoErrorMessageVisible = false;
        }


        private void Email(object sender, TextChangedEventArgs e)
        {
          
            _viewModel.IsVisibleMailErrorMessage = false;

        }
    }
}