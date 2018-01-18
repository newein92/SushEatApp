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
//using Gcm.Client;

//using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
//using Microsoft.Azure; //Namespace for CloudConfigurationManager

using System.Collections.Generic;


//using Microsoft.WindowsAzure.MobileServices;

//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
using System.Linq;

namespace SushEat.Droid
{
    [Activity (Label = "SushEat",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.DeviceDefault")]
    public class MainActivity : Activity
    {
       

        protected override void OnCreate(Bundle bundle){
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainPage);
            Button PrivateUser = FindViewById<Android.Widget.Button>(Resource.Id.PrivateUser);
            Button RestaurantCustomer = FindViewById<Android.Widget.Button>(Resource.Id.RestaurantCustomer);
            Button RestaurantChef = FindViewById<Android.Widget.Button>(Resource.Id.RestaurantChef);
            
            RestaurantCustomer.Click += delegate
            {
                var intent = new Intent(this, typeof(RestaurantCustomer));
                StartActivity(intent);

            };
            PrivateUser.Click += delegate
            {
                var intent = new Intent(this, typeof(PrivateUser));
                StartActivity(intent);
            };
        }

    }
  

    public class BluetoothConnection
    {
        public void getAdapter() { this.thisAdapter = BluetoothAdapter.DefaultAdapter; }
        public void getDevice() { this.thisDevice = (from bd in this.thisAdapter.BondedDevices where bd.Name == "SUSHEAT" select bd).FirstOrDefault(); }
        public BluetoothAdapter thisAdapter { get; set; }
        public BluetoothDevice thisDevice { get; set; }
        public BluetoothSocket thisSocket { get; set; }
    }

    

    public class User : TableEntity { }

    public class Customer : User
    {
        public String fullName { get; set; }
        public String email { get; set; }
        public Order order { get; set; }
        public String newOrder { get; set; }
        public Customer ()
        {
            this.fullName = "NULL";
            this.email = "NULL";
            this.order = new Order();
            this.PartitionKey = "NOT ORDERED YET";
            this.RowKey = this.email;
        }
    }

    public class Restaurant : User
    {
        public String name { get; set; }
    }
}

