using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException() : base()
        {

        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
