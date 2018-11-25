using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Pesan_Driver_Transaksi
    {
        #region Deklarasi Properties

        private Ride_User _Ride_User;
        public Ride_User ID_User
        {
            get { return _Ride_User; }
            set { _Ride_User = value; }
        }


        private Harga_Pemesanan _Harga_Pemesanan;
        public Harga_Pemesanan Harga_Pemesanan
        {
            get { return _Harga_Pemesanan; }
            set { _Harga_Pemesanan = value; }
        }

        #endregion
    }
}
