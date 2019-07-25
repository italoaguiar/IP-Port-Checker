using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chkipport
{
    class Program
    {
        static void Main(string[] args)
        {

            var f = File.ReadAllLines("ips.txt");

            foreach (var k in f)
            {
                Thread t = new Thread(runThread);
                t.Start(k);
            }

        }
        public static void runThread(object parameter)
        {
            var ip = IPNetwork.Parse(parameter.ToString());
            var ips = ip.ListIPAddress();

            foreach (var i in ips)
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine(i);
                        tcpClient.Connect(i, 8080);
                        System.Diagnostics.Process.Start($"http://{i}:8080");
                        Console.WriteLine(i);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
