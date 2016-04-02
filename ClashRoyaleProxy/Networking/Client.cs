using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClashRoyaleProxy
{
    class Client
    {
        private const string CRHost = "game.clashroyaleapp.com";
        private Socket ClientSocket, ServerSocket;

        public Client(Socket s)
        {
            ClientSocket = s;
        }

        public void Enqueue()
        {
            // Connect to the official supercell server
            IPHostEntry ipHostInfo = Dns.GetHostEntry(CRHost);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 9339);
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Connect(remoteEndPoint);
            Logger.Log("Proxy attached to " + CRHost + " (" + ServerRemoteAdr + ")!", LogType.INFO);
            Logger.Log("Starting recv/send threads..", LogType.INFO);

            // Receive/Send procedure. 
            Task SendRecvTask = Task.Factory.StartNew(new Action(() =>
            {
                    while (true)
                    {
                        if (ClientSocket.Available > 0)
                        {
                            // data from client 
                            byte[] buf = new byte[ClientSocket.Available];
                            ClientSocket.Receive(buf);
                            Packet p = new Packet(buf);
                            Logger.Log("Packet " + p.ID + " from " + ((p.Destination == PacketDestination.CLIENT_SIDED) ? "client" : "server") + ":", LogType.PACKET);
                            Logger.Log(Encoding.UTF8.GetString(p.DecryptedPayload), LogType.PACKET);
                            ServerSocket.Send(p.Raw);
                        }
                        if (ServerSocket.Available > 0)
                        {
                            // data from game[a].clashofclans
                            byte[] buf = new byte[ServerSocket.Available];
                            ServerSocket.Receive(buf);
                            Packet p = new Packet(buf);
                            Logger.Log("Packet " + p.ID + " from " + ((p.Destination == PacketDestination.CLIENT_SIDED) ? "client" : "server") + ":", LogType.PACKET);
                            Logger.Log(Encoding.UTF8.GetString(p.DecryptedPayload), LogType.PACKET);
                            ClientSocket.Send(p.Raw);
                        }
                    }
                

            }));
        }

        public Socket Socket_Client
        {
            get
            {
                return ClientSocket;
            }
        }

        public Socket Socket_Server
        {
            get
            {
                return ServerSocket;
            }
        }
        public IPAddress ClientRemoteAdr
        {
            get
            {
                return ((IPEndPoint)ClientSocket.RemoteEndPoint).Address;
            }
        }

        public IPAddress ServerRemoteAdr
        {
            get
            {
                return ((IPEndPoint)ServerSocket.RemoteEndPoint).Address;
            }
        }
    }
}
