using System;
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
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
//using Microsoft.Azure; // Namespace for CloudConfigurationManager

namespace SushEat.Droid
{
    [Activity(Label = "Choose Category")]
    public class RestaurantCustomer : MainActivity
    {
        public static Customer customer = new Customer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Menu);
            var fullName = FindViewById<EditText>(Resource.Id.fullName);
            var email = FindViewById<EditText>(Resource.Id.email);
            Button Veg = FindViewById<Android.Widget.Button>(Resource.Id.Veg);
            Button Sauce = FindViewById<Android.Widget.Button>(Resource.Id.Sauce);
            Button Rolls = FindViewById<Android.Widget.Button>(Resource.Id.Rolls);
            Button Debug = FindViewById<Android.Widget.Button>(Resource.Id.Debug);
            Button sendOrder = FindViewById<Android.Widget.Button>(Resource.Id.sendOrder);

            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susheattable;AccountKey=JnYEen+TGNIkA722hAMJTMfo+qNT3flVGDVScX158B3GckOPN+dtUOfWU2not3cjRPuqI4fQyhFq8wx/GY0I2g==;EndpointSuffix=core.windows.net");
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("Customers");

            // Create the table if it doesn't exist.
            table.CreateIfNotExistsAsync();


            Veg.Click += delegate
            {
                var intent = new Intent(this, typeof(Veg));
                StartActivity(intent);

            };
            Sauce.Click += delegate
            {
                var intent = new Intent(this, typeof(Sauce));
                StartActivity(intent);
            };
            Rolls.Click += delegate
            {
                var intent = new Intent(this, typeof(Rolls));
                StartActivity(intent);
            };
            Debug.Click += async delegate
            {
                /*Console.WriteLine(customer.fullName);
                Console.WriteLine(customer.email);
                Console.WriteLine("Selected vegs:");
                for (int i = 0; i < 6; i++)
                {
                    if (customer.order.selectedVegItems.ElementAt<VegItem>(i).selected)
                    {
                        Console.WriteLine(customer.order.selectedVegItems.ElementAt<VegItem>(i).item);
                    }
                    
                }
                Console.WriteLine("Selected Sauces:");
                for (int i = 0; i < 7; i++)
                {
                    if (customer.order.selectedSauceItems.ElementAt<SauceItem>(i).selected)
                    {
                        Console.WriteLine(customer.order.selectedSauceItems.ElementAt<SauceItem>(i).item);
                    }

                }
                Console.WriteLine("Selected Rolls:");
                for (int i = 0; i < 3; i++)
                {
                    if (customer.order.selectedRollItems.ElementAt<RollItem>(i).selected)
                    {
                        Console.WriteLine(customer.order.selectedRollItems.ElementAt<RollItem>(i).item);
                    }

                }*/


                /*
                // Create a retrieve operation that takes a customer entity.
                TableOperation retrieveOperation = TableOperation.Retrieve<Customer>("Email", "Full name");

                // Execute the retrieve operation.
                TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);



                // Print the phone number of the result.
                if (retrievedResult.Result != null)
                {
                    Console.WriteLine(((Customer)retrievedResult.Result).email);
                }
                else
                {
                    Console.WriteLine("The email could not be retrieved.");
                }*/
            };
            sendOrder.Click += delegate
            {
                customer.fullName = fullName.Text;
                customer.email = email.Text;
                customer.RowKey = email.Text;
                customer.PartitionKey = "ORDERED";
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(customer);
                // Execute the insert operation.
                table.ExecuteAsync(insertOperation);
            };
        }
    }

    public class customerItem
    {
        public String item { get; set; }
        public bool selected { get; set; }
        public customerItem(String item, bool selected)
        {
            this.item = item;
            this.selected = selected;
        }
        public customerItem()
        {
            this.item = "NULL";
            this.selected = false;
        }
    }

    public class VegItem : customerItem
    {
        public VegItem(String item, bool selected)
        {
            this.item = item;
            this.selected = selected;
        }
    }

    public class SauceItem : customerItem
    {
        public SauceItem(String item, bool selected)
        {
            this.item = item;
            this.selected = selected;
        }
    }

    public class RollItem : customerItem
    {
        public RollItem(String item, bool selected)
        {
            this.item = item;
            this.selected = selected;
        }
    }

    public class Order
    {
        // NOTE: selectedVegItems or selectedRollItems has to be an empty list.
        public List<VegItem> selectedVegItems { get; set; }
        public List<SauceItem> selectedSauceItems { get; set; }
        public List<RollItem> selectedRollItems { get; set; }
        public Order()
        {
            this.selectedVegItems = new List<VegItem>();
            selectedVegItems.Insert(0, new VegItem("Cucumber", false));
            selectedVegItems.Insert(1, new VegItem("Sweet Potato", false));
            selectedVegItems.Insert(2, new VegItem("Chives", false));
            selectedVegItems.Insert(3, new VegItem("Carrot", false));
            selectedVegItems.Insert(4, new VegItem("Avocado", false));
            selectedVegItems.Insert(5, new VegItem("Shitake Mushrooms", false));

            this.selectedSauceItems = new List<SauceItem>();
            selectedSauceItems.Insert(0, new SauceItem("Spicy Mayonnaise", false));
            selectedSauceItems.Insert(1, new SauceItem("Chili", false));
            selectedSauceItems.Insert(2, new SauceItem("Lemon", false));
            selectedSauceItems.Insert(3, new SauceItem("Soy", false));
            selectedSauceItems.Insert(4, new SauceItem("Ginger", false));
            selectedSauceItems.Insert(5, new SauceItem("Teriyaki", false));
            selectedSauceItems.Insert(6, new SauceItem("Wasabi", false));
            
            this.selectedRollItems = new List<RollItem>();
            selectedRollItems.Insert(0, new RollItem("Green Roll", false));
            selectedRollItems.Insert(1, new RollItem("Vegetarian Roll", false));
            selectedRollItems.Insert(2, new RollItem("Winter Roll", false));
        }
    }
}