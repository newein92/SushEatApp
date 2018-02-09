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
    [Activity(Label = "Incoming orders")]
    public class RestaurantChef : MainActivity
    {
        ListView listOfOrders;
        public List<String> incomingOrders;
        public List<Customer> customerList;
        public static int currentOrder = -1;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.incomingOrders);
            
            
            listOfOrders = FindViewById<ListView>(Resource.Id.order_list);
            Button refresh = FindViewById<Android.Widget.Button>(Resource.Id.refresh);

            // Retrieve the storage account from the connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susheattable;AccountKey=JnYEen+TGNIkA722hAMJTMfo+qNT3flVGDVScX158B3GckOPN+dtUOfWU2not3cjRPuqI4fQyhFq8wx/GY0I2g==;EndpointSuffix=core.windows.net");
            // Create the table client.
            //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "Customers" table.
            //CloudTable table = tableClient.GetTableReference("Customers");


            // Create the table if it doesn't exist.
            //await table.CreateIfNotExistsAsync();
            customerList = new List<Customer>();
            incomingOrders = new List<String>();

            TableQuery<Customer> query = new TableQuery<Customer>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "ORDERED"));
            TableContinuationToken continuationToken = null;

            refresh.Click += delegate
            {
                this.Recreate();
            };

            listOfOrders.ItemClick += listOfOrders_ItemClick;

            
            do
            {
                try
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

                }
                catch (Exception e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                }
                
            } while (continuationToken != null);


        }

        void listOfOrders_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            currentOrder = e.Position;
            var intent = new Intent(this, typeof(processOrder));
            this.Finish();
            StartActivity(intent);
        }
    }

    
}