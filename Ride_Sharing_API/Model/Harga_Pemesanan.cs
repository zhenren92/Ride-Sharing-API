using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace Ride_Sharing_API.Model
{
    public class Harga_Pemesanan
    {
        #region Deklarasi Properties

        private GeoCoordinate _Lokasi_Jemput;
        public GeoCoordinate Lokasi_Jemput
        {
            get { return _Lokasi_Jemput; }
            set { _Lokasi_Jemput = value; }
        }

        private GeoCoordinate _Lokasi_Tujuan;
        public GeoCoordinate Lokasi_Tujuan
        {
            get { return _Lokasi_Tujuan; }
            set { _Lokasi_Tujuan = value; }
        }

        private decimal _Jarak;
        public decimal Jarak
        {
            get { return _Jarak; }
            set { _Jarak = value; }
        }

        private decimal _Total_Harga;
        public decimal Total_Harga
        {
            get { return _Total_Harga; }
            set { _Total_Harga = value; }
        }

        private TimeSpan _Estimasi_Lama_Perjalanan;
        public TimeSpan Estimasi_Lama_Perjalanan
        {
            get { return _Estimasi_Lama_Perjalanan; }
            set { _Estimasi_Lama_Perjalanan = value; }
        }

        private Jenis_Fasilitas _Jenis_Fasilitas;
        public Jenis_Fasilitas Jenis_Fasilitas
        {
            get { return _Jenis_Fasilitas; }
            set { _Jenis_Fasilitas = value; }
        }


        #endregion
    }
}
