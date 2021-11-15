using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace reservation_managment_lab_project_server
{
    class NetWorking
    {
        public enum Requestes { GET_RESERVATION, ADD_DISH, DELETE_DISH, GET_ALL_WORKERS };
        const int SIZE_PARAMETERS = 2;
        public static string getStringOverNetStream(NetworkStream stream)
        {
            byte[] size_buffer = new byte[4];//size of integer
            byte[] string_buffer;
            int stringSize;
            stream.Read(size_buffer, 0, size_buffer.Length);
            stringSize = BitConverter.ToInt32(size_buffer, 0);
            string_buffer = new byte[stringSize];
            stream.Read(string_buffer, 0, string_buffer.Length);
            return Encoding.UTF8.GetString(string_buffer);
        }
        public static void sentStringOverNetStream(NetworkStream stream, string str)
        {
            byte[] buffer = BitConverter.GetBytes(str.Length);//always the size of the array will be 4
            stream.Write(buffer, 0, buffer.Length);
            buffer = Encoding.UTF8.GetBytes(str);
            stream.Write(buffer, 0, buffer.Length);
        }
        public static void SendRequest(NetworkStream stream,Requestes request,int table_number) 
        {
            byte[] request_buffer = new byte[SIZE_PARAMETERS];
            request_buffer[0] = Convert.ToByte(request);
            request_buffer[1] = Convert.ToByte(table_number);
            stream.Write(request_buffer, 0, request_buffer.Length);
        }
        public static byte[] GetRequest(NetworkStream stream)
        {
            int bytesRead = 0;
            byte[] request_buffer = new byte[SIZE_PARAMETERS];
            while (bytesRead < SIZE_PARAMETERS)
            {
                bytesRead += stream.Read(request_buffer, bytesRead, SIZE_PARAMETERS - bytesRead);
            }
            return request_buffer;
        }
    }
}
