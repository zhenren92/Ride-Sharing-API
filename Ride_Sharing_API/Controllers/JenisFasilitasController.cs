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
    [Route("api/JenisFasilitas")]
    public class JenisFasilitasController : Controller
    {
        [Produces("application/json")]
        [HttpGet("GetJenisFasilitas")]
        public async Task<IActionResult> GetJenisFasilitas(string IDJenisFasilitas = "",
                                                          string NamaJenisFasilitas = "")
        {
            try
            {
                Build_Query bld = new Build_Query();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Jenis_Fasilitas",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDJenisFasilitas,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Nama_Jenis_Fasilitas",
                    Operator = Build_Query.Operator_Query.Contains,
                    Value = NamaJenisFasilitas,
                    Operator_Next = Build_Query.Operator_Next_Query.OR
                });

                string Query_where = bld.Build_Query_Where();

                List<Model.Jenis_Fasilitas> obj = await new Model_Action.Jenis_Fasilitas_Action().Pencarian_Data(Query_where);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Produces("application/json")]
        [HttpPost("SaveJenisFasilitas")]
        public async Task<IActionResult> SaveJenisFasilitas([FromBody] Jenis_Fasilitas prm_obj)
        {
            try
            {
                if (prm_obj != null)
                {
                    object obj = await new Model_Action.Jenis_Fasilitas_Action().Simpan_Data(prm_obj);
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

        [HttpDelete("DeleteJenisFasilitas/{IDJenisFasilitas}")]
        public async Task<IActionResult> DeleteJenisFasilitas(string IDJenisFasilitas = "")
        {
            try
            {
                if (IDJenisFasilitas != "")
                {
                    object obj = await new Model_Action.Jenis_Fasilitas_Action().Hapus_Data(IDJenisFasilitas);
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
