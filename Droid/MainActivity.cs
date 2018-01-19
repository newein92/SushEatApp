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
        // Retrieve the storage account from the connection string.
        public static CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susheattable;AccountKey=JnYEen+TGNIkA722hAMJTMfo+qNT3flVGDVScX158B3GckOPN+dtUOfWU2not3cjRPuqI4fQyhFq8wx/GY0I2g==;EndpointSuffix=core.windows.net");
        // Create the table client.
        public static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        // Create the CloudTable object that represents the "people" table.
        public static CloudTable table = tableClient.GetTableReference("Customers");

        protected override void OnCreate(Bundle bundle)
        {
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
            RestaurantChef.Click += delegate
            {
                var intent = new Intent(this, typeof(RestaurantChef));
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
        public String Sorder { get; set; }
        public Customer ()
        {
            this.fullName = "NULL";
            this.email = "NULL";
            this.order = new Order();
            this.Sorder = "NULL";
            this.PartitionKey = "NOT ORDERED YET";
            this.RowKey = this.email;
        }
    }

    public class Restaurant : User
    {
        public String name { get; set; }
    }
}

