using System;

namespace Ex03.GarageLogic
{
    public class WrongUserInputException : Exception
    {
        private readonly string r_ErrorMessage;

        public WrongUserInputException(string i_ErrorMessage) : base(i_ErrorMessage)
        {
            r_ErrorMessage = i_ErrorMessage;
        }

        public override string Message
        {
            get
            {
                return r_ErrorMessage;
            }
        }
    }
}
