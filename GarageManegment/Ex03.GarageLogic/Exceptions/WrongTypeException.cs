using System;

namespace Ex03.GarageLogic
{
    public class WrongTypeException : Exception
    {
        private readonly string r_ErrorMessage;

        public WrongTypeException(string i_ErrorMessage) : base(i_ErrorMessage)
        {
            r_ErrorMessage = i_ErrorMessage;
        }

        public string Massege
        {
            get
            {
                return r_ErrorMessage;
            }
        }
    }
}
