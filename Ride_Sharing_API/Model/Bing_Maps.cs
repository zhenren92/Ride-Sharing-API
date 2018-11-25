using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ride_Sharing_API.Model
{
    public class Bing_Maps
    {
        public class Post_Matrix
        {
            #region Deklarasi Struktur

            public struct Struct_Location
            {
                public Double latitude { set; get; }
                public Double longitude { set; get; }
            }

            #endregion

            #region Deklarasi Properties

            private List<Struct_Location> _origins;
            public List<Struct_Location> origins
            {
                get { return _origins; }
                set { _origins = value; }
            }

            private List<Struct_Location> _destinations;
            public List<Struct_Location> destinations
            {
                get { return _destinations; }
                set { _destinations = value; }
            }

            private string _travelMode;
            public string travelMode
            {
                get { return _travelMode; }
                set { _travelMode = value; }
            }

            private int _resolution;
            public int resolution
            {
                get { return _resolution; }
                set { _resolution = value; }
            }

            private string _timeUnit;
            public string timeUnit
            {
                get { return _timeUnit; }
                set { _timeUnit = value; }
            }

            private string _distanceUnit;
            public string distanceUnit
            {
                get { return _distanceUnit; }
                set { _distanceUnit = value; }
            }
            
            #endregion

        }

        public class Result_Rest_Matrix
        {
            #region Deklarasi Properties

            private string _authenticationResultCode;
            public string authenticationResultCode
            {
                get { return _authenticationResultCode; }
                set { _authenticationResultCode = value; }
            }

            private string _brandLogoUri;
            public string brandLogoUri
            {
                get { return _brandLogoUri; }
                set { _brandLogoUri = value; }
            }

            private string _copyright;
            public string copyright
            {
                get { return _copyright; }
                set { _copyright = value; }
            }

            private List<Recource_Rest_Matrix> _resourceSets;

            public List<Recource_Rest_Matrix> resourceSets
            {
                get { return _resourceSets; }
                set { _resourceSets = value; }
            }

            private int _statusCode;
            public int statusCode
            {
                get { return _statusCode; }
                set { _statusCode = value; }
            }

            private string _statusDescription;
            public string statusDescription
            {
                get { return _statusDescription; }
                set { _statusDescription = value; }
            }

            private string _traceId;
            public string traceId
            {
                get { return _traceId; }
                set { _traceId = value; }
            }
            
            #endregion

        }

        public class Recource_Rest_Matrix
        {
            #region Deklarasi Struktur
            
            #endregion

            #region Deklarasi Propeties

            private int _estimatedTotal;
            public int estimatedTotal
            {
                get { return _estimatedTotal; }
                set { _estimatedTotal = value; }
            }

            private List<Resource> _resources;
            public List<Resource> resources
            {
                get { return _resources; }
                set { _resources = value; }
            }

            #endregion
        }

        public class Resource
        {
            public string __type { get; set; }
            public List<Post_Matrix.Struct_Location> destinations { get; set; }
            public string errorMessage { get; set; }
            public List<Post_Matrix.Struct_Location> origins { get; set; }
            public List<result> results { get; set; }
        }

        public class result
        {
            public int destinationIndex { get; set; }
            public int originIndex { get; set; }
            public decimal totalWalkDuration { get; set; }
            public decimal travelDistance { get; set; }
            public decimal travelDuration { get; set; }
        }
    }
}
