using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Linq;

namespace SushEat.Droid{
	[Activity (Label = "SushEat.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : Activity
    {
        BluetoothConnection myConnection = new BluetoothConnection();
        protected override void OnCreate(Bundle bundle){
            base.OnCreate(bundle);

           

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.operateMachine);
            // Get our button from the layout resource,
            // and attach an event to it
            Android.Widget.Button buttonConnect = FindViewById<Android.Widget.Button>(Resource.Id.button1);
            Android.Widget.Button buttonDisconnect = FindViewById<Android.Widget.Button>(Resource.Id.button2);
            Android.Widget.Button button1On = FindViewById<Android.Widget.Button>(Resource.Id.button3);
            Android.Widget.Button button2On = FindViewById<Android.Widget.Button>(Resource.Id.button4);

            //Android.Widget.Button SushiMenu = FindViewById<Android.Widget.Button>(Resource.Id.button5);





            TextView connected = FindViewById<TextView>(Resource.Id.textView1);
            BluetoothSocket _socket = null;
            System.Threading.Thread listenThread = new System.Threading.Thread(listener);
            listenThread.Abort();

            //SushiMenu.Click += delegate
            //{
                //SetContentView(Resource.Layout.SushiMenu);
                // Code omitted for clarity

                /*int initialSelection = 4;

                // Initialize the ListView
                Android.Widget.ListView listView = FindViewById<Android.Widget.ListView>(Resource.Id.listView1);
                //listView.Adapter = new HomeScreenAdapter(this, tableItems);

                // Important - Set the ChoiceMode
                listView.ChoiceMode = ChoiceMode.Single;

                // Select the 4th item
                listView.SetItemChecked(initialSelection, true);*/
            //};



            buttonConnect.Click += delegate {
                listenThread.Start();
                myConnection = new BluetoothConnection();
                myConnection.getAdapter();
                myConnection.thisAdapter.StartDiscovery();
                try{
                    myConnection.getDevice();
                    myConnection.thisDevice.SetPairingConfirmation(false);
                    myConnection.thisDevice.SetPairingConfirmation(true);
                    myConnection.thisDevice.CreateBond();
                }
                catch (Exception deviceEX){ }
                myConnection.thisAdapter.CancelDiscovery();
                _socket = myConnection.thisDevice.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                myConnection.thisSocket = _socket;
                try{
                    myConnection.thisSocket.Connect();
                    connected.Text = "Connected!";
                    buttonDisconnect.Enabled = true;
                    buttonConnect.Enabled = false;
                    if (listenThread.IsAlive == false){
                        listenThread.Start();
                    }
                }
                catch (Exception CloseEX){ }
            };
            buttonDisconnect.Click += delegate {
                try{
                    buttonConnect.Enabled = true;
                    listenThread.Abort();
                    myConnection.thisDevice.Dispose();
                    myConnection.thisSocket.OutputStream.WriteByte(187);
                    myConnection.thisSocket.OutputStream.Close();
                    myConnection.thisSocket.Close();
                    myConnection = new BluetoothConnection();
                    _socket = null;
                    connected.Text = "Disconnected!";
                }
                catch { }
            };
            button1On.Click += delegate {
                try{
                    myConnection.thisSocket.OutputStream.WriteByte(1);
                    myConnection.thisSocket.OutputStream.Close();
                }
                catch (Exception outPutEX){ }
            };
            button2On.Click += delegate {
                try{
                    myConnection.thisSocket.OutputStream.WriteByte(2);
                    myConnection.thisSocket.OutputStream.Close();
                }
                catch (Exception outPutEX) { }
            }; 
        }
        void listener(){
            byte[] read = new byte[1];
            TextView readTextView = FindViewById<TextView>(Resource.Id.textView2);
            TextView timeTextView = FindViewById<TextView>(Resource.Id.textView3);
            while (true){
                try{
                    myConnection.thisSocket.InputStream.Read(read, 0, 1);
                    myConnection.thisSocket.InputStream.Close();
                    RunOnUiThread(() =>{
                        if (read[0] == 1){
                            readTextView.Text = "Relais AN";
                        }
                        else if (read[0] == 0){
                            readTextView.Text = "Relais AUS";
                            timeTextView.Text = "";
                        }
                    });
                }
                catch { }
            }
        }
    }
//original xamarin template------------------------------------------------
    /*public class MainActivity : FormsApplicationActivity
	{
        BluetoothConnection myConnection = new BluetoothConnection();


        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Initialize Azure Mobile Apps
			CurrentPlatform.Init();

			// Initialize Xamarin Forms
			Forms.Init (this, bundle);

			// Load the main application
			LoadApplication (new App ());
        }
    }*/
//------------------------------------------------------------------
    public class BluetoothConnection{
        public void getAdapter() { this.thisAdapter = BluetoothAdapter.DefaultAdapter; }
        public void getDevice() { this.thisDevice = (from bd in this.thisAdapter.BondedDevices where bd.Name == "SUSHEAT" select bd).FirstOrDefault(); }
        public BluetoothAdapter thisAdapter { get; set; }
        public BluetoothDevice thisDevice { get; set; }
        public BluetoothSocket thisSocket { get; set; }
    }
}

