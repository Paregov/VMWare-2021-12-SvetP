using System;

namespace WarehouseBackend.Core.Exceptions
{
    public class WarehouseBackendException : Exception
    {
        public WarehouseBackendException(string message) :
            base(message)
        {

        }

        public WarehouseBackendException(string message,
            Exception innerException) :
            base(message, innerException)
        {

        }
    }
}
