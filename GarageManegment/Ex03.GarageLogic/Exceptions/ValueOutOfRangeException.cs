using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;
        private readonly string r_ErrorMessage;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base(string.Empty)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
            r_ErrorMessage = string.Format("Invalid input, please enter a value between {0} to {1}", r_MinValue, r_MaxValue);
        }

        public float MaxVal
        {
            get
            {
                return r_MaxValue;
            }
        }

        public float MinVal
        {
            get
            {
                return r_MinValue;
            }
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
