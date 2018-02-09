using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;


namespace SushEat.Droid
{
    [Activity(Label = "Choose Sauces")]
    public class Sauce : RestaurantCustomer
    {
        ListView listsauce;
        List<SauceItem> selectedSauceItems;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Sauce);
            selectedSauceItems = new List<SauceItem>();
            selectedSauceItems.Insert(0, new SauceItem("Spicy Mayonnaise", false));
            selectedSauceItems.Insert(1, new SauceItem("Chili", false));
            selectedSauceItems.Insert(2, new SauceItem("Lemon", false));
            selectedSauceItems.Insert(3, new SauceItem("Soy", false));
            selectedSauceItems.Insert(4, new SauceItem("Ginger", false));
            selectedSauceItems.Insert(5, new SauceItem("Teriyaki", false));
            selectedSauceItems.Insert(6, new SauceItem("Wasabi", false));
            var saucelist = new string[]
            {
                "Spicy Mayonnaise","Chili","Lemon","Soy","Ginger",
                "Teriyaki","Wasabi"
            };
            listsauce = FindViewById<ListView>(Resource.Id.sauce_list);
            listsauce.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, saucelist);
            listsauce.ChoiceMode = ChoiceMode.Multiple;
            Button sauceOrder = FindViewById<Android.Widget.Button>(Resource.Id.sauceOrder);
            sauceOrder.Click += delegate
            {
                customer.order.selectedSauceItems = selectedSauceItems;
                Toast.MakeText(this, "The selected items were added to cart", ToastLength.Long).Show();
            };
            listsauce.ItemClick += listSauce_ItemClick;
        }
        void listSauce_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedSauceItems.ElementAt<SauceItem>(e.Position).selected = !(selectedSauceItems.ElementAt<SauceItem>(e.Position).selected);
        }
    }
}