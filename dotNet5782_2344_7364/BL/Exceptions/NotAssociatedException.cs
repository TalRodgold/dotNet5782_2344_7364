using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class NotAssociatedException : Exception
    {
        public string Text;
        public int Id;

        public NotAssociatedException()
        {
        }

        public NotAssociatedException(string message) : base(message)
        {
        }

        public NotAssociatedException(string v, int droneId)
        {
            this.Text = v;
            this.Id = droneId;
        }

        public NotAssociatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotAssociatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"BL EXCEPTION: {Text} number {Id} was not associated";
        }
    }
}