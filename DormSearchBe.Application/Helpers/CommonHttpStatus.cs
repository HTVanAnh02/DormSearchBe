using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Helpers
{
    public static class CommonHttpStatus
    {
        // Informational 1xx
        public const int Continue = 100;
        public const int SwitchingProtocols = 101;
        public const int Processing = 102;

        // Success 2xx
        public const int OK = 200;
        public const int Created = 201;
        public const int Accepted = 202;
        public const int NonAuthoritativeInformation = 203;
        public const int NoContent = 204;
        public const int ResetContent = 205;
        public const int PartialContent = 206;
        public const int MultiStatus = 207;
        public const int AlreadyReported = 208;
        public const int IMUsed = 226;

        // Redirection 3xx
        public const int MultipleChoices = 300;
        public const int MovedPermanently = 301;
        public const int Found = 302;
        public const int SeeOther = 303;
        public const int NotModified = 304;
        public const int UseProxy = 305;
        public const int TemporaryRedirect = 307;
        public const int PermanentRedirect = 308;

        // Client Error 4xx
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int PaymentRequired = 402;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int MethodNotAllowed = 405;
        public const int NotAcceptable = 406;
        public const int ProxyAuthenticationRequired = 407;
        public const int RequestTimeout = 408;
        public const int Conflict = 409;
        public const int Gone = 410;
        public const int LengthRequired = 411;
        public const int PreconditionFailed = 412;
        public const int PayloadTooLarge = 413;
        public const int UriTooLong = 414;
        public const int UnsupportedMediaType = 415;
        public const int RangeNotSatisfiable = 416;
        public const int ExpectationFailed = 417;
        public const int ImATeapot = 418;
        public const int MisdirectedRequest = 421;
        public const int UnprocessableEntity = 422;
        public const int Locked = 423;
        public const int FailedDependency = 424;
        public const int UpgradeRequired = 426;
        public const int PreconditionRequired = 428;
        public const int TooManyRequests = 429;
        public const int RequestHeaderFieldsTooLarge = 431;
        public const int UnavailableForLegalReasons = 451;

        // Server Error 5xx
        public const int InternalServerError = 500;

    }
}
