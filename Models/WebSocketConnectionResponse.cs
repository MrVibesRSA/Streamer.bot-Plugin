using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Models
{
    public class WebSocketConnectionResponse
    {
        public string timestamp = "";
        public string session = "";
        public string request = "";
        public Info info;
        public Authentication authentication; 
        public string status = "";

        public override string ToString()
        {
            return $"Timestamp: {timestamp}, Session: {session}, Request: {request}, " +
                   $"Instance ID: {info.instanceId}, Name: {info.name}, Version: {info.version}, " +
                   $"OS: {info.os}, OS Version: {info.osVersion}, Mode: {info.mode}, Source: {info.source}, " +
                   $"Salt: {authentication.salt}, Challenge: {authentication.challenge}, Status: {status}";
        }
    }

    public class Info
    {
        public string instanceId = "";
        public string name = "";
        public string version = "";
        public string os = "";
        public string osVersion = "";
        public string mode = "";
        public string source = "";
    }

    public class Authentication
    {
        public string salt = "";
        public string challenge = "";
    }
}
