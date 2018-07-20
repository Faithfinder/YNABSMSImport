using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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

        protected OtherYNABException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}