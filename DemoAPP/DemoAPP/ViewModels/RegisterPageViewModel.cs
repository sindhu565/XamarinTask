using DemoAPP.DBFolder;
using DemoAPP.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
        private ImageSource imageSource;
        private MediaFile file;

        public RegisterPageViewModel()
        {


        }
        #endregion

        #region Commands
        public ICommand TakePictureCommand => new Command(async () => await TakePicture());
        public ICommand SaveButtonCommand => new Command(async () => await onSaveButtonClick());
        public ICommand CancelCommand => new Command(async () => OnCancelClick());
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
                    details.thumbNail = ThumbNail;
                    details.gender = gender;
                    await App.Database.SaveNoteAsync(details);
                    var action = await App.Current.MainPage.DisplayActionSheet("Information", "Done", null, "Your details saved in DB");
                    if (action != null)
                    {
                        clearUserDetials();
                    }

                    // to show OtherPage and be able to go back
                    await (Application.Current.MainPage as NavigationPage).PushAsync(new UserDetails());

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void clearUserDetials()
        {
            UserName = string.Empty;
            UserImage = string.Empty;
            ThumbNail = string.Empty;
        }

        public async Task TakePicture()
        {
            //await CrossMedia.Current.Initialize();
            //try
            //{

            //    var action = await App.Current.MainPage.DisplayActionSheet("Choose Camera", "Cancel", null, "Camera", "Gallery");
            //    if (action == "Gallery")
            //    {

            //        var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            //        {
            //            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            //        });

            //        if (file == null) return;
            //        UserImage = ImageSource.FromStream(() =>
            //        {
            //            var stream = file.GetStream();
            //            return stream;
            //        });


            //    }


            //    else
            //    {
            //        await CrossMedia.Current.Initialize();

            //        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //        {
            //            // await DisplayAlert("No Camera", ":( No camera available.", "OK");
            //            return;
            //        }
            //        var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //        {
            //            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            //        });
            //        if (file == null)
            //            return;
            //        UserImage = ImageSource.FromStream(() =>
            //        {
            //            var stream = file.GetStream();
            //            return stream;
            //        });


            //    }
            try
            {
                await CrossMedia.Current.Initialize();

                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    var source = await App.Current.MainPage.DisplayActionSheet("Choose Camera", "Cancel", null, "Camera", "Gallery");

                    if (source == "Cancel")
                    {

                        file = null;
                    }
                    else if (source == "Gallery")
                    {

                        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            PhotoSize = PhotoSize.Small
                        });
                       
                    }
                    else if (source == "Camera")
                    {

                        
                            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Name = "prueba.jpeg",
                                PhotoSize = PhotoSize.Small,
                                SaveToAlbum = true
                            }); 
                       
                        
                    }
                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Small
                    });
                }

                if (file != null)
                {
                    imageSource = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                    UserImage = imageSource;
                }
            }

            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        public bool ValidationsCheck()
        {

            if (string.IsNullOrEmpty(UserName))
            {
                IsVisibleUserNameErrorMessage = true;
                UserNameErrorMessage = "Please Enter UserName.";
            }
            else if (string.IsNullOrEmpty(Gender))
            {

                IsGenderMessageVisible = true;
                GenderErrorMessage = "Please Select Gender.";

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


        private bool isGenderMessageVisible;
        public bool IsGenderMessageVisible
        {
            get { return isGenderMessageVisible; }
            set { isGenderMessageVisible = value; OnPropertyChanged(nameof(IsGenderMessageVisible)); }
        }
        private string genderErrorMessage;
        public string GenderErrorMessage
        {
            get { return genderErrorMessage; }
            set { genderErrorMessage = value; OnPropertyChanged(nameof(GenderErrorMessage)); }
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
        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged(nameof(Gender)); }

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
