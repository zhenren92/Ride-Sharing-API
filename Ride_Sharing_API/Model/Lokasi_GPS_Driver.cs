using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Lokasi_GPS_Driver
    {
        #region Deklarasi Enum

        public enum Jenis_Status_Driver
        {
            Tidak_Aktif = 0 ,
            Aktif = 1 ,
            Proces = 2
        }

        #endregion

        #region Deklarasi Properties

        private Ride_Driver _ID_Driver;
        public Ride_Driver ID_Driver
        {
            get { return _ID_Driver; }
            set { _ID_Driver = value; }
        }

        private Jenis_Status_Driver _Status_Driver ;
        public Jenis_Status_Driver Status_Driver
        {
            get { return _Status_Driver ; }
            set { _Status_Driver  = value; }
        }

        private GeoCoordinate _Lokasi_Driver_Lan_Long;
        public GeoCoordinate Lokasi_Driver_Lan_Long
        {
            get { return _Lokasi_Driver_Lan_Long; }
            set { _Lokasi_Driver_Lan_Long = value; }
        }

        private decimal _Jarak_Dengan_Lokasi_Jemput;
        public decimal Jarak_Dengan_Lokasi_Jemput
        {
            get { return _Jarak_Dengan_Lokasi_Jemput; }
            set { _Jarak_Dengan_Lokasi_Jemput = value; }
        }

        #endregion
    }
}
