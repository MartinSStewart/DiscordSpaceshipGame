using System;
using System.Runtime.Serialization;

namespace Spaceship
{
    [Serializable]
    internal class UnreachableException : Exception
    {
        public UnreachableException()
        {
        }

        public UnreachableException(string message) : base(message)
        {
        }

        public UnreachableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnreachableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}