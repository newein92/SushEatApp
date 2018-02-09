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
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount


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

        // Retrieve the storage account from the connection string.
        public static CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susheattable;AccountKey=JnYEen+TGNIkA722hAMJTMfo+qNT3flVGDVScX158B3GckOPN+dtUOfWU2not3cjRPuqI4fQyhFq8wx/GY0I2g==;EndpointSuffix=core.windows.net");
        // Create the table client.
        public static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        // Create the CloudTable object that represents the "Customers" table.
        public static CloudTable table = tableClient.GetTableReference("Customers");

        protected override async void OnCreate(Bundle bundle)
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


            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

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

