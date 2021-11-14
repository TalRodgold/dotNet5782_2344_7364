using System;
using System.Runtime.Serialization;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        internal class IdAlreadyExsistsExceptions : Exception
        {
            int ID;
            public IdAlreadyExsistsExceptions(int id)
            {
                ID = id;
            }

            public IdAlreadyExsistsExceptions(string message, int id) : base(message)
            {
                ID = id;
            }

            public IdAlreadyExsistsExceptions(string message, int id, Exception innerException) : base(message, innerException)
            {
                ID = id;
            }

            protected IdAlreadyExsistsExceptions(int id, SerializationInfo info, StreamingContext context) : base(info, context)
            {
                ID = id;
            }
            public override string ToString()
            {
                return $"ERROR: There is already a {Message} with the ID number {ID}";
            }
        }
    }

}