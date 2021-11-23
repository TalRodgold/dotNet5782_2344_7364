using System;
using System.Runtime.Serialization;

namespace DalObjects
{
    [Serializable]
    internal class NoFreeSpace : Exception
    {
        public NoFreeSpace()
        {
        }

        public NoFreeSpace(string message) : base(message)
        {
        }

        public NoFreeSpace(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoFreeSpace(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}