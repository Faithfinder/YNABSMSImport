using System;
using System.Runtime.Serialization;

namespace YNABConnector.Exceptions
{
    [Serializable]
    public class DuplicateImportIdException : Exception
    {
        public DuplicateImportIdException()
        {
        }

        public DuplicateImportIdException(string message) : base(message)
        {
        }

        public DuplicateImportIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateImportIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}