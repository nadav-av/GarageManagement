using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    // $G$ DSN-999 (-7) Truck is no different from car or motorcycle - FuelTruck and ElectricTruck
    public class Truck : FuelBasedVehicle
    {
        private const int k_NumberOfWheels = 16;
        private const float k_WheelsMaxAirPressure = 26;
        private const float k_MaxFuelCapacity = 120f;
        private const Fuel.eFuelTypes k_FuelType = Fuel.eFuelTypes.Soler;
        private bool m_CanCarryHazardousMaterials;
        private float m_MaxCargoWeight;

        public Truck(string i_ModelName, string i_RegistrationNumber) : base(i_ModelName, i_RegistrationNumber, k_FuelType, k_MaxFuelCapacity)
        {
        }

        public static int WheelsAmount
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public static float WheelsMaxAirPressure
        {
            get
            {
                return k_WheelsMaxAirPressure;
            }
        }

        public override string ToString()
        {
            string yesOrNo = string.Empty;
            yesOrNo += m_CanCarryHazardousMaterials ? "Yes" : "No";

            string FuelTruckString = base.ToString();
            FuelTruckString += string.Format(
                @"
Truck Data:
Can Carry Hazardous Materials: {0}
Max Cargo Weight: {1} Kg
",
yesOrNo,
m_MaxCargoWeight);

            return FuelTruckString;
        }

        public override int NumberOfWheels()
        {
            return WheelsAmount;
        }

        public override float WheelsMaxPSI()
        {
            return WheelsMaxAirPressure;
        }

        public override List<Parameter> CreateParameterList()
        {
            List<Parameter> paramsList = base.CreateParameterList();
            paramsList.Add(new Parameter(typeof(bool), "if can carry hazardous materials (true/false): ", null));
            paramsList.Add(new Parameter(typeof(float), "max cargo weight: ", null));
            return paramsList;
        }

        public override void FillValues(List<Parameter> i_ListOfParams)
        {
            base.FillValues(i_ListOfParams);
            m_CanCarryHazardousMaterials = (bool)i_ListOfParams[1].ParameterContent;
            m_MaxCargoWeight = (float)i_ListOfParams[2].ParameterContent;

            if (m_MaxCargoWeight <= 0)
            {
                throw new WrongUserInputException("Max cargo weight is invalid. Truck wasn't added in the garage.");
            }
        }
    }
}
