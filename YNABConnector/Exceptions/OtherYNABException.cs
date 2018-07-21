using System;
using System.Runtime.Serialization;

namespace YNABConnector.Exceptions
{
    public class OtherYNABException : Exception
    {
        public OtherYNABException()
        {
        }

        public OtherYNABException(string message) : base(message)
        {
        }

        public OtherYNABException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}