using System;
using System.Runtime.Serialization;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        internal class BLException : Exception
        {
            public BLException()
            {
            }

            public BLException(string message) : base(message)
            {
            }

            public BLException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected BLException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }

}