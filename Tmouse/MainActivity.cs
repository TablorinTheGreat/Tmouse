using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using Android.Content;
using Android.Runtime;
using static Android.Views.View;
using Android.Text.Method;
using Android.Text;
using System.Runtime.InteropServices;

namespace Tmouse
{
    [Activity(Label = "Tmouse", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, GestureDetector.IOnGestureListener
    {
      
      public static Socket f;
        TextView p,m;
        double x1, y1,dir,len,dy;
        bool ok = false,drag=false;
        Thread o;
        GestureDetector _gestureDetector;
        int k = 5,fid;
        string serv ,txt="";

      

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            p = FindViewById<TextView>(Resource.Id.velocity_text_view);
            m = FindViewById<TextView>(Resource.Id.text_view);
            f = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            FindViewById<Button>(Resource.Id.button1).Click += delegate { StartActivity(typeof(addons)); };
            _gestureDetector = new GestureDetector(this);
            FindViewById<Button>(Resource.Id.Right).Touch += (sender, args) =>
            {
                if (args.Event.Action == MotionEventActions.Down)
                {
                    Thread.Sleep(50);
                    f.Send(Encoding.UTF8.GetBytes("(rightdown),"));
                }
                if ( args.Event.Action == MotionEventActions.Up)
                {
                    Thread.Sleep(50);
                    f.Send(Encoding.UTF8.GetBytes("(rightup),"));
                }
            };
            FindViewById<Button>(Resource.Id.Left).Touch += (sender, args) =>
            {
                 
                
                if (args.Event.Action == MotionEventActions.Down)
                {
                    Thread.Sleep(50);
                    f.Send(Encoding.UTF8.GetBytes("(leftdown),"));
                }
                if (args.Event.Action == MotionEventActions.Up)
                {
                    Thread.Sleep(50);
                    f.Send(Encoding.UTF8.GetBytes("(leftup),"));
                }
            };
        
        }
         public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
             var r=(SearchView)menu.FindItem(Resource.Id.search).ActionView;
            r.SetIconifiedByDefault(false);
            r.QueryTextChange += c;
            r.QueryTextSubmit += s;
            return true;
        }
     public void c(object s,EventArgs e)
        {
            var a = (SearchView)s;
            if (txt != a.Query)
            {
                if (txt.Length > a.Query.Length)
                {
                    Thread.Sleep(50);
                    f.Send(Encoding.UTF8.GetBytes("_/d,"));
                }
                else
                {
                    if(a.Query[a.Query.Length-1]==' ')
                    {
                        Thread.Sleep(50);
                        f.Send(Encoding.UTF8.GetBytes("_/s,"));
                    }
                    else
                    {
                        Thread.Sleep(50);
                        f.Send(Encoding.UTF8.GetBytes("_" +a.Query[a.Query.Length - 1] + ","));
                    
                }
                }
            }
            txt = a.Query;
        }
     public void s(object s, EventArgs e) {
            Thread.Sleep(50); 
            f.Send(Encoding.UTF8.GetBytes("_/e,"));
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                switch (requestCode)
                {
                    case 1:
                        string g = data.GetStringExtra("serv");
                        k = data.GetIntExtra("sens",5);
                        if (g != "")
                        {
                            serv = g;
                            if (f.Connected)
                            {
                                f.Close();
                                f = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            }
                            IPHostEntry a = Dns.GetHostEntry(serv);
                            IPAddress b = a.AddressList[0];
                            IPEndPoint remoteEP = new IPEndPoint(b, 11000);
                            f.Connect(remoteEP);
                        }
                        break;
                }
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.settings:
                    Intent i = new Intent(this,typeof(settings));
                    i.PutExtra("serv", serv);
                    i.PutExtra("sens", k);
                    StartActivityForResult(i,1);
                    break;
               
            }
            return true;
        }

        public void send(string g)
        {
            Thread.Sleep(50);
            f.Send(Encoding.UTF8.GetBytes(len.ToString()+";"+dir.ToString()+";"+g+","));
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            _gestureDetector.OnTouchEvent(e);
          
            if (e.Action == MotionEventActions.Up && drag == true)
            {
                drag = false;
                f.Send(Encoding.UTF8.GetBytes("(leftup),"));
            }
            else
            {
                if (e.Action == MotionEventActions.Pointer2Down)
                 {
                    dy = e.GetY();
                    ok = false;
                }
                else 
                if (e.PointerCount == 2&&(MotionEventActions.Move==e.Action|| MotionEventActions.Pointer2Up == e.Action))
                {
                   
                    if (e.GetY() > dy)
                    {
                        Thread.Sleep(50);
                        f.Send(Encoding.UTF8.GetBytes("(wheeldown),"));
                    }
                    else
                    {
                        Thread.Sleep(50);
                        f.Send(Encoding.UTF8.GetBytes("(wheelup),"));
                    }

                }
                else
                {
                    if (e.PointerCount == 1)
                    {
                        if (e.Action != MotionEventActions.Down && ok == true)
                        {
                            len = length(x1, e.GetX(), y1, e.GetY());
                            dir = direction(x1, e.GetX(), y1, e.GetY());
                            p.Text = "len:" + len.ToString();
                            m.Text = "dir:" + dir.ToString();
                            //o.Start();
                            string g;
                            if (x1 < e.GetX())
                                g = "+";
                            else
                                g = "-";
                            send(g);
                            ok = false;
                        }
                        else
                        {
                            x1 = e.GetX();
                            y1 = e.GetY();
                            ok = true;
                        }
                    }
                }
            }
            
            return base.OnTouchEvent(e);
        }
        public void checksend() {
          
        }
     public double length(double x1, double x2, double y1, double y2)
        {
            x1 = x1 - x2;
            y1 = y1 - y2;
            x1 = Math.Pow(x1,2);
            y1 = Math.Pow(y1,2);
            x1 = x1 + y1;
            x1 = Math.Sqrt(x1);
            return x1*k;
        }
        public double direction(double x1, double x2, double y1, double y2)
        {
            x1 = x1 - x2;
            y1 = y1 - y2;
            x1 = y1 / x1;
            return x1;
        }

        public bool OnDown(MotionEvent e)
        {
            return true;
        }
      
        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return true;
        }

        public void OnLongPress(MotionEvent e)
        {
            Thread.Sleep(50);
            f.Send(Encoding.UTF8.GetBytes("(leftdown),"));
            p.Text = "drag";
            m.Text = "drag";
            drag = true;
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
           
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            Thread.Sleep(50);
            f.Send(Encoding.UTF8.GetBytes("(click),"));
            p.Text = "click";
            m.Text = "click";
            return true;
        }

      
    }
}

