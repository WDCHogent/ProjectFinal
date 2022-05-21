using System;

namespace FitnessReservatieBL.Exceptions
{
    public class ReservatieInfoManagerException : Exception
    {
        public ReservatieInfoManagerException(string message) : base(message)
        {
        }
        public ReservatieInfoManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
