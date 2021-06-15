namespace Ex03.GarageLogic
{
    public class VehicleStatus
    {
        public enum eVehicleStatus
        {
            WorkInProgress = 1,
            WorkIsDone,
            Paid
        }
        
        public static string PrintStatus(VehicleStatus.eVehicleStatus i_StatusToPrint)
        {
            string statusPrint = string.Empty;
            switch (i_StatusToPrint)
            {
                case VehicleStatus.eVehicleStatus.WorkInProgress:
                    statusPrint = "work in progress";
                    break;
                case VehicleStatus.eVehicleStatus.WorkIsDone:
                    statusPrint = "work is done";
                    break;
                case VehicleStatus.eVehicleStatus.Paid:
                    statusPrint = "paid";
                    break;
            }

            return statusPrint;
        }
    }
}
