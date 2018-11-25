using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ride_Sharing_API.Model;
using GeoCoordinatePortable;
using static Ride_Sharing_API.Modul.Mdl_Ref_Tools;
using static Ride_Sharing_API.Model.Transaksi_User;

namespace Ride_Sharing_API.Model_Action
{
    public class Lokasi_GPS_Driver_Action: Lokasi_GPS_Driver
    {

        #region Deklarasi Properties

        private List<Lokasi_GPS_Driver> Daftar_Lokasi_Driver = new List<Lokasi_GPS_Driver>();

        #endregion

        #region Method

        public void Simpan_Lokasi_Driver(Lokasi_GPS_Driver Lokasi_Real_Driver)
        {
            if (Daftar_Lokasi_Driver.Exists(x => x.ID_Driver.ID_Driver == Lokasi_Real_Driver.ID_Driver.ID_Driver) == false)
            {
                Daftar_Lokasi_Driver.Add(Lokasi_Real_Driver);
            }
            else
            {
                Daftar_Lokasi_Driver.Find(x => x.ID_Driver.ID_Driver == Lokasi_Real_Driver.ID_Driver.ID_Driver).Lokasi_Driver_Lan_Long = Lokasi_Real_Driver.Lokasi_Driver_Lan_Long;
            }
        }

        public void Hapus_Lokasi_Driver(Lokasi_GPS_Driver Lokasi_Real_Driver)
        {
            if (Daftar_Lokasi_Driver.Exists(x => x.ID_Driver.ID_Driver == Lokasi_Real_Driver.ID_Driver.ID_Driver && Lokasi_Real_Driver.Status_Driver == Jenis_Status_Driver.Tidak_Aktif) == true)
            {
                Daftar_Lokasi_Driver.Remove(Lokasi_Real_Driver);
            }
        }

        public void Data_Temp()
        {
            Daftar_Lokasi_Driver.Clear();

            Daftar_Lokasi_Driver.Add(new Lokasi_GPS_Driver
            {
                ID_Driver = new Ride_Driver
                {
                    ID_Driver = "DRV.2211201801",
                    Nama_Driver = "Dadang"
                },
                Lokasi_Driver_Lan_Long = new GeoCoordinate(-2.956439, 104.812288)
            }
            );

            Daftar_Lokasi_Driver.Add(new Lokasi_GPS_Driver
            {
                ID_Driver = new Ride_Driver
                {
                    ID_Driver = "DRV.2211201802",
                    Nama_Driver = "Rojali"
                },
                Lokasi_Driver_Lan_Long = new GeoCoordinate(-2.955780, 104.808500)
            }
            );

            Daftar_Lokasi_Driver.Add(new Lokasi_GPS_Driver
            {
                ID_Driver = new Ride_Driver
                {
                    ID_Driver = "DRV.2411201801",
                    Nama_Driver = "Rojali xxx"
                },
                Lokasi_Driver_Lan_Long = new GeoCoordinate(-2.957970, 104.802250)
            }
            );

            Daftar_Lokasi_Driver.Add(new Lokasi_GPS_Driver
            {
                ID_Driver = new Ride_Driver
                {
                    ID_Driver = "DRV.2411201802",
                    Nama_Driver = "mukidin 12"
                },
                Lokasi_Driver_Lan_Long = new GeoCoordinate(-2.958775, 104.813673)
            }


            );
        }

        public async Task<List<Lokasi_GPS_Driver>> Pencarian_Lokasi_Driver(GeoCoordinate Lokasi_Jemput, double Radius, int Banyak_Driver)
        {
            Data_Temp();

            List<Lokasi_GPS_Driver> Daftar_Driver = new List<Lokasi_GPS_Driver>();
            List<Transaksi_User> Daftar_Transaksi = new List<Transaksi_User>();

            try
            {
                Daftar_Driver = new Modul.Mdl_Map_GPS().Cari_Latitude_Longitude(Daftar_Lokasi_Driver, Lokasi_Jemput, Radius);

                foreach (var item in Daftar_Driver)
                {
                    item.Jarak_Dengan_Lokasi_Jemput = decimal.Round(Convert.ToDecimal(new Modul.Mdl_Map_GPS().Hitung_Jarak_Jemput_Tujuan(Lokasi_Jemput.Latitude, Lokasi_Jemput.Longitude,
                                                                                                         item.Lokasi_Driver_Lan_Long.Latitude, item.Lokasi_Driver_Lan_Long.Longitude,
                                                                                                         'K') * 1000),2,MidpointRounding.AwayFromZero);
                }

                Daftar_Driver = Daftar_Driver.OrderBy(x => x.Jarak_Dengan_Lokasi_Jemput).ToList();

                Build_Query bld = new Build_Query();

                foreach (var item in Daftar_Driver)
                {
                    bld.Daftar_Query.Add(new Build_Query.Structur_Query
                    {
                        Nama_Field = "ID_Driver",
                        Operator = Build_Query.Operator_Query.Equal,
                        Value = item.ID_Driver.ID_Driver,
                        Operator_Next = Build_Query.Operator_Next_Query.OR
                    });
                }

                string Query_where = bld.Build_Query_Where();

                bld.Daftar_Query.Clear();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Status",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = "2",
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                Query_where = bld.Build_Query_Where() + " AND (" + Query_where + ")";

                Daftar_Transaksi = await new Transaksi_User_Action().Pencarian_Data(Query_where);

                if (Daftar_Transaksi.Count() != 0)
                {
                    foreach (var item in Daftar_Transaksi)
                    {
                        Daftar_Driver.RemoveAll(x => x.ID_Driver.ID_Driver == item.ID_Driver.ID_Driver);
                    }
                }

                if (Banyak_Driver < Daftar_Driver.Count() && Banyak_Driver != 0)
                {
                    Daftar_Driver.RemoveRange(Banyak_Driver, Daftar_Driver.Count() - Banyak_Driver);
                }
            }
            catch (Exception ex)
            {
                Daftar_Driver = null;
            }

            return Daftar_Driver;
        }
        
        #endregion
    }
}
