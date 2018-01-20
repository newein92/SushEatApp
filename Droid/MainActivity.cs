using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using Android.Util;

namespace SushEat.Droid
{
    [Activity (Label = "SushEat",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.DeviceDefault")]
    public class MainActivity : Activity
    {
        public static bool newCustomerActivity = false;
        
        public const string TAG = "MainActivity";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainPage);
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }
            Button PrivateUser = FindViewById<Android.Widget.Button>(Resource.Id.PrivateUser);
            Button RestaurantCustomer = FindViewById<Android.Widget.Button>(Resource.Id.RestaurantCustomer);
            Button RestaurantChef = FindViewById<Android.Widget.Button>(Resource.Id.RestaurantChef);
            
            RestaurantCustomer.Click += delegate
            {
                newCustomerActivity = true;
                var intent = new Intent(this, typeof(RestaurantCustomer));
                this.Finish();
                StartActivity(intent);

            };
            PrivateUser.Click += delegate
            {
                var intent = new Intent(this, typeof(PrivateUser));
                this.Finish();
                StartActivity(intent);
            };
            RestaurantChef.Click += delegate
            {
                var intent = new Intent(this, typeof(RestaurantChef));
                this.Finish();
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

