using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class EnumsBL
        {
            /// <summary>
            /// contains enums
            /// </summary>
            public enum WeightCategories // 3 types of weight categories
            { Light, Medium, Heavy }
            public enum Priorities  // 3 types of proirities
            { Regular, Express, Urgent }
            public enum DroneStatuses // 3 types of Drone status
            { Available, Delivery, Maintenance }
            public enum ParcelStatus // 4 types of parcel status
            { Defined, Associated, Collected, Supplied }
            public enum RandomBases // random list of basses names
            { Gilo, Baaka, Pissgat_zev, Katamon, Malha, Givat_shaul, City_center, San_simon, Givat_mordehai, Arnona }
            public enum RandomNames // random list of names
            { Yossi, Moti, Avraham, Kobi, David, Haim, Hana, Shoshi, Malkishua, Yossef, Hagit, Yarden, Erez, Yoni, Gali }
        }
    }
    
    
}
