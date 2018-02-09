using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Bluetooth;
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using Android.Content;

namespace SushEat.Droid
{
    [Activity(Label = "Process Order")]
    public class processOrder : RestaurantChef
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.processOrder);
            

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

            // Retrieve the storage account from the connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susheattable;AccountKey=JnYEen+TGNIkA722hAMJTMfo+qNT3flVGDVScX158B3GckOPN+dtUOfWU2not3cjRPuqI4fQyhFq8wx/GY0I2g==;EndpointSuffix=core.windows.net");
            // Create the table client.
            //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            //CloudTable table = tableClient.GetTableReference("Customers");

            // Create the table if it doesn't exist.
            //await table.CreateIfNotExistsAsync();

            markOrderProcessed.Click += async delegate
            {
                if (currentOrder > -1)
                {
                    Customer deleteEntity = customerList.ElementAt<Customer>(currentOrder);
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    try
                    {
                        await table.ExecuteAsync(deleteOperation);
                    }
                    catch (Exception e)
                    {
                        //Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                        Console.WriteLine(e.Message);
                    }


                    Customer insertEntity = customerList.ElementAt<Customer>(currentOrder);
                    insertEntity.PartitionKey = "PROCESSED";
                    
                    TableOperation insertOperation = TableOperation.Insert(insertEntity);
                    try
                    {
                        await table.ExecuteAsync(insertOperation);
                    }
                    catch (Exception e)
                    {
                        //Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                        Console.WriteLine(e.Message);
                    }


                    //customerList.RemoveAt(currentOrder);

                    Toast.MakeText(this, "The order has been processed", ToastLength.Long).Show();

                    var intent = new Intent(this, typeof(RestaurantChef));
                    this.Finish();
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
                catch (Exception e) { Toast.MakeText(this, e.Message, ToastLength.Long).Show(); }
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
                catch (Exception e) { Toast.MakeText(this, e.Message, ToastLength.Long).Show(); }
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
                catch (Exception e) { Toast.MakeText(this, e.Message, ToastLength.Long).Show(); }
            };
            button2On.Click += delegate {
                try
                {
                    myConnection.thisSocket.OutputStream.WriteByte(2);
                    myConnection.thisSocket.OutputStream.Close();
                }
                catch (Exception e) { Toast.MakeText(this, e.Message, ToastLength.Long).Show(); }
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