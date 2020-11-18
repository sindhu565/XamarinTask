using DemoAPP.DBFolder;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoAPP.ViewModels
{
    public class RegisterPageViewModel : BaseviewModel
    {
        #region Punlic Properties
        private static RegisterPageViewModel _instance = new RegisterPageViewModel();
        public static RegisterPageViewModel GetInstance()
        {
            return _instance;

        }
        public RegisterPageViewModel()
        {


        } 
        #endregion

        #region Commands
        public ICommand TakePictureCommand => new Command(async () => await TakePicture());
        public ICommand SaveButtonCommand => new Command(async () => await onSaveButtonClick());
        public ICommand CancelCommand => new Command(async () =>  OnCancelClick());
        #endregion

        #region PublicMethods
        private void OnCancelClick()
        {
            clearUserDetials();
        }

        private async Task onSaveButtonClick()
        {

            try
            {
                if (ValidationsCheck())
                {

                    UserDetailsTable details = new UserDetailsTable();
                    details.userName = UserName;
                    details.MobileNumber = MobileNumber;
                    details.Email = Email;
                    details.designation = Designation;
                    details.thumbNail = ThumbNail;
                    await App.Database.SaveNoteAsync(details);
                    var action = await App.Current.MainPage.DisplayActionSheet("Information", "Done", null, "Your details saved in DB");
                    if (action != null)
                    {
                        clearUserDetials();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void clearUserDetials()
        {
            UserName = string.Empty;
            MobileNumber = string.Empty;
            Designation = string.Empty;
            Email = string.Empty;
            UserImage = string.Empty;
            ThumbNail = string.Empty;
        }

        public async Task TakePicture()
        {
            await CrossMedia.Current.Initialize();
            try
            {

                var action = await App.Current.MainPage.DisplayActionSheet("Choose Camera", "Cancel", null, "Camera", "Gallery");
                if (action == "Gallery")
                {

                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });

                    if (file == null) return;
                    UserImage = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });


                }


                else
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        // await DisplayAlert("No Camera", ":( No camera available.", "OK");
                        return;
                    }
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });
                    if (file == null)
                        return;
                    UserImage = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });


                }
            }


            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        public bool ValidationsCheck()
        {

            if (string.IsNullOrEmpty(userName))
            {
                IsVisibleUserNameErrorMessage = true;
                UserNameErrorMessage = "Please Enter UserName.";
            }
            else if (string.IsNullOrEmpty(Designation))
            {

                IsVisibileDesignationErrmsg = true;
                DesignationErrmsg = "Please Enter Designation.";

            }
            else if (string.IsNullOrEmpty(Email))
            {

                IsVisibleMailErrorMessage = true;
                EmailErrorMessage = "Please Enter MailId.";

            }
            else if (string.IsNullOrEmpty(MobileNumber))
            {

                IsMobileNoErrorMessageVisible = true;
                MobileErrorMessage = "Please Enter MObileNumber.";

            }
            //else if (string.IsNullOrEmpty(UserImage))
            //{
            //    ImageErrorMessage = true;
            //    IsvisibileImageErrorMsg = "Please choose File";
            //}

            else
            {
                return true;
            }

            return default;
        } 
        #endregion

        #region Models

        private bool imageErrorMessage;
        public bool ImageErrorMessage
        {
            get { return imageErrorMessage; }
            set { imageErrorMessage = value; OnPropertyChanged(nameof(ImageErrorMessage)); }
        }
        private string isvisibileImageErrorMsg;
        public string IsvisibileImageErrorMsg
        {
            get { return isvisibileImageErrorMsg; }
            set { isvisibileImageErrorMsg = value; OnPropertyChanged(nameof(IsvisibileImageErrorMsg)); }
        }
        private bool isVisibleMailErrorMessage;
        public bool IsVisibleMailErrorMessage
        {
            get { return isVisibleMailErrorMessage; }
            set { isVisibleMailErrorMessage = value; OnPropertyChanged(nameof(IsVisibleMailErrorMessage)); }
        }
        private string emailErrorMessage;
        public string EmailErrorMessage
        {
            get { return emailErrorMessage; }
            set { emailErrorMessage = value; OnPropertyChanged(nameof(EmailErrorMessage)); }
        }
        private string designationErrmsg;
        public string DesignationErrmsg
        {
            get { return designationErrmsg; }
            set { designationErrmsg = value; OnPropertyChanged(nameof(DesignationErrmsg)); }
        }
        private bool isVisibileDesignationErrmsg;
        public bool IsVisibileDesignationErrmsg
        {
            get { return isVisibileDesignationErrmsg; }
            set { isVisibileDesignationErrmsg = value; OnPropertyChanged(nameof(IsVisibileDesignationErrmsg)); }
        }

        private bool isMobileNoErrorMessageVisible;
        public bool IsMobileNoErrorMessageVisible
        {
            get { return isMobileNoErrorMessageVisible; }
            set { isMobileNoErrorMessageVisible = value; OnPropertyChanged(nameof(IsMobileNoErrorMessageVisible)); }
        }
        private string mobileErrorMessage;
        public string MobileErrorMessage
        {
            get { return mobileErrorMessage; }
            set { mobileErrorMessage = value; OnPropertyChanged(nameof(MobileErrorMessage)); }
        }

        private string thumbNail;
        public string ThumbNail
        {
            get { return thumbNail; }
            set { thumbNail = value; OnPropertyChanged(nameof(ThumbNail)); }
        }
        private ImageSource userImage;
        public ImageSource UserImage
        {
            get
            {
                return userImage;
            }
            set
            {
                userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName)); }
        }
        private string mobileNumber;
        public string MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; OnPropertyChanged(nameof(MobileNumber)); }

        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string designation;
        public string Designation
        {
            get { return designation; }
            set { designation = value; OnPropertyChanged(nameof(Designation)); }
        }

        private bool isVisibleUserNameErrorMessage;
        public bool IsVisibleUserNameErrorMessage
        {
            get { return isVisibleUserNameErrorMessage; }
            set { isVisibleUserNameErrorMessage = value; OnPropertyChanged(nameof(IsVisibleUserNameErrorMessage)); }
        }
        private string userNameErrorMessage;
        public string UserNameErrorMessage
        {
            get { return userNameErrorMessage; }
            set { userNameErrorMessage = value; OnPropertyChanged(nameof(UserNameErrorMessage)); }
        }
        #endregion
    }
}
