using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class ElectricCar : ElectricBasedVehicle
    {
        private const float k_MaxBatteryHours = 3.2f;
        private PrivateCar m_PrivateCar;

        public ElectricCar(string i_ModelName, string i_RegistrationNumber) : base(i_ModelName, i_RegistrationNumber, k_MaxBatteryHours)
        {
        }

        public override string ToString()
        {
            string electricCarString = base.ToString();
            electricCarString += m_PrivateCar.ToString();
            return electricCarString;
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
