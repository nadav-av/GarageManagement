using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class FuelMotorcycle : FuelBasedVehicle
    {
        private const float k_MaxFuelCapacity = 6f;
        private const Fuel.eFuelTypes k_FuelType = Fuel.eFuelTypes.Octan98;
        private Motorcycle m_Motorcycle;

        public FuelMotorcycle(string i_ModelName, string i_RegistrationNumber) : base(i_ModelName, i_RegistrationNumber, k_FuelType, k_MaxFuelCapacity)
        {
        }

        public override string ToString()
        {
            string FuelMotorcycleString = base.ToString();
            FuelMotorcycleString += m_Motorcycle.ToString();
            return FuelMotorcycleString;
        }

        public override List<Parameter> CreateParameterList()
        {
            List<Parameter> paramsList = base.CreateParameterList();
            foreach (Parameter param in Motorcycle.CreateParameterList())
            {
                paramsList.Add(param);
            }

            return paramsList;
        }

        public override int NumberOfWheels()
        {
            return Motorcycle.WheelsAmount;
        }

        public override float WheelsMaxPSI()
        {
            return Motorcycle.WheelMaxPSI;
        }

        public override void FillValues(List<Parameter> i_ListOfParams)
        {
            base.FillValues(i_ListOfParams);
            Registrations.eRegistrationTypes regType = i_ListOfParams[1].ConvertEnum<Registrations.eRegistrationTypes>();
            int engineVol = (int)i_ListOfParams[2].ParameterContent;

            if (engineVol <= 0)
            {
                throw new WrongUserInputException("Engine volume is invalid. Motorcycle wasn't added in the garage.");
            }
            else
            {
                m_Motorcycle = new Motorcycle(regType, engineVol);
            }
        }
    }
}
