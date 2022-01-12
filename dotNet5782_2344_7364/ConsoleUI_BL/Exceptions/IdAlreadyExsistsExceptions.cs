using System;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// throw exception for error where id already exsists
    /// </summary>
    [Serializable]
    public class IdAlreadyExsistsExceptions : Exception
    {
        public string Text;
        public int? ID;
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
        public IdAlreadyExsistsExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected IdAlreadyExsistsExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"DAL EXCEPTION: There is already a {Text} with the ID number: {ID}";
        }
    }
}