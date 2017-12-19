using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO.Ports;
using System.Net.Http;
using System.Threading;

namespace ArduinoListener
{
    class Program
    {
        private const string APP_PATH = "https://0e3dc657.ngrok.io/";
        private static int ID_QUEUE = 1;

        private static string AutodetectArduinoPort()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();

                    if (desc.Contains("Arduino"))
                    {
                        return deviceId;
                    }
                }
            }
            catch (ManagementException e)
            {
                /* Do Nothing */
            }

            return "COM4"; ;
        }

        private static string result = "";

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            result = sp.ReadLine();
            Console.WriteLine(result);


            string temp;
            string resultParse = "";

            int space1 = result.IndexOf(" ");
            temp = Convert.ToString(Convert.ToInt32(result.Substring(0, space1)), 16);
            if (temp.Length == 1)
            {
                temp = "0" + temp;
            }
            resultParse += temp;

            int space2 = result.IndexOf(" ", space1 + 1);
            temp = Convert.ToString(Convert.ToInt32(result.Substring(space1 + 1, space2 - space1)), 16);
            if (temp.Length == 1)
            {
                temp = "0" + temp;
            }
            resultParse += temp;

            int space3 = result.IndexOf(" ", space2 + 1);
            temp = Convert.ToString(Convert.ToInt32(result.Substring(space2 + 1, space3 - space2)), 16);
            if (temp.Length == 1)
            {
                temp = "0" + temp;
            }
            resultParse += temp;

            int space4 = result.IndexOf(" ", space3 + 1);
            temp = Convert.ToString(Convert.ToInt32(result.Substring(space3 + 1, space4 - space3)), 16);
            if (temp.Length == 1)
            {
                temp = "0" + temp;
            }
            resultParse += temp;
            resultParse = resultParse.ToUpper();
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(APP_PATH + "api/Results/Success?idQueue=" + ID_QUEUE + "&uidCard=" + resultParse, null).Result;
                Console.WriteLine(response.ToString());
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter id queue");
            ID_QUEUE = Int32.Parse(Console.ReadLine());

            string port = AutodetectArduinoPort();

            Console.WriteLine(port);

            SerialPort mySerialPort = new SerialPort(port);

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            Console.WriteLine("Use your card");
            Console.ReadKey();

            mySerialPort.Close();
        }
    }
}
