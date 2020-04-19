using System;
using System.Runtime.Serialization;

namespace Prime
{
    //[Serializable]
    public class PrimeErrorsException : Exception
    {

        public PrimeErrorsException()
            : base()
        {
        }

        public PrimeErrorsException(string message)
            : base(message)
        {
        }

        protected PrimeErrorsException(SerializationInfo info, StreamingContext context):
            base(info, context)
        {
        }

        public PrimeErrorsException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

    }

}
