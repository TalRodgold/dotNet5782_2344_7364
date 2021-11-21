using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
namespace IBL
{
    public partial class BL : IBl
    {
        public List<DroneToList> ListOfDronsBL = new List<DroneToList>();
        double ElectricityUseAvailiblity;
        double ElectricityUseLightWeight;
        double ElectricityUseMediumWeight;
        double ElectricityUseHeavyWeight;
        double DroneChargingPaste;
        BL bl;
        IDal dal;
        #region//BL constructor 
        /// <summary>
        /// BL constructor 
        /// </summary>
        public BL()//BL constructor 
        { 
            dal = new DalObjects.DalObjects();//call to constructor
            double[] arr = dal.Electricity();//retun arrey from config contain electrisity data
            ElectricityUseAvailiblity = arr[0];
            ElectricityUseLightWeight = arr[1];
            ElectricityUseMediumWeight = arr[2];
            ElectricityUseHeavyWeight = arr[3];
            DroneChargingPaste = arr[4];
            var listOfDrones = dal.GetListOfDrone();//get the list of drone from datasource
            foreach (var item in listOfDrones)//insert the list od data source drone to list of drone to list
            {
                ListOfDronsBL.Add(ConvertDroneDalToList(item));
            }
        }
        #endregion
        #region//Base station addition
        /// <summary>
        /// Base station addition
        /// </summary>
        /// <param name="b"></param>
        public void AddBaseStation(BaseStation b)//Base station addition
        {
            try
            {
                dal.ConstructBaseStation(b.Id, b.Name, b.NumberOfFreeChargingSlots, b.Location.Longitude, b.Location.Latitude);
            }
            catch (IDAL.DO.IdAlreadyExsistsExceptions exception) // if base station id already exsists and was thrown from dal objects
            {

                throw new IdAlreadyExsistsExceptions(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Drone addition
        /// <summary>
        /// Drone addition
        /// </summary>
        /// <param name="d"></param>
        /// <param name="startingBaseStation"></param>
        public void AddDrone(Drone d, int startingBaseStation)//Drone addition
        {
            try
            {
                if (!dal.IfBaseStationExsists(startingBaseStation))
                {
                    throw new IdNotExsistException("base station", startingBaseStation);
                }
                d.CurrentLocation = GetBaseStationById(startingBaseStation).Location;
                dal.ConstructDrone(d.Id, d.Model, (IDAL.DO.WeightCategories)d.Weight); // creat drone
                dal.UpdateDroneCharge(d.Id, startingBaseStation); // connect drone to charging base station
                ListOfDronsBL.Add(new DroneToList(d.Id, d.Model, d.Weight, d.Battery, d.DroneStatuses, d.CurrentLocation, d.ParcelInTransit.Id));
            }
            catch (IDAL.DO.IdAlreadyExsistsExceptions exception) // if drone id already exsists and was thrown from dal objects
            {
                throw new IdAlreadyExsistsExceptions(exception.Text, exception.ID, exception); // throw
            }
            //catch( IdAlreadyExsistsExceptions exception)
            //{
            //    throw new IdAlreadyExsistsExceptions(exception.Message, startingBaseStation, exception); // throw
            //}
        }
        #endregion
        #region//Customer addition
        /// <summary>
        /// Customer addition
        /// </summary>
        /// <param name="c"></param>
        public void AddCustomer(Customer c)//Customer addition
        {
            try
            {
                dal.ConstructCustomer(c.Id, c.Name, c.Phone, c.Location.Longitude, c.Location.Latitude);
            }
            catch (IDAL.DO.IdAlreadyExsistsExceptions exception) // if customer id already exsists and was thrown from dal objects
            {

                throw new IdAlreadyExsistsExceptions(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Parcel addition
        /// <summary>
        /// Parcel addition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reciver"></param>
        /// <param name="weight"></param>
        /// <param name="prioritie"></param>
        public void AddParcel(CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie)
        {
            int id = dal.ConstructParcel(sender.Id, reciver.Id, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)prioritie, DateTime.Now, -1, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            Parcel newParcel = new Parcel(id, sender, reciver, weight, prioritie, null, DateTime.Now, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }
        #endregion
    }

    public partial class BL : IBl
    {
        #region//Get customer from data-source by id
        /// <summary>
        /// //Get customer from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int id)//Get customer from data-source by id
        {
            try
            {
                IDAL.DO.Customer idalCustomer = dal.GetCustomer(id); // get a customer of dal type
                Customer newCustomer = new Customer(idalCustomer.Id, idalCustomer.Name, idalCustomer.Phone, new Location(idalCustomer.Longtitude, idalCustomer.Latitude)); // creat and add to a customer of bl type


                Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == id; // predicat to find parcel based on senders id
                Predicate<IDAL.DO.Parcel> predicate1 = element => element.TargetId == id; // predicat to find parcel based on targets id

                List<IDAL.DO.Parcel> newSenderParcel = dal.GetListOfParcel (predicate).ToList(); // get parcel of dal type based on senders id
                List<ParcelAtCustomer> list = new List<ParcelAtCustomer>();
                foreach (var item in newSenderParcel)
                {
                    list.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, StatusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(newCustomer.Id, newCustomer.Name)));
                }
                newCustomer.ParcelFromCustomer = list; // add to new customer the parcel from customer

                List<IDAL.DO.Parcel> newReciveParcels= dal.GetListOfParcel(predicate1).ToList(); // get parcel of dal type based on reciver id
                List<ParcelAtCustomer> list1 = new List<ParcelAtCustomer>();
                foreach (var item in newReciveParcels)
                {
                    list1.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, StatusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(newCustomer.Id, newCustomer.Name)));

                }
                newCustomer.ParcelToCustomer = list1; // add to new customer the parcel from customer
                return newCustomer;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if customer id does not exsists and was thrown from dal objects
            {

                 throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region //Get parcel from data-source by id
        /// <summary>
        /// from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Parcel GetParcelById(int id, Predicate<IDAL.DO.Parcel> predicate = null)//from data-source by id
        {
            try
            {
                IDAL.DO.Parcel idalParcel = dal.GetParcel(id, predicate);
                if (idalParcel.Id <= 0)
                {
                    return null;
                }
                Customer senderCustomer = GetCustomerById(idalParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(idalParcel.TargetId); // creat a new customer based on the reciver of the parcel

                CustomerInParcel senderCustomerInParcel = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name); // creat a customer in parcel based on current customer
                CustomerInParcel reciverCustomerInParcel = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name); // creat a customer in parcel based on current customer
                DroneInParcel newDroneInParcel;
                if (idalParcel.DroneId == -1)
                {
                    newDroneInParcel = null;
                }
                else
                {
                    Drone newDrone = GetDroneById(idalParcel.DroneId);
                    newDroneInParcel = new DroneInParcel(newDrone.Id, newDrone.Battery, newDrone.CurrentLocation);

                }


                Parcel newParcel = new Parcel(idalParcel.Id, senderCustomerInParcel, reciverCustomerInParcel, (Enums.WeightCategories)idalParcel.Weight, (Enums.Priorities)idalParcel.Priority, newDroneInParcel, idalParcel.Requsted, idalParcel.Scheduled, idalParcel.PickedUp, idalParcel.Deliverd);
                return newParcel;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if parcel id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region //Get drone from data-source by id
        /// <summary>
        /// Get drone from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone GetDroneById(int id)//Get drone from data-source by id
        {
            try
            {
                if (id == -1)
                {
                    throw new IdNotExsistException("drone", id); // throw
                }
                Predicate<IDAL.DO.Parcel> predicate = element => element.DroneId == id;
                ParcelInTransit newParcelInTransit;
                if (GetDroneToList(id).NumberOfParcelInTransit == -1)
                {
                    newParcelInTransit = new ParcelInTransit();
                }
                else
                {
                    newParcelInTransit = GetParcelInTransitById(GetDroneToList(id).NumberOfParcelInTransit);
                }
                DroneToList droneToList = GetDroneToList(id);
                Drone newDrone = new Drone(droneToList.Id, droneToList.Model, droneToList.Weight, droneToList.Battery, droneToList.DroneStatuses, newParcelInTransit, droneToList.CurrentLocation);
                return newDrone;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if drone id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get drone from list that is held here by id
        /// <summary>
        /// Get drone from list that is held here by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DroneToList GetDroneToList(int id)//Get drone from list that is held here by id
        {
            try
            {
                if (!dal.IfDroneExsists(id))
                {
                    throw new IdNotExsistException("drone", id, new IDAL.DO.IdNotExsistException("drone", id));
                }
                return ListOfDronsBL.Find(element => element.Id == id);

            }
            catch (IDAL.DO.IdNotExsistException exception) // if base station id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get base station from data-source by id
        /// <summary>
        /// Get base station from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseStation GetBaseStationById(int id)//Get base station from data-source by id
        {
            try
            {
                if (!dal.IfBaseStationExsists(id))
                {
                    throw new IdNotExsistException("base station", id, new IDAL.DO.IdNotExsistException("base station", id));
                }
                IDAL.DO.BaseStation idalBaseStation = dal.GetBaseStation(id);

                IEnumerable<IDAL.DO.DroneCharge> listOfDronesCharging = dal.GetListOfDroneCharge();
                List<DroneInCharging> listToAdd = new List<DroneInCharging>();
                foreach (var item in listOfDronesCharging)
                {
                    if (item.StationId == idalBaseStation.Id)
                    {
                        listToAdd.Add(new DroneInCharging(item.DroneId, ListOfDronsBL.Find(element => element.Id == item.DroneId).Battery));

                    }
                }
                BaseStation newBaseStation = new BaseStation(idalBaseStation.Id, idalBaseStation.Name, new Location(idalBaseStation.Longtitude, idalBaseStation.Latitude), idalBaseStation.ChargeSlots, listToAdd);
                return newBaseStation;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if base station id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get parceltolist with manipulation from data source by id
        /// <summary>
        /// //Get parceltolist with manipulation from data source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ParcelToList GetParcelToListById(int id)//Get parceltolist with manipulation from data source by id
        {
            try
            {
                Predicate<IDAL.DO.Parcel> predicate = element => element.Id == id; // predicat to find parcel based on senders id

                IDAL.DO.Parcel newParcel = dal.GetParcel(0, predicate);
                Customer senderCustomer = GetCustomerById(newParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(newParcel.TargetId); // creat a new customer based on the reciver of the parcel
                ParcelToList newParcelToList = new ParcelToList(newParcel.Id, senderCustomer.Name, reciverCustomer.Name, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, StatusCalculate(dal.GetParcel(GetParcelById(newParcel.Id, predicate).Id)));
                return newParcelToList;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if parcel or customer id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get parceltolist with manipulation from data source by id
        /// <summary>
        /// //Get parceltolist with manipulation from data source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ParcelInTransit GetParcelInTransitById(int id)//Get parceltolist with manipulation from data source by id
        {
            try
            {
                Predicate<IDAL.DO.Parcel> predicate = element => element.Id == id; // predicat to find parcel based on senders id
                IDAL.DO.Parcel newParcel = dal.GetParcel(0, predicate);
                bool status = true;
                if (newParcel.PickedUp != DateTime.MinValue)
                {
                    status = false;
                }
                Customer senderCustomer = GetCustomerById(newParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(newParcel.TargetId); // creat a new customer based on the reciver of the parcel
                CustomerInParcel sCustomer = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name);
                CustomerInParcel rCustomer = new CustomerInParcel(reciverCustomer.Id, reciverCustomer.Name);

                return new ParcelInTransit(id, status, (Enums.Priorities)newParcel.Priority, (Enums.WeightCategories)newParcel.Weight, sCustomer, rCustomer, senderCustomer.Location, reciverCustomer.Location, CalculateDistance(senderCustomer.Location, reciverCustomer.Location));

            }
            catch (IDAL.DO.IdNotExsistException exception) // if customer or parcel id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
    }


    public partial class BL : IBl
    {
        #region//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// <summary>
        /// //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="drone1"></param>
        /// <returns></returns>
        public double CalculateBattery(DroneToList drone = null, Drone drone1 = null)//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        {
            int baseStationId;
            double distance;
            double battery = 0;
            if (drone1 == null)
            {
                Random rnd = new Random();
                //double banan = ListOfDronsBL.Find(element => element.Id == id).Battery);
                baseStationId = CalculateMinDistance(drone.CurrentLocation);
                IDAL.DO.BaseStation newBaseStation = dal.GetBaseStation(baseStationId);
                distance = CalculateDistance(drone.CurrentLocation, new Location(newBaseStation.Longtitude, newBaseStation.Latitude));
                switch (drone.DroneStatuses)
                {
                    case Enums.DroneStatuses.Available:
                        battery = distance * ElectricityUseAvailiblity;
                        battery = battery / 100;
                        battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                        break;
                    case Enums.DroneStatuses.Delivery:
                        switch (drone.Weight)
                        {
                            case Enums.WeightCategories.Light:
                                battery = distance * ElectricityUseLightWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                                break;
                            case Enums.WeightCategories.Medium:
                                battery = distance * ElectricityUseMediumWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                                break;
                            case Enums.WeightCategories.Heavy:
                                battery = distance * ElectricityUseHeavyWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                                break;
                        }
                        break;
                    case Enums.DroneStatuses.Maintenance:
                        battery = (double)rnd.Next(0, 20) / 100;//i think it not lottering good
                        break;
                }
            }
            else
            {
                distance = CalculateDistance(drone1.CurrentLocation, drone1.ParcelInTransit.DeliveryLocation);
                switch (drone.Weight)
                {
                    case Enums.WeightCategories.Light:
                        battery = distance * ElectricityUseLightWeight;
                        break;
                    case Enums.WeightCategories.Medium:
                        battery = distance * ElectricityUseMediumWeight;
                        break;
                    case Enums.WeightCategories.Heavy:
                        battery = distance * ElectricityUseHeavyWeight;
                        break;
                }
            }
            return battery;
        }
        #endregion
        #region//calculate 
        public void CalculateLocation(DroneToList drone)
        {
            switch (drone.DroneStatuses)
            {
                case Enums.DroneStatuses.Available:
                    return;
                case Enums.DroneStatuses.Delivery:

                    double a = CalculateDistance(drone.CurrentLocation, GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location);
                    double b = CalculateDistance(drone.CurrentLocation, GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).SenderId).Location);
                    double c = CalculateDistance(drone.CurrentLocation, GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).TargetId).Location);
                    if (a >= b && a >= c)
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location;
                        return;
                    }
                    if (b >= a && b >= c)
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).SenderId).Location;
                        return;
                    }
                    else
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).TargetId).Location;
                        return;
                    }
                case Enums.DroneStatuses.Maintenance:
                    ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location;
                    return;

            }
        }
        #endregion
        #region//Calculate distance between 2 location return double
        /// <summary>
        /// CalculateDistance
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double CalculateDistance(Location x, Location y)//Calculate distance between 2 location return double
        {
            double ConvertToRadians(double angle)
            {
                return (Math.PI / 180) * angle;
            }
            int R = 6371;

            double f1 = ConvertToRadians(x.Latitude);
            double f2 = ConvertToRadians(y.Latitude);

            double df = ConvertToRadians(x.Latitude - y.Latitude);
            double dl = ConvertToRadians(x.Longitude - y.Longitude);

            double a = Math.Sin(df / 2) * Math.Sin(df / 2) +
            Math.Cos(f1) * Math.Cos(f2) *
            Math.Sin(dl / 2) * Math.Sin(dl / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate the distance.
            double d = R * c;

            return d;
        }
        #endregion
        #region//Convert battery to distance by state and weight 
        /// <summary>
        /// //Convert battery to distance by state and weight 
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        public double ConvertBatteryToDistance(DroneToList drone)//Convert battery to distance by state and weight 
        {
            double battery = drone.Battery;
            double distance = 0;
            switch (drone.DroneStatuses)
            {
                case Enums.DroneStatuses.Available:

                    distance = (battery * 100) / ElectricityUseAvailiblity;
                    break;
                case Enums.DroneStatuses.Delivery:
                    switch (drone.Weight)
                    {
                        case Enums.WeightCategories.Light:
                            distance = (battery * 100) / ElectricityUseLightWeight;
                            break;
                        case Enums.WeightCategories.Medium:
                            distance = (battery * 100) / ElectricityUseMediumWeight;
                            break;
                        case Enums.WeightCategories.Heavy:
                            distance = (battery * 100) / ElectricityUseHeavyWeight;
                            break;
                    }
                    break;
                //case Enums.DroneStatuses.Maintenance:
                  //  distance = (battery * 100) /DroneInCharging;////
                    //break;
            }
            return distance;
        }
        #endregion
        #region//Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        public int CalculateMinDistance(Location y, Predicate<BaseStation> predicate = null, Predicate<BaseStation> predicate1 = null)
        {
            double min = double.MaxValue;
            int baseStationId = 0;
            double distance;
            if (predicate == null && predicate1 == null)
            {

                foreach (var item in dal.GetListOfBaseStation())//check which station is clothest for the sender
                {
                    distance = CalculateDistance(new Location(item.Latitude, item.Latitude), y);
                    if (distance < min)
                    {
                        baseStationId = item.Id;
                        min = distance;
                    }
                }
            }
            else
            {
                foreach (var item in dal.GetListOfBaseStation())//check which station is clothest for the sender
                {
                    distance = CalculateDistance(new Location(item.Latitude, item.Latitude), y);
                    if (distance < min && (predicate!=null) && (predicate1!=null))
                    {
                        baseStationId = item.Id;
                        min = distance;
                    }
                }
            }
            return baseStationId;
        }
        #endregion
        
        public int ReciveParcelId(Parcel parcel)
        {
            Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == parcel.Sender.Id; // predicat to find parcel based on senders id

            IDAL.DO.Parcel newParcel = dal.GetParcel(parcel.Sender.Id, predicate); // get parcel of dal type based on senders id
            return newParcel.Id;

        }
        #region//calculate state of parcel 
        /// <summary>
        /// calculate state of parcel 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Enums.ParcelStatus StatusCalculate(IDAL.DO.Parcel p)
        {
            if (p.Deliverd != DateTime.MinValue)
                return Enums.ParcelStatus.Supplied;
            if (p.PickedUp  != DateTime.MinValue)
                return Enums.ParcelStatus.Collected;
            if (p.Scheduled  != DateTime.MinValue)//AssociationTime
                return Enums.ParcelStatus.Associated;
            return Enums.ParcelStatus.Defined;

        }
        #endregion
    }
    public partial class BL : IBl
    {
        public void UpdateDroneModel(int id, string newModel)
        {
            DroneToList newDroneToList = GetDroneToList(id);
            newDroneToList.Model = newModel;
            int index = ListOfDronsBL.FindIndex(element => element.Id == id);
            ListOfDronsBL[index] = newDroneToList;
            dal.UpdateDroneModel(id, newModel);
        }
        public void UpdateBaseStation(int id, string name = "", int numberOfChargingSlots = -1)
        {
            if (name != "")
            {
                dal.UpdateBaseStationName(id, name);
            }
            if (numberOfChargingSlots != -1)
            {
                dal.UpdateChargingSlotsNumber(id, numberOfChargingSlots);
            }
        }
        public void UpdateCustomer(int id, string name = "", string phone = "")
        {
            if (name != "")
            {
                dal.UpdateCustomerName(id, name);
            }
            if (phone != "")
            {
                dal.UpdateCustomerPhone(id, phone);
            }
        }
        public void UpdateSendDroneToCharge(int id)
        {
            try
            {
                DroneToList newDrone = ListOfDronsBL.Find(element => element.Id == id);
                if (newDrone.DroneStatuses != Enums.DroneStatuses.Available)
                    throw new UnavailableExeption("drone", id);
                int stationId = CalculateMinDistance(GetDroneById(id).CurrentLocation, element => element.NumberOfFreeChargingSlots > 0, element => CalculateDistance(element.Location, newDrone.CurrentLocation) <= ConvertBatteryToDistance(newDrone));
                if (stationId == 0)
                    throw new UnavailableExeption("base station", stationId);
                BaseStation station = GetBaseStationById(stationId);
                newDrone.Battery = CalculateBattery(newDrone);
                newDrone.CurrentLocation = station.Location;
                newDrone.DroneStatuses = Enums.DroneStatuses.Maintenance;
                int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                ListOfDronsBL[index] = newDrone;
                station.NumberOfFreeChargingSlots -= 1;
                dal.UpdateBaseStationNumOfFreeDroneCharges(station.Id, station.NumberOfFreeChargingSlots);
            }
            catch (IDAL.DO.IdNotExsistException exception) // if base station id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        public void UpdateReleseDrone(int id, double time)
        {
            DroneToList drone = GetDroneToList(id);
            if (drone.DroneStatuses != Enums.DroneStatuses.Maintenance)
            {
                throw new UnavailableExeption("drone", id);
            }
            drone.Battery = drone.Battery * time * DroneChargingPaste;
            drone.DroneStatuses = Enums.DroneStatuses.Available;
            IDAL.DO.DroneCharge droneCharge = dal.GetDroneCharge(0,element => element.DroneId == id);
            IDAL.DO.BaseStation station = dal.getBaseStationByDroneId(droneCharge.StationId);//
            station.ChargeSlots -= 1;
            dal.UpdateBaseStationNumOfFreeDroneCharges(station.Id, station.ChargeSlots);
            dal.ReleaseDroneCharge(id, station.Id);
        }
        public void UpdateAssosiateDrone(int id)
        {
            try
            {
                Drone drone = GetDroneById(id);
                if (drone.DroneStatuses != Enums.DroneStatuses.Available)
                {
                    throw new UnavailableExeption("drone", id);
                }
                List<Parcel> list = new List<Parcel>();

                foreach (var item in dal.GetListOfParcel())
                {
                    list.Add(GetParcelById(item.Id, element => element.DroneId > 0));
                }
                Parcel currentParcel;
                foreach (var item in list)
                {
                    if (item.AssociationTime == DateTime.MinValue)
                    {
                        if (item.Weight > drone.Weight)
                        {
                            list.Remove(item);
                        }
                    }
                    else
                    {
                        list.Remove(item);
                    }

                }
                if (list.Count() == 0)
                {
                    throw new UnavailableExeption("drones", 0);
                }
                if (list.Count() == 1)
                {
                    currentParcel = list[0];
                }
                else
                {
                    currentParcel = list[0];
                    for (int i = 1; i < list.Count(); i++)
                    {
                        if (list[i].Prioritie >= currentParcel.Prioritie)
                        {
                            if (list[i].Prioritie > currentParcel.Prioritie)
                            {
                                currentParcel = list[i];
                            }
                            else if (list[i].Weight > currentParcel.Weight)
                            {
                                currentParcel = list[i];
                            }
                        }
                    }
                }
                ListOfDronsBL.Find(element => element.Id == id).DroneStatuses = Enums.DroneStatuses.Delivery;
                dal.AssociateDroneToParcel(id, currentParcel.Id);

            }
            catch (IDAL.DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        public void PickupParcelByDrone(int droneId)
        {
            //DroneToList droneInList = ListOfDronsBL.Find(element => element.Id == droneId);
            Drone drone = GetDroneById(droneId);
            try
            {
                if (drone.DroneStatuses != Enums.DroneStatuses.Available || drone.ParcelInTransit.Status)
                {
                    throw new UnavailableExeption("drone", droneId);
                }
                drone.Battery = drone.Battery * ElectricityUseAvailiblity * CalculateDistance(drone.CurrentLocation, drone.ParcelInTransit.PickupLocation);
                drone.CurrentLocation = drone.ParcelInTransit.PickupLocation;
                DroneToList newDrone = ConvertDroneBlToList(drone);
                int index = ListOfDronsBL.FindIndex(element => element.Id == droneId);
                ListOfDronsBL[index] = newDrone;
                dal.UpdateParclePickup(drone.ParcelInTransit.Id);

            }
            catch (IDAL.DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        public void DilaveryParcelByDrone(int droneId)
        {
            Drone drone = GetDroneById(droneId);
            try
            {
                if (drone.DroneStatuses != Enums.DroneStatuses.Available || !drone.ParcelInTransit.Status)
                {
                    throw new UnavailableExeption("drone", droneId);
                }
                drone.Battery = CalculateBattery(null, drone);
                drone.CurrentLocation = drone.ParcelInTransit.DeliveryLocation;
                drone.DroneStatuses = Enums.DroneStatuses.Available;
                DroneToList newDrone = ConvertDroneBlToList(drone);
                int index = ListOfDronsBL.FindIndex(element => element.Id == droneId);
                ListOfDronsBL[index] = newDrone;
                dal.UpdateParcleDelivery(drone.ParcelInTransit.Id);
            }
            catch (IDAL.DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }

        }
    }
    public partial class BL : IBl
    {
        public DroneToList ConvertDroneDalToList(IDAL.DO.Drone idalDrone)
        {

            Random rnd = new Random();
            DroneToList newDrone = new DroneToList();
            newDrone.Id = idalDrone.Id;
            newDrone.Model = idalDrone.Model;
            newDrone.Weight = (Enums.WeightCategories)idalDrone.MaxWeight;


            var listOfParcels = dal.GetListOfParcel().ToList();
            IDAL.DO.Parcel newParcel = listOfParcels.Find(element => element.DroneId == idalDrone.Id);
            if (newParcel.Id!=0)//(!newParcel.Equals(null))//if there have a parcel with this drone id
            {
                newDrone.DroneStatuses = Enums.DroneStatuses.Delivery;
                if (newParcel.Scheduled <= DateTime.MinValue)//the parcel didn't pick-upp
                {
                    Location newLocation = new Location(dal.GetCustomer(newParcel.SenderId).Longtitude, dal.GetCustomer(newParcel.SenderId).Latitude);
                    int baseStationId = CalculateMinDistance(newLocation);
                    IDAL.DO.BaseStation newBaseStation = dal.GetBaseStation(baseStationId);
                    newDrone.CurrentLocation = new Location(newBaseStation.Longtitude, newBaseStation.Latitude);
                }
                else//the parcel was pick-upp
                {
                    newDrone.CurrentLocation = new Location(dal.GetCustomer(newParcel.SenderId).Longtitude, dal.GetCustomer(newParcel.SenderId).Latitude);//he get the the sender customer address
                }
                newDrone.NumberOfParcelInTransit = newParcel.Id;
            }
            else //if there no have parcel with this drone id
            {
                var myValues = new int[] { 0, 2 }; // Will work with array or list
                newDrone.DroneStatuses = (Enums.DroneStatuses)myValues[rnd.Next(0, 1)];
                if (newDrone.DroneStatuses == Enums.DroneStatuses.Maintenance)//if the drone in maintence status
                {
                    List<IDAL.DO.BaseStation> baseStationsList = dal.GetListOfBaseStation().ToList();
                    int index = rnd.Next(baseStationsList.Count);
                    newDrone.CurrentLocation = new Location(baseStationsList[index].Longtitude, baseStationsList[index].Latitude);
                    newDrone.Battery = CalculateBattery(newDrone);
                }
                else//if the drone is in avilible status
                {
                    Predicate<IDAL.DO.Parcel> predicate1 = element => element.Deliverd > DateTime.MinValue;
                    List<IDAL.DO.Parcel> listOfDeliveredParcel = dal.GetListOfParcel(predicate1).ToList();//all the parcel that delivered
                   // listOfDeliveredParcel = listOfDeliveredParcel.FindAll(element => element.DroneId == newDrone.Id);
                    if (listOfDeliveredParcel.Count != 0)//if there have pacel that has been delivered
                    {
                        IDAL.DO.Parcel newParcel_ = listOfDeliveredParcel[rnd.Next(listOfDeliveredParcel.Count)];
                        IDAL.DO.Customer newCustomer = dal.GetCustomer(newParcel_.TargetId);
                        newDrone.CurrentLocation = new Location(newCustomer.Longtitude, newCustomer.Latitude);
                    }
                    else//if there no have delivered parcel 
                    {
                        List<BaseStation> baseTationList = GetListOfBaseStations();
                        newDrone.CurrentLocation = baseTationList[rnd.Next(0, baseTationList.Count)].Location;
                    }   
                }
                newDrone.NumberOfParcelInTransit = -1;
            }
            newDrone.Battery = CalculateBattery(newDrone);

            return newDrone;
        }
        public DroneToList ConvertDroneBlToList(Drone blDrone)
        {
            DroneToList newDrone = new DroneToList(blDrone.Id, blDrone.Model, blDrone.Weight, blDrone.Battery, blDrone.DroneStatuses, blDrone.CurrentLocation, blDrone.ParcelInTransit.Id);
            return newDrone;
        }
        public Customer convertCustomerDalToBl(IDAL.DO.Customer customer)
        {
            List<IDAL.DO.Parcel> parcelList = dal.GetListOfParcel(element => (element.SenderId != -1)).ToList();
            List<IDAL.DO.Parcel> parcelListEnder = parcelList.FindAll(element => element.SenderId == customer.Id);
            List<IDAL.DO.Parcel> parcelListReciver = parcelList.FindAll(element => element.TargetId == customer.Id);
            if (parcelList.Count != 0)
            {
                List<ParcelAtCustomer> parcelAtCustomersListSender = null;
                foreach (var item in parcelListEnder)
                {
                    parcelAtCustomersListSender.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, StatusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(item.SenderId, GetCustomerById(item.SenderId).Name)));
                }
                List<ParcelAtCustomer> parcelAtCustomersListReciver = null;
                foreach (var item in parcelListReciver)
                {
                    parcelAtCustomersListReciver.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, StatusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(item.TargetId, GetCustomerById(item.TargetId).Name)));
                }
                return new Customer(customer.Id, customer.Name, customer.Phone, new Location(customer.Longtitude, customer.Latitude), parcelAtCustomersListSender, parcelAtCustomersListReciver);
            }
            else return null;//maybe exption
        }
    }
    public partial class BL : IBl
    {
        public List<BaseStation> GetListOfBaseStations()
        {
            List<BaseStation> list = new List<BaseStation>();
            foreach (var item in dal.GetListOfBaseStation())
            {
                list.Add(GetBaseStationById(item.Id));
            }
            return list;
        }
        public List<Drone> GetListOfDrones()
        {
            List<Drone> list = new List<Drone>();
            foreach (var item in dal.GetListOfDrone())
            {
                list.Add(GetDroneById(item.Id));
            }
            return list;
        }
        public List<Customer> GetListOfCustomers()
        {
            List<Customer> list = new List<Customer>();
            foreach (var item in dal.GetListOfCustomer())
            {
                list.Add(GetCustomerById(item.Id));
            }
            return list;
        }
        public List<Parcel> GetListOfParcels()
        {
            List<Parcel> list = new List<Parcel>();
            foreach (var item in dal.GetListOfParcel())
            {
                list.Add(GetParcelById(item.Id));
            }
            return list;
        }
        public List<Parcel> GetListOfNotAssigned()
        {
            List<Parcel> list = new List<Parcel>();
            foreach (var item in dal.GetListOfParcel())
            {
                if (GetParcelById(item.Id, element => element.DroneId > 0) != null)
                {
                    list.Add(GetParcelById(item.Id, element => element.DroneId > 0));
                }
            }
            return list;
        }
        public List<BaseStation> GetListOfFreeChargingStations()
        {
            List<BaseStation> list = new List<BaseStation>();
            foreach (var item in dal.GetListOfBaseStation())
            {
                if (GetBaseStationById(item.Id).NumberOfFreeChargingSlots > 0)
                {
                    list.Add(GetBaseStationById(item.Id));
                }
            }
            return list;
        }
        public List<Customer> GetListOfCustomerDalivered()
        {
            var customerDalList = dal.GetListOfCustomer().ToList();
            List<Customer> customerBlList = null;
            foreach (var item in customerDalList)
            {
                customerBlList.Add(convertCustomerDalToBl(item));
            }
            return customerBlList.FindAll(element => element.ParcelToCustomer.Find(elementi => elementi.ParcelStatus == Enums.ParcelStatus.Supplied).Equals(null));
        }
    }
}


