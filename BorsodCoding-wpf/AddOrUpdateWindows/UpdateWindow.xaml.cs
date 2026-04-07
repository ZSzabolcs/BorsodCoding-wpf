using BorsodCoding_WPF_Admin.Tablak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata;
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
                UIElement input = new UIElement();

                if (item.Name == "Id" || item.Name == "UserName")
                {
                    continue;
                }

                if (type == typeof(string) || type == typeof(int))
                {
                    input = new TextBox();
                    (input as TextBox).Name = $"tbx{item.Name}";
                    var ertek = objectForm.GetType().GetProperty(item.Name).GetValue(objectForm) ?? "";
                    (input as TextBox).Text = ertek.ToString();


                }

                if (type == typeof(DateTime))
                {
                    input = new DatePicker();
                    (input as DatePicker).Name = $"dp{item.Name}";
                    (input as DatePicker).SelectedDate = DateTime.Parse(objectForm.GetType().GetProperty(item.Name).GetValue(objectForm).ToString());
                }

                if (type == typeof(bool))
                {
                    input = new CheckBox();
                    (input as CheckBox).Name = $"cbx{item.Name}";
                    (input as CheckBox).IsChecked = (bool)objectForm.GetType().GetProperty(item.Name).GetValue(objectForm);
                }

                stpInputs.Children.Add(label);
                stpInputs.Children.Add(input);

            }
            Button button = new Button();
            button.Margin = new Thickness(0, 15, 0, 0);
            button.Content = "Rekord módosítása";
            button.Click += ModifyRecord;
            stpInputs.Children.Add(button);
        }

        private async void ModifyRecord(object sender, RoutedEventArgs e)
        {
            var state = CheckInputs.IsThereError(stpInputs, ref objectForm);
            if (state.vanEHiba)
            {
                MessageBox.Show($"Hiba a {state.hibaOszlopNev} bemenetnél! Rosszul adott meg egy adatot!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var resp = await actualTabla.UpdateAData(objectForm, token);
                if (resp is HttpResponseMessage)
                {
                    if ((resp as HttpResponseMessage).IsSuccessStatusCode)
                    {
                        Close();
                    }
                }
            }
        }
    }
}
