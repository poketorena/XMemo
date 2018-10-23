using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XMemo.Droid
{
    [Activity(Label = "XMemo.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            MemoHolder.Current.Memo = new Memo()
            {
                Data = DateTime.Now,
                Subject = "",
                Text = "",
            };

            DisplayMemo();
            FindViewById<EditText>(Resource.Id.DateText).Click += this.MainActivity_Click;

            var saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            saveButton.Click += async (s, e) =>
             {
                 try
                 {
                     await MemoHolder.Current.SaveAsync();
                 }
                 catch (Exception err)
                 {
                     throw err;
                 }
             };

            var loadButton = FindViewById<Button>(Resource.Id.LoadButton);
            loadButton.Click += async (s, e) =>
            {
                await MemoHolder.Current.LoadAsync();
                DisplayMemo();
            };
        }

        private void MainActivity_Click(object sender, EventArgs e)
        {
            var datePicker = new MyDatePicker();
            datePicker.InitialDate = MemoHolder.Current.Memo.Data;
            datePicker.Selected += (s2, e2) =>
              {
                  MemoHolder.Current.Memo.Data = e2.SelectedDate;
                  DisplayMemo();
              };
            datePicker.Show(FragmentManager, "tag");
        }

        private void DisplayMemo()
        {
            var memo = MemoHolder.Current.Memo;
            FindViewById<EditText>(Resource.Id.DateText).Text = string.
                Format("{0:yyyy/MM/dd}", memo.Data);
            FindViewById<EditText>(Resource.Id.SubjectText).Text = memo.Subject;
            FindViewById<TextView>(Resource.Id.MemoText).Text = memo.Text;

            var subjectText = FindViewById<EditText>(Resource.Id.SubjectText);
            subjectText.TextChanged += (s, e) => MemoHolder.Current.Memo.Subject = subjectText.Text;

            var memoText = FindViewById<TextView>(Resource.Id.MemoText);
            memoText.TextChanged += (s, e) => MemoHolder.Current.Memo.Text = memoText.Text;
        }
    }
}


