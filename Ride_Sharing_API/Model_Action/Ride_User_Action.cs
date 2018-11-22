using Ride_Sharing_API.Modul;
using Ride_Sharing_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model_Action
{
    public class Ride_User_Action: Ride_User
    {
        #region Method

        public async Task<object> Simpan_Data(Ride_User obj)
        {
            object result = null;

            try
            {
                Dictionary<string, object> Daftar_Field = new Dictionary<string, object>();

                Daftar_Field.Add("Nama_User", obj.Nama_User);
                Daftar_Field.Add("Jenis_Kelamin", obj.Jenis_Kelamin);
                Daftar_Field.Add("No_Handphone", obj.No_Handphone);
                Daftar_Field.Add("Email", obj.Email);
                Daftar_Field.Add("Password", new Mdl_Ref_Tools().Hash_MD5(obj.Password));

                if (obj.ID_User == null)
                {
                    result = await new Mdl_Action().Tambah_Data("M_Ride_User", Daftar_Field);
                }
                else
                {
                    result = await new Mdl_Action().Ubah_Data("M_Ride_User", Daftar_Field, "ID_User = '" + obj.ID_User + "'");
                }
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<object> Hapus_Data(string obj)
        {
            object result = null;

            try
            {
                result = await new Mdl_Action().Hapus_Data("M_Ride_User", "ID_User = '" + obj + "'");
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<List<Ride_User>> Pencarian_Data(string Prm_Query_Where = "")
        {
            DataTable dtb = await new Mdl_Action().Setting_SP_Tampil_Data
                ("M_Ride_User",
                "ID_User, Nama_User, Jenis_Kelamin, No_Handphone, Email, Password",
                Prm_Query_Where,
                "Nama_User ASC");

            List<Ride_User> Daftar_Data = new List<Ride_User>();

            try
            {
                if (dtb.Rows.Count != 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        Daftar_Data.Add(new Ride_User
                        {
                            ID_User = (row["ID_User"].ToString() != string.Empty) ? row["ID_User"].ToString() : string.Empty,
                            Nama_User = (row["Nama_User"].ToString() != string.Empty) ? row["Nama_User"].ToString() : string.Empty,
                            Jenis_Kelamin = (Status_Jenis_Kelamin) Enum.Parse(typeof(Status_Jenis_Kelamin), (row["Jenis_Kelamin"].ToString() != string.Empty) ? row["Jenis_Kelamin"].ToString() : "0"),
                            No_Handphone = (row["No_Handphone"].ToString() != string.Empty) ? row["No_Handphone"].ToString() : string.Empty,
                            Email = (row["Email"].ToString() != string.Empty) ? row["Email"].ToString() : string.Empty,
                            Password = (row["Password"].ToString() != string.Empty) ? row["Password"].ToString() : string.Empty
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                //Console.WriteLine(ex.Message);
                throw;
            }

            return Daftar_Data;
        }

        #endregion
    }
}
