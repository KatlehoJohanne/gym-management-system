using System;

namespace GymManagement.Classes
{
    // custom exception for when login fails
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException() : base("Invalid username or password.")
        {
        }

        public InvalidLoginException(string message) : base(message)
        {
        }
    }

    // thrown when account is locked after 3 attempts
    public class AccountLockedException : Exception
    {
        public AccountLockedException() : base("Account has been locked due to too many failed attempts.")
        {
        }

        public AccountLockedException(string message) : base(message)
        {
        }
    }

    // used when member data is not valid
    public class MemberValidationException : Exception
    {
        public MemberValidationException() : base("Member information is not valid.")
        {
        }

        public MemberValidationException(string message) : base(message)
        {
        }
    }

    // for database errors
    public class DatabaseException : Exception
    {
        public DatabaseException() : base("A database error has occurred.")
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    // for class/program related issues
    public class ClassNotFoundException : Exception
    {
        public ClassNotFoundException() : base("The class or program was not found.")
        {
        }

        public ClassNotFoundException(string message) : base(message)
        {
        }
    }
}
