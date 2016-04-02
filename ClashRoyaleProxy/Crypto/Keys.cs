using System;
using System.Text;

namespace ClashRoyaleProxy
{
    class Keys
    {
        /// <summary>
        /// The generated private key, according to the modded public key.
        /// </summary>
        public static byte[] GeneratedPrivateKey
        {
            get
            {
                return Helper.HexToByteArray("1891d401fadb51d25d3a9174d472a9f691a45b974285d47729c45c6538070d85");
            }
        }

        /// <summary>
        /// The modded Clash Royale public key.
        /// Offset 0x0039A01C [ARM / ANDROID]
        /// </summary>
        public static byte[] ModdedPublicKey
        {
            get
            {
                return Helper.HexToByteArray("72f1a4a4c48e44da0c42310f800e96624e6dc6a641a9d41c3b5039d8dfadc27e");
            }
        }

        /// <summary>
        /// The original, unmodified Clash Royale public key.
        /// Offset 0x0039A01C [ARM / ANDROID]
        /// </summary>
        public static byte[] OriginalPublicKey
        {
            get
            {
                return Helper.HexToByteArray("BA105F0D3A099414D154046F41D80CF122B49902EAB03B78A912F3C66DBA2C39".ToLower());

            }
        }

        /// <summary>
        /// An old nonce used by Supercell.
        /// Paradoxon - the nonce was "nonce"!
        /// </summary>
        public static byte[] OldNonce
        {
            get
            {
                return Encoding.UTF8.GetBytes("nonce");
            }
        }
    }
}
