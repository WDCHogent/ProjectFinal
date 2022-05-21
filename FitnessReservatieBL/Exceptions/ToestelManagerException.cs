using System;

namespace FitnessReservatieBL.Exceptions
{
    public class ToestelManagerException : Exception
    {
        public ToestelManagerException(string message) : base(message)
        {
        }
        public ToestelManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
