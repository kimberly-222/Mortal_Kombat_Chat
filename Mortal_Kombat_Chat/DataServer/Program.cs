using System;
using System.ServiceModel;

namespace DataServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the data server");
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(DataServer));

            host.AddServiceEndpoint(typeof(DServerInterface), tcp, "net.tcp://localhost:8100/DataService");
            host.Open();
            Console.WriteLine("The system is now online");
            Console.ReadLine();
            host.Close();
        }
    }
}