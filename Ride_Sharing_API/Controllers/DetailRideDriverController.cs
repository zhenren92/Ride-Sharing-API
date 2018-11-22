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
    [Route("api/DetailRideDriver")]
    public class DetailRideDriverController : Controller
    {
        [Produces("application/json")]
        [HttpGet("GetDetailRideDriver")]
        public async Task<IActionResult> GetDetailRideDriver(string IDKendaraan = "",
                                                          string IDDriver = "",
                                                          string NamaKendaraan = "",
                                                          string IDJenisFasilitas = "")
        {
            try
            {
                Build_Query bld = new Build_Query();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Kendaraan",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDKendaraan,
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
                    Nama_Field = "Nama_Kendaraan",
                    Operator = Build_Query.Operator_Query.Contains,
                    Value = NamaKendaraan,
                    Operator_Next = Build_Query.Operator_Next_Query.OR
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Jenis_Fasilitas",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDJenisFasilitas,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                string Query_where = bld.Build_Query_Where();

                List<Model.Detail_Ride_Driver> obj = await new Model_Action.Detail_Ride_Driver_Action().Pencarian_Data(Query_where);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Produces("application/json")]
        [HttpPost("SaveDetailRideDriver")]
        public async Task<IActionResult> SaveDetailRideDriver([FromBody] Detail_Ride_Driver prm_obj)
        {
            try
            {
                if (prm_obj != null)
                {
                    object obj = await new Model_Action.Detail_Ride_Driver_Action().Simpan_Data(prm_obj);
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

        [HttpDelete("DeleteDetailRideDriver/{IDKendaraan}")]
        public async Task<IActionResult> DeleteDetailRideDriver(string IDKendaraan = "")
        {
            try
            {
                if (IDKendaraan != "")
                {
                    object obj = await new Model_Action.Detail_Ride_Driver_Action().Hapus_Data(IDKendaraan);
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
