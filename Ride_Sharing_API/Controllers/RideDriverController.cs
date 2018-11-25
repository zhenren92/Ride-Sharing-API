using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ride_Sharing_API.Model;
using static Ride_Sharing_API.Modul.Mdl_Ref_Tools;

namespace Ride_Sharing_API.Controllers
{
    [Produces("application/json")]
    [Route("api/RideDriver")]
    public class RideDriverController : Controller
    {
        [Produces("application/json")]
        [HttpGet("GetRideDriver")]
        public async Task<IActionResult> GetRideDriver(string IDDriver = "",
                                                      string NamaDriver = "",
                                                      string Email = "")
        {
            try
            {
                Build_Query bld = new Build_Query();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_Driver",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDDriver,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Nama_Driver",
                    Operator = Build_Query.Operator_Query.Contains,
                    Value = NamaDriver,
                    Operator_Next = Build_Query.Operator_Next_Query.OR
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Email",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = Email,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                string Query_where = bld.Build_Query_Where();

                List<Model.Ride_Driver> obj = await new Model_Action.Ride_Driver_Action().Pencarian_Data(Query_where);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Produces("application/json")]
        [HttpPost("SaveDriver")]
        public async Task<IActionResult> SaveDriver([FromBody] Ride_Driver prm_obj)
        {
            try
            {
                if (prm_obj != null)
                {
                    object obj = await new Model_Action.Ride_Driver_Action().Simpan_Data(prm_obj);
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

        [HttpDelete("DeleteDriver/{IDDriver}")]
        public async Task<IActionResult> DeleteDriver(string IDDriver = "")
        {
            try
            {
                if (IDDriver != "")
                {
                    object obj = await new Model_Action.Ride_Driver_Action().Hapus_Data(IDDriver);
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
        [HttpGet("GetLokasiDriver")]
        public async Task<IActionResult> GetLokasiDriver(double Latitude, double Longitude, double Radius, int BanyakDriver = 0)
        {
            try
            {
                GeoCoordinate Lokasi_Jemput = new GeoCoordinate(Latitude, Longitude);

                List<Model.Lokasi_GPS_Driver> obj = await new Model_Action.Lokasi_GPS_Driver_Action().Pencarian_Lokasi_Driver(Lokasi_Jemput, Radius, BanyakDriver);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
