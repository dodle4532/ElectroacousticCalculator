using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Calculator.Class
{
    public static class VisualTreeHelperExtension
    {
        public static T GetVisualParent<T>(this DependencyObject child) where T : DependencyObject
        {
            while (child != null && !(child is T))
                child = System.Windows.Media.VisualTreeHelper.GetParent(child);
            return child as T;
        }
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null && !(child is T))
                child = VisualTreeHelper.GetParent(child);
            return child as T;
        }
    }
}
