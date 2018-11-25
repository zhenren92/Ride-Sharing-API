using System;
using System.Collections.Generic;
using GeoCoordinatePortable;
using Ride_Sharing_API.Model;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ride_Sharing_API.Modul
{
    public class Mdl_Map_GPS
    {
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles (default)                         :::
        //:::                  'K' is kilometers                                      :::
        //:::                  'N' is nautical miles                                  :::
        //:::                                                                         :::
        //:::  Worldwide cities and other features databases with latitude longitude  :::
        //:::  are available at https://www.geodatasource.com                         :::
        //:::                                                                         :::
        //:::  For enquiries, please contact sales@geodatasource.com                  :::
        //:::                                                                         :::
        //:::  Official Web site: https://www.geodatasource.com                       :::
        //:::                                                                         :::
        //:::           GeoDataSource.com (C) All Rights Reserved 2018                :::
        //:::                                                                         :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


        #region Method Hitung Jarak

        public double Hitung_Jarak_Jemput_Tujuan(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        #endregion

        #region Method Cari Latitude Longitude

        public List<Lokasi_GPS_Driver> Cari_Latitude_Longitude(List<Lokasi_GPS_Driver> Lokasi_Driver , GeoCoordinate Lokasi_jemput ,double Radius)
        {
            List<GeoCoordinate> locations = new List<GeoCoordinate>();

            foreach (var item in Lokasi_Driver)
            {
                locations.Add(new GeoCoordinate
                {
                    Latitude = item.Lokasi_Driver_Lan_Long.Latitude,
                    Longitude = item.Lokasi_Driver_Lan_Long.Longitude
                });
            }

            var result = Lokasi_Driver.FindAll(x => x.Lokasi_Driver_Lan_Long.GetDistanceTo(Lokasi_jemput) < Radius);

            return result;

//            var result = locations.Where(l => l.GetDistanceTo(Lokasi_jemput) < Radius);

        }

        #endregion

        #region Hitung Jarak Pakai Google API

        /// <summary>
        /// Get Driving Distance In Miles based on Source and Destination.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public double GetDrivingDistanceInMiles(GeoCoordinate Lokasi_Jemput, GeoCoordinate Lokasi_Tujuan)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();


            //string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&mode=driving&sensor=false&language=en-EN&units=imperial";
            string url = String.Format("https://maps.googleapis.com/maps/api/distancematrix/xml?units=imperial&origins={0},{1}&destinations={2},{3}&key=" + configuration["GoogleMAPSKey:DefaultKey"], 
                                        Lokasi_Jemput.Latitude, Lokasi_Jemput.Longitude, Lokasi_Tujuan.Latitude, Lokasi_Tujuan.Longitude);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            {
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                return Convert.ToDouble(distance[0].ChildNodes[1].InnerText.Replace(" mi", ""));
            }

            return 0;
        }

        /// <summary>
        /// Get Location based on Latitude and Longitude.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public string GetLocation(double latitude, double longitude)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            {
                XmlNodeList location = xmldoc.GetElementsByTagName("distance");
                return xmldoc.GetElementsByTagName("formatted_address")[0].ChildNodes[0].InnerText;
            }

            return "";
        }
        #endregion

        #region Hitung Jarak Pakai Bing Maps API

        public async Task<object> GetLocationBingMaps(GeoCoordinate Lokasi_Jemput, GeoCoordinate Lokasi_Tujuan)
        {
            string url = "https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix?key=AnmKUVIMBHqJKGwktc0mXt4xE2b4DNUpYqtGS9sALVDvqIUW4MRRScJk2tjB4dqy";
            object result = new object();

            Bing_Maps.Post_Matrix obj = new Bing_Maps.Post_Matrix();

            obj.origins = new List<Bing_Maps.Post_Matrix.Struct_Location>();
            obj.origins.Add(new Bing_Maps.Post_Matrix.Struct_Location
                                {
                                    latitude = Lokasi_Jemput.Latitude,
                                    longitude = Lokasi_Jemput.Longitude
                                }
                            );

            obj.destinations = new List<Bing_Maps.Post_Matrix.Struct_Location>();
            obj.destinations.Add(new Bing_Maps.Post_Matrix.Struct_Location
                                    {
                                        latitude = Lokasi_Tujuan.Latitude,
                                        longitude = Lokasi_Tujuan.Longitude
                                    }
                                );
            obj.travelMode = "driving";
            obj.resolution = 1;
            obj.timeUnit = "minute";
            obj.distanceUnit = "km";

            using (var client = new HttpClient())
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                string resultContent = await response.Content.ReadAsStringAsync();

                try
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject(resultContent, typeof(Bing_Maps.Result_Rest_Matrix));
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }

            return result;
        }


        #endregion
    }
}
