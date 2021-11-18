using System;
using System.Runtime.Serialization;

namespace IDAL.DO
{
    [Serializable]
    public class SizeProblemException : Exception
    {
        public int Number;
        public SizeProblemException()
        {
        }

        public SizeProblemException(string message, int number) : base(message)
        {
            Number = number;
        }

        public SizeProblemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SizeProblemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"ERROR: The number entered must be bigger than {Number}";
        }
    }
}