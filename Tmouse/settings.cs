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
    [Activity(Label = "settings")]
    public class settings : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settings);
            var serv = FindViewById<EditText>(Resource.Id.editText1);
            var s= FindViewById<SeekBar>(Resource.Id.seekBar1);
            s.Progress = Intent.GetIntExtra("sens",5)-1;
            string r = Intent.GetStringExtra("serv");
            serv.Text = r;
            FindViewById<Button>(Resource.Id.submit).Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                if(serv.Text!=r)
                i.PutExtra("serv", serv.Text);
                else i.PutExtra("serv", "");
                i.PutExtra("sens", s.Progress + 1);
                SetResult(Result.Ok, i);
                Finish();
            };
        }
    }
}