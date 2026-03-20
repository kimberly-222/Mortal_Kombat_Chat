using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DLL
{
    public class Player
    {
        private int ID;
        private string name;
        private bool loggedIn = false;
        private int isLogin;
        private List<string> room = new List<string> { };

        [DataMember]
        public string userName
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int playerID
        {
            get { return ID; }
            set { ID = value; }
        }

        [DataMember]
        public bool LoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; }
        }

        public List<string> Room
        {
            get { return room; }
            set { room = value; }
        }
    }
}