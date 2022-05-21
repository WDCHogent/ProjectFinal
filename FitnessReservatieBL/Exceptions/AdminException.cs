using System;

namespace FitnessReservatieBL.Exceptions
{
    public class AdminException : Exception
    {
        public AdminException(string message) : base(message)
        {
        }
        public AdminException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
