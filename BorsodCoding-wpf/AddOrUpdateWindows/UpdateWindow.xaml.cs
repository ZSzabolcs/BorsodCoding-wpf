using BorsodCoding_WPF_Admin.Tablak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
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
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private object objectForm;
        private string token;
        private readonly Tabla actualTabla;
        public UpdateWindow(string token, object objectForm, Tabla actualTabla)
        {
            InitializeComponent();
            this.objectForm = objectForm;
            this.token = token;
            this.actualTabla = actualTabla;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var oszlopok = objectForm.GetType().GetProperties();

            foreach (var item in oszlopok)
            {
                Type type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType;
                Label label = new Label();
                label.Content = item.Name;
                object input = new UIElement();

                if (type == typeof(string) || type == typeof(int))
                {
                    var textbox = input as TextBox;
                    textbox.Name = $"tbx{item.Name}";
                    textbox.Text = objectForm.GetType().GetProperty(item.Name).GetValue(objectForm) as string;
                }

                if (type == typeof(DateTime))
                {
                    var datepicker = input as DatePicker;
                    datepicker.Name = $"dp{item.Name}";
                    datepicker.SelectedDate = DateTime.Parse(objectForm.GetType().GetProperty(item.Name).GetValue(objectForm) as string);
                }

                if (type == typeof(bool))
                {
                    var checkbox = input as CheckBox;
                    checkbox.Name = $"cbx{item.Name}";
                    checkbox.IsChecked = (bool)objectForm.GetType().GetProperty(item.Name).GetValue(objectForm);
                }

                stpInputs.Children.Add(label);
                stpInputs.Children.Add(input as UIElement);

            }
            Button button = new Button();
            button.Margin = new Thickness(0, 15, 0, 0);
            button.Content = "Rekord módosítása";
            button.Click += ModifyRecord;
            stpInputs.Children.Add(button);
        }

        private async void ModifyRecord(object sender, RoutedEventArgs e)
        {
            string oszlop = "";
            foreach (var item in stpInputs.Children)
            {
                if (item is Label)
                {
                    Label label = new Label();
                    oszlop = label.Content.ToString();
                }

                if (item is TextBox)
                {
                    var textbox = item as TextBox;
                    object content;
                    int szam;
                    if (int.TryParse(textbox.Text, out szam))
                    {
                        content = szam;
                    }
                    content = textbox.Text;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, content);
                }

                if (item is CheckBox)
                {
                    var checkbox = item as CheckBox;
                    var ertek = checkbox.IsChecked ?? false;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, ertek);
                }

                if (item is DatePicker)
                {
                    var datepicker = item as DatePicker;
                    var ertek = datepicker.SelectedDate;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, ertek);
                }
            }

            var resp = await actualTabla.UpdateAData(objectForm, token);
            string jsonBody = await (resp as HttpResponseMessage).Content.ReadAsStringAsync();
            if ((resp as HttpResponseMessage).IsSuccessStatusCode)
            {
                Database.ShowJsonProperty(jsonBody, "message");
                Close();
            }
            MessageBox.Show(jsonBody);
        }
    }
}
