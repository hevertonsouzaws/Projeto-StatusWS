﻿namespace StatusWS.Errors
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        { }
    }
}
