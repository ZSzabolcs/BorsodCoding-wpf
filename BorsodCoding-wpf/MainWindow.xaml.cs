using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {



                if (Address.Text == "localhost" && DatabaseName.Text == "for_the_potato")
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7159/auth/login");
                    var content = new StringContent("{\r\n  \"userName\" : \""+UserName.Text+"\",\r\n  \"password\" : \""+GetPassword(Passw)+"\"\r\n}", null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    string jsonBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show(jsonBody.ToString(), "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string token = "";

                    using (JsonDocument jsonDocument = JsonDocument.Parse(jsonBody))
                    {
                        if (jsonDocument.RootElement.TryGetProperty("message", out var messageElement))
                        {
                            MessageBox.Show(messageElement.ToString(), "Infó", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (jsonDocument.RootElement.TryGetProperty("token", out var tokenElement))
                        {
                            token = tokenElement.ToString();
                            Database databaseWindow = new Database(token);
                            databaseWindow.Show();
                            Close();

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


        
        public static string GetPassword(PasswordBox passwordBox)
        {
            // A jelszó a SecureString típusban érkezik
            SecureString securePassword = passwordBox.SecurePassword;

            // Kezdetben null értékű string
            string plainPassword = null;

            // Memóriacím (pointer) a jelszóhoz
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                // 1. A SecureString tartalmát nem menedzselt memóriába (IntPtr) másoljuk
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);

                // 2. A nem menedzselt memóriából stringet hozunk létre
                plainPassword = Marshal.PtrToStringUni(unmanagedString);

                // Ekkor a plainPassword tartalmazza a jelszót
                return plainPassword;
            }
            finally
            {
                // 3. A memóriát felszabadítjuk, HIBÁTLANUL TÖRÖLNI KELL A MEMÓRIÁBÓL
                if (unmanagedString != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                }
            }
        }
    }
}