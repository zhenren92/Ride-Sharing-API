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
    public class Harga_Pemesanan_Action : Harga_Pemesanan
    {
        #region Method

        public async Task<Harga_Pemesanan> Pencarian_Data(GeoCoordinate Lokasi_Jemput, GeoCoordinate Lokasi_Tujuan ,string IDJenisFasilitas)
        {
            Harga_Pemesanan Daftar_Data = new Harga_Pemesanan();

            try
            {
                Daftar_Data.Lokasi_Jemput = Lokasi_Jemput;
                Daftar_Data.Lokasi_Tujuan = Lokasi_Tujuan;

                object Data_Calculate = await new Mdl_Map_GPS().GetLocationBingMaps(Lokasi_Jemput, Lokasi_Tujuan);
                Bing_Maps.Result_Rest_Matrix Data_Map = new Bing_Maps.Result_Rest_Matrix();

                if (Data_Calculate != null && Data_Calculate.GetType() != typeof(object))
                {
                    Data_Map = (Bing_Maps.Result_Rest_Matrix) Data_Calculate;
                }

                Build_Query bld = new Build_Query();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Jenis_Fasilitas",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDJenisFasilitas,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                string Query_where = bld.Build_Query_Where();

                List<Model.Jenis_Fasilitas> obj = await new Model_Action.Jenis_Fasilitas_Action().Pencarian_Data(Query_where);

                if (obj.Count() != 0)
                {
                    if (Data_Map != null)
                    {
                        Daftar_Data.Total_Harga = obj[0].ID_Harga_Fasilitas.Harga * (decimal.Round(Data_Map.resourceSets[0].resources[0].results[0].travelDistance,2,MidpointRounding.AwayFromZero));
                        Daftar_Data.Jarak = (decimal.Round(Data_Map.resourceSets[0].resources[0].results[0].travelDistance, 2, MidpointRounding.AwayFromZero));
                        Daftar_Data.Jenis_Fasilitas = obj[0];
                    }
                }

                return Daftar_Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}
