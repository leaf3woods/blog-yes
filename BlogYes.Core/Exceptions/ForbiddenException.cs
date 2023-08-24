namespace BlogYes.Core.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string exceptionCode) : base(exceptionCode)
        {
            ExceptionCode = exceptionCode;
        }

        public string ExceptionCode { get; private set; }
    }
}
