using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClashRoyaleProxy
{
    class Packet
    {
        private byte[] rawPacket;
        private ushort packetID;
        private int payloadLen;
        private ushort messageVer;
        private byte[] payload;
        private string packetType;
        private byte[] encryptedPayload;
        private byte[] decryptedPayload;

        public Packet(byte[] dataBuffer)
        {
            // Read content
            using (var br = new BinaryReader(new MemoryStream(dataBuffer)))
            {
                this.rawPacket = dataBuffer;
                this.packetID = br.ReadUShortWithEndian();
                this.payloadLen = br.ReadMedium();
                this.messageVer = br.ReadUShortWithEndian();
                this.payload = br.ReadBytes(payloadLen);
                this.packetType = PacketType.GetPacketTypeByID(packetID);
            }

            // En/Decrypt payload
            this.decryptedPayload = EnDecrypt.DecryptPayload(this);
            this.encryptedPayload = EnDecrypt.EncryptPayload(this);
        }

        /// <summary>
        /// Raw, encrypted packet (header included)
        /// 7 byte header + n byte payload
        /// Reverse() because of little endian byte order
        /// </summary>
        public byte[] Raw
        {
            get
            {
                List<Byte> builtPacket = new List<Byte>();
                builtPacket.AddRange(BitConverter.GetBytes(ID).Reverse());
                builtPacket.AddRange(BitConverter.GetBytes(EncryptedPayload.Length).Reverse().Skip(1));
                builtPacket.AddRange(BitConverter.GetBytes(MessageVersion).Reverse());
                builtPacket.AddRange(EncryptedPayload);
                return builtPacket.ToArray();
            }
        }

        /// <summary>
        /// Self-explaining.
        /// 10100, 20100, 10101, 20104 [...]
        /// </summary>
        public ushort ID
        {
            get
            {
                return this.packetID;
            }
        }

        /// <summary>
        /// 2 bytes nobody has exact info about.
        /// </summary>
        public ushort MessageVersion
        {
            get
            {
                return this.messageVer;
            }
        }
        /// <summary>
        /// String representation according to the ID.
        /// 10100 => LoginMessage
        /// 10108 => KeepAlive
        /// </summary>
        public string Type
        {
            get
            {
                return this.packetType;
            }
        }

        /// <summary>
        /// Destination. Either client or server.
        /// Admittedly, the Substring method is pretty nasty.
        /// </summary>
        public PacketDestination Destination
        {
            get
            {
                if (ID.ToString().Substring(0, 1) == "1")
                    return PacketDestination.CLIENT_SIDED;
                else
                    return PacketDestination.SERVER_SIDED;
            }
        }

        /// <summary>
        /// Normal payload from the received packet.
        /// </summary>
        public byte[] Payload
        {
            get
            {
                return this.payload;
            }
        }
        /// <summary>
        /// Encrypted payload by <seealso cref="EnDecrypt.EncryptPayload(Packet)"/>
        /// </summary>
        public byte[] EncryptedPayload
        {
            get
            {
                return this.encryptedPayload;
            }
        }

        /// <summary>
        /// Decrypted payload by <seealso cref="EnDecrypt.DecryptPayload(Packet)"/>
        /// </summary>
        public byte[] DecryptedPayload
        {
            get
            {
                return this.decryptedPayload;
            }
        }

        // This hides both Equals() and GetHashCode() methods from my IntelliSense
        // Both piss me off 
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Destination: " + Destination);
            sb.AppendLine("ID: " + ID);
            sb.AppendLine("Type: " + Type);
            sb.AppendLine("PayloadLen: " + DecryptedPayload.Length);
            sb.AppendLine("Payload: " + Encoding.UTF8.GetString(DecryptedPayload));
            return sb.ToString();
        }
    }
}



