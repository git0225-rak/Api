using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Helper
{
    public class PrintHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <param name="ZPL"></param>
        /// <returns></returns>
        public static string PrintUsingIP(string ipAddress, int port,string ZPL)
        {
            try
            {
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer =
                new System.IO.StreamWriter(client.GetStream());
                //getZPLString();
                writer.Write(ZPL);
                writer.Flush();
                //getZPLString();
                // Close Connection
                writer.Close();
                client.Close();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



    }
}
