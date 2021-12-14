using System;
using System.Runtime.Serialization;

namespace BO
{
    /// <summary>
    /// throw exception for error where there is a size problem
    /// </summary>
    [Serializable]
    public class SizeProblemException : Exception
    {
        public int Number;
        public string Text;
        public SizeProblemException()
        {
        }

        public SizeProblemException(string message, int number) : base(message)
        {
            Number = number;
            Text = message;
        }

        public SizeProblemException(string message, int number, Exception innerException) : base(message, innerException)
        {
            Number = number;
            Text = message;

        }

        protected SizeProblemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"DAL EXCEPTION: The number entered must be {Text} than {Number}";
        }
    }
}