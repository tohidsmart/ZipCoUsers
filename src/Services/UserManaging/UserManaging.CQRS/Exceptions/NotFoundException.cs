using System;
using System.Net;

namespace UserManaging.CQRS.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public NotFoundException(object entity, string key) : base($" entity {entity} with key : {key} was not found  ")
        {
            HttpStatusCode = HttpStatusCode.NotFound;
        }
    }
}
