using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;


public class TCPConsoleClient
{

    private static void printHelp()
    {
        Console.WriteLine("exit     :   cikis yapar");
        Console.WriteLine("help     :   yardimi goruntuler");
        Console.WriteLine("clear    :   ekrani temizler");
    }


    public static void Main()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        try
        {
            TcpClient tcpclnt = new TcpClient();
            Console.Write("TCP Test Tool by Ozgun ESIM\nServer IP: ");
            string ip = Console.ReadLine();
            Console.Write("Port Number: ");
            int port = Convert.ToInt32(Console.ReadLine());
            tcpclnt.Connect(ip, port); // use the ipaddress as in the server program

            Console.WriteLine("Successfully Connected to " + ip + ":" + port + "\n\n");
            Console.Write("Enter the string to be transmitted (type help for help) : ");
            bool isWorking = true;
            while (true)
            {
                String str = Console.ReadLine();
                if (str == "exit")
                {
                    isWorking = false;
                    break;
                }
                else if (str == "help")
                {
                    printHelp();
                    continue;
                }
                else if (str == "clear")
                {
                    Console.Clear();
                    continue;
                }
                else if (str.Trim() == "")
                {
                    str = "[empty]";
                }

                Stream stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting...");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);

                Console.Write("Response: ");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
                Console.WriteLine("\n_________________________");
            }
            

            tcpclnt.Close();
        }

        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
            Console.ReadLine();
        }
        //Console.ReadLine();
    }

    

}