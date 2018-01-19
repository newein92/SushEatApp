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

using Android.App;
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

namespace SushEat.Droid
{
    [Activity(Label = "Process Order")]
    public class processOrder : RestaurantChef
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.processOrder);
            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            BluetoothConnection myConnection = new BluetoothConnection();
            Button buttonConnect = FindViewById<Android.Widget.Button>(Resource.Id.button1);
            Button buttonDisconnect = FindViewById<Android.Widget.Button>(Resource.Id.button2);
            Button button1On = FindViewById<Android.Widget.Button>(Resource.Id.button3);
            Button button2On = FindViewById<Android.Widget.Button>(Resource.Id.button4);
            Button markOrderProcessed = FindViewById<Android.Widget.Button>(Resource.Id.button5);
            TextView connected = FindViewById<TextView>(Resource.Id.textView1);
            BluetoothSocket _socket = null;
            System.Threading.Thread listenThread = new System.Threading.Thread(listener);
            listenThread.Abort();

            markOrderProcessed.Click += async delegate
            {
                if (currentOrder > -1)
                {
                    Customer deleteEntity = customerList.ElementAt<Customer>(currentOrder);
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await table.ExecuteAsync(deleteOperation);

                    Customer temp = customerList.ElementAt<Customer>(currentOrder);
                    temp.PartitionKey = "PROCESSED";
                    
                    TableOperation insertOperation = TableOperation.Insert(temp);
                    await table.ExecuteAsync(insertOperation);

                    customerList.RemoveAt(currentOrder);
                    var intent = new Intent(this, typeof(RestaurantChef));
                    StartActivity(intent);
                }
            };
            buttonConnect.Click += delegate
            {
                listenThread.Start();
                myConnection = new BluetoothConnection();
                myConnection.getAdapter();
                myConnection.thisAdapter.StartDiscovery();
                try
                {
                    myConnection.getDevice();
                    myConnection.thisDevice.SetPairingConfirmation(false);
                    myConnection.thisDevice.SetPairingConfirmation(true);
                    myConnection.thisDevice.CreateBond();
                }
                catch (Exception deviceEX) { }
                myConnection.thisAdapter.CancelDiscovery();
                _socket = myConnection.thisDevice.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                myConnection.thisSocket = _socket;
                try
                {
                    myConnection.thisSocket.Connect();
                    connected.Text = "Connected!";
                    buttonDisconnect.Enabled = true;
                    buttonConnect.Enabled = false;
                    if (listenThread.IsAlive == false)
                    {
                        listenThread.Start();
                    }
                }
                catch (Exception CloseEX) { }
            };
            buttonDisconnect.Click += delegate {
                try
                {
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
                try
                {
                    myConnection.thisSocket.OutputStream.WriteByte(1);
                    myConnection.thisSocket.OutputStream.Close();
                }
                catch (Exception outPutEX) { }
            };
            button2On.Click += delegate {
                try
                {
                    myConnection.thisSocket.OutputStream.WriteByte(2);
                    myConnection.thisSocket.OutputStream.Close();
                }
                catch (Exception outPutEX) { }
            };
            void listener()
            {
                byte[] read = new byte[1];
                TextView readTextView = FindViewById<TextView>(Resource.Id.textView2);
                TextView timeTextView = FindViewById<TextView>(Resource.Id.textView3);
                while (true)
                {
                    try
                    {
                        myConnection.thisSocket.InputStream.Read(read, 0, 1);
                        myConnection.thisSocket.InputStream.Close();
                        RunOnUiThread(() => {
                            if (read[0] == 1)
                            {
                                readTextView.Text = "Relais AN";
                            }
                            else if (read[0] == 0)
                            {
                                readTextView.Text = "Relais AUS";
                                timeTextView.Text = "";
                            }
                        });
                    }
                    catch { }
                }
            }
        }
    }
}