using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace BlApi
{
    internal sealed partial class BL : IBl
    {
        public IEnumerable<IGrouping<Enums.DroneStatuses,DroneToList>> GroupingStatuses()
        {
            return ListOfDronsBL.GroupBy(element => element.DroneStatuses);
        }
        public IEnumerable<IGrouping<Enums.WeightCategories, DroneToList>> GroupingWeight()
        {
            return ListOfDronsBL.GroupBy(element => element.Weight);
        }
        public IEnumerable<IGrouping<int, BaseStationToList>> GroupingFreeChargingSlots()
        {
            return GetListOfBaseStationsToList().GroupBy(element => element.FreeChargingSlots);
        }
        public IEnumerable<IGrouping<string, ParcelToList>> GroupingSender()
        {
            return GetListOfParcelToList().GroupBy(element => element.SendersName);
        }
        public IEnumerable<IGrouping<string, ParcelToList>> GroupingReciver()
        {
            return GetListOfParcelToList().GroupBy(element => element.ReciversName);
        }
    }
}
