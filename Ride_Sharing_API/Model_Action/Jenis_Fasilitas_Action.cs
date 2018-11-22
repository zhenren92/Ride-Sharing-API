using Ride_Sharing_API.Modul;
using Ride_Sharing_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model_Action
{
    public class Jenis_Fasilitas_Action: Jenis_Fasilitas
    {
        #region Method

        public async Task<object> Simpan_Data(Jenis_Fasilitas obj)
        {
            object result = null;

            try
            {
                Dictionary<string, object> Daftar_Field = new Dictionary<string, object>();

                Daftar_Field.Add("Nama_Jenis_Fasilitas", obj.Nama_Jenis_Fasilitas);
                Daftar_Field.Add("ID_Harga_Fasilitas", obj.ID_Harga_Fasilitas.ID_Harga_Fasilitas);

                if (obj.ID_Jenis_Fasilitas == null)
                {
                    result = await new Mdl_Action().Tambah_Data("M_Jenis_Fasilitas", Daftar_Field);
                }
                else
                {
                    result = await new Mdl_Action().Ubah_Data("M_Jenis_Fasilitas", Daftar_Field, "ID_Jenis_Fasilitas = '" + obj.ID_Jenis_Fasilitas + "'");
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
                result = await new Mdl_Action().Hapus_Data("M_Jenis_Fasilitas", "ID_Jenis_Fasilitas = '" + obj + "'");
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<List<Jenis_Fasilitas>> Pencarian_Data(string Prm_Query_Where = "")
        {
            DataTable dtb = await new Mdl_Action().Setting_SP_Tampil_Data
                ("M_Jenis_Fasilitas",
                "ID_Jenis_Fasilitas, Nama_Jenis_Fasilitas, ID_Harga_Fasilitas",
                Prm_Query_Where,
                "Nama_Jenis_Fasilitas ASC");

            List<Jenis_Fasilitas> Daftar_Data = new List<Jenis_Fasilitas>();

            try
            {
                if (dtb.Rows.Count != 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        Daftar_Data.Add(new Jenis_Fasilitas
                        {
                            ID_Jenis_Fasilitas = (row["ID_Jenis_Fasilitas"].ToString() != string.Empty) ? row["ID_Jenis_Fasilitas"].ToString() : string.Empty,
                            Nama_Jenis_Fasilitas = (row["Nama_Jenis_Fasilitas"].ToString() != string.Empty) ? row["Nama_Jenis_Fasilitas"].ToString() : string.Empty ,
                            ID_Harga_Fasilitas = new Harga_Fasilitas
                            {
                                ID_Harga_Fasilitas = (row["ID_Harga_Fasilitas"].ToString() != string.Empty) ? row["ID_Harga_Fasilitas"].ToString() : string.Empty
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
