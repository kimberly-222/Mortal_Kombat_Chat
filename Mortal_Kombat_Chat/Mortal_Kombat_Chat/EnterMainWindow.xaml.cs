using BusinessServer;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for EnterMainWindow.xaml
    /// </summary>
    public delegate void UpdatedRoomListDelegate();
    public partial class EnterMainWindow : Window
    {
        string username = ""; //loggin in user username
        MainWindow loginMenu;
        private BServerInterface foob;
        private Thread serverListenerThread;
        private UpdatedRoomListDelegate roomListDelegate;
        public EnterMainWindow(BServerInterface inFoob, string inUsername, MainWindow inLoginMenu)
        {
            InitializeComponent();
            this.foob = inFoob;
            this.username = inUsername;
            this.loginMenu = inLoginMenu;
            UsernameLabel.Content = username;
            Initials.Content = GetInitials(username);

            List<string> allSevers = foob.GetAllRoom();
            foreach (string server in allSevers)
            {
                Room_List.Items.Add(server);
            }
            refresh();

            roomListDelegate += updateRoomList;

            serverListenerThread = new Thread(new ThreadStart(ListenToServer));
            serverListenerThread.Start();
        }

        private string GetInitials(string username)
        {
            return username.Substring(0, 2).ToUpper();
        }

        private void ListenToServer()
        {
            while(true)
            {
                Thread.Sleep(5000);
                roomListDelegate.Invoke();
            }
        }

        private void updateRoomList()
        {
            Dispatcher.Invoke(() =>
            {
                refresh();
            });
        }

        private void refresh()
        {
            Room_List.Items.Clear();
            List<string> allSevers = foob.GetAllRoom();
            if(allSevers.Count > 0)
            {
                foreach (string server in allSevers)
                {
                    Room_List.Items.Add(server);
                }
            }
        }

        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {
            if(Room_List.SelectedItem != null)
            {
                string selectServer = Room_List.SelectedItem.ToString();
                Room_List.SelectedItem = null;
                if(foob.addJoinedServer(username, selectServer))
                {
                    Lobby lobby = new Lobby(foob, selectServer, username, this);
                    lobby.Show();
                    this.Hide();
                    // MessageBox.Show(username + " joined " + selectServer);
                }
            }
            else
            {
                MessageBox.Show("Please select a server to join.");
            }
        }

        private void CreateLobbyButton_Click_1(object sender, RoutedEventArgs e)
        {
            string roomName = Create_Lobby_TextBox.Text.ToString();

            if (!roomName.Equals("") && !foob.isRoomExist(roomName))
            {
                foob.CreateRoom(username, roomName);
                Lobby lobby = new Lobby(foob, roomName, username, this);
                lobby.Show();
                this.Hide();
                refresh();
            }
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            foob.logout(username);
            loginMenu.Show();
            this.Close();
        }
    }
}