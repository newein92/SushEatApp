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
//using System;

//using Android.App;
//using Android.Content;
using Android.Content.PM;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.OS;
using Android.Bluetooth;

//using Microsoft.WindowsAzure.MobileServices;

//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
using System.Linq;

namespace SushEat.Droid
{
    [Activity(Label = "Choose Sauces")]
    public class Sauce : Activity
    {
        ListView listsauce;
        //private List<string> saucelist;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Sauce);

            var saucelist = new string[]
            {
                "Spicy Mayonnaise","Chili","Lemon","Soy","Ginger",
                "Teriyaki","Wasabi"
            };

            //var OrderButton = FindViewById<Button>(Resource.Id.buttonorder);
            //OrderButton.Click += OrderButton_Click;

            listsauce = FindViewById<ListView>(Resource.Id.listView1);
            listsauce.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, saucelist);
            listsauce.ChoiceMode = ChoiceMode.Multiple;

            //listnames.ItemClick += Listnames_ItemClick;
        }

        // private void Listnames_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //  Toast.MakeText(this, e.Position.ToString(), ToastLength.Long).Show();
        //}
     /*  void OrderButton_Click(object sender, EventArgs e)
        {
            var builder = new StringBuilder();
            var sparseArray = FindViewById<ListView>(Resource.Id.listView1).CheckedItemPositions;
            for (var i = 0; i < sparseArray.Size(); i++)
            {
                builder.AppendLine(string.Format("{0}={1}", sparseArray.KeyAt(i), sparseArray.ValueAt(i)));
            }
        }*/

    }
}