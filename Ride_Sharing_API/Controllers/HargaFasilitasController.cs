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
    [Route("api/HargaFasilitas")]
    public class HargaFasilitasController : Controller
    {
        [Produces("application/json")]
        [HttpGet("GetHargaFasilitas")]
        public async Task<IActionResult> GetHargaFasilitas(string IDHargaFasilitas = "",
                                                          string NamaHargaFasilitas = "",
                                                          string Satuan = "")
        {
            try
            {
                Build_Query bld = new Build_Query();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Harga_Fasilitas",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDHargaFasilitas,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Nama_Harga_Fasilitas",
                    Operator = Build_Query.Operator_Query.Contains,
                    Value = NamaHargaFasilitas,
                    Operator_Next = Build_Query.Operator_Next_Query.OR
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Satuan_Harga",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = Satuan,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                string Query_where = bld.Build_Query_Where();

                List<Model.Harga_Fasilitas> obj = await new Model_Action.Harga_Fasilitas_Action().Pencarian_Data(Query_where);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Produces("application/json")]
        [HttpPost("SaveHargaFasilitas")]
        public async Task<IActionResult> SaveHargaFasilitas([FromBody] Harga_Fasilitas prm_obj)
        {
            try
            {
                if (prm_obj != null)
                {
                    object obj = await new Model_Action.Harga_Fasilitas_Action().Simpan_Data(prm_obj);
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

        [HttpDelete("DeleteHargaFasilitas/{IDHargaFasilitas}")]
        public async Task<IActionResult> DeleteHargaFasilitas(string IDHargaFasilitas = "")
        {
            try
            {
                if (IDHargaFasilitas != "")
                {
                    object obj = await new Model_Action.Harga_Fasilitas_Action().Hapus_Data(IDHargaFasilitas);
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
