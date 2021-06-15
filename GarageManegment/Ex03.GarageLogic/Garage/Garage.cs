using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Record> r_VehiclesRecords;

        public Garage()
        {
            r_VehiclesRecords = new Dictionary<string, Record>();
        }

        public bool AddRecordToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNum)
        {
            string regNumber = i_Vehicle.RegNum;
            bool exists = false;
            if (IfVehicleExistsInGarage(regNumber))
            {
                Record toChange = GetRecordFromDictionary(regNumber);
                toChange.Status = VehicleStatus.eVehicleStatus.WorkInProgress;
                exists = true;
            }
            else
            {
                Record recordToBeAdded = new Record(i_Vehicle, i_OwnerName, i_OwnerPhoneNum);
                r_VehiclesRecords.Add(i_Vehicle.RegNum, recordToBeAdded);
            }

            return exists;
        }

        public bool IfVehicleExistsInGarage(string i_RegNum)
        {
            return r_VehiclesRecords.ContainsKey(i_RegNum);
        }

        public Record GetRecordFromDictionary(string i_RegNum)
        {
            bool vehicleExists = r_VehiclesRecords.TryGetValue(i_RegNum, out Record garageVehicle);
            if (!vehicleExists)
            {
                garageVehicle = null;
            }

            return garageVehicle;
        }

        public List<string> GetRegNumbersOfChosenStatus(VehicleStatus.eVehicleStatus i_StatusInGarage)
        {
            List<string> licenseList = new List<string>();

            foreach (Record garageRecord in r_VehiclesRecords.Values)
            {
                if (i_StatusInGarage == garageRecord.Status)
                {
                    licenseList.Add(garageRecord.Vehicle.RegNum);
                }
            }

            return licenseList;
        }

        public void ChangeExistingRecordStatus(string i_RegNum, VehicleStatus.eVehicleStatus i_StatusToSet)
        {
            Record recordToChange = GetRecordFromDictionary(i_RegNum);
            if (recordToChange == null)
            {
                throw new StringNotFoundException(i_RegNum);
            }
            else
            {
                recordToChange.Status = i_StatusToSet;
            }
        }

        public void FuelExistingVehicle(string i_RegNum, Fuel.eFuelTypes i_FuelType, float i_AmountToAdd)
        {
            Record vehicleInGarage = GetRecordFromDictionary(i_RegNum);
            
            if (vehicleInGarage == null)
            {
                throw new StringNotFoundException(i_RegNum);
            }

            if (vehicleInGarage.Vehicle is ElectricBasedVehicle)
            {
                throw new WrongTypeException("The vehicle type is invalid.");
            }

            float minToHour = i_AmountToAdd / 60;
            (vehicleInGarage.Vehicle as FuelBasedVehicle).FuelUp(minToHour, i_FuelType);
        }

        public void ChargeExistingVehicle(string i_RegNum, float i_MinuteToCharge)
        {
            Record vehicleInGarage = GetRecordFromDictionary(i_RegNum);
            if (vehicleInGarage == null)
            {
                throw new StringNotFoundException(i_RegNum);
            }

            if (vehicleInGarage.Vehicle is FuelBasedVehicle)
            {
                throw new WrongTypeException("The vehicle type is invalid.");
            }

            float minToHour = i_MinuteToCharge / 60;
            (vehicleInGarage.Vehicle as ElectricBasedVehicle).ChargeUp(minToHour);
        }

        public void InflateWheelsToMaximum(string i_RegNum)
        {
            Record vehicleInGarage = GetRecordFromDictionary(i_RegNum);
            if (vehicleInGarage == null)
            {
                throw new StringNotFoundException(i_RegNum);
            }

            List<Wheel> WheelsOfVehicle = vehicleInGarage.Vehicle.Wheels;
            foreach (Wheel wheelToInflate in WheelsOfVehicle)
            {
                wheelToInflate.CurrentPressuer = wheelToInflate.MaxPressuer;
            }
        }

        public class Record
        {
            private readonly Vehicle r_Vehicle;
            private readonly string r_OwnerName;
            private readonly string r_OwnerPhone;
            private VehicleStatus.eVehicleStatus m_GarageStatus;

            public Record(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
            {
                r_Vehicle = i_Vehicle;
                r_OwnerName = i_OwnerName;
                r_OwnerPhone = i_OwnerPhone;
                m_GarageStatus = VehicleStatus.eVehicleStatus.WorkInProgress;
            }

            public Vehicle Vehicle
            {
                get
                {
                    return r_Vehicle;
                }
            }

            public VehicleStatus.eVehicleStatus Status
            {
                get
                {
                    return m_GarageStatus;
                }

                set
                {
                    m_GarageStatus = value;
                }
            }

            public override string ToString()
            {
                string stringifyRecord = string.Format(
                    @"
Record Data:
Owner Name: {0}
Owner Phone: {1}
Maintenence Status: {2}
",
r_OwnerName,
r_OwnerPhone,
VehicleStatus.PrintStatus(m_GarageStatus));

                stringifyRecord += r_Vehicle.ToString();

                return stringifyRecord;
            }
        }
    }
}
