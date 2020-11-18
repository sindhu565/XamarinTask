using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DemoAPP.CustomRenderers
{
   public class CustomEntry:Entry
    {
        public static BindableProperty IsNumericProperty = BindableProperty.Create(propertyName: "IsNumeric", returnType: typeof(bool), declaringType: typeof(CustomEntry), defaultValue: false);
        //public bool IsNumeric { get; set; }
        public bool IsNumeric
        {
            get { return (bool)GetValue(IsNumericProperty); }
            set { SetValue(IsNumericProperty, value); }
        }

        public static readonly BindableProperty IsMailValidproperty = BindableProperty.Create("IsVisibleEmail", typeof(bool), typeof(CustomEntry), true);
        public bool IsVisibleEmail
        {
            get { return (bool)GetValue(IsMailValidproperty); }
            set { SetValue(IsMailValidproperty, value); }
        }
    }
}
