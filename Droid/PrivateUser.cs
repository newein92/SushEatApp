using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Bluetooth;

namespace SushEat.Droid
{
    [Activity(Label = "Make your own Sushi")]
    public class PrivateUser : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.operateMachine);
            BluetoothConnection myConnection = new BluetoothConnection();
            Button buttonConnect = FindViewById<Android.Widget.Button>(Resource.Id.button1);
            Button buttonDisconnect = FindViewById<Android.Widget.Button>(Resource.Id.button2);
            Button button1On = FindViewById<Android.Widget.Button>(Resource.Id.button3);
            Button button2On = FindViewById<Android.Widget.Button>(Resource.Id.button4);
            TextView connected = FindViewById<TextView>(Resource.Id.textView1);
            BluetoothSocket _socket = null;
            System.Threading.Thread listenThread = new System.Threading.Thread(listener);
            listenThread.Abort();
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