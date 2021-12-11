using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    /// <summary>
    /// Exception if 2 id's are the same
    /// </summary>
    [Serializable]
    public class SameIdException : Exception
    {
        public string Text;
        public int? Id;
        public SameIdException()
        {
        }

        public SameIdException(string message, int? id) : base(message)
        {
            this.Id = id;
            this.Text = message;
        }

        public SameIdException(string message, int? id, Exception innerException) : base(message, innerException)
        {
            this.Id = id;
            this.Text = message;
        }

        protected SameIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
        public override string ToString()
        {
            return $"BL EXCEPTION: {Text} is the same: {Id}";
        }
    }
}