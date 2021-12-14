using System;
using System.Runtime.Serialization;

namespace BO
{
    /// <summary>
    /// throw exception for error where id dosent exsists
    /// </summary>
    [Serializable]
    public class IdNotExsistException : Exception
    {
        public string Text;
        public int? ID;
        public IdNotExsistException()
        {
        }

        public IdNotExsistException(string message, int? id) : base(message)
        {
            this.Text = message;
            this.ID = id;
        }

        public IdNotExsistException(string message, int? id, Exception innerException) : base(message, innerException)
        {
            this.Text = message;
            this.ID = id;
        }

        protected IdNotExsistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"BL EXCEPTION: There is no {Text} with this ID number: {ID}";
        }
    }
}