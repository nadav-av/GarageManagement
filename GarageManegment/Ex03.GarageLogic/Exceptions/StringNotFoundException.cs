using System;

namespace Ex03.GarageLogic
{
    public class StringNotFoundException : Exception
    {
        private readonly string r_LookedFor;

        public StringNotFoundException(string i_LookedFor)
          : base()
        {
            r_LookedFor = i_LookedFor;
        }

        public string StringVal
        {
            get { return r_LookedFor; }
        }
    }
}
