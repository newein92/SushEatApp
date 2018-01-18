﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;

//using Microsoft.WindowsAzure.MobileServices;

//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
using System.Linq;

namespace SushEat.Droid
{
    [Activity(Label = "Choose Vegetables")]
    public class Veg : RestaurantCustomer
    {
        ListView listvegs;
        List<VegItem> selectedVegItems;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Veg);
            selectedVegItems = new List<VegItem>();
            selectedVegItems.Insert(0, new VegItem("Cucumber", false));
            selectedVegItems.Insert(1, new VegItem("Sweet Potato", false));
            selectedVegItems.Insert(2, new VegItem("Chives", false));
            selectedVegItems.Insert(3, new VegItem("Carrot", false));
            selectedVegItems.Insert(4, new VegItem("Avocado", false));
            selectedVegItems.Insert(5, new VegItem("Shitake Mushrooms", false));
            var veglist = new string[]
            {
                "Cucumber","Sweet Potato","Chives","Carrot",
                "Avocado","Shitake Mushrooms"
            };
            listvegs = FindViewById<ListView>(Resource.Id.listView1);
            listvegs.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, veglist);
            listvegs.ChoiceMode = ChoiceMode.Multiple;
            Button vegOrder = FindViewById<Android.Widget.Button>(Resource.Id.vegOrder);
            vegOrder.Click += delegate
            {
                customer.order.selectedVegItems = selectedVegItems;
                Toast.MakeText(this, "The selected items were added to cart", ToastLength.Long).Show();
            };
            listvegs.ItemClick += listvegs_ItemClick;
        }
        void listvegs_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedVegItems.ElementAt<VegItem>(e.Position).selected = !(selectedVegItems.ElementAt<VegItem>(e.Position).selected);
        }

    }
}