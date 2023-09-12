using InnoClinic.Domain.Enums;

namespace InnoClinic.Domain.Exceptions
{
    public class ClinicException : Exception
    {
        public ClinicException(string message, ExceptionCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ExceptionCode ErrorCode { get; }
    }
}
