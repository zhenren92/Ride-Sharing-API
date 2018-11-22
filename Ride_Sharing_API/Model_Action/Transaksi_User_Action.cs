using Ride_Sharing_API.Modul;
using Ride_Sharing_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model_Action
{
    public class Transaksi_User_Action: Transaksi_User
    {
        #region Method

        public async Task<object> Simpan_Data(Transaksi_User obj)
        {
            object result = null;

            try
            {
                Dictionary<string, object> Daftar_Field = new Dictionary<string, object>();

                Daftar_Field.Add("ID_User", obj.ID_Driver.ID_Driver);
                Daftar_Field.Add("ID_Driver", obj.ID_Driver.ID_Driver);
                Daftar_Field.Add("ID_Jenis_Fasilitas", obj.ID_Jenis_Fasilitas.ID_Jenis_Fasilitas);
                Daftar_Field.Add("Lokasi_Jemput_Latitude", obj.Lokasi_Jemput_Latitude);
                Daftar_Field.Add("Lokasi_Jemput_Longitude", obj.Lokasi_Jemput_Longitude);
                Daftar_Field.Add("Lokasi_Tujuan_Latitude", obj.Lokasi_Tujuan_Latitude);
                Daftar_Field.Add("Lokasi_Tujuan_Longtitute", obj.Lokasi_Tujuan_Longtitute);
                Daftar_Field.Add("Nama_Lokasi_Jemput", obj.Nama_Lokasi_Jemput);
                Daftar_Field.Add("Nama_Lokasi_Tujuan", obj.Nama_Lokasi_Tujuan);
                Daftar_Field.Add("Jarak", obj.Jarak);
                Daftar_Field.Add("Estimasi_Lama_Perjalanan", obj.Estimasi_Lama_Perjalanan);
                Daftar_Field.Add("Harga", obj.Harga);
                Daftar_Field.Add("Status", obj.Status);

                if (obj.ID_Transaksi == null)
                {
                    result = await new Mdl_Action().Tambah_Data("M_Ride_Transaksi_User", Daftar_Field);
                }
                else
                {
                    result = await new Mdl_Action().Ubah_Data("M_Ride_Transaksi_User", Daftar_Field, "ID_Transaksi = '" + obj.ID_Transaksi + "'");
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
                result = await new Mdl_Action().Hapus_Data("M_Ride_Transaksi_User", "ID_Transaksi = '" + obj + "'");
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<List<Transaksi_User>> Pencarian_Data(string Prm_Query_Where = "")
        {
            DataTable dtb = await new Mdl_Action().Setting_SP_Tampil_Data
                ("M_Ride_Transaksi_User",
                "ID_Transaksi, ID_User, ID_Driver, ID_Jenis_Fasilitas, Lokasi_Jemput_Latitude, Lokasi_Jemput_Longitude, Lokasi_Tujuan_Latitude, Lokasi_Tujuan_Longtitute, " +
                "Nama_Lokasi_Jemput, Nama_Lokasi_Tujuan, Jarak, Estimasi_Lama_Perjalanan, Harga, Status, Waktu_Request, Waktu_Start, Waktu_Selesai, Lama_Perjalanan",
                Prm_Query_Where,
                "ID_User ASC , Waktu_Request DESC");

            List<Transaksi_User> Daftar_Data = new List<Transaksi_User>();

            try
            {
                if (dtb.Rows.Count != 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        Daftar_Data.Add(new Transaksi_User
                        {
                            ID_Transaksi = (row["ID_Transaksi"].ToString() != string.Empty) ? row["ID_Transaksi"].ToString() : string.Empty,
                            ID_User = new Ride_User
                            {
                                ID_User = (row["ID_User"].ToString() != string.Empty) ? row["ID_User"].ToString() : string.Empty
                            },
                            ID_Driver = new Ride_Driver
                            {
                                ID_Driver = (row["ID_Driver"].ToString() != string.Empty) ? row["ID_Driver"].ToString() : string.Empty
                            } ,
                            ID_Jenis_Fasilitas = new Jenis_Fasilitas
                            {
                                ID_Jenis_Fasilitas = (row["ID_Jenis_Fasilitas"].ToString() != string.Empty) ? row["ID_Jenis_Fasilitas"].ToString() : string.Empty,
                            } ,
                            Lokasi_Jemput_Latitude = (row["Lokasi_Jemput_Latitude"].ToString() != string.Empty) ? row["Lokasi_Jemput_Latitude"].ToString() : string.Empty,
                            Lokasi_Jemput_Longitude = (row["Lokasi_Jemput_Longitude"].ToString() != string.Empty) ? row["Lokasi_Jemput_Longitude"].ToString() : string.Empty,
                            Lokasi_Tujuan_Latitude = (row["Lokasi_Tujuan_Latitude"].ToString() != string.Empty) ? row["Lokasi_Tujuan_Latitude"].ToString() : string.Empty,
                            Lokasi_Tujuan_Longtitute = (row["Lokasi_Tujuan_Longtitute"].ToString() != string.Empty) ? row["Lokasi_Tujuan_Longtitute"].ToString() : string.Empty,
                            Nama_Lokasi_Jemput = (row["Nama_Lokasi_Jemput"].ToString() != string.Empty) ? row["Nama_Lokasi_Jemput"].ToString() : string.Empty,
                            Nama_Lokasi_Tujuan = (row["Nama_Lokasi_Tujuan"].ToString() != string.Empty) ? row["Nama_Lokasi_Tujuan"].ToString() : string.Empty,
                            Jarak = Convert.ToDecimal( (row["Jarak"].ToString() != string.Empty) ? row["Jarak"].ToString() : "0"),
                            Estimasi_Lama_Perjalanan = TimeSpan.Parse((row["Estimasi_Lama_Perjalanan"].ToString() != string.Empty) ? row["Estimasi_Lama_Perjalanan"].ToString() : TimeSpan.MinValue.ToString()),
                            Harga = Convert.ToDecimal((row["Harga"].ToString() != string.Empty) ? row["Harga"].ToString() : "0"),
                            Status = (Status_Transaksi) Enum.Parse(typeof(Status_Transaksi) ,(row["Status"].ToString() != string.Empty) ? row["Status"].ToString() : "0"),
                            Waktu_Request = DateTime.Parse((row["Waktu_Request"].ToString() != string.Empty) ? row["Waktu_Request"].ToString() : DateTime.MinValue.ToString()),
                            Waktu_Start = DateTime.Parse((row["Waktu_Start"].ToString() != string.Empty) ? row["Waktu_Start"].ToString() : DateTime.MinValue.ToString()),
                            Waktu_Selesai = DateTime.Parse((row["Waktu_Selesai"].ToString() != string.Empty) ? row["Waktu_Selesai"].ToString() : DateTime.MinValue.ToString()),
                            Lama_Perjalanan = TimeSpan.Parse((row["Lama_Perjalanan"].ToString() != string.Empty) ? row["Lama_Perjalanan"].ToString() : TimeSpan.MinValue.ToString())
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
