using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
namespace BlApi
{
    internal sealed partial class BL : IBl
    {
        public List<DroneToList> ListOfDronsBL = new List<DroneToList>();
        double ElectricityUseAvailiblity;
        double ElectricityUseLightWeight;
        double ElectricityUseMediumWeight;
        double ElectricityUseHeavyWeight;
        double DroneChargingPaste;
        IDal dal;
        private static readonly Lazy<BL> instance = new Lazy<BL>(() => new BL());// using lazy to improve performance and avoid wasteful computation, and reduce program memory requirements.  
        static BL() { }
        #region//BL constructor 
        /// <summary>
        /// BL constructor 
        /// </summary>
        public BL()//BL constructor 
        { 
            dal =  DalFactory.GetDal("DalObject");//call to constructor.//dal=DalFactory.GetDal("DalXml");
            double[] arr = dal.Electricity();//retun arrey from config contain electrisity data.
            ElectricityUseAvailiblity = arr[0];
            ElectricityUseLightWeight = arr[1];
            ElectricityUseMediumWeight = arr[2];
            ElectricityUseHeavyWeight = arr[3];
            DroneChargingPaste = arr[4];
            UpdateListOfDronsBL();
        }
        #endregion
        #region
        public static IBl Instance { get => instance.Value; } // The public Instance property to use
        public void UpdateListOfDronsBL()
        {
            var listOfDrones = dal.GetListOfDrone();//get the list of drone from datasource
            foreach (var item in listOfDrones)//insert the list od data source drone to list of drone to list
            {
                ListOfDronsBL.Add(convertDroneDalToList(item));
            }
        }
        #endregion

    }
}


