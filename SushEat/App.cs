using System;

using Xamarin.Forms;


namespace SushEat
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new ButtonDemoPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        /////////////////////////////////////////







	}

    class ButtonDemoPage : ContentPage
    {
        Label label;
        int clickTotal = 0;




    






        public ButtonDemoPage()
        {
            Label header = new Label
            {
                Text = "SushEat",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };

            Button RollButton = new Button
            {
                Text = "Roll Me",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            RollButton.Clicked += OnButtonClicked;

            Button FoldButton = new Button
            {
                Text = "Fold Me",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            FoldButton.Clicked += OnButtonClicked;

            label = new Label
            {
                Text = "0 button clicks",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    RollButton,
                    FoldButton,
                    label
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            clickTotal += 1;
            label.Text = String.Format("{0} button click{1}",
                                       clickTotal, clickTotal == 1 ? "" : "s");
        }
    }
}

