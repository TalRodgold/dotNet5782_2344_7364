using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
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
            Text = message;
        }

        protected NoFreeSpace(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"BL EXCEPTION: The {Text} are full";
        }
    }
}