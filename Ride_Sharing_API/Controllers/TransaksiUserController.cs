using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ride_Sharing_API.Model;
using static Ride_Sharing_API.Modul.Mdl_Ref_Tools;
using GeoCoordinatePortable;

namespace Ride_Sharing_API.Controllers
{
    [Produces("application/json")]
    [Route("api/TransaksiUser")]
    public class TransaksiUserController : Controller
    {
        [Produces("application/json")]
        [HttpGet("GetTransaksiUser")]
        public async Task<IActionResult> GetTransaksiUser(string IDTransaksi = "",
                                                          string IDUser = "",
                                                          string IDDriver = "",
                                                          string IDJenisFasilitas = "",
                                                          string NamaLokasiJemput = "",
                                                          string NamaLokasiTujuan = "")
        {
            try
            {
                Build_Query bld = new Build_Query();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Transaksi",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDTransaksi,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_User",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDUser,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Driver",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDDriver,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Jenis_Fasilitas",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDJenisFasilitas,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Nama_Lokasi_Jemput",
                    Operator = Build_Query.Operator_Query.Contains,
                    Value = NamaLokasiJemput,
                    Operator_Next = Build_Query.Operator_Next_Query.OR
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Nama_Lokasi_Tujuan",
                    Operator = Build_Query.Operator_Query.Contains,
                    Value = NamaLokasiTujuan,
                    Operator_Next = Build_Query.Operator_Next_Query.OR
                });

                string Query_where = bld.Build_Query_Where();

                List<Model.Transaksi_User> obj = await new Model_Action.Transaksi_User_Action().Pencarian_Data(Query_where);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpPost("SaveTransaksiUser")]
        public async Task<IActionResult> SaveTransaksiUser([FromBody] Transaksi_User prm_obj)
        {
            try
            {
                if (prm_obj != null)
                {
                    object obj = await new Model_Action.Transaksi_User_Action().Simpan_Data(prm_obj);
                    return Ok((obj == null) ? null : obj);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTransaksiUser/{IDTransaksi}")]
        public async Task<IActionResult> DeleteTransaksiUser(string IDTransaksi = "")
        {
            try
            {
                if (IDTransaksi != "")
                {
                    object obj = await new Model_Action.Transaksi_User_Action().Hapus_Data(IDTransaksi);
                    return Ok((obj == null) ? null : obj);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("GetCariHargaPemesanan")]
        public async Task<IActionResult> GetCariHargaPemesanan(double LokasiJemputLantitude , double LokasiJemputLongtitude, 
                                                                double LokasiTujuanLantitude, double LokasiTujuanLongtitude, 
                                                                string IDJenisFasilitas)
        {
            try
            {
                GeoCoordinate LokasiJemput_Geo = new GeoCoordinate(LokasiJemputLantitude, LokasiJemputLongtitude);
                GeoCoordinate LokasiTujuan_Geo = new GeoCoordinate(LokasiTujuanLantitude, LokasiTujuanLongtitude);

                Model.Harga_Pemesanan obj = await new Model_Action.Harga_Pemesanan_Action().Pencarian_Data(LokasiJemput_Geo, LokasiTujuan_Geo, IDJenisFasilitas);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpPost("PesanDriver")]
        public async Task<IActionResult> PesanDriver([FromBody] Pesan_Driver_Transaksi Pemesanan)
        {
            try
            {
                if (Pemesanan.ID_User.ID_User != null && Pemesanan.Harga_Pemesanan.Lokasi_Jemput != null)
                {
                    object obj = await new Model_Action.Transaksi_User_Action().Pesan_Driver(Pemesanan);
                    return Ok((obj == null) ? null : obj);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpPost("BatalPesanDriver/{IDTransaksi}")]
        public async Task<IActionResult> BatalPesanDriver(string IDTransaksi)
        {
            try
            {
                if (IDTransaksi != null)
                {
                    object obj = await new Model_Action.Transaksi_User_Action().Batal_Pesan_Driver(IDTransaksi);
                    return Ok((obj == null) ? null : obj);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
