using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    /// <summary>
    /// throw exception for invalid id
    /// </summary>
    [Serializable]
    public class InvalidIdException : Exception
    {
        public string Text;
        public int Id;
        public InvalidIdException()
        {
        }

        public InvalidIdException(string message, int id) : base(message)
        {
            Text = message;
            Id = id;
        }

        public InvalidIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"BL EXCEPTION: The ID {Id} is {Text} ";
        }
    }
}