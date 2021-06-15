using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle
    {
        private const int k_NumberOfWheels = 2;
        private const float k_WheelsMaxAirPressue = 30;
        private readonly Registrations.eRegistrationTypes r_RegistrationType;
        private readonly int r_EngineVolume;

        public Motorcycle(Registrations.eRegistrationTypes i_RegType, int i_EngineVol)
        {
            r_RegistrationType = i_RegType;
            r_EngineVolume = i_EngineVol;
        }

        public static int WheelsAmount
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public static float WheelMaxPSI
        {
            get
            {
                return k_WheelsMaxAirPressue;
            }
        }

        public static List<Parameter> CreateParameterList()
        {
            List<Parameter> paramsList = new List<Parameter>();
            string regString = string.Format(@"registration type:
1.A
2.B1
3.AA
4.BB
");
            paramsList.Add(new Parameter(typeof(Registrations.eRegistrationTypes), regString, null));
            paramsList.Add(new Parameter(typeof(int), "engine volume: ", null));
            return paramsList;
        }

        public override string ToString()
        {
            string stringifyMotorcycle = string.Format(
@"
Motorcycle Data:
Registration Type: {0}
Engine Voulume: {1} 
",
r_RegistrationType,
r_EngineVolume);

            return stringifyMotorcycle;
        }
    }
}
