using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Harga_Fasilitas
    {
        #region Deklarasi Properties

        private string _ID_Harga_Fasilitas;

        public string ID_Harga_Fasilitas
        {
            get { return _ID_Harga_Fasilitas; }
            set { _ID_Harga_Fasilitas = value; }
        }

        private string _Nama_Harga_Fasilitas;

        public string Nama_Harga_Fasilitas
        {
            get { return _Nama_Harga_Fasilitas; }
            set { _Nama_Harga_Fasilitas = value; }
        }

        private string _Satuan_Harga;

        public string Satuan_Harga
        {
            get { return _Satuan_Harga; }
            set { _Satuan_Harga = value; }
        }

        private decimal _Harga;

        public decimal Harga
        {
            get { return _Harga; }
            set { _Harga = value; }
        }



        #endregion
    }
}
