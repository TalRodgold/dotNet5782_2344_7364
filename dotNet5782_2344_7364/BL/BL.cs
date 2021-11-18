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
        public BL()
        {
            dal = new DalObjects.DalObjects();
            double[] arr = dal.Electricity();
            ElectricityUseAvailiblity = arr[0];
            ElectricityUseLightWeight = arr[1];
            ElectricityUseMediumWeight = arr[2];
            ElectricityUseHeavyWeight = arr[3];
            DroneChargingPaste = arr[4];
            var listOfDrones = dal.GetListOfDrone();
            foreach (var item in listOfDrones)
            {
                ListOfDronsBL.Add(ConvertDroneToList(item));
            }
        }

        public void AddBaseStation(BaseStation b)
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
        public void AddDrone(Drone d, int startingBaseStation)
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
        public void AddCustomer(Customer c)
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
        public void AddParcel(CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie)
        {
            int id = dal.ConstructParcel(sender.Id, reciver.Id, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)prioritie, DateTime.Now, -1, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            Parcel newParcel = new Parcel(id, sender, reciver, weight, prioritie, null, DateTime.Now, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }
    }

    public partial class BL : IBl
    {

        public Customer GetCustomerById(int id)
        {
            try
            {
                IDAL.DO.Customer idalCustomer = dal.GetCustomer(id); // get a customer of dal type
                Customer newCustomer = new Customer(idalCustomer.Id, idalCustomer.Name, idalCustomer.Phone, new Location(idalCustomer.Longtitude, idalCustomer.Latitude)); // creat and add to a customer of bl type


                Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == id; // predicat to find parcel based on senders id
                Predicate<IDAL.DO.Parcel> predicate1 = element => element.TargetId == id; // predicat to find parcel based on targets id

                IDAL.DO.Parcel newParcel = dal.GetParcel(id, predicate); // get parcel of dal type based on senders id

                CustomerInParcel newCustomerInParcel = new CustomerInParcel(idalCustomer.Id, idalCustomer.Name); // creat a customer in parcel based on current customer
                ParcelAtCustomer newparcelAtCustomer = new ParcelAtCustomer(newParcel.Id, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, newCustomerInParcel);

                newParcel = dal.GetParcel(id, predicate1); // get parcel of dal type based on target id

                ParcelAtCustomer newparcelAtCustomer1 = new ParcelAtCustomer(newParcel.Id, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, newCustomerInParcel);

                newCustomer.ParcelFromCustomer.Add(newparcelAtCustomer); // add to new customer the parcel from customer
                newCustomer.ParcelToCustomer.Add(newparcelAtCustomer1); // add to new customer the parcel to customer

                return newCustomer;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if customer id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        public Parcel GetParcelById(int id, Predicate<IDAL.DO.Parcel> predicate = null)
        {
            try
            {
                IDAL.DO.Parcel idalParcel = dal.GetParcel(id, predicate);
                Customer senderCustomer = GetCustomerById(idalParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(idalParcel.TargetId); // creat a new customer based on the reciver of the parcel

                CustomerInParcel senderCustomerInParcel = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name); // creat a customer in parcel based on current customer
                CustomerInParcel reciverCustomerInParcel = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name); // creat a customer in parcel based on current customer

                Drone newDrone = GetDroneById(idalParcel.DroneId);
                DroneInParcel newDroneInParcel = new DroneInParcel(newDrone.Id, newDrone.Battery, newDrone.CurrentLocation);


                Parcel newParcel = new Parcel(idalParcel.Id, senderCustomerInParcel, reciverCustomerInParcel, (Enums.WeightCategories)idalParcel.Weight, (Enums.Priorities)idalParcel.Priority, newDroneInParcel, idalParcel.Requsted, idalParcel.Scheduled, idalParcel.PickedUp, idalParcel.Deliverd);
                return newParcel;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if parcel id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        public Drone GetDroneById(int id)
        {
            try
            {
                Predicate<IDAL.DO.Parcel> predicate = element => element.DroneId == id;
                Parcel newParcel = GetParcelById(0, predicate);
                ParcelInTransit newParcelInTransit = GetParcelInTransitById(GetDroneToList(id).NumberOfParcelInTransit);
                Drone newDrone = new Drone(GetDroneToList(id).Id, GetDroneToList(id).Model, GetDroneToList(id).Weight, GetDroneToList(id).Battery, GetDroneToList(id).DroneStatuses, newParcelInTransit, GetDroneToList(id).CurrentLocation);
                return newDrone;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if drone id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        public DroneToList GetDroneToList(int id)
        {
            return ListOfDronsBL.Find(element => element.Id == id);
        }
        public BaseStation GetBaseStationById(int id)
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
        public ParcelToList GetParcelToListById(int id)
        {
            try
            {
                Predicate<IDAL.DO.Parcel> predicate = element => element.Id == id; // predicat to find parcel based on senders id

                IDAL.DO.Parcel newParcel = dal.GetParcel(0, predicate);
                Customer senderCustomer = GetCustomerById(newParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(newParcel.TargetId); // creat a new customer based on the reciver of the parcel
                ParcelToList newParcelToList = new ParcelToList(newParcel.Id, senderCustomer.Name, reciverCustomer.Name, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, StatusCalculate(GetParcelById(newParcel.Id, predicate)));
                return newParcelToList;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if parcel or customer id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        public ParcelInTransit GetParcelInTransitById(int id)
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
    }


    public partial class BL : IBl
    {
        public double CalculateBattery(DroneToList drone = null, Drone drone1 = null)
        {
            int baseStationId;
            double distance;
            double battery = 0;
            if (drone1.Equals(null))
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

        public double CalculateDistance(Location x, Location y)
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
        public double ConvertBatteryToDistance(DroneToList drone)
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
                case Enums.DroneStatuses.Maintenance:
                    distance = (battery * 100);
                    break;
            }
            return distance;
        }
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
                    if (distance < min && predicate.Equals(true) && predicate1.Equals(true))
                    {
                        baseStationId = item.Id;
                        min = distance;
                    }
                }
            }
            return baseStationId;
        }
        public int ReciveParcelId(Parcel parcel)
        {
            Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == parcel.Sender.Id; // predicat to find parcel based on senders id

            IDAL.DO.Parcel newParcel = dal.GetParcel(parcel.Sender.Id, predicate); // get parcel of dal type based on senders id
            return newParcel.Id;

        }
        public Enums.ParcelStatus StatusCalculate(Parcel p)
        {
            if (p.DeliveryTime != DateTime.MinValue)
                return Enums.ParcelStatus.Supplied;
            if (p.PickupTime != DateTime.MinValue)
                return Enums.ParcelStatus.Collected;
            if (p.AssociationTime != DateTime.MinValue)
                return Enums.ParcelStatus.Associated;
            return Enums.ParcelStatus.Defined;

        }
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
                if (newDrone.DroneStatuses == Enums.DroneStatuses.Available)
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
            IDAL.DO.BaseStation station = dal.getBaseStationByDroneId(id);
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
            if (!newParcel.Equals(null))//if there have a parcel with this drone id
            {
                newDrone.DroneStatuses = Enums.DroneStatuses.Delivery;
                if (newParcel.Scheduled < DateTime.MinValue)//the parcel didn't pick-upp
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
                    List<IDAL.DO.Parcel> listOfDeliveredParcel = dal.GetListOfParcel(predicate1).ToList();
                    listOfDeliveredParcel = listOfDeliveredParcel.FindAll(element => element.DroneId == newDrone.Id);
                    IDAL.DO.Parcel newParcel_ = listOfDeliveredParcel[rnd.Next(listOfDeliveredParcel.Count)];
                    IDAL.DO.Customer newCustomer = dal.GetCustomer(newParcel_.TargetId);
                    newDrone.CurrentLocation = new Location(newCustomer.Longtitude, newCustomer.Latitude);
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
                list.Add(GetParcelById(item.Id, element => element.DroneId > 0));
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
    }
}


