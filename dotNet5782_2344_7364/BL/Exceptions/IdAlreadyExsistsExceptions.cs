using System;
using System.Runtime.Serialization;

namespace BO
{
    /// <summary>
    /// throw exception for error where id already exsists
    /// </summary>
    [Serializable]
    public class IdAlreadyExsistsExceptions : Exception
    {
        private string Text;
        private int? ID;

        public IdAlreadyExsistsExceptions()
        {
        }

        public IdAlreadyExsistsExceptions(string message) : base(message)
        {
        }

        public IdAlreadyExsistsExceptions(string message, int? id) : base(message)
        {
            this.Text = message;
            this.ID = id;
        }

        public IdAlreadyExsistsExceptions(string message, int? id, Exception innerException) : base(message, innerException)
        {
            this.Text = message;
            this.ID = id;
        }

        protected IdAlreadyExsistsExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"BL EXCEPTION: There is already a {Text} with the ID number: {ID}";
        }
    }
}