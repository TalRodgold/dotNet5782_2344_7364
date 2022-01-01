using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;

namespace DalXml
{


    internal sealed class DalXml//:
    {
        private static readonly Lazy<DalXml> instance = new Lazy<DalXml>(() => new DalXml());// using lazy to improve performance and avoid wasteful computation, and reduce program memory requirements.  
        static DalXml() { }// Explicit static constructor to ensure instance initialization
                           // is done just before first usage
        DalXml() // constructor.
        {
            //DataSource.DroneList = new List<Drone>();/
            //DataSource.BaseStationList = new List<BaseStation>();
            //DataSource.CustomerList = new List<Customer>();
            //DataSource.ParcelList = new List<Parcel>();
            //DataSource.DroneChargeList = new List<DroneCharge>();
            //DataSource.Initialize();
        }
        //public static IDal Instance { get => instance.Value; } // The public Instance property to use
    }
}

