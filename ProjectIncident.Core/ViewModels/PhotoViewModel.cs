using ProjectIncident.Core.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectIncident.Core.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {

        public ObservableCollection<string> Photos
        {
            get => (ObservableCollection<string>)GetProperty();
            set => SetProperty(value);
        }
        public string SelectedPhoto
        {
            get => (string)GetProperty();
            set => SetProperty(value);
        }

        public DelegateCommand TakePhotoCommand
        {
            get => new DelegateCommand(async () =>
            {
                var imageString = "";

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable)
                {
                    await Application.Current.MainPage.DisplayAlert("Prise de photo", "Impossible de prendre une photo : votre appareil ne possède pas de caméra.", "OK");
                    return;
                }

                if (!Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Prise de photo", "Impossible de prendre une photo : votre appareil ne propose pas la prise de photos.", "OK");
                    return;
                }

                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                            new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                            {
                                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                                RotateImage = true,
                                CustomPhotoSize = 5,
                                AllowCropping = true,
                                CompressionQuality = 20
                            });

                if (photo != null)
                {
                    using (var stream = photo.GetStream())
                    {
                        byte[] imageBinary = new byte[stream.Length];
                        stream.Read(imageBinary, 0, (int)stream.Length);
                        imageString = Tools.Convert.BytesToBase64String(imageBinary);
                    }
                    System.IO.File.Delete(photo.Path);
                    Photos.Add(imageString);
                    SelectedPhoto = imageString;
                }
            });
        }

        public PhotoViewModel()
        {
            Photos = new ObservableCollection<string>();
            SelectedPhoto = "";
        }

    }
}
