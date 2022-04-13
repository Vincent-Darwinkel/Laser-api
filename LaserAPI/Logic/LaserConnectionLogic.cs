﻿using LaserAPI.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LaserAPI.Logic
{
    public static class LaserConnectionLogic
    {
        public static bool RanByUnitTest { get; set; } = false;
        public static string IpAddress { get; set; }
        public static LaserMessage PreviousMessage { get; set; } = new();
        public static bool Connected { get; private set; }
        private static TcpListener _server;
        private static NetworkStream _stream;
        private static TcpClient _client;

        public static void Connect()
        {
            try
            {
                IPAddress localAddress = IPAddress.Parse(IpAddress);
                _server = new TcpListener(localAddress, 50000)
                {
                    Server =
                    {
                        SendTimeout = -1
                    }
                };
                _server.Start();

                Console.WriteLine("Waiting");
                _client = _server.AcceptTcpClient();
                Console.WriteLine("Connected");
                _stream = _client.GetStream();
            }
            catch (Exception)
            {
                _client.Close();
            }
        }

        public static async Task SendMessages(List<LaserMessage> messages)
        {
            if (RanByUnitTest)
            {
                return;
            }

            Connected = _client != null && _client.Connected;
            if (!Connected)
            {
                Connect();
            }

            try
            {
                if (!messages.Any())
                {
                    return;
                }

                string json = "[";
                int messageLength = messages.Count;
                for (int i = 0; i < messageLength; i++)
                {
                    LaserMessage message = messages[i];
                    string jsonMessage = "{\"r\":" + message.RedLaser + ",\"g\":" + message.GreenLaser + ",\"b\":" + message.BlueLaser + ",\"x\":" + message.X + ",\"y\":" + message.Y + "}";
                    if (i + 1 != messageLength)
                    {
                        jsonMessage += ",";
                    }

                    json += jsonMessage;
                }

                json += "]";
                UTF8Encoding utf8 = new();

                byte[] msg = utf8.GetBytes(json);
                await _stream.WriteAsync(msg);

                byte[] bytes = new byte[msg.Length];
                await _stream.ReadAsync(bytes);
                PreviousMessage = messages.Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _client.Close();
                _server.Stop();
                Connect();
            }
        }
    }
}
