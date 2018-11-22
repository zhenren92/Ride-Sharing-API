using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Jenis_Fasilitas
    {
        #region Deklarasi Properties

        private string _ID_Jenis_Fasilitas;

        public string ID_Jenis_Fasilitas
        {
            get { return _ID_Jenis_Fasilitas; }
            set { _ID_Jenis_Fasilitas = value; }
        }

        private string _Nama_Jenis_Fasilitas;

        public string Nama_Jenis_Fasilitas
        {
            get { return _Nama_Jenis_Fasilitas; }
            set { _Nama_Jenis_Fasilitas = value; }
        }

        private Harga_Fasilitas _ID_Harga_Fasilitas;

        public Harga_Fasilitas ID_Harga_Fasilitas
        {
            get { return _ID_Harga_Fasilitas; }
            set { _ID_Harga_Fasilitas = value; }
        }



        #endregion

    }
}
