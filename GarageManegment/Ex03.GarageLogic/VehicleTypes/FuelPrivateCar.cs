using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{ 
    public class FuelPrivateCar : FuelBasedVehicle
    {
        private const float k_MaxFuelCapacity = 45f;
        private const Fuel.eFuelTypes k_FuelType = Fuel.eFuelTypes.Octan95;
        private PrivateCar m_PrivateCar;

        public FuelPrivateCar(string i_ModelName, string i_RegistrationNumber) : base(i_ModelName, i_RegistrationNumber, k_FuelType, k_MaxFuelCapacity)
        {
        }

        public override string ToString()
        {
            string FuelCarString = base.ToString();
            FuelCarString += m_PrivateCar.ToString();
            return FuelCarString;
        }

        public override List<Parameter> CreateParameterList()
        {
            List<Parameter> paramsList = base.CreateParameterList();
            foreach (Parameter param in PrivateCar.CreateParameterList())
            {
                paramsList.Add(param);
            }

            return paramsList;
        }

        public override int NumberOfWheels()
        {
            return PrivateCar.WheelsAmount;
        }

        public override float WheelsMaxPSI()
        {
            return PrivateCar.WheelsMaxPSI;
        }

        public override void FillValues(List<Parameter> i_ListOfParams)
        {
            base.FillValues(i_ListOfParams);
            Color.eCarColors carColor = i_ListOfParams[1].ConvertEnum<Color.eCarColors>();
            Doors.eNumberOfDoors numberOfDoors = i_ListOfParams[2].ConvertEnum<Doors.eNumberOfDoors>();
            m_PrivateCar = new PrivateCar(carColor, numberOfDoors);
        }
    }
}
