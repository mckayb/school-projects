using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AgCubio
{

    public static class Network
    {
        // The port number for the remote device.
        private const int port = 11000;

        // The response from the remote device.
        private static string response = String.Empty;

        // A copy of our preserved state.
        //private static Preserved_Socket_State ps = new Preserved_Socket_State();

        // Events for signaling completion.
        private static ManualResetEvent sendDone = new ManualResetEvent(false);

        private static string outgoing = "";

        private static bool currentlySending = false;

        public static string errorMessage = "";

        private static readonly object sendSync = new object();

        private static Encoding encoding = Encoding.UTF8;


        /// <summary>
        /// Function that gets called when the user needs to connect to the server.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static Socket Connect_to_Server(Delegate callback, string hostname)
        {
            try
            {
                Preserved_Socket_State ps = new Preserved_Socket_State();
                IPHostEntry he = Dns.GetHostEntry(hostname);
                IPAddress ip = he.AddressList[0];
                IPEndPoint ep = new IPEndPoint(ip, port);
                Socket connection = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                ps.callback = callback;
                ps.workSocket = connection;
                connection.BeginConnect(ep, Connected_to_Server, ps);
                return connection;
            }
            catch (Exception e)
            {
                //ps.errorMessage = "Problem connecting.";
                //ps.callback.DynamicInvoke(ps);
            }
            return null;

        }

        /// <summary>
        /// Function that gets called when the user establishes a connection to the server.
        /// </summary>
        /// <param name="state_in_an_ar_object"></param>
        public static void Connected_to_Server(IAsyncResult state_in_an_ar_object)
        {
            Preserved_Socket_State state = (Preserved_Socket_State)state_in_an_ar_object.AsyncState;
            Socket connection = state.workSocket;
            

            try
            {
                //TODO: I moved this down here so that it would catch the error,
                //      but it needs to be handled with a message.
                connection.EndConnect(state_in_an_ar_object);
                state.callback.DynamicInvoke(state);

                connection.BeginReceive(state.buffer, 0, Preserved_Socket_State.bufferSize,
                    SocketFlags.None, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Function that gets called when we have received data from the server.
        /// </summary>
        /// <param name="state_in_an_ar_object"></param>
        public static void ReceiveCallback(IAsyncResult state_in_an_ar_object)
        {
            Preserved_Socket_State state = (Preserved_Socket_State)state_in_an_ar_object.AsyncState;

            Socket client = state.workSocket;

            int bytesRead = client.EndReceive(state_in_an_ar_object);

            if (bytesRead > 0)
            {
                state.bytesRead = bytesRead;
                state.callback.DynamicInvoke(state);
            }
            else
            {
                client.Close();
            }
        }


        /// <summary>
        /// Function that happens every time we want more data. Starts receiving from the server.
        /// </summary>
        /// <param name="state"></param>
        public static void i_want_more_data(Preserved_Socket_State state)
        {
            try
            {
                state.workSocket.BeginReceive(state.buffer, 0, Preserved_Socket_State.bufferSize,
                    SocketFlags.None, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                // state.errorMessage = "I Want More Data: " + e.Message;
                //state.callback.DynamicInvoke(state);
            }
        }

        /// <summary>
        /// Function that gets called when we want to send information to the server,
        /// such as sending the player name and sending move/split commands.
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        public static void Send(Socket socket, String message)
        {
            Preserved_Socket_State state = new Preserved_Socket_State();
            state.workSocket = socket;
            state.buffer = encoding.GetBytes(message);
            socket.BeginSend(state.buffer, 0, state.buffer.Length,
                SocketFlags.None, SendCallback, state);
        }

        /// <summary>
        /// Callback for the send function. Reads in the byte data, converts it to a string,
        /// appends the previous data, converts it back to bytes, and continues sending.
        /// When done, it closes the socket connection.
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {

            Preserved_Socket_State state = (Preserved_Socket_State)ar.AsyncState;
            Socket socket = state.workSocket;
            byte[] stateBuffer = state.buffer;

            int bytes = socket.EndSend(ar);

            if (bytes == 0)
            {
                socket.Close();
            }
            else
            {
                outgoing = encoding.GetString(stateBuffer, bytes,
                    stateBuffer.Length - bytes) + outgoing;

                if (outgoing != String.Empty)
                {
                    byte[] outgoingBuffer = encoding.GetBytes(outgoing);
                    state.buffer = outgoingBuffer;
                    outgoing = String.Empty;
                    socket.BeginSend(outgoingBuffer, 0, outgoingBuffer.Length,
                        SocketFlags.None, SendCallback, state);
                }
            }
        }

        /// <summary>
        /// Method used to gracefully disconnect from the server.
        /// </summary>
        /// <param name="socket"></param>
        public static void Disconnect(Socket socket)
        {
            socket.BeginDisconnect(false, DisconnectCallback, socket);
        }

        /// <summary>
        /// Callback for when the socket begins to disconnect.
        /// Ends the disconnect and disposes of the socket.
        /// </summary>
        /// <param name="ar"></param>
        public static void DisconnectCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndDisconnect(ar);
            socket.Close();
        }
    }
}
