using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ride_Sharing_API.Model;
using static Ride_Sharing_API.Modul.Mdl_Ref_Tools;

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
    }
}
