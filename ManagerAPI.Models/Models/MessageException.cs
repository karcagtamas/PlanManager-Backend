using System;

namespace ManagerAPI.Models.Models
{
    public class MessageException : Exception
    {
        public MessageException() {}

        public MessageException(string msg): base(msg) {
        }
    }
}