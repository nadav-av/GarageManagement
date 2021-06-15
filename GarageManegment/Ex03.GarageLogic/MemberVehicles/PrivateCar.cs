using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class PrivateCar
    {
        private const int k_NumberOfWheels = 4;
        private const float k_WheelsMaxAirPressue = 32;
        private readonly Doors.eNumberOfDoors r_NumberOfDoors;
        private readonly Color.eCarColors r_CarPaint;

        public PrivateCar(Color.eCarColors i_CarPaint, Doors.eNumberOfDoors i_NumberOfDoors)
        {
            r_CarPaint = i_CarPaint;
            this.r_NumberOfDoors = i_NumberOfDoors;
        }

        public static int WheelsAmount
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public static float WheelsMaxPSI
        {
            get
            {
                return k_WheelsMaxAirPressue;
            }
        }

        public static List<Parameter> CreateParameterList()
        {
            List<Parameter> paramsList = new List<Parameter>();
            string colorString = string.Format(@"car color:
1.Red
2.Silver
3.White
4.Black
");
            paramsList.Add(new Parameter(typeof(Color.eCarColors), colorString, null));
            string doorsString = string.Format(@"number of doors:
1.2 doors
2.3 doors
3.4 doors
4.5 doors
");
            paramsList.Add(new Parameter(typeof(Doors.eNumberOfDoors), doorsString, null));
            return paramsList;
        }

        public override string ToString()
        {
            string stringifyPrivateCar = string.Format(
@"
Private Car Data:
Car Paint: {0}
Number Of Doors: {1} 
",
r_CarPaint,
(int)r_NumberOfDoors);

            return stringifyPrivateCar;
        }
    }
}
