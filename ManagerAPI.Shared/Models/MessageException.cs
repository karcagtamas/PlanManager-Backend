using System;

namespace ManagerAPI.Shared.Models
{
    public class MessageException : Exception
    {
        public MessageException() {}

        public MessageException(string msg): base(msg) {
        }
    }
}