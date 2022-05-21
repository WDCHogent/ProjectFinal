using System;

namespace FitnessReservatieBL.Exceptions
{
    public class ToestelException : Exception
    {
        public ToestelException(string message) : base(message)
        {
        }
        public ToestelException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
