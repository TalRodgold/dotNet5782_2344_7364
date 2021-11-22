﻿using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class UnavailableExeption : Exception
    {
        public string Text;
        public int ID;
        public UnavailableExeption()
        {
        }

        public UnavailableExeption(string message,int id) : base(message)
        {
            this.Text = message;
            this.ID = id;
        }

        public UnavailableExeption(string message, int id, Exception innerException) : base(message, innerException)
        {

            this.Text = message;
            this.ID = id;
        }

        protected UnavailableExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            if (ID != 0)
            {
                return $"ERROR: The {Text} {ID} is Unaviliable ";
            }
            else
            {
                return $"ERROR: there are no {Text}s aviliable ";
            }
        }
    }
}