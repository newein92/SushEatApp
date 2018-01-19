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
    [Activity(Label = "Incoming orders")]
    public class RestaurantChef : MainActivity
    {
        ListView listOfOrders;
        public List<String> incomingOrders;
        public static List<Customer> customerList = new List<Customer>();
        public static int currentOrder = -1;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.incomingOrders);
            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();
            incomingOrders = new List<String>();
            listOfOrders = FindViewById<ListView>(Resource.Id.listView1);
            listOfOrders.ItemClick += listOfOrders_ItemClick;

            TableQuery<Customer> query = new TableQuery<Customer>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "ORDERED"));
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<Customer> tableQueryResult = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = tableQueryResult.ContinuationToken;

                foreach (Customer customer in tableQueryResult.Results)
                {
                    customerList.Add(customer);
                    incomingOrders.Add(customer.Sorder);
                }
                var incomingOrdersArray = incomingOrders.ToArray();
                listOfOrders.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, incomingOrdersArray);
                listOfOrders.DeferNotifyDataSetChanged();
            } while (continuationToken != null);


        }

        void listOfOrders_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            currentOrder = e.Position;
            var intent = new Intent(this, typeof(processOrder));
            StartActivity(intent);
        }
    }

    
}