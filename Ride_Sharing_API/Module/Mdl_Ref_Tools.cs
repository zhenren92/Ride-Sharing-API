namespace Kairos_Web_Api.Modul
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="Mdl_Ref_Tools" />
    /// </summary>
    public class Mdl_Ref_Tools
    {
        /// <summary>
        /// Konversi dari Object Ke Array Byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] Convert_Object_To_Array(object obj)
        {
            if (obj != null)
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Konversi dari Array Bytes Ke Object
        /// </summary>
        /// <param name="Array_Bytes"></param>
        /// <returns></returns>
        public object Convert_Array_To_Object(byte[] Array_Bytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(Array_Bytes, 0, Array_Bytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        /// <summary>
        /// The Clean_String_Query
        /// </summary>
        /// <param name="str">The str<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string Clean_String_Query(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            
            sb.Replace("&", "and");
            sb.Replace(",", "");
            sb.Replace("  ", " ");
            sb.Replace(" ", "-");
            sb.Replace("'", "");
            sb.Replace(".", "");
            sb.Replace("eacute;", "é");

            return sb.ToString().ToLower();
        }

        public class Build_Query
        {
            /// <summary>
            /// Equal = "=" 
            /// NotEqual = "<>"
            /// Contains = "LIKE *value*"
            /// StartWith = "LIKE value*"
            /// EndWith = "LIKE *value"
            /// </summary>
            public enum Operator_Query
            {
                Equal = 0 ,
                NotEqual = 1,
                Contains = 2 ,
                StartsWith = 3 ,
                EndsWith = 4
            }

            /// <summary>
            /// Contoh :
            /// Empty = ""
            /// AND = AND
            /// OR = OR
            /// BETWEEN = BETWEEN 'Value1' AND 'Value2'
            /// </summary>
            public enum Operator_Next_Query
            {
                Empty = 0 ,
                AND = 1,
                OR = 2,
                BETWEEN = 3
            }

            /// <summary>
            /// Structur Query untuk membuat query
            /// Contoh : 
            /// Nama_Field = ID
            /// Operator = LIKE
            /// Value = 1234
            /// Hasil Query = (ID LIKE 1234)
            /// </summary>
            public struct Structur_Query
            {
                public string Nama_Field { get; set; }
                public Operator_Query Operator { get; set; }
                public string Value { get; set; }
                public string[] Value_Between { set; get; }
                public Operator_Next_Query Operator_Next { get; set; }
            }

            public List<Structur_Query> Daftar_Query = new List<Structur_Query>();

            public string Build_Query_Where()
            {
                string Result = "";
                if (Daftar_Query.Count != 0)
                {
                    Structur_Query Last_Query = new Structur_Query();
                    foreach (Structur_Query item in Daftar_Query.FindAll(x => x.Nama_Field != null).FindAll(x => x.Value != null).FindAll(x => x.Value.ToString() != string.Empty))
                    {
                        if (item.Value_Between == null)
                        {
                            Result = Result + Check_Result_Query_Where(item);
                        }
                        else
                        {
                            Result = Result + " (" + item.Nama_Field + " BETWEEN '" + item.Value_Between[0] + "' AND '" + item.Value_Between[1] + "') " + (item.Operator_Next != Operator_Next_Query.Empty ? item.Operator_Next.ToString() : "");
                        }

                        Last_Query = item;
                    }

                    if (Result != string.Empty)
                    {
                        Result = Result.Remove(Result.Length - Last_Query.Operator_Next.ToString().Length, Last_Query.Operator_Next.ToString().Length);
                    }
                }

                return Result;
            }

            private string Check_Result_Query_Where(Structur_Query item)
            {
                string Result = "";

                switch (item.Operator)
                {
                    case Operator_Query.Equal:
                        Result = " (" + item.Nama_Field + " = '" + item.Value + "') ";
                        break;
                    case Operator_Query.NotEqual:
                        Result = " (" + item.Nama_Field + " <> '" + item.Value + "') ";
                        break;
                    case Operator_Query.Contains:
                        Result = " (" + item.Nama_Field + " LIKE '%" + item.Value + "%') ";
                        break;
                    case Operator_Query.StartsWith:
                        Result = " (" + item.Nama_Field + " LIKE '" + item.Value + "%') ";
                        break;
                    case Operator_Query.EndsWith:
                        Result = " (" + item.Nama_Field + " LIKE '%" + item.Value + "') ";
                        break;
                }
                
                Result = Result + (item.Operator_Next != Operator_Next_Query.Empty ? item.Operator_Next.ToString() : "");

                return Result;
            }
        }
    }
}
