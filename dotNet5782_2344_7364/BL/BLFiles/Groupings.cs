using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

namespace BlApi
{
    internal sealed partial class BL : IBl
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<Enums.DroneStatuses,DroneToList>> GroupingStatuses()
        {
            lock (dal)
            {
                return ListOfDronsBL.GroupBy(element => element.DroneStatuses);

            }        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<Enums.WeightCategories, DroneToList>> GroupingWeight()
        {
            return ListOfDronsBL.GroupBy(element => element.Weight);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<int, BaseStationToList>> GroupingFreeChargingSlots()
        {
            lock (dal)
            {
                return GetListOfBaseStationsToList().GroupBy(element => element.FreeChargingSlots);

            }        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<string, ParcelToList>> GroupingSender()
        {
            lock (dal)
            {
                return GetListOfParcelToList().GroupBy(element => element.SendersName);

            }        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<string, ParcelToList>> GroupingReciver()
        {
            lock (dal)
            {
                return GetListOfParcelToList().GroupBy(element => element.ReciversName);

            }        }
    }
}
