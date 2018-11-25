using Ride_Sharing_API.Modul;
using Ride_Sharing_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using static Ride_Sharing_API.Modul.Mdl_Ref_Tools;

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

                Daftar_Field.Add("ID_User", obj.ID_User.ID_User);
                Daftar_Field.Add("ID_Driver", obj.ID_Driver.ID_Driver);
                Daftar_Field.Add("ID_Jenis_Fasilitas", obj.Harga_Pemesanan.Jenis_Fasilitas.ID_Jenis_Fasilitas);
                Daftar_Field.Add("Lokasi_Jemput_Latitude", obj.Harga_Pemesanan.Lokasi_Jemput.Latitude.ToString());
                Daftar_Field.Add("Lokasi_Jemput_Longitude", obj.Harga_Pemesanan.Lokasi_Jemput.Longitude.ToString());
                Daftar_Field.Add("Lokasi_Tujuan_Latitude", obj.Harga_Pemesanan.Lokasi_Tujuan.Latitude.ToString());
                Daftar_Field.Add("Lokasi_Tujuan_Longtitute", obj.Harga_Pemesanan.Lokasi_Tujuan.Longitude.ToString());
                Daftar_Field.Add("Nama_Lokasi_Jemput", obj.Nama_Lokasi_Jemput);
                Daftar_Field.Add("Nama_Lokasi_Tujuan", obj.Nama_Lokasi_Tujuan);
                Daftar_Field.Add("Jarak", obj.Harga_Pemesanan.Jarak);
                Daftar_Field.Add("Estimasi_Lama_Perjalanan", obj.Harga_Pemesanan.Estimasi_Lama_Perjalanan);
                Daftar_Field.Add("Total_Harga", obj.Harga_Pemesanan.Total_Harga);
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

        public async Task<object> Batal_Pesan_Driver(string ID_Transaksi)
        {
            object result = null;

            try
            {
                Dictionary<string, object> Daftar_Field = new Dictionary<string, object>();

                Daftar_Field.Add("Status", Status_Transaksi.Dibatalkan);

                result = await new Mdl_Action().Ubah_Data("M_Ride_Transaksi_User", Daftar_Field, "(ID_Transaksi = '" + ID_Transaksi + "') AND " +
                                                                                                 "(Status = '2')");
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
                ("V_Ride_Transaksi_User",
                "ID_Transaksi, ID_User, Nama_User, Jenis_Kelamin_User, No_Handphone_User, Email_User, " +
                "ID_Driver, Nama_Driver, Jenis_Kelamin_Driver, No_Handphone_Driver, Email_Driver, " +
                "ID_Jenis_Fasilitas, Nama_Jenis_Fasilitas, ID_Harga_Fasilitas, Nama_Harga_Fasilitas, Satuan_Harga, Harga, " +
                "Lokasi_Jemput_Latitude, Lokasi_Jemput_Longitude, Lokasi_Tujuan_Latitude, Lokasi_Tujuan_Longtitute, " +
                "Nama_Lokasi_Jemput, Nama_Lokasi_Tujuan, Jarak, Estimasi_Lama_Perjalanan, Total_Harga, Status, Waktu_Request, Waktu_Start, Waktu_Selesai, Lama_Perjalanan",
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
                                ID_User = (row["ID_User"].ToString() != string.Empty) ? row["ID_User"].ToString() : string.Empty,
                                Nama_User = (row["Nama_User"].ToString() != string.Empty) ? row["Nama_User"].ToString() : string.Empty,
                                Jenis_Kelamin = (Ride_User.Status_Jenis_Kelamin)  Enum.Parse(typeof(Ride_User.Status_Jenis_Kelamin), (row["Jenis_Kelamin_User"].ToString() != string.Empty) ? row["Jenis_Kelamin_User"].ToString() : "0"),
                                No_Handphone = (row["No_Handphone_User"].ToString() != string.Empty) ? row["No_Handphone_User"].ToString() : string.Empty,
                                Email = (row["Email_User"].ToString() != string.Empty) ? row["Email_User"].ToString() : string.Empty,
                            },
                            ID_Driver = new Ride_Driver
                            {
                                ID_Driver = (row["ID_Driver"].ToString() != string.Empty) ? row["ID_Driver"].ToString() : string.Empty ,
                                Nama_Driver = (row["Nama_Driver"].ToString() != string.Empty) ? row["Nama_Driver"].ToString() : string.Empty,
                                Jenis_Kelamin = (Ride_Driver.Status_Jenis_Kelamin) Enum.Parse(typeof(Ride_Driver.Status_Jenis_Kelamin), (row["Jenis_Kelamin_Driver"].ToString() != string.Empty) ? row["Jenis_Kelamin_Driver"].ToString() : "0"),
                                No_Handphone = (row["No_Handphone_Driver"].ToString() != string.Empty) ? row["No_Handphone_Driver"].ToString() : string.Empty,
                                Email = (row["Email_Driver"].ToString() != string.Empty) ? row["Email_Driver"].ToString() : string.Empty,
                            },
                            ID_Jenis_Fasilitas = new Jenis_Fasilitas
                            {
                                ID_Jenis_Fasilitas = (row["ID_Jenis_Fasilitas"].ToString() != string.Empty) ? row["ID_Jenis_Fasilitas"].ToString() : string.Empty,
                                Nama_Jenis_Fasilitas = (row["Nama_Jenis_Fasilitas"].ToString() != string.Empty) ? row["Nama_Jenis_Fasilitas"].ToString() : string.Empty,
                                ID_Harga_Fasilitas = new Harga_Fasilitas
                                {
                                    ID_Harga_Fasilitas = (row["ID_Harga_Fasilitas"].ToString() != string.Empty) ? row["ID_Harga_Fasilitas"].ToString() : string.Empty,
                                    Nama_Harga_Fasilitas = (row["Nama_Harga_Fasilitas"].ToString() != string.Empty) ? row["Nama_Harga_Fasilitas"].ToString() : string.Empty,
                                    Satuan_Harga = (row["Satuan_Harga"].ToString() != string.Empty) ? row["Satuan_Harga"].ToString() : string.Empty,
                                    Harga = Convert.ToDecimal((row["Harga"].ToString() != string.Empty) ? row["Harga"].ToString() : "0")
                                }
                            },
                            Harga_Pemesanan = new Harga_Pemesanan
                            {
                                Lokasi_Jemput = new GeoCoordinate
                                {
                                    Latitude = Convert.ToDouble((row["Lokasi_Jemput_Latitude"].ToString() != string.Empty) ? row["Lokasi_Jemput_Latitude"].ToString() : "0"),
                                    Longitude = Convert.ToDouble((row["Lokasi_Jemput_Longitude"].ToString() != string.Empty) ? row["Lokasi_Jemput_Longitude"].ToString() : "0")
                                },
                                Lokasi_Tujuan = new GeoCoordinate
                                {
                                    Latitude = Convert.ToDouble((row["Lokasi_Tujuan_Latitude"].ToString() != string.Empty) ? row["Lokasi_Tujuan_Latitude"].ToString() : "0"),
                                    Longitude = Convert.ToDouble((row["Lokasi_Tujuan_Longtitute"].ToString() != string.Empty) ? row["Lokasi_Tujuan_Longtitute"].ToString() : "0")
                                },
                                Jarak = Convert.ToDecimal((row["Jarak"].ToString() != string.Empty) ? row["Jarak"].ToString() : "0"),
                                Total_Harga = Convert.ToDecimal((row["Total_Harga"].ToString() != string.Empty) ? row["Total_Harga"].ToString() : "0"),
                                Estimasi_Lama_Perjalanan = TimeSpan.Parse((row["Estimasi_Lama_Perjalanan"].ToString() != string.Empty) ? row["Estimasi_Lama_Perjalanan"].ToString() : TimeSpan.MinValue.ToString())
                            } ,
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
        
        public async Task<object> Pesan_Driver(Pesan_Driver_Transaksi Pemesanan)
        {
            object result = new object();
            try
            {
                List<Lokasi_GPS_Driver> Lokasi_Driver = await new Lokasi_GPS_Driver_Action().Pencarian_Lokasi_Driver(Pemesanan.Harga_Pemesanan.Lokasi_Jemput, 1000, 1);

                if (Lokasi_Driver.Count() != 0)
                {
                    result = await Simpan_Data(new Transaksi_User
                                                {
                                                    ID_User = Pemesanan.ID_User,
                                                    ID_Driver = Lokasi_Driver[0].ID_Driver,
                                                    ID_Jenis_Fasilitas = Pemesanan.Harga_Pemesanan.Jenis_Fasilitas,
                                                    Harga_Pemesanan = Pemesanan.Harga_Pemesanan,
                                                    Nama_Lokasi_Jemput = "" ,
                                                    Nama_Lokasi_Tujuan = "",
                                                    Status = Status_Transaksi.Proces
                                                });

                    if (result == "Success")
                    {
                        Build_Query bld = new Build_Query();

                        bld.Daftar_Query.Add(new Build_Query.Structur_Query
                        {
                            Nama_Field = "ID_User",
                            Operator = Build_Query.Operator_Query.Equal,
                            Value = Pemesanan.ID_User.ID_User,
                            Operator_Next = Build_Query.Operator_Next_Query.AND
                        });

                        bld.Daftar_Query.Add(new Build_Query.Structur_Query
                        {
                            Nama_Field = "ID_Driver",
                            Operator = Build_Query.Operator_Query.Equal,
                            Value = Lokasi_Driver[0].ID_Driver.ID_Driver,
                            Operator_Next = Build_Query.Operator_Next_Query.AND
                        });

                        bld.Daftar_Query.Add(new Build_Query.Structur_Query
                        {
                            Nama_Field = "ID_Jenis_Fasilitas",
                            Operator = Build_Query.Operator_Query.Equal,
                            Value = Pemesanan.Harga_Pemesanan.Jenis_Fasilitas.ID_Jenis_Fasilitas,
                            Operator_Next = Build_Query.Operator_Next_Query.AND
                        });

                        bld.Daftar_Query.Add(new Build_Query.Structur_Query
                        {
                            Nama_Field = "Status",
                            Operator = Build_Query.Operator_Query.Equal,
                            Value = "2",
                            Operator_Next = Build_Query.Operator_Next_Query.AND
                        });

                        string Query_where = bld.Build_Query_Where();

                        List<Model.Transaksi_User> obj = await new Model_Action.Transaksi_User_Action().Pencarian_Data(Query_where);

                        result = obj;
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }
        #endregion
    }
}
