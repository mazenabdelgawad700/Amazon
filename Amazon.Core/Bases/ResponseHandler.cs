using System.Net;

namespace Amazon.Core.Bases
{
    public class ResponseHandler
    {
        public Response<T> Success<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Done Successfully",
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnauthorizedAccess"
            };
        }
        public Response<T> Failed<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.ExpectationFailed,
                Succeeded = false,
                Message = "ExpectationFailed"
            };
        }
        public Response<T> BadRequest<T>(string message = null!)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message ?? "BadRequest"
            };
        }
        public Response<T> UnprocessableEntity<T>(string message = null!)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = message ?? "UnprocessableEntity"
            };
        }
        public Response<T> NotFound<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = "Not Found",
            };
        }
        public Response<T> Created<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created Successfully",
            };
        }
        public Response<T> Updated<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Updated Successfully",
            };
        }
        public Response<T> Deleted<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully",
            };
        }
    }
}