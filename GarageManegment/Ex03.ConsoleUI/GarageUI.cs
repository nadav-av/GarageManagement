using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{

    // $G$ CSS-999 (-3) The Class must have an access modifier.
    class GarageUI
    {
        private const string k_MainMenu =
        @"Please choose from the following options (1-8):
        1. Add a new vehicle.
        2. Display registration numbers of all vehicles by working status.
        3. Modify a vehicle's status.
        4. Inflate a vehicle's wheels to maximum.
        5. Fuel a fuel based vehicle.
        6. Charge an electric vehicle.
        7. Display full details of a vehicle.
        8. Quit.
        ";

        private static readonly Garage sr_Garage = new Garage();

        private enum eMenuOptions
        {
            AddNewVehicle = 1,
            DisplayRegNumsByStatus,
            ChangeStatus,
            InflateWheels,
            Refuel,
            Charge,
            DisplayFullDetails,
            Exit
        }

        public static void RunGarageManagmentProgram()
        {
            bool cont = true;

            while (cont)
            {
                try
                {
                    Console.Clear();
                    printMenu();
                    string userInput = getValidEnumValue(typeof(eMenuOptions));
                    int.TryParse(userInput, out int intUserInput);
                    eMenuOptions chosenTask = (eMenuOptions)intUserInput;
                    executeTask(chosenTask, ref cont);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: bad input format.");
                }
                catch (WrongUserInputException err)
                {
                    Console.WriteLine(err.Message);
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(string.Format(exception.Message, exception.MinVal, exception.MaxVal));
                }
                catch (StringNotFoundException exception)
                {
                    Console.WriteLine(string.Format(
                                      "Error: vehicle with registration number: {0} was not found.",
                                      exception.StringVal));
                }
                catch (WrongTypeException exception)
                {
                    Console.WriteLine(exception.Massege);
                }

                if (cont)
                {
                    Console.WriteLine("press any key to return to selection menu");
                    Console.ReadLine();
                }
            }
        }

        private static void printMenu()
        {
            Console.WriteLine(k_MainMenu);
        }

        private static void executeTask(eMenuOptions i_ChosenTask, ref bool io_Cont)
        {
            switch (i_ChosenTask)
            {
                case eMenuOptions.AddNewVehicle:
                    addVehicleToGarage();
                    break;

                case eMenuOptions.DisplayRegNumsByStatus:
                    printRegNumsByStatus();
                    break;

                case eMenuOptions.ChangeStatus:
                    changeVehicleStatus();
                    break;

                case eMenuOptions.InflateWheels:
                    inflateVehicleWheels();
                    break;

                case eMenuOptions.Refuel:
                    refuelVehicle();
                    break;

                case eMenuOptions.Charge:
                    chargeVehicle();
                    break;

                case eMenuOptions.DisplayFullDetails:
                    printVehicleByRegNum();
                    break;

                case eMenuOptions.Exit:
                    io_Cont = false;
                    break;
            }
        }

        private static void printVehicleByRegNum()
        {
            Console.Write("In order to print vehicle's details, please enter registration number of this vehicle: ");
            string regNum = Console.ReadLine();
            if (sr_Garage.IfVehicleExistsInGarage(regNum))
            {
                Garage.Record chosenVehicle = sr_Garage.GetRecordFromDictionary(regNum);
                Console.WriteLine(chosenVehicle.ToString());
            }
            else
            {
                throw new StringNotFoundException(regNum);
            }
        }

        private static void chargeVehicle()
        {
            Console.Write("In order to charge vehicle, please enter registration number of this vehicle: ");
            string regNum = Console.ReadLine();
            if (sr_Garage.IfVehicleExistsInGarage(regNum))
            {
                Garage.Record chosenVehicle = sr_Garage.GetRecordFromDictionary(regNum);
                if (chosenVehicle.Vehicle is ElectricBasedVehicle)
                {
                    Console.WriteLine("please enter mintues to charge: ");
                    string inputString = Console.ReadLine();
                    float.TryParse(inputString, out float minuteToCharge);
                    sr_Garage.ChargeExistingVehicle(regNum, minuteToCharge);
                    Console.WriteLine("Charging vehicle was done successfully");
                }
                else
                {
                    throw new WrongTypeException("The vehicle type is invalid.");
                }
            }
            else
            {
                throw new StringNotFoundException(regNum);
            }
        }

        private static void refuelVehicle()
        {
            Console.Write("In order to refuel vehicle, please enter registration number of this vehicle: ");
            string regNum = Console.ReadLine();
            if (sr_Garage.IfVehicleExistsInGarage(regNum))
            {
                Garage.Record chosenVehicle = sr_Garage.GetRecordFromDictionary(regNum);
                if (chosenVehicle.Vehicle is FuelBasedVehicle)
                {
                    Console.Write("please enter fuel type, ");
                    Fuel.eFuelTypes fuelType = (Fuel.eFuelTypes)getChoiseFromEnums(typeof(Fuel.eFuelTypes));
                    Console.WriteLine("please enter amount of fuel to add: ");
                    string inputString = Console.ReadLine();
                    float.TryParse(inputString, out float amountToAdd);
                    sr_Garage.FuelExistingVehicle(regNum, fuelType, amountToAdd);
                    Console.WriteLine("Refueling vehicle was done successfully");
                }
                else
                {
                    throw new WrongTypeException("The vehicle type is invalid.");
                }
            }
            else
            {
                throw new StringNotFoundException(regNum);
            }
        }

        private static void inflateVehicleWheels()
        {
            Console.Write("In order to inflate vehicle wheels, please enter registration number of this vehicle: ");
            string regNum = Console.ReadLine();
            if (sr_Garage.IfVehicleExistsInGarage(regNum))
            {
                sr_Garage.InflateWheelsToMaximum(regNum);
                Console.WriteLine("Inflating vehicle wheels was done successfully");
            }
            else
            {
                throw new StringNotFoundException(regNum);
            }
        }

        private static void changeVehicleStatus()
        {
            Console.Write("In order to change vehicle status, please enter registration number of this vehicle: ");
            string regNum = Console.ReadLine();
            if (sr_Garage.IfVehicleExistsInGarage(regNum))
            {
                Console.Write("Please enter the new status, ");
                VehicleStatus.eVehicleStatus vehicleType = (VehicleStatus.eVehicleStatus)getChoiseFromEnums(typeof(VehicleStatus.eVehicleStatus));
                sr_Garage.ChangeExistingRecordStatus(regNum, vehicleType);
                Console.WriteLine("Vehicle status was changed successfully");
            }
            else
            {
                throw new StringNotFoundException(regNum);
            }
        }

        private static void printRegNumsByStatus()
        {
            Console.Write("Display registration numbers of vehicles by status. Please ");
            VehicleStatus.eVehicleStatus vehicleType = (VehicleStatus.eVehicleStatus)getChoiseFromEnums(typeof(VehicleStatus.eVehicleStatus));

            List<string> listOfRegNums = sr_Garage.GetRegNumbersOfChosenStatus(vehicleType);
            if (listOfRegNums.Count != 0)
            {
                Console.WriteLine("The registration numbers with the specific status: ");
                foreach (string regNum in listOfRegNums)
                {
                    Console.WriteLine(regNum);
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in the garage with the chosen status.");
            }
        }

        private static void addVehicleToGarage()
        {
            string ownerName = string.Empty;
            string ownerPhone = string.Empty;
            VehicleCreator.eVechicleTypes vehicleType = 0;
            Vehicle toAdd = getDetailsOfVehicle(ref ownerName, ref ownerPhone, ref vehicleType);
            bool isExistInGarage = sr_Garage.AddRecordToGarage(toAdd, ownerName, ownerPhone);
            if (isExistInGarage)
            {
                Console.WriteLine(string.Format(
@"vehicle is already exist,
Changing status to WorkInProgress"));
            }
            else
            {
                Console.WriteLine("Vehicle was added successfully");
            }
        }

        private static Vehicle getDetailsOfVehicle(ref string io_OwnerName, ref string io_OwnerPhone, ref VehicleCreator.eVechicleTypes io_VehicleType)
        {
            Console.Write("Please enter the owner`s full name: ");
            object tempObj = getValidValue(typeof(string));
            io_OwnerName = tempObj.ToString();
            Console.Write("Please enter the owner`s phone number: ");
            tempObj = getValidValue(typeof(string));
            io_OwnerPhone = tempObj.ToString();

            io_VehicleType = (VehicleCreator.eVechicleTypes)getChoiseFromEnums(typeof(VehicleCreator.eVechicleTypes));
            return fillParameterList(io_VehicleType);
        }

        private static Vehicle fillParameterList(VehicleCreator.eVechicleTypes i_VehicleType)
        {
            Console.Write("Please enter model name: ");
            object modelNameObj = getValidValue(typeof(string));
            string modelName = modelNameObj.ToString();
            Console.Write("Please enter registration number: ");
            object regNumObj = getValidValue(typeof(string));
            string regNum = regNumObj.ToString();
            Vehicle chosenVehicle = VehicleCreator.CreateVehicle(i_VehicleType, modelName, regNum);
            if (chosenVehicle != null)
            {
                List<Parameter> paramsList = chosenVehicle.CreateParameterList();
                foreach (Parameter param in paramsList)
                {
                    Console.Write(string.Format("please enter {0}", param.ParameterName));
                    if (param.ParameterType.IsEnum)
                    {
                        param.ParameterContent = getValidEnumValue(param.ParameterType);
                    }
                    else
                    {
                        param.ParameterContent = getValidValue(param.ParameterType);
                    }
                }

                List<Wheel> wheelsOfVehicle = getWheelsListForVehicle(chosenVehicle.NumberOfWheels(), chosenVehicle.WheelsMaxPSI());
                chosenVehicle.AddWheels(wheelsOfVehicle);
                chosenVehicle.FillValues(paramsList);
            }

            return chosenVehicle;
        }

        private static object getValidValue(Type i_ValType)
        {
            object currentVal = null;
            bool isValid = false;
            while (!isValid)
            {
                string stringInput = Console.ReadLine();

                try
                {
                    if (stringInput.Trim().Equals(string.Empty))
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        currentVal = Convert.ChangeType(stringInput, i_ValType);
                        isValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.Write("Wrong Input. please enter a valid value: ");
                }
            }

            return currentVal;
        }

        private static string getValidEnumValue(Type i_EnumType)
        {
            string stringInput = Console.ReadLine();

            if (stringInput.Equals(string.Empty))
            {
                Console.Write("Not in Range. Please choose again: ");
                stringInput = Console.ReadLine();
            }

            while (!int.TryParse(stringInput, out int intValue) || !Enum.IsDefined(i_EnumType, Enum.ToObject(i_EnumType, intValue)))
            {
                Console.Write("Not in Range. Please choose again: ");
                stringInput = Console.ReadLine();
            }

            return stringInput;
        }

        private static List<Wheel> getWheelsListForVehicle(int i_amountOfWheels, float i_MaxPsi)
        {
            List<Wheel> wheelsListToReturn = new List<Wheel>();

            for (int i = 1; i <= i_amountOfWheels; i++)
            {
                Console.WriteLine($"Wheel {i}:");
                Console.Write("Please enter wheel manufacturer: ");
                string wheelManufacturer = Console.ReadLine();
                Console.Write("Please enter wheel current air pressure: ");
                string currentWheelPressure = Console.ReadLine();
                float.TryParse(currentWheelPressure, out float floatWheelPsi);
                try
                {
                    if (floatWheelPsi > i_MaxPsi || floatWheelPsi <= 0)
                    {
                        throw new ValueOutOfRangeException(0, i_MaxPsi);
                    }

                    Wheel wheelToAdd = new Wheel(wheelManufacturer, floatWheelPsi, i_MaxPsi);
                    wheelsListToReturn.Add(wheelToAdd);
                }
                catch (ValueOutOfRangeException err)
                {
                    Console.WriteLine(err.Message);
                    i--;
                }
            }

            return wheelsListToReturn;
        }

        private static object getChoiseFromEnums(Type i_Enum)
        {
            object enumValueToReturn = null;

            if (!i_Enum.IsEnum)
            {
                throw new WrongTypeException("Incorrect type.");
            }

            Console.WriteLine("Choose one of the following: ");
            int currentValueIndex = 1;
            foreach (object enumValue in Enum.GetValues(i_Enum))
            {
                Console.WriteLine(string.Format("{0}- {1}", currentValueIndex, enumValue));
                currentValueIndex++;
            }

            currentValueIndex--;

            string chosenEnumValue = Console.ReadLine();
            bool sucsess = int.TryParse(chosenEnumValue, out int NumericChosenEnumValue);

            if (!sucsess)
            {
                throw new FormatException();
            }

            if (NumericChosenEnumValue < 1 || NumericChosenEnumValue > currentValueIndex)
            {
                throw new ValueOutOfRangeException(1, currentValueIndex);
            }

            currentValueIndex = 1;
            foreach (object enumValue in Enum.GetValues(i_Enum))
            {
                if (NumericChosenEnumValue == currentValueIndex)
                {
                    enumValueToReturn = enumValue;
                    break;
                }

                currentValueIndex++;
            }

            return enumValueToReturn;
        }
    }
}
