using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AgCubio
{
    public class Preserved_Socket_State
    {
        /// <summary>
        /// Socket that we're connected to.
        /// </summary>
        public Socket workSocket = null;

        /// <summary>
        /// Size of the buffer
        /// </summary>
        public const int bufferSize = 256;

        /// <summary>
        /// Byte array of size bufferSize
        /// </summary>
        public byte[] buffer = new byte[bufferSize];

        /// <summary>
        /// String builder used to store data receieved.
        /// </summary>
        public StringBuilder sb = new StringBuilder();

        public int bytesRead;

        public byte[] testSend;


        public string errorMessage;

        /// <summary>
        /// The callback delegate for the preserved state.
        /// </summary>
        //public Action<Preserved_Socket_State> callback;
        public Delegate callback;

    }
}
