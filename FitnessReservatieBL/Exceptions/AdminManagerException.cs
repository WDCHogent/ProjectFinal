using System;

namespace FitnessReservatieBL.Exceptions
{
    public class AdminManagerException : Exception
    {
        public AdminManagerException(string message) : base(message)
        {
        }
        public AdminManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
