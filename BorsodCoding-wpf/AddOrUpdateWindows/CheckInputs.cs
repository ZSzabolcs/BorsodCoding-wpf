using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BorsodCoding_WPF_Admin.AddOrUpdateWindows
{
    public record ErrorMessage(bool vanEHiba, string hibaOszlopNev);

    public class CheckInputs
    {
        public static ErrorMessage IsThereError(StackPanel stpInputs, ref object objectForm)
        {
            string oszlopNev = "";
            string hibaOszlopNev = "";
            bool vanHiba = false;
            foreach (var item in stpInputs.Children)
            {

                if (item is Label)
                {
                    Label label = item as Label;
                    oszlopNev = label.Content.ToString();
                }

                if (item is TextBox)
                {
                    var textbox = item as TextBox;
                    object content = null;
                    int szam;
                    Type type = Nullable.GetUnderlyingType(objectForm.GetType().GetProperty(oszlopNev).PropertyType) ?? objectForm.GetType().GetProperty(oszlopNev).PropertyType;
                    if (type == typeof(string))
                    {
                        if (textbox.Text != "")
                        {
                            content = textbox.Text;
                        }

                    }
                    else if (type == typeof(int) && int.TryParse(textbox.Text, out szam))
                    {

                        content = szam;

                    }
                    else
                    {
                        vanHiba = true;
                        hibaOszlopNev = oszlopNev;
                    }

                    objectForm.GetType().GetProperty(oszlopNev).SetValue(objectForm, content);
                }

                if (item is CheckBox)
                {
                    var checkbox = item as CheckBox;
                    var ertek = checkbox.IsChecked ?? false;
                    objectForm.GetType().GetProperty(oszlopNev).SetValue(objectForm, ertek);
                }

                if (item is DatePicker)
                {
                    var datepicker = item as DatePicker;
                    var ertek = datepicker.SelectedDate;
                    objectForm.GetType().GetProperty(oszlopNev).SetValue(objectForm, ertek);
                }
            }

            return new ErrorMessage(vanHiba, hibaOszlopNev);
        }
    }
}
