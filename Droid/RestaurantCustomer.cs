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

namespace SushEat.Droid
{
    [Activity(Label = "Choose Category")]
    public class RestaurantCustomer : Activity
    {
        //private ListView listnames;
        //private List<string> itemlist;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Menu);



            Button Veg = FindViewById<Android.Widget.Button>(Resource.Id.Veg);
            Button Sauce = FindViewById<Android.Widget.Button>(Resource.Id.Sauce);
            Button Rolls = FindViewById<Android.Widget.Button>(Resource.Id.Rolls); 

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
            // private void Listnames_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
            //{
            //  Toast.MakeText(this, e.Position.ToString(), ToastLength.Long).Show();
            //}
            /* void OrderButton_Click(object sender, EventArgs e)
             {
                 var builder = new StringBuilder();
                 var sparseArray = FindViewById<ListView>(Resource.Id.listView1).CheckedItemPositions;
                 for (var i=0; i < sparseArray.Size() ; i++)
                 {
                     builder.AppendLine(string.Format("{0}={1}", sparseArray.KeyAt(i), sparseArray.ValueAt(i)));
                 }
             }*/

        }

    }
}