
namespace BlogYes.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string exceptionCode) : base(exceptionCode) 
        {
            ExceptionCode = exceptionCode;
        }

        public string ExceptionCode { get; private set; }
    }
}
