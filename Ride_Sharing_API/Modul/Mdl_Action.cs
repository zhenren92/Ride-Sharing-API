using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;

namespace Ride_Sharing_API.Modul
{
    public class Mdl_Action
    {
        private SqlConnection conn = new SqlConnection();
        protected void OnConfiguring()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();

                conn.ConnectionString = configuration["ConnectionStrings:DefaultConnection"];

                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    conn.Close();
                    throw;
                }
            }
        }

        public async Task<object> Tambah_Data(string Nama_Table = "",
                                              Dictionary<string, object> Daftar_Field = null)
        {
            object result = null;
            string Str_Nama_Field = "";
            string Str_Parameter = "";

            try
            {
                foreach (var item in Daftar_Field)
                {
                    if (Str_Parameter == "")
                    {
                        Str_Nama_Field = item.Key;
                        Str_Parameter = "@" + item.Key;
                    }
                    else
                    {
                        Str_Nama_Field = Str_Nama_Field + "," + item.Key;
                        Str_Parameter = Str_Parameter + ",@" + item.Key;
                    }
                }

                OnConfiguring();
                SqlCommand cmd = new SqlCommand("INSERT INTO " + Nama_Table + "(" + Str_Nama_Field + ")  VALUES(" + Str_Parameter + ")", conn);

                result = await Execute_Data(cmd, Daftar_Field);

                if (Convert.ToInt16(result) != 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "Failed to insert";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<object> Ubah_Data(string Nama_Table = "",
                                            Dictionary<string, object> Daftar_Field = null,
                                            string Query_Where = "")
        {
            object result = null;
            string Str_Parameter = "";

            try
            {
                foreach (var item in Daftar_Field)
                {
                    if (Str_Parameter == "")
                    {
                        Str_Parameter = item.Key + " = @" + item.Key;
                    }
                    else
                    {
                        Str_Parameter = Str_Parameter + "," +item.Key + " = @" + item.Key;
                    }
                }

                OnConfiguring();
                SqlCommand cmd = new SqlCommand("UPDATE " + Nama_Table + " SET " + Str_Parameter + ((Query_Where != "") ? " WHERE " + Query_Where : ""), conn);

                result = await Execute_Data(cmd, Daftar_Field);

                if (Convert.ToInt16(result) != 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "Not Found";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<object> Hapus_Data(string Nama_Table , 
                                             string Query_Where = "")
        {
            object result = null;
            OnConfiguring();

            SqlCommand cmd = new SqlCommand("DELETE FROM " + Nama_Table + ((Query_Where != "") ? " WHERE " + Query_Where : ""),conn);

            try
            {
                result = await cmd.ExecuteNonQueryAsync();

                if (Convert.ToInt16(result) != 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "Not Found";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }


        private async Task<object> Execute_Data(SqlCommand cmd,
                                                Dictionary<string, object> Daftar_Field)
        {
            object result = null;

            try
            {

                cmd.Parameters.Clear();

                foreach (var prm in Daftar_Field)
                {
                    if (prm.Value != null)
                    {
                        if (prm.Value.GetType() == typeof(string))
                        {
                            cmd.Parameters.AddWithValue("@" + prm.Key, prm.Value);
                        } else if (prm.Value.GetType() == typeof(DateTime))
                        {
                            cmd.Parameters.AddWithValue("@" + prm.Key, DateTime.Parse(prm.Value.ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        }
                        else if (prm.Value.GetType() == typeof(DateTime))
                        {
                            cmd.Parameters.AddWithValue("@" + prm.Key, prm.Value);
                        }
                        else if (prm.Value.GetType() == typeof(byte[]))
                        {
                            cmd.Parameters.AddWithValue("@" + prm.Key, prm.Value);
                        }
                        else if (prm.Value.GetType() == typeof(TimeSpan))
                        {
                            cmd.Parameters.AddWithValue("@" + prm.Key, TimeSpan.Parse(prm.Value.ToString()));
                        }
                        else
                        {
                            Console.WriteLine(prm.Value.GetType());
                            cmd.Parameters.AddWithValue("@" + prm.Key, prm.Value);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@" + prm.Key, DBNull.Value);
                    }
                }

                result = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        public async Task<DataTable> Setting_SP_Tampil_Data(string Nama_Table, 
                                                    string Nama_Field,
                                                    string Query_Where = "",
                                                    string Query_Sortir = "")
        {
            OnConfiguring();

            SqlCommand cmd = new SqlCommand("SP_Tampil_Data_Standard",conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            DataTable dtb = new DataTable();

            cmd.Parameters.AddWithValue("@Nama_Table", Nama_Table);
            cmd.Parameters.AddWithValue("@Nama_Columns", Nama_Field);
            cmd.Parameters.AddWithValue("@Query_Where", Query_Where);
            cmd.Parameters.AddWithValue("@Query_Sort", Query_Sortir);
            cmd.Dispose();

            try
            {
                dtb.Load(await cmd.ExecuteReaderAsync());

                return dtb;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
