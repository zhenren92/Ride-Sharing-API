using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Ride_User
    {
        #region Deklarasi Enum
        public enum Status_Jenis_Kelamin
        {
            Laki_Laki = 0 ,
            Perempuan = 1
        }

        #endregion

        #region Deklarasi Properties

        private string _ID_User;

        public string ID_User
        {
            get { return _ID_User; }
            set { _ID_User = value; }
        }

        private string _Nama_User;

        public string Nama_User
        {
            get { return _Nama_User; }
            set { _Nama_User = value; }
        }

        private Status_Jenis_Kelamin _Jenis_Kelamin;

        public Status_Jenis_Kelamin Jenis_Kelamin
        {
            get { return _Jenis_Kelamin; }
            set { _Jenis_Kelamin = value; }
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
