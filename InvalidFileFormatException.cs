using System;

public class InvalidFileFormatException : Exception
{
    public InvalidFileFormatException() { }

    public InvalidFileFormatException(string message) : base(message) { }

    public InvalidFileFormatException(string message, Exception innerException)
        : base(message, innerException) { }
}