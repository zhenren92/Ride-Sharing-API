using Ride_Sharing_API.Modul;
using Ride_Sharing_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model_Action
{
    public class Detail_Ride_Driver_Action: Detail_Ride_Driver
    {
        #region Method

        public async Task<object> Simpan_Data(Detail_Ride_Driver obj)
        {
            object result = null;

            try
            {
                Dictionary<string, object> Daftar_Field = new Dictionary<string, object>();

                Daftar_Field.Add("ID_Driver", obj.ID_Driver.ID_Driver);
                Daftar_Field.Add("Nama_Kendaraan", obj.Nama_Kendaraan);
                Daftar_Field.Add("No_Kendaraan", obj.No_Kendaraan);
                Daftar_Field.Add("Kapasitas_Orang", obj.Kapasitas_Orang);
                Daftar_Field.Add("ID_Jenis_Fasilitas", obj.ID_Jenis_Fasilitas.ID_Jenis_Fasilitas);

                if (obj.ID_Kendaraan == null)
                {
                    result = await new Mdl_Action().Tambah_Data("M_D_Ride_Driver", Daftar_Field);
                }
                else
                {
                    result = await new Mdl_Action().Ubah_Data("M_D_Ride_Driver", Daftar_Field, "ID_Kendaraan = '" + obj.ID_Kendaraan + "'");
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
                result = await new Mdl_Action().Hapus_Data("M_D_Ride_Driver", "ID_Kendaraan = '" + obj + "'");
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<List<Detail_Ride_Driver>> Pencarian_Data(string Prm_Query_Where = "")
        {
            DataTable dtb = await new Mdl_Action().Setting_SP_Tampil_Data
                ("M_D_Ride_Driver",
                "ID_Driver, ID_Kendaraan, Nama_Kendaraan, No_Kendaraan, Kapasitas_Orang, ID_Jenis_Fasilitas",
                Prm_Query_Where,
                "ID_Driver, Nama_Kendaraan ASC");

            List<Detail_Ride_Driver> Daftar_Data = new List<Detail_Ride_Driver>();

            try
            {
                if (dtb.Rows.Count != 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        Daftar_Data.Add(new Detail_Ride_Driver
                        {
                            ID_Driver = new Ride_Driver
                            {
                                ID_Driver = (row["ID_Driver"].ToString() != string.Empty) ? row["ID_Driver"].ToString() : string.Empty
                            } ,
                            ID_Kendaraan = (row["ID_Kendaraan"].ToString() != string.Empty) ? row["ID_Kendaraan"].ToString() : string.Empty,
                            Nama_Kendaraan = (row["Nama_Kendaraan"].ToString() != string.Empty) ? row["Nama_Kendaraan"].ToString() : string.Empty,
                            No_Kendaraan = (row["No_Kendaraan"].ToString() != string.Empty) ? row["No_Kendaraan"].ToString() : string.Empty,
                            Kapasitas_Orang = Convert.ToInt32((row["Kapasitas_Orang"].ToString() != string.Empty) ? row["Kapasitas_Orang"].ToString() : "0") ,
                            ID_Jenis_Fasilitas = new Jenis_Fasilitas
                            {
                                ID_Jenis_Fasilitas = (row["ID_Jenis_Fasilitas"].ToString() != string.Empty) ? row["ID_Jenis_Fasilitas"].ToString() : string.Empty
                            }
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
