using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    /// <summary>
    /// Exception if a location is out of range
    /// </summary>
    [Serializable]
    public class LocationOutOfRangeException : Exception
    {
        string Text;
        public LocationOutOfRangeException()
        {
        }
        public LocationOutOfRangeException(string message) : base(message)
        {
            Text = message;
        }
        public LocationOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected LocationOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"BL EXCEPTION: {Text} entered is out of range";
        }
    }
}