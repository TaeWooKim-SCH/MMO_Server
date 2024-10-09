﻿using ServerCore;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyClient {
    class GameSession: Session {
        public override void OnConnected(EndPoint endPoint) {
            Console.WriteLine($"OnConnected: {endPoint}");

            // 보낸다
            for (int i = 0;i < 5;i++) {
                byte[] sendBuff = Encoding.UTF8.GetBytes($"Hello World {i}");
                Send(sendBuff);
            }
        }

        public override void OnDisConnected(EndPoint endPoint) {
            Console.WriteLine($"OnDisconnected: {endPoint}");
        }

        public override int OnRecv(ArraySegment<byte> buffer) {
            string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"[From Server] {recvData}");
            return buffer.Count;
        }

        public override void OnSend(int numOfBytes) {
            Console.WriteLine($"Transferd bytes: {numOfBytes}");
        }
    }

    internal class Program {
        static void Main(string[] args) {
            // DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Connector connector = new Connector();

            connector.Connect(endPoint, () => { return new GameSession(); });

            while (true) {
                try {
                   
                } catch (Exception e) {
                    Console.Write(e.ToString());
                }

                Thread.Sleep(100);
            }
        }
    }
}
