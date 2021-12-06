
using System;

namespace WarehouseBackend.ApiClient
{
    public class WarehouseApiClientException : Exception
    {
        public WarehouseApiClientException(string message) :
            base(message)
        {
            
        }

        public WarehouseApiClientException(string message, Exception innerException) :
            base(message, innerException)
        {

        }
    }
}
