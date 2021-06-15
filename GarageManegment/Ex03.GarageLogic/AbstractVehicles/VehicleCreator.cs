using System;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        public enum eVechicleTypes
        {
            ElectricPrivateCar,
            ElectricMotorcycle,
            FuelMotorcycle,
            FuelPrivatCar,
            Truck
        }

        public static Vehicle CreateVehicle(eVechicleTypes i_TypeOfVehicle, string i_ModelName, string i_RegistrationNum)
        {
            Vehicle toReturn = null;

            switch (i_TypeOfVehicle)
            {
                case eVechicleTypes.ElectricPrivateCar:
                    toReturn = new ElectricCar(i_ModelName, i_RegistrationNum);
                    break;
                case eVechicleTypes.ElectricMotorcycle:
                    toReturn = new ElectricMotorcycle(i_ModelName, i_RegistrationNum);
                    break;
                case eVechicleTypes.FuelPrivatCar:
                    toReturn = new FuelPrivateCar(i_ModelName, i_RegistrationNum);
                    break;
                case eVechicleTypes.FuelMotorcycle:
                    toReturn = new FuelMotorcycle(i_ModelName, i_RegistrationNum);
                    break;
                case eVechicleTypes.Truck:
                    toReturn = new Truck(i_ModelName, i_RegistrationNum);
                    break;
                default:
                    break;
            }

            return toReturn;
        }
    }
}
