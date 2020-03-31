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

namespace Tmouse
{
    [Activity(Label = "addons")]
    public class addons : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            FindViewById<Button>(Resource.Id.button1).Click += delegate {
                MainActivity.f.Send(Encoding.UTF8.GetBytes("Vup,"));
            };
            FindViewById<Button>(Resource.Id.button2).Click += delegate {
                MainActivity.f.Send(Encoding.UTF8.GetBytes("Vdown,"));
            };
            FindViewById<Button>(Resource.Id.button3).Click += delegate {
                MainActivity.f.Send(Encoding.UTF8.GetBytes("mute,"));
            };
            FindViewById<Button>(Resource.Id.button4).Click += delegate {
                MainActivity.f.Send(Encoding.UTF8.GetBytes("his,"));
            };
            FindViewById<Button>(Resource.Id.button5).Click += delegate {
                MainActivity.f.Send(Encoding.UTF8.GetBytes("clo,"));
            };
        }
    }
}