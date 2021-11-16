using System;
using System.Runtime.Serialization;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        internal class IdNotExsistException : Exception
        {
            private string Text;
            private int ID;
            public IdNotExsistException()
            {
            }

            public IdNotExsistException(string message, int id) : base(message)
            {
                this.Text = message;
                this.ID = id;
            }

            public IdNotExsistException(string message, Exception innerException) : base(message, innerException)
            {

            }

            protected IdNotExsistException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
            public override string ToString()
            {
                return $"ERROR: There is no {Text} with this ID number: {ID}";
            }
        }

    }
}