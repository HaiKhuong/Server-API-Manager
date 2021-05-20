using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace APIServerTest
{
    class Program
    {
        public static IPEndPoint ipep;
        public static Socket server;
        public static byte[] data = new byte[1024];
        public static byte[] dataSend = new byte[1024];
        public static int recv;
        public static List<Socket> clientList;

        static void Main(string[] args)
        {

            Console.WriteLine("TEST API SERVER \n");
            Console.WriteLine("LOGIN \n login:{sdt}:{MD5(password)} \n");
            Console.WriteLine("LOGOUT \n logout:{sdt}:{soTien} \n");
            Console.WriteLine("NAP TIEN \n naptien:{sdt}:{soTien} \n");
            Console.WriteLine("CHAT \n chat:{STTMayTinh}:{mess} \n");
            Console.WriteLine("DICH VU \n dichvu:{sdt}#{tenDichVu}:{soLuong}#{tenDichVu}:{soLuong}#{tongTienTatCaDichVu} \n");


            clientList = new List<Socket>();
            ipep = new IPEndPoint(IPAddress.Any, 9050);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(ipep);

            try
            {
                while (true)
                {
                    server.Listen(100);
                    Socket client = server.Accept();
                    recv = client.Receive(data);
                    string s = Encoding.ASCII.GetString(data, 0, recv);
                    string[] ss = s.Split(":");

                    if (ss[0] == "login")
                    {
                        if (ss[1].Length == 10)
                        {
                            if (ss[2].Length == 32)
                                Console.WriteLine("Login Success");
                            else
                                Console.WriteLine("Password is not valid");
                        }
                        else
                            Console.WriteLine("Phone is not valid");
                    }
                    if (ss[0] == "logout")
                    {
                        if (ss[1].Length == 10)
                        {
                            if (Convert.ToInt32(ss[2]) > 0)
                            {
                                Console.WriteLine("Log out Success");
                            }
                            else
                                Console.WriteLine("So Tien is not valid");
                        }
                        else
                            Console.WriteLine("Phone is not valid");
                    }
                    if (ss[0] == "naptien")
                    {
                        if (ss[1].Length == 10)
                        {
                            if (Convert.ToInt32(ss[2]) > 0)
                            {
                                Console.WriteLine("Nap Tien Success");
                            }
                            else
                                Console.WriteLine("So Tien is not valid");
                        }
                        else
                            Console.WriteLine("Phone is not valid");
                    }
                    if (ss[0] == "chat")
                    {
                        if (ss[1] != "")
                        {
                            if (ss[2] != null)
                            {
                                Console.WriteLine("Hello..... Send mess succes");
                            }
                            else
                                Console.WriteLine("Mess is not valid");
                        }
                        else
                            Console.WriteLine("Name PC is not valid");
                    }
                    if (ss[0] == "dichvu")
                    {
                        string[] sss = s.Split('#');
                        string[] ssss = sss[0].Split(':');
                        if (ssss[1].Length == 10)
                        {
                            if (int.Parse(sss[sss.Length - 1]) > 0)
                            {
                                for (int i = 1; i < sss.Length; i++)
                                {
                                    string[] sssss = sss[i].Split(':');
                                    if (sssss[0] != "")
                                    {
                                        if (int.Parse(sssss[1]) > 0)
                                        {
                                            Console.WriteLine("Confirm service success");
                                        }
                                    }
                                    else
                                        Console.WriteLine("Ten Dich Vu is not valid");
                                }
                            }
                            else
                                Console.WriteLine("So Tien is not valid");
                        }
                        else
                            Console.WriteLine("Phone is not valid");
                    }
                    else
                        Console.WriteLine("API is not valid");
                }
            }
            catch
            {
                ipep = new IPEndPoint(IPAddress.Any, 9050);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }

        }
    }
}
