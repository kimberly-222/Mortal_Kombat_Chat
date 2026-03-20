using System;
using System.ServiceModel;

namespace BusinessServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Business Server Online");
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(BServer));

            host.AddServiceEndpoint(typeof(BServerInterface), tcp, "net.tcp://localhost:8200/BusinessService");
            host.Open();
            Console.WriteLine("System Online");
            Console.ReadLine();
            host.Close();
        }
    }
}
