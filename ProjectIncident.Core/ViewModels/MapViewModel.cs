using ProjectIncident.Core.Commands;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProjectIncident.Core.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public bool IsLoading
        {
            get => (bool)GetProperty();
            set => SetProperty(value);
        }
        public MapType MapType
        {
            get => (MapType)GetProperty();
            set => SetProperty(value);
        }
        public ObservableCollection<Pin> Pins
        {
            get => (ObservableCollection<Pin>)GetProperty();
            set => SetProperty(value);
        }
        public MapSpan VisibleRegion
        {
            get => (MapSpan)GetProperty();
            set => SetProperty(value);
        }
        public string NewAddress
        {
            get => (string)GetProperty();
            set => SetProperty(value);
        }

        public DelegateCommand AddPinCommand
        {
            get => new DelegateCommand(async () => 
            {
                if (string.IsNullOrEmpty(NewAddress)) return;

                IsLoading = false;

                var locator = new Geocoder();
                var positions = await locator.GetPositionsForAddressAsync(NewAddress);
                var position = positions.FirstOrDefault();

                if (position == null)
                {
                    IsLoading = false;
                    return;
                }

                var mposition = new Position(position.Latitude, position.Longitude);

                IsLoading = false;

                VisibleRegion = MapSpan.FromCenterAndRadius(mposition, Distance.FromMeters(500));
                Pins.Add(new Pin()
                {
                    Address = NewAddress,
                    Label = "",
                    Position = mposition,
                    Type = PinType.Place
                });
                NewAddress = "";
            });
        }
        public DelegateCommand LocalizeCommand
        {
            get => new DelegateCommand(async () =>
            {
                IsLoading = true;

                if (!CrossGeolocator.Current.IsGeolocationAvailable)
                {
                    await Application.Current.MainPage.DisplayAlert("Localisation", "Le service de localisation n'est pas disponible sur votre appareil.", "OK");
                    IsLoading = false;
                    return;
                }
                if (!CrossGeolocator.Current.IsGeolocationEnabled)
                {
                    await Application.Current.MainPage.DisplayAlert("Localisation", "Le service de localisation n'est pas activé sur votre appareil.", "OK");
                    IsLoading = false;
                    return;
                }

                var position = await CrossGeolocator.Current.GetPositionAsync();
                var mposition = new Position(position.Latitude, position.Longitude);

                IsLoading = false;

                VisibleRegion = MapSpan.FromCenterAndRadius(mposition, Distance.FromMeters(500));
                Pins.Add(new Pin()
                {
                    Address = "",
                    Label = "Je suis ici !",
                    Position = mposition,
                    Type = PinType.SavedPin
                });
            });
        }
        public DelegateCommand<string> ChangeMapTypeCommand
        {
            get => new DelegateCommand<string>((param) =>
            {
                MapType = (MapType)Enum.Parse(typeof(MapType), param);
            });
        }


        public MapViewModel()
        {
            IsLoading = false;
            MapType = MapType.Street;
            Pins = new ObservableCollection<Pin>();
            NewAddress = "31 rue Smith, 69002 LYON";
        }

    }
}
