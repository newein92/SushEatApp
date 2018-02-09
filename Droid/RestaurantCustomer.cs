using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace SushEat.Droid
{
    [Activity(Label = "Choose Category")]
    public class RestaurantCustomer : MainActivity
    {
        public static Customer customer = new Customer();

        protected override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Menu);

            
       

            // Retrieve the storage account from the connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susheattable;AccountKey=JnYEen+TGNIkA722hAMJTMfo+qNT3flVGDVScX158B3GckOPN+dtUOfWU2not3cjRPuqI4fQyhFq8wx/GY0I2g==;EndpointSuffix=core.windows.net");
            // Create the table client.
            //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "Customers" table.
            //CloudTable table = tableClient.GetTableReference("Customers");
            
            Button Veg = FindViewById<Android.Widget.Button>(Resource.Id.Veg);
            Button Sauce = FindViewById<Android.Widget.Button>(Resource.Id.Sauce);
            Button Rolls = FindViewById<Android.Widget.Button>(Resource.Id.Rolls);
            Button sendOrder = FindViewById<Android.Widget.Button>(Resource.Id.sendOrder);
            //customer = new Customer();
            var fullName = FindViewById<EditText>(Resource.Id.fullName);
            var email = FindViewById<EditText>(Resource.Id.email);

            if (newCustomerActivity)
            {
                customer = new Customer();
            }
            newCustomerActivity = false;

            // Create the table if it doesn't exist.
            //await table.CreateIfNotExistsAsync();

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
            
            sendOrder.Click += async delegate
            {
                customer.fullName = fullName.Text;
                customer.email = email.Text;

                String vegs = "Vegetables: ";
                foreach (VegItem veg in customer.order.selectedVegItems)
                {
                    if (veg.selected)
                    {
                        vegs += (veg.item + ", ");
                    }
                }
                if (vegs.LastIndexOf(',') > -1) vegs.Remove(vegs.LastIndexOf(','), 2);

                String sauces = "Sauces: ";
                foreach (SauceItem sauce in customer.order.selectedSauceItems)
                {
                    if (sauce.selected)
                    {
                        sauces += (sauce.item + ", ");
                    }
                }
                if (sauces.LastIndexOf(',') > -1) sauces.Remove(sauces.LastIndexOf(','), 2);

                String rolls = "Rolls: ";
                foreach (RollItem roll in customer.order.selectedRollItems)
                {
                    if (roll.selected)
                    {
                        rolls += (roll.item + ", ");
                    }
                }
                if (rolls.LastIndexOf(',') > -1) rolls.Remove(rolls.LastIndexOf(','), 2);



                customer.Sorder = "Customer: " + customer.fullName + "\nEmail: " + customer.email + "\n" + vegs + "\n" + sauces + "\n" + rolls;
                customer.RowKey = customer.email;
                customer.PartitionKey = "ORDERED";
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.InsertOrReplace(customer);
                // Execute the insert operation.
                try 
                {
                    await table.ExecuteAsync(insertOperation);
                }
                catch (Exception e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                }
                customer = new Customer();
                Toast.MakeText(this, "The order has been sent", ToastLength.Long).Show();
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