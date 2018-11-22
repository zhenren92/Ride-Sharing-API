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
    [Route("api/RideUser")]
    public class RideUserController : Controller
    {
        [Produces("application/json")]
        [HttpGet("GetRideUser")]
        public async Task<IActionResult> GetRideUser(string IDUser = "",
                                                      string NamaUser = "",
                                                      string Email = "")
        {
            try
            {
                Build_Query bld = new Build_Query();

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "ID_User",
                    Operator = Build_Query.Operator_Query.Equal,
                    Value = IDUser,
                    Operator_Next = Build_Query.Operator_Next_Query.AND
                });

                bld.Daftar_Query.Add(new Build_Query.Structur_Query
                {
                    Nama_Field = "Nama_User",
                    Operator = Build_Query.Operator_Query.Contains,
                    Value = NamaUser,
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

                List<Model.Ride_User> obj = await new Model_Action.Ride_User_Action().Pencarian_Data(Query_where);

                return Ok((obj == null) ? null : obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Produces("application/json")]
        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser([FromBody] Ride_User prm_obj)
        {
            try
            {
                if (prm_obj != null)
                {
                    object obj = await new Model_Action.Ride_User_Action().Simpan_Data(prm_obj);
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

        [HttpDelete("DeleteUser/{IDUser}")]
        public async Task<IActionResult> DeleteUser(string IDUser = "")
        {
            try
            {
                if (IDUser != "")
                {
                    object obj = await new Model_Action.Ride_User_Action().Hapus_Data(IDUser);
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
