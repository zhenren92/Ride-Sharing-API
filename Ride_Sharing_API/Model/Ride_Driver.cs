using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Ride_Driver
    {
        #region Deklarasi Enum
        public enum Status_Jenis_Kelamin
        {
            Laki_Laki = 0,
            Perempuan = 1
        }

        public enum Status_Tipe_Driver
        {
            Motor = 0,
            Mobil = 2
        }
        #endregion

        #region Deklarasi Properties

        private string _ID_Driver;
        public string ID_Driver
        {
            get { return _ID_Driver; }
            set { _ID_Driver = value; }
        }

        private string _Nama_Driver;
        public string Nama_Driver
        {
            get { return _Nama_Driver; }
            set { _Nama_Driver = value; }
        }

        private Status_Jenis_Kelamin _Jenis_Kelamin;
        public Status_Jenis_Kelamin Jenis_Kelamin
        {
            get { return _Jenis_Kelamin; }
            set { _Jenis_Kelamin = value; }
        }

        private string _No_Handphone;
        public string No_Handphone
        {
            get { return _No_Handphone; }
            set { _No_Handphone = value; }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        
        #endregion
    }
}
