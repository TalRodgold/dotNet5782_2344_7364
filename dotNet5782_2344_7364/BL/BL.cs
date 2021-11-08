using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBl
    {
        public List<DroneToList> droneToLists2 = new List<DroneToList>();
        BL bl;
        IDal dal;
        //public DroneToList  = new list<DroneToList>;

        public BL()
        {
            dal = new DalObjects.DalObjects();
        }

        public void AddBaseStation(int id, string name, double longtitude, double latitude, int freeChargingSlots)
        {
            try
            {
                dal.ConstructBaseStation(id, name, freeChargingSlots, longtitude, latitude);
            }
            catch (Exception)
            {

                throw;
            }
            //Location location = new Location(longtitude, latitude); // creat location
            //List<DroneInCharging> listDroneInCharging = new List<DroneInCharging>(); // creat empty list
            //BaseStation newBaseStation = new BaseStation(id, name, location, freeChargingSlots, listDroneInCharging); // creat new base station
            //if (dal.IfBaseStationExsists(newBaseStation.Id)) // if id already exisists
            //{
            //    throw "?"; // throw expretion
            //}

        }
        public void AddDrone(int id, string model, int maxWeight, int startingBaseStation)
        {
            try
            {
                dal.ConstructDrone(id, model, (IDAL.DO.WeightCategories)maxWeight); // creat drone
                if (!dal.IfBaseStationExsists(startingBaseStation)) // check if start station exisists
                {
                    throw "?";
                }
                dal.UpdateDroneCharge(id, startingBaseStation); // connect drone to charging base station
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddCustomer(int id, string name, string phone, double longtitude, double latitude)
        {
            try
            {
                dal.ConstructCustomer(id, name, phone, longtitude, latitude);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddParcel(int sender, int reciver, int weight, int priority)
        {
            try
            {
                dal.ConstructParcel(dal.GenerateParcelId(),sender,reciver, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)priority,DateTime.MinValue,NULL, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue)
            }
            catch (Exception)
            {

                throw;
            }
        }
    }



}
