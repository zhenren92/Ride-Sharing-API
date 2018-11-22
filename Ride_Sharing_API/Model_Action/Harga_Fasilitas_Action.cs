using Ride_Sharing_API.Modul;
using Ride_Sharing_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model_Action
{
    public class Harga_Fasilitas_Action : Harga_Fasilitas
    {
        #region Method

        public async Task<object> Simpan_Data(Harga_Fasilitas obj)
        {
            object result = null;

            try
            {
                Dictionary<string, object> Daftar_Field = new Dictionary<string, object>();

                Daftar_Field.Add("Nama_Harga_Fasilitas", obj.Nama_Harga_Fasilitas);
                Daftar_Field.Add("Satuan_Harga", obj.Satuan_Harga);
                Daftar_Field.Add("Harga", obj.Harga);

                if (obj.ID_Harga_Fasilitas == null)
                {
                    result = await new Mdl_Action().Tambah_Data("M_Harga_Fasilitas", Daftar_Field);
                }
                else
                {
                    result = await new Mdl_Action().Ubah_Data("M_Harga_Fasilitas", Daftar_Field, "ID_Harga_Fasilitas = '" + obj.ID_Harga_Fasilitas + "'");
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
                result = await new Mdl_Action().Hapus_Data("M_Harga_Fasilitas", "ID_Harga_Fasilitas = '" + obj + "'");
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<List<Harga_Fasilitas>> Pencarian_Data(string Prm_Query_Where = "")
        {
            DataTable dtb = await new Mdl_Action().Setting_SP_Tampil_Data
                ("M_Harga_Fasilitas",
                "ID_Harga_Fasilitas, Nama_Harga_Fasilitas, Satuan_Harga, Harga",
                Prm_Query_Where,
                "Nama_Harga_Fasilitas ASC");

            List<Harga_Fasilitas> Daftar_Data = new List<Harga_Fasilitas>();

            try
            {
                if (dtb.Rows.Count != 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        Daftar_Data.Add(new Harga_Fasilitas
                        {
                            ID_Harga_Fasilitas = (row["ID_Harga_Fasilitas"].ToString() != string.Empty) ? row["ID_Harga_Fasilitas"].ToString() : string.Empty,
                            Nama_Harga_Fasilitas = (row["Nama_Harga_Fasilitas"].ToString() != string.Empty) ? row["Nama_Harga_Fasilitas"].ToString() : string.Empty ,
                            Satuan_Harga = (row["Satuan_Harga"].ToString() != string.Empty) ? row["Satuan_Harga"].ToString() : string.Empty ,
                            Harga = Convert.ToDecimal((row["Harga"].ToString() != string.Empty) ? row["Harga"].ToString() : "0")
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
