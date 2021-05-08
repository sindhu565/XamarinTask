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
           // picker.ShowDividers = ShowDividers.None
        }



        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {

                var details = (UserDetailsTable)BindingContext;
                details.userName = _viewModel.UserName;
                details.gender = _viewModel.Gender;
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

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
              var picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;

                if (selectedIndex != -1)
                {
                   _viewModel.Gender = (string)picker.ItemsSource[selectedIndex];
                }
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _viewModel.TakePicture();
        }
    }
}