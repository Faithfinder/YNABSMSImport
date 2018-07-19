using System;
using YNABConnector.YNABObjectModel;

namespace YNABConnector.Exceptions
{
    internal static class ExceptionFactory
    {
        internal static Exception GenerateExceptionFromErrorResponse(ErrorResponse errorResponse)
        {
            var details = errorResponse.error;
            switch (details.id)
            {
                case "401":
                    return new AuthorizationException($"YNAB didn't authorize us. Is access token fine?");

                default:
                    return new OtherYNABException($"YNAB answered with error. ID: {details.id}. Name: {details.name}. Details: {details.detail}");
            }
        }
    }
}