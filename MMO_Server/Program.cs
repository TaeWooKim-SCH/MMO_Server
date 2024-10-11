using System.Net;
using System.Net.Sockets;
using System.Text;
using ServerCore;

namespace MMO_Server {
    class Knight {
        public int hp;
        public int attack;
    }

    class GameSession: Session {
        public override void OnConnected(EndPoint endPoint) {
            Console.WriteLine($"OnConnected: {endPoint}");

            Knight knight = new Knight() { hp = 100, attack = 10 };

            ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
            // [] [] [] [] [] [] [] -> [hp] [attack]
            byte[] buffer1 = BitConverter.GetBytes(knight.hp);
            byte[] buffer2 = BitConverter.GetBytes(knight.attack);
            Array.Copy(buffer1, 0, openSegment.Array, openSegment.Offset, buffer1.Length);
            Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer1.Length, buffer2.Length);
            ArraySegment<byte> sendBuff = SendBufferHelper.Close(buffer1.Length + buffer2.Length);

            // 100명
            // 1 -> 이동패킷이 100명
            // 100 -> 이동패킷이 100 * 100 = 1만

            Send(sendBuff);
            Thread.Sleep(1000);
            Disconnect();
        }

        public override void OnDisConnected(EndPoint endPoint) {
            Console.WriteLine($"OnDisconnected: {endPoint}");
        }

        public override int OnRecv(ArraySegment<byte> buffer) {
            string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"[From Client] {recvData}");
            return buffer.Count;
        }

        public override void OnSend(int numOfBytes) {
            Console.WriteLine($"Transferd bytes: {numOfBytes}");
        }
    }

    internal class Program {
        static Listener _listener = new Listener();

        static void Main(string[] args) {
            // DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);


            _listener.Init(endPoint, () => { return new GameSession(); });
            Console.WriteLine("Listening...");

            while (true) {

            }
        }
    }
}
