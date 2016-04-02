using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleProxy
{
    class PacketType
    {
        private static Dictionary<int, string> KnownPackets = new Dictionary<int, string>
        {
            { 10100, "SessionMessage" },
            { 10101, "LoginMessage" },
            { 10108, "KeepAliveMessage" },
            { 20100, "SessionOkMessage" },
            { 20103, "LoginFailedMessage" },
            { 20104, "LoginOkMessage" },
            { 20108, "KeepAliveOkMessage" },
            { 24101, "OwnHomeDataMessage" },
            { 24104, "OutOfSyncMessage" },
            { 24114, "HomeBattleReplayMessage" },
            { 24115, "ServerErrorMessage" },
            { 24124, "CancelMatchmakeDone" },
            { 24125, "CancelMatchmakeDone" },
            { 12951, "BattleActionMessage" },
            { 00000, "-- TO BE CONTINUED --" }

        };
        
        public static string GetPacketTypeByID(int messageID)
        {
            if (KnownPackets.ContainsKey(messageID))
            {
                string ret = String.Empty;
                KnownPackets.TryGetValue(messageID, out ret);
                return ret;
            }
            return "UnknownMessage";
        }

        public static int GetPacketIDByType(string type)
        {
            return KnownPackets.FirstOrDefault(x => x.Value == type).Key;
        }
    }
}
