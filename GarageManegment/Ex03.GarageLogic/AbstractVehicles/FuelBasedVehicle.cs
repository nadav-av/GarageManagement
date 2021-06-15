using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class FuelBasedVehicle : Vehicle
    {
        private readonly float r_MaxFuelCapacity;
        private readonly Fuel.eFuelTypes r_FuelType;
        private float m_RemainingFuel;

        public FuelBasedVehicle(string i_ModelName, string i_RegistrationNumber, Fuel.eFuelTypes i_FuelType, float i_MaxCapacity) : base(i_ModelName, i_RegistrationNumber)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelCapacity = i_MaxCapacity;
        }

        public float TankCapacity
        {
            get
            {
                return r_MaxFuelCapacity;
            }
        }

        public float RemainingFuel
        {
            get
            {
                return m_RemainingFuel;
            }

            set
            {
                m_RemainingFuel = value;
            }
        }

        public Fuel.eFuelTypes FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public void FuelUp(float i_AmountToAdd, Fuel.eFuelTypes i_FuelToAdd)
        {
            if ((RemainingFuel + i_AmountToAdd) < 0 || (RemainingFuel + i_AmountToAdd) > TankCapacity)
            {
                throw new ValueOutOfRangeException(0, (TankCapacity - RemainingFuel) * 60);
            }
            else if (i_FuelToAdd != FuelType)
            {
                throw new WrongTypeException("The type of fuel you entered is not right");
            }
            else
            {
                RemainingFuel += i_AmountToAdd;
                PowerPercentLeft = (RemainingFuel * 100) / TankCapacity;
            }
        }

        public override string ToString()
        {
            string stringifyFuelVehicle = base.ToString();
            stringifyFuelVehicle += string.Format(
@"Fuel Tank Data:
Fuel Type: {0}
Maximum Fuel Tank Capacity: {1} Liters
Remaining Fuel Left: {2} Liters
",
r_FuelType,
r_MaxFuelCapacity,
m_RemainingFuel);

            return stringifyFuelVehicle;
        }

        public override void FillValues(List<Parameter> i_ListOfParams)
        {
            base.FillValues(i_ListOfParams);
            m_RemainingFuel = getRemainingFuelAmount();
        }

        private float getRemainingFuelAmount()
        {
            return PowerPercentLeft * TankCapacity / 100;
        }
    }
}
