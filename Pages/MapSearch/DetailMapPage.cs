﻿using System.Threading.Tasks;
using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Localization;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using System;

namespace dpark.Pages.MapSearch
{
    public class DetailMapPage : ModelBoundContentPage<DetailInfoViewModel>
    {
        Map map;

        public DetailMapPage()
        {
            this.Title = "Map";
            map = new Map()
            {
                MapType = MapType.Street,
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await MakeMap();
        }
       
        public async Task MakeMap()
        {
            Task<Pin> getPinTask = ViewModel.GetPin();

            Pin pin = await getPinTask;

            map.Pins.Clear();
            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: map,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            Content = relativeLayout;
        }
    }
}
