using System;
using System.Net;

namespace UserManaging.CQRS.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public BadRequestException(object entity, string message) : base($" entity {entity} has bad or invalid data." +
            $"look at the message:\n{message} ")
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
        }
    }
}
