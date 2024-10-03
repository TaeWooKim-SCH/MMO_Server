using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore {
    internal class Listener {
        Socket _listenSocket;
        Action<Socket> _onAcceptHandler;

        public void Init(IPEndPoint endPoint, Action<Socket> onAcceptHandler) {
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _onAcceptHandler = onAcceptHandler;

            // 문지기 교육
            _listenSocket.Bind(endPoint);

            // 영업 시작
            // backlog: 최대 대기 수
            _listenSocket.Listen(10);

            // 처음에만 인위적으로 만들고 다음부턴 RegisterAccept -> OnAcceptCompleted -> RegisterAccept 반복
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted); // 바로 물지 않으면 입질을 기다림
            RegisterAccept(args); // 이부분 accept는 낚시대를 그냥 휙 하고 던진 것
        }

        void RegisterAccept(SocketAsyncEventArgs args) {
            args.AcceptSocket = null;

            bool pending = _listenSocket.AcceptAsync(args); // 비동기를 이용해 바로 처리될 수도 있고 아닐 수도 있음
            if (pending == false) { // 낚시대를 강물에 던지자마자 물고기가 잡힘
                OnAcceptCompleted(null, args);
            }
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args) {
            if (args.SocketError == SocketError.Success) { // 통에다 물고기를 집어 넣는 과정
                // TODO
                _onAcceptHandler.Invoke(args.AcceptSocket);
            } else {
                Console.WriteLine(args.SocketError.ToString());
            }

            RegisterAccept(args); // 볼 일을 다 보고 끝났으면 다시 낚시대를 바다로 던지는 과정
        }

        public Socket Accept() {
            return _listenSocket.Accept();
        }
    }
}
