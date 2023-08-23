
namespace BlogYes.Core.Exceptions
{
    public class NotAcceptableException : Exception
    {
        public NotAcceptableException(string exceptionCode) : base(exceptionCode)
        {
            ExceptionCode = exceptionCode;
        }

        public string ExceptionCode { get; private set; }
    }
}
