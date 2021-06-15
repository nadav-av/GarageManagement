using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_RegistrationNumber;
        private float m_PowerPercentageLeft;
        private List<Wheel> m_WheelsOfVehicle;

        public Vehicle(string i_ModelName, string i_RegistrationNumber)
        {
            this.r_ModelName = i_ModelName;
            this.r_RegistrationNumber = i_RegistrationNumber;
        }

        public float PowerPercentLeft
        {
            get
            {
                return m_PowerPercentageLeft;
            }

            set
            {
                m_PowerPercentageLeft = value;
            }
        }

        public string RegNum
        {
            get { return r_RegistrationNumber; }
        }

        public List<Wheel> Wheels
        {
            get { return m_WheelsOfVehicle; }
        }

        public override string ToString()
        {
            string stringifyVehicle = string.Format(
@"
Elementary Vehicle Data:
Model Name: {0}
Registration Number: {1}
Power Left: {2}%

",
r_ModelName,
r_RegistrationNumber,
m_PowerPercentageLeft);

            int i = 1;
            foreach (Wheel wheel in m_WheelsOfVehicle)
            {
                stringifyVehicle += $"Wheel {i}";
                i++;
                stringifyVehicle += wheel.ToString();
            }

            return stringifyVehicle;
        }

        public virtual List<Parameter> CreateParameterList()
        {
            List<Parameter> paramsList = new List<Parameter>();
            paramsList.Add(new Parameter(typeof(float), "power percentage left: ", null));
            return paramsList;
        }

        public abstract int NumberOfWheels();

        public abstract float WheelsMaxPSI();

        public void AddWheels(List<Wheel> i_ListOfWheels)
        {
            m_WheelsOfVehicle = i_ListOfWheels;
        }

        public virtual void FillValues(List<Parameter> i_ListOfParams)
        {
            m_PowerPercentageLeft = (float)i_ListOfParams[0].ParameterContent;
            if (m_PowerPercentageLeft < 0 || m_PowerPercentageLeft > 100)
            {
                throw new WrongUserInputException("Power percentage left is invalid. Vehicle wasn't added in the garage.");
            }
        }
    }
}
