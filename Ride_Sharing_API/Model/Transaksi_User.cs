using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Transaksi_User
    {

        #region Deklarasi Enum

        #endregion

        #region Deklarasi Properties

        private string _ID_Transaksi;

        public string ID_Transaksi
        {
            get { return _ID_Transaksi; }
            set { _ID_Transaksi = value; }
        }

        private Ride_User _ID_User;

        public Ride_User ID_User
        {
            get { return _ID_User; }
            set { _ID_User = value; }
        }


        private Ride_Driver _ID_Driver;

        public Ride_Driver ID_Driver
        {
            get { return _ID_Driver; }
            set { _ID_Driver = value; }
        }

        private string _Lokasi_Jemput_Latitude;

        public string Lokasi_Jemput_Latitude
        {
            get { return _Lokasi_Jemput_Latitude; }
            set { _Lokasi_Jemput_Latitude = value; }
        }

        private string _Lokasi_Jemput_Longitude;

        public string Lokasi_Jemput_Longitude
        {
            get { return _Lokasi_Jemput_Longitude; }
            set { _Lokasi_Jemput_Longitude = value; }
        }

        private string _Lokasi_Tujuan_Latitude;

        public string Lokasi_Tujuan_Latitude
        {
            get { return _Lokasi_Tujuan_Latitude; }
            set { _Lokasi_Tujuan_Latitude = value; }
        }

        private string _Lokasi_Tujuan_Longtitute;

        public string Lokasi_Tujuan_Longtitute
        {
            get { return _Lokasi_Tujuan_Longtitute; }
            set { _Lokasi_Tujuan_Longtitute = value; }
        }

        private string _Nama_Lokasi_Jemput;

        public string Nama_Lokasi_Jemput
        {
            get { return _Nama_Lokasi_Jemput; }
            set { _Nama_Lokasi_Jemput = value; }
        }

        private string _Nama_Lokasi_Tujuan;

        public string Nama_Lokasi_Tujuan
        {
            get { return _Nama_Lokasi_Tujuan; }
            set { _Nama_Lokasi_Tujuan = value; }
        }

        private decimal _Jarak;

        public decimal Jarak
        {
            get { return _Jarak; }
            set { _Jarak = value; }
        }

        private TimeSpan _Estimasi_Lama_Perjalanan;

        public TimeSpan Estimasi_Lama_Perjalanan
        {
            get { return _Estimasi_Lama_Perjalanan; }
            set { _Estimasi_Lama_Perjalanan = value; }
        }

        private decimal _Harga;

        public decimal Harga
        {
            get { return _Harga; }
            set { _Harga = value; }
        }

        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private DateTime _Waktu_Request;

        public DateTime Waktu_Request
        {
            get { return _Waktu_Request; }
            set { _Waktu_Request = value; }
        }

        private DateTime _Waktu_Start;

        public DateTime Waktu_Start
        {
            get { return _Waktu_Start; }
            set { _Waktu_Start = value; }
        }

        private DateTime _Waktu_Selesai;

        public DateTime Waktu_Selesai
        {
            get { return _Waktu_Selesai; }
            set { _Waktu_Selesai = value; }
        }

        private TimeSpan _Lama_Perjalanan;

        public TimeSpan Lama_Perjalanan
        {
            get { return _Lama_Perjalanan; }
            set { _Lama_Perjalanan = value; }
        }
        
        #endregion

    }
}
