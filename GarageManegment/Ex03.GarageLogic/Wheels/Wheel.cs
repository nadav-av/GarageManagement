using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private readonly string r_Manufacturer;
        private float m_CurrentAirPressure;

        public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxPressure)
        {
            if (i_CurrentAirPressure > i_MaxPressure || i_CurrentAirPressure <= 0)
            {
                throw new ValueOutOfRangeException(0, i_MaxPressure);
            }

            r_Manufacturer = i_Manufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxPressure;
        }

        public float CurrentPressuer
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxPressuer
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public void InflateAir(float i_PressureTobeAdded)
        {
            if (CurrentPressuer + i_PressureTobeAdded > MaxPressuer)
            {
                throw new ValueOutOfRangeException(0, MaxPressuer - CurrentPressuer);
            }
            else
            {
                m_CurrentAirPressure += i_PressureTobeAdded;
            }
        }

        public override string ToString()
        {
            string stringifyWheel = string.Format(
@"
Manufacturer: {0}
Current air pressure {1}
Max air pressure {2}

",
r_Manufacturer,
m_CurrentAirPressure,
r_MaxAirPressure);

            return stringifyWheel;
        }
    }
}
