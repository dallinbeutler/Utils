using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Extensions.WPF
{
    public static class ExtWPF
    {
        //gets the child of a visual component in wpf
        public static T GetVisualChild<T>(this Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
    /// <summary>
    /// This handy class alows you to output any text for a databound bool
    /// 
    /// usage:
    /// {local:SwitchBinding yourBool, yes!, no...}
    /// </summary>
    public class SwitchBindingExtension : Binding
    {
        public SwitchBindingExtension()
        {
            Initialize();
        }

        public SwitchBindingExtension(string path)
            : base(path)
        {
            Initialize();
        }

        public SwitchBindingExtension(string path, object valueIfTrue, object valueIfFalse)
            : base(path)
        {
            Initialize();
            this.ValueIfTrue = valueIfTrue;
            this.ValueIfFalse = valueIfFalse;
        }

        private void Initialize()
        {
            this.ValueIfTrue = Binding.DoNothing;
            this.ValueIfFalse = Binding.DoNothing;
            this.Converter = new SwitchConverter(this);
        }

        [ConstructorArgument("valueIfTrue")]
        public object ValueIfTrue { get; set; }

        [ConstructorArgument("valueIfFalse")]
        public object ValueIfFalse { get; set; }

        private class SwitchConverter : IValueConverter
        {
            public SwitchConverter(SwitchBindingExtension switchExtension)
            {
                _switch = switchExtension;
            }

            private SwitchBindingExtension _switch;

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    bool b = System.Convert.ToBoolean(value);
                    return b ? _switch.ValueIfTrue : _switch.ValueIfFalse;
                }
                catch
                {
                    return System.Windows.DependencyProperty.UnsetValue;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return Binding.DoNothing;
            }

            #endregion
        }

    }
}
