using BusinessServer;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private string username = "";
        private BServerInterface foob;
        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<BServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8200/BusinessService";
            foobFactory = new ChannelFactory<BServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // reset visibility
            Status_Label.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(username) || username.Contains(" "))
            {
                Status_Label.Content = "Invalid username";
                Status_Label.Foreground = Brushes.Red;
                Status_Label.Visibility = Visibility.Visible;
                return;
            }

            if (!foob.IsUserNameExist(username))
            {
                Status_Label.Content = "Login Success as " + "'" + username + "'";
                Status_Label.Foreground = Brushes.LimeGreen;
                Status_Label.Visibility = Visibility.Visible;

                foob.addUserAccountInfo(username);
                foob.login(username);

                OpenNextWindow();
            }
            else
            {
                foob.getUserAccountInfo(username);
                if (foob.IfLoggedIn(username))
                {
                    Status_Label.Content = "Username Already Logged In";
                    Status_Label.Foreground = Brushes.Red;
                    Status_Label.Visibility = Visibility.Visible;
                }
                else
                {
                    Status_Label.Content = "Login Success as " + "'" + username + "'";
                    Status_Label.Foreground = Brushes.LimeGreen;
                    Status_Label.Visibility = Visibility.Visible;

                    foob.login(username);
                    OpenNextWindow();
                }
            }
        }

        private async void OpenNextWindow()
        {
            await Task.Delay(800); // let user see message

            EnterMainWindow enterMainWindow = new EnterMainWindow(foob, username, this);
            enterMainWindow.Show();
            this.Hide();
        }

        private void Add_Client_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            username = UsernameBox.Text;
        }
    }
}
