using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class ElectricBasedVehicle : Vehicle
    {
        private readonly float r_MaxBatteryHours;
        private float m_RemainingBatteryHours;

        public ElectricBasedVehicle(string i_ModelName, string i_RegistrationNumber, float i_MaxHours) : base(i_ModelName, i_RegistrationNumber)
        {
            r_MaxBatteryHours = i_MaxHours;
        }

        public float BatteryCapacity
        {
            get
            {
                return r_MaxBatteryHours;    
            }
        }

        public float RemainingBattery
        {
            get
            {
                return m_RemainingBatteryHours;
            }

            set
            {
                m_RemainingBatteryHours = value;
            }
        }

        public void ChargeUp(float i_ToBeAdded)
        {
            if ((RemainingBattery + i_ToBeAdded) < 0 || (this.RemainingBattery + i_ToBeAdded) > BatteryCapacity)
            {
                throw new ValueOutOfRangeException(0, (BatteryCapacity - RemainingBattery) * 60);
            }
            else
            {
                RemainingBattery += i_ToBeAdded;
                PowerPercentLeft = (RemainingBattery * 100) / BatteryCapacity;
            }
        }

        public override string ToString()
        {
            string stringifyElectricVehicle = base.ToString();
            stringifyElectricVehicle += string.Format(
@"Electric Battery Data:
Maximum Hours Capacity: {0}
Remaining Hours Left: {1}
",
r_MaxBatteryHours,
m_RemainingBatteryHours);

            return stringifyElectricVehicle;
        }

        public override void FillValues(List<Parameter> i_ListOfParams)
        {
            base.FillValues(i_ListOfParams);
            m_RemainingBatteryHours = getRemainingHoursOfBattery();
        }

        private float getRemainingHoursOfBattery()
        {
            return (PowerPercentLeft * BatteryCapacity) / 100;
        }
    }
}
