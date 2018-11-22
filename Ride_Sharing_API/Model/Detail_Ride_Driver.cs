using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Detail_Ride_Driver
    {
        #region Deklarasi Properties

        private Ride_Driver _ID_Driver;
        public Ride_Driver ID_Driver
        {
            get { return _ID_Driver; }
            set { _ID_Driver = value; }
        }

        private string _ID_Kendaraan;
        public string ID_Kendaraan
        {
            get { return _ID_Kendaraan; }
            set { _ID_Kendaraan = value; }
        }


        private string _Nama_Kendaraan;
        public string Nama_Kendaraan
        {
            get { return _Nama_Kendaraan; }
            set { _Nama_Kendaraan = value; }
        }

        private string _No_Kendaraan;
        public string No_Kendaraan
        {
            get { return _No_Kendaraan; }
            set { _No_Kendaraan = value; }
        }

        private int _Kapasitas_Orang;
        public int Kapasitas_Orang
        {
            get { return _Kapasitas_Orang; }
            set { _Kapasitas_Orang = value; }
        }

        private Jenis_Fasilitas _ID_Jenis_Fasilitas;
        public Jenis_Fasilitas ID_Jenis_Fasilitas
        {
            get { return _ID_Jenis_Fasilitas; }
            set { _ID_Jenis_Fasilitas = value; }
        }

        #endregion
    }
}
