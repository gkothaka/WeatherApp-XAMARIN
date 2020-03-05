﻿using System;
using System.Globalization;
using Xamarin.Forms;
namespace TodoXamarinForms
{
    class ChangeCompleteActionTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isCompleted = (bool)value;
            return isCompleted ? "Interested" : "Partly Intrested";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not used since we only want to convert a boolean to text, and not the other way around
            throw new NotImplementedException();
        }
    }
}