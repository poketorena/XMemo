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

namespace XMemo.Droid
{
    class MyDatePicker : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        public DateTime InitialDate { get; set; }

        public event EventHandler<MyDatePickerEventArgs> Selected;

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(Activity, this,
                InitialDate.Year, InitialDate.Month - 1, InitialDate.Day);
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            var selectedDate = new DateTime(year, month + 1, dayOfMonth);
            Selected?.Invoke(this, new MyDatePickerEventArgs(selectedDate));
        }
    }
}