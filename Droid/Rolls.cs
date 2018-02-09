using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;

namespace SushEat.Droid
{
    [Activity(Label = "Choose Rolls")]
    public class Rolls : RestaurantCustomer
    {
        ListView listrolls;
        List<RollItem> selectedRollItems;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Rolls);
            selectedRollItems = new List<RollItem>();
            selectedRollItems.Insert(0, new RollItem("Green Roll", false));
            selectedRollItems.Insert(1, new RollItem("Vegetarian Roll", false));
            selectedRollItems.Insert(2, new RollItem("Winter Roll", false));
            var rolllist = new string[]
            {
               "Green Roll- cucumber, chives, cream cheese, red tuna",
                "Vegetarian Roll- cucumber, carrot, avocado",
                "Winter Roll- salmon, mushrooms, sweet potato"
            };
            listrolls = FindViewById<ListView>(Resource.Id.roll_list);
            listrolls.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, rolllist);
            listrolls.ChoiceMode = ChoiceMode.Multiple;
            Button rollOrder = FindViewById<Android.Widget.Button>(Resource.Id.rollOrder);
            rollOrder.Click += delegate
            {
                customer.order.selectedRollItems = selectedRollItems;
                Toast.MakeText(this, "The selected items were added to cart", ToastLength.Long).Show();
            };
            listrolls.ItemClick += listrolls_ItemClick;
        }
        void listrolls_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedRollItems.ElementAt<RollItem>(e.Position).selected = !(selectedRollItems.ElementAt<RollItem>(e.Position).selected);
        }
    }
}