﻿using System;
using System.Runtime.Serialization;

namespace ConsoleUI_BL
{
    [Serializable]
    internal class InvalidValueException : Exception
    {
        public InvalidValueException()
        {
        }

        public InvalidValueException(string message) : base(message)
        {
        }

        public InvalidValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}