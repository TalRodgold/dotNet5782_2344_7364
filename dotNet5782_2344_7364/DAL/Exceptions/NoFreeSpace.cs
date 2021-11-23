using System;
using System.Runtime.Serialization;

namespace IDAL.DO
{
    /// <summary>
    /// Exception when there is no free space
    /// </summary>
    [Serializable]
    public class NoFreeSpace : Exception
    {
        public string Text;
        public NoFreeSpace()
        {
        }
        public NoFreeSpace(string message) : base(message)
        {
            Text = message;
        }
        public NoFreeSpace(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected NoFreeSpace(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"DAL EXCEPTION: The {Text} are full";
        }
    }
}