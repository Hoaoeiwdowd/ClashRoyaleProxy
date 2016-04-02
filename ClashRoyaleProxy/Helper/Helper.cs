using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleProxy
{

    class Helper
    { 
        /// <summary>
        /// Uses LINQ to convert a hexlified string to a byte array.
        /// </summary>
        public static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Converts a byte array to a hexlified string.
        /// </summary>
        public static string ByteArrayToHex(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", " ").ToUpper();
        }

        public static List<string> Credits
        {
            get
            {
                return new List<string>()
                {
                    "ExPl0itR",
                    "you..?"
                };
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Remove(5);
            }
        }
    }
}
