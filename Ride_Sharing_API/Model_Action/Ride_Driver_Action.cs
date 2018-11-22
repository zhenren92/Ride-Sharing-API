using Ride_Sharing_API.Modul;
using Ride_Sharing_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model_Action
{
    public class Ride_Driver_Action: Ride_Driver
    {
        #region Method

        public async Task<object> Simpan_Data(Ride_Driver obj)
        {
            object result = null;

            try
            {
                Dictionary<string, object> Daftar_Field = new Dictionary<string, object>();

                Daftar_Field.Add("Nama_Driver", obj.Nama_Driver);
                Daftar_Field.Add("Jenis_Kelamin", obj.Jenis_Kelamin);
                Daftar_Field.Add("No_Handphone", obj.No_Handphone);                
                Daftar_Field.Add("Email", obj.Email);
                Daftar_Field.Add("Password", obj.Password);

                if (obj.ID_Driver == null)
                {
                    result = await new Mdl_Action().Tambah_Data("M_Ride_Driver", Daftar_Field);
                }
                else
                {
                    result = await new Mdl_Action().Ubah_Data("M_Ride_Driver", Daftar_Field, "ID_Driver = '" + obj.ID_Driver + "'");
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
                result = await new Mdl_Action().Hapus_Data("M_Ride_Driver", "ID_Driver = '" + obj + "'");
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<List<Ride_Driver>> Pencarian_Data(string Prm_Query_Where = "")
        {
            DataTable dtb = await new Mdl_Action().Setting_SP_Tampil_Data
                ("M_Ride_Driver",
                "ID_Driver, Nama_Driver, Jenis_Kelamin, No_Handphone, Email, Password",
                Prm_Query_Where,
                "Nama_Driver ASC");

            List<Ride_Driver> Daftar_Data = new List<Ride_Driver>();

            try
            {
                if (dtb.Rows.Count != 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        Daftar_Data.Add(new Ride_Driver
                        {
                            ID_Driver = (row["ID_Driver"].ToString() != string.Empty) ? row["ID_Driver"].ToString() : string.Empty,
                            Nama_Driver = (row["Nama_Driver"].ToString() != string.Empty) ? row["Nama_Driver"].ToString() : string.Empty,
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
