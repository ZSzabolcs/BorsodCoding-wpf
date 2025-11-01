using System.Runtime.InteropServices;
using System.Security;
using System.Text;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (UserName.Text == "root" && Address.Text == "localhost" && GetPassword(Passw) == "" && DatabaseName.Text == "for_the_potatoe")
            {
                Database databaseWindow = new Database();
                ConnectToDatabase d = new ConnectToDatabase(Address.Text, UserName.Text, "", DatabaseName.Text);
                databaseWindow.Show();
                this.Close();

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