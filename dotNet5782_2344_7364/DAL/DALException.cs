using System;
using System.Runtime.Serialization;

namespace DalObjects
{
    [Serializable]
    internal class DALException : Exception
    {
        private Exception exception;

        public DALException()
        {
        }

        public DALException(Exception exception)
        {
            this.exception = exception;
        }

        public DALException(string message) : base(message)
        {
        }

        public DALException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DALException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}