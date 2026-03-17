using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BorsodCoding_WPF_Admin.AddOrUpdateWindows
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly string token;
        private object objectForm;
        public AddWindow()
        {
            InitializeComponent();
        }

        public AddWindow(string token, object objectFormat)
        {
            InitializeComponent();
            this.token = token;
            objectForm = objectFormat;
        }

        private void UjRekord(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < objectForm.GetType().GetProperties().Length; i++)
            {
                string oszlop = objectForm.GetType().GetProperties()[i].Name;
                int j = (i == 0) ? i + 1 : i + 2;
                if (stpInputs.Children[j] is TextBox)
                {
                    var textbox = stpInputs.Children[j] as TextBox;
                    object content;
                    int szam;
                    if(int.TryParse(textbox.Text, out szam))
                    {
                        content = szam;
                    }
                    content = textbox.Text;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, content);
                }

                if (stpInputs.Children[j] is CheckBox)
                {
                    var checkbox = stpInputs.Children[j] as CheckBox;
                    var ertek = checkbox.IsChecked ?? false;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, ertek);
                }

                if (stpInputs.Children[j] is DatePicker)
                {
                    var datepicker = stpInputs.Children[j] as DatePicker;
                    var ertek = datepicker.SelectedDate;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, ertek);
                }
            }

            MessageBox.Show(objectForm.ToString());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var oszlopok = objectForm.GetType().GetProperties();
            
            foreach (var item in oszlopok)
            {
                Type type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType;
                Label label = new Label();
                label.Content = item.Name;
                UIElement input = new UIElement();

                if (item.Name == "Id")
                {
                    continue;
                }

                if (type == typeof(string) || type == typeof(int))
                {
                    input = new TextBox();
                    (input as TextBox).Name = $"tbx{item.Name}";
                }

                if (type == typeof(DateTime))
                {
                    input = new DatePicker();
                    (input as DatePicker).Name = $"dp{item.Name}";
                    (input as DatePicker).Text = DateTime.Now.ToString();
                }

                if (type == typeof(bool))
                {
                    input = new CheckBox();
                    (input as CheckBox).Name = $"cbx{item.Name}";
                }

                stpInputs.Children.Add(label);
                stpInputs.Children.Add(input);

            }
            Button button = new Button();
            button.Margin = new Thickness(0, 15, 0, 0);
            button.Content = "Új rekord hozzáadása";
            button.Click += UjRekord;
            stpInputs.Children.Add(button);
        }
    }
}
