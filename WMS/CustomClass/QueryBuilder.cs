using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS.Models;

namespace WMS.CustomClass
{
    public class QueryBuilder
    {
        public DataTable GetValuesfromDB(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TAS2013ConnectionString"].ConnectionString);

            using (SqlCommand cmdd = Conn.CreateCommand())
            using (SqlDataAdapter sda = new SqlDataAdapter(cmdd))
            {
                cmdd.CommandText = query;
                cmdd.CommandType = CommandType.Text;
                Conn.Open();
                sda.Fill(dt);
                Conn.Close();
            }
            return dt;
        }
        public string MakeCustomizeQueryForUserAccess(User _user)
        {
            TAS2013Entities db = new TAS2013Entities();
            string query = "where";
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = db.UserAccesses.Where(aa => aa.UserID == _user.UserID).ToList();
            foreach (var access in uAcc)
            {
                if (access.Criteria.Contains("L"))
                    query = query + " LocID = " + access.CriteriaData + " ";
                 if (access.Criteria.Contains("S"))
                       query = query + " LocID>0";
            
            }
            return query;
        
        }
        public string MakeCustomizeQuery(User _user)
        {
            string query = " where ";
            string subQuery = "";
            string subQueryLoc = "";
            List<string> _Criteria = new List<string>();
            List<string> _CriteriaForOr = new List<string>();
            List<string> _CriteriaForOrLoc = new List<string>();
            //if (_user.ViewLocation == true)
            // {
            //     _Criteria.Add(" LocID = " + _user.LocationID.ToString());
            // }
            TAS2013Entities db = new TAS2013Entities();
            List<UserAccess> ulocs = new List<UserAccess>();
            ulocs = db.UserAccesses.Where(aa => aa.UserID == _user.UserID).ToList();
            foreach (var uloc in ulocs)
            {      
                if(uloc.Criteria == "Z")
                _CriteriaForOrLoc.Add(" ZoneID = " + uloc.CriteriaData + " ");
                if(uloc.Criteria == "R")
                    _CriteriaForOrLoc.Add(" RegionID = " + uloc.CriteriaData + " ");
                if (uloc.Criteria == "C")
                    _CriteriaForOrLoc.Add(" CityID = " + uloc.CriteriaData + " ");
            }
            switch (_user.RoleID)
            {
                case 1:
                    break;
                case 2:
                    _Criteria.Add(" CompanyID= 1 or CompanyID = 2 ");
                    break;
                case 3:
                    _Criteria.Add(" CompanyID>= 3");
                    break;
                case 4:
                    _Criteria.Add(" CompanyID = " + _user.CompanyID.ToString());
                    break;
                case 5:
                    break;
            }
            for (int i = 0; i < _Criteria.Count; i++)
            {
                query = query + _Criteria[i] + " and ";
            }
            for (int i = 0; i < _CriteriaForOrLoc.Count - 1; i++)
            {
                subQueryLoc = subQueryLoc + _CriteriaForOrLoc[i] + " or ";
            }
            if(_CriteriaForOrLoc.Count>0)
            subQueryLoc = " and  ( " + subQueryLoc + _CriteriaForOrLoc[_CriteriaForOrLoc.Count - 1] + " ) ";
            //query = query + " ) and (";
            //query = query + _Criteria[_Criteria.Count-1];

            subQuery = " ( ";
            for (int i = 0; i < _CriteriaForOr.Count - 1; i++)
            {
                subQuery = subQuery + _CriteriaForOr[i] + " or ";
            }
            subQuery = subQuery + _CriteriaForOr[_CriteriaForOr.Count - 1];
            subQuery = subQuery + " ) ";
            query = query + subQuery + subQueryLoc;
            return query;
        }

        public string QueryForCompanySegeration(User _user)
        {
            string query = "";
            if (query != "")
            {
                query = " where " + query;
            }
            return query;
        }
        public string QueryForLocationSegeration(User _user)
        {
            TAS2013Entities db = new TAS2013Entities();
            List<UserLocation> ulocs = new List<UserLocation>();
            List<string> _CriteriaForOrLoc = new List<string>();
            ulocs = db.UserLocations.Where(aa => aa.UserID == _user.UserID).ToList();
            string query = " where ";
            foreach (var uloc in ulocs)
            {
                _CriteriaForOrLoc.Add(" LocationID = " + uloc.LocationID + " ");
            }
            for (int i = 0; i < _CriteriaForOrLoc.Count - 1; i++)
            {
                query = query + _CriteriaForOrLoc[i] + " or ";
            }
            query = query + _CriteriaForOrLoc[_CriteriaForOrLoc.Count - 1];
            return query;
        }
        public string QueryForLocationTableSegeration(User _user)
        {
            TAS2013Entities db = new TAS2013Entities();
            List<UserLocation> ulocs = new List<UserLocation>();
            List<string> _CriteriaForOrLoc = new List<string>();
            ulocs = db.UserLocations.Where(aa => aa.UserID == _user.UserID).ToList();
            string query = " where ";
            foreach (var uloc in ulocs)
            {
                _CriteriaForOrLoc.Add(" LocID = " + uloc.LocationID + " ");
            }
            for (int i = 0; i < _CriteriaForOrLoc.Count - 1; i++)
            {
                query = query + _CriteriaForOrLoc[i] + " or ";
            }
            query = query + _CriteriaForOrLoc[_CriteriaForOrLoc.Count - 1];
            return query;
        }
        public string QueryForCompanyView(User _User)
        {
            string query = "";
            switch (_User.RoleID)
            {
                case 1:
                    break;
                case 2:
                    query = " where CompID= 1 or CompID = 2 ";
                    break;
                case 3:
                    query = " where  CompID>= 3";
                    break;
                case 4:
                    query = " where  CompID = " + _User.CompanyID.ToString();
                    break;
                case 5:
                    break;
            }
            return query;
        }
        public string QueryForCompanyFilters(User _User)
        {
            string query = "";
            switch (_User.RoleID)
            {
                case 1:
                    break;
                case 2:
                    query = " where CompanyID= 1 or CompanyID = 2 ";
                    break;
                case 3:
                    query = " where  CompanyID>= 3";
                    break;
                case 4:
                    query = " where  CompanyID = " + _User.CompanyID.ToString();
                    break;
                case 5:
                    break;
            }
            return query;
        }

        public string QueryForCompanyViewLinq(User _User)
        {
            string query = "";
            switch (_User.RoleID)
            {
                case 1: query = "CompID > 0";
                    break;
                case 2:
                    query = "CompID= 1 or CompID = 2 ";
                    break;
                case 3:
                    query = "CompID>= 3";
                    break;
                case 4:
                    query = "CompID = " + _User.CompanyID.ToString();
                    break;
                case 5:
                    break;
            }
            return query;
        }
        public string QueryForUserAccess(User _User, string view)
        {
             string role="";
             string  query = "";
            using (var context = new TAS2013Entities())
                    {
                        role = context.UserRoles.Where(aa => aa.RoleID == _User.RoleID).FirstOrDefault().RoleName;
                    }
          
             
            switch (view)
            {
                case "Region":  query= UserAcccessGetRegion(_User,role);
                    return query;
                case "Zone": query = UserAcccessGetZone(_User, role);
                    return query;
                case "Location": query = UserAcccessGetLocation(_User, role);
                    return query;
                case "City": query = UserAccessGetCity(_User, role);
                    return query;
              
            
            
            
            }
            return query;
        
        
        }

        private string UserAcccessGetRegion(User _User,string role)
        { 
             string query ="";
             TAS2013Entities ctx = new TAS2013Entities();
             List<UserAccess> uAcc = new List<UserAccess>();
             uAcc = ctx.UserAccesses.Where(aa => aa.UserID == _User.UserID).ToList();
            switch (role)
            { 
                case "Region": 
                                 foreach(var uaccess in uAcc)
                                 {
                                 query = query + "RegionID = " + uaccess.CriteriaData+" or ";
                                 
                                 }
                                 query = query.Substring(0, query.Length - 4);
                    return query;
                case "City":   foreach (var uaccess in uAcc)
                                    {
                                        City city = ctx.Cities.Where(aa => aa.CityID == uaccess.CriteriaData).FirstOrDefault();
                                        query = query + "RegionID=" + city.RegionID + " or ";
                    
                    
                                         }
                     query = query.Substring(0, query.Length - 4);
                    return query;
                case "Zone": foreach (var uaccess in uAcc)
                                     {
                                         List<Region> region = ctx.Regions.Where(aa => aa.ZoneID == uaccess.CriteriaData).ToList();
                                         foreach (var re in region)
                                         {
                                             query = query + "RegionID=" + re.RegionID + " or ";
                                         
                                         }
                                        


                                         }
                               query = query.Substring(0, query.Length - 4);
                    return query;
                case "Location": foreach (var uaccess in uAcc)
                    {
                        Location loc = ctx.Locations.Where(aa => aa.LocID == uaccess.CriteriaData).FirstOrDefault();
                       City city = ctx.Cities.Where(aa => aa.CityID == loc.CityID).FirstOrDefault();
                       query = query + "RegionID=" + city.RegionID + " or ";

                        
                }query = query.Substring(0, query.Length - 4);
                    return query;
                case "SuperUser": query = "RegionID>0";
                    return query;
            
            
            }return query;
        }


        private string UserAcccessGetZone(User _User, string role)
        {
            string query = "";
            TAS2013Entities ctx = new TAS2013Entities();
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = ctx.UserAccesses.Where(aa => aa.UserID == _User.UserID).ToList();
            switch (role)
            {
                case "Region":
                    foreach (var uaccess in uAcc)
                    {
                        Region region = ctx.Regions.Where(aa => aa.RegionID == uaccess.CriteriaData).FirstOrDefault();
                        query = query + "ZoneID = " + region.ZoneID + " or ";

                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "City": foreach (var uaccess in uAcc)
                    {
                        City city = ctx.Cities.Where(aa => aa.CityID == uaccess.CriteriaData).FirstOrDefault();
                        Region region = ctx.Regions.Where(aa => aa.RegionID ==city.RegionID).FirstOrDefault();
                        query = query + "ZoneID=" + region.ZoneID + " or ";


                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "Zone": foreach (var uaccess in uAcc)
                    {
                        
                            query = query + "ZoneID=" + uaccess.CriteriaData + " or ";

                       



                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "Location": foreach (var uaccess in uAcc)
                    {
                        Location loc = ctx.Locations.Where(aa => aa.LocID == uaccess.CriteriaData).FirstOrDefault();
                        City city = ctx.Cities.Where(aa => aa.CityID == loc.CityID).FirstOrDefault();
                        Region region = ctx.Regions.Where(aa => aa.RegionID == city.RegionID).FirstOrDefault();
                        query = query + "ZoneID=" + region.ZoneID + " or ";


                    } query = query.Substring(0, query.Length - 4);
                    return query;
                case "SuperUser": query = "ZoneID>0";
                    return query;


            } return query;
        }


        private string UserAcccessGetLocation(User _User, string role)
        {
            string query = "";
            TAS2013Entities ctx = new TAS2013Entities();
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = ctx.UserAccesses.Where(aa => aa.UserID == _User.UserID).ToList();
            switch (role)
            {
                case "Region":
                    foreach (var uaccess in uAcc)
                    {
                        Region region = ctx.Regions.Where(aa => aa.RegionID == uaccess.CriteriaData).FirstOrDefault();
                        List<City> cities = ctx.Cities.Where(aa => aa.RegionID == region.RegionID).ToList();
                        foreach (var city in cities)
                        {
                            List<Location> loc = ctx.Locations.Where(aa => aa.CityID == city.CityID).ToList();
                            foreach (var lo in loc)
                            {
                                query = query + "LocID = " + lo.LocID + " or ";
                        
                            
                            }
                            
                        }
                       

                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "City": foreach (var uaccess in uAcc)
                    {
                        List<City> cities = ctx.Cities.Where(aa => aa.CityID == uaccess.CriteriaData).ToList();
                        foreach (var city in cities)
                        {
                            List<Location> loc = ctx.Locations.Where(aa => aa.CityID == city.CityID).ToList();
                            foreach (var lo in loc)
                            {
                                query = query + "LocID = " + lo.LocID + " or ";


                            }

                        }


                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "Zone": foreach (var uaccess in uAcc)
                    {
                       
                        Region region = ctx.Regions.Where(aa => aa.ZoneID == uaccess.CriteriaData).FirstOrDefault();
                        List<City> cities = ctx.Cities.Where(aa => aa.RegionID == region.RegionID).ToList();
                        foreach (var city in cities)
                        {
                            List<Location> loc = ctx.Locations.Where(aa => aa.CityID == city.CityID).ToList();
                            foreach (var lo in loc)
                            {
                                query = query + "LocID = " + lo.LocID + " or ";


                            }

                        }





                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "Location": foreach (var uaccess in uAcc)
                    {
                       
                        query = query + "LocID=" + uaccess.CriteriaData + " or ";


                    } query = query.Substring(0, query.Length - 4);
                    return query;
                case "SuperUser": query = "LocID>0";
                    return query;



            } return query;
        }


        private string UserAccessGetCity(User _User, string role)
        {
            string query = "";
            TAS2013Entities ctx = new TAS2013Entities();
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = ctx.UserAccesses.Where(aa => aa.UserID == _User.UserID).ToList();
            switch (role)
            {
                case "Region":
                    foreach (var uaccess in uAcc)
                    {
                        Region region = ctx.Regions.Where(aa => aa.RegionID == uaccess.CriteriaData).FirstOrDefault();
                        List<City> cities = ctx.Cities.Where(aa => aa.CityID == region.RegionID).ToList();

                        foreach (var city in cities)
                        {
                            query = query + "CityID=" + city.CityID + " or ";
                        
                        
                        }

                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "City": foreach (var uaccess in uAcc)
                    {

                        query = query + "CityID=" + uaccess.CriteriaData + " or ";


                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "Zone": foreach (var uaccess in uAcc)
                    {
                        Zone zone = ctx.Zones.Where(aa => aa.ZoneID == uaccess.CriteriaData).FirstOrDefault();
                        List<Region> region = ctx.Regions.Where(aa => aa.ZoneID == zone.ZoneID).ToList();
                        foreach (var re in region)
                        {
                            List<City> cities = ctx.Cities.Where(aa => aa.RegionID == re.RegionID).ToList();
                            foreach (var city in cities)
                            {
                                query = query + "CityID=" + city.CityID + " or ";

                            }
                            
                        }



                    }
                    query = query.Substring(0, query.Length - 4);
                    return query;
                case "Location": foreach (var uaccess in uAcc)
                    {
                        Location loc = ctx.Locations.Where(aa => aa.LocID == uaccess.CriteriaData).FirstOrDefault();
                        City city = ctx.Cities.Where(aa => aa.CityID == loc.CityID).FirstOrDefault();
                        query = query + "CityID=" + city.CityID + " or ";


                    } query = query.Substring(0, query.Length - 4);
                    return query;
                case "SuperUser": query = "CityID>0";
                    return query;


            } return query;
        
        
        }





        public string QueryForCompanyViewForLinq(User _User)
        {
            string query = "";
            switch (_User.RoleID)
            {
                case 1: query = "CompanyID > 0";
                    break;
                case 2:
                    query = "CompanyID= 1 or CompanyID = 2 ";
                    break;
                case 3:
                    query = "CompanyID>= 3";
                    break;
                case 4:
                    query = "CompanyID = " + _User.CompanyID.ToString();
                    break;
                case 5:
                    break;
            }
            return query;
        }

        internal string QueryForLocationTableSegerationForLinq(User LoggedInUser)
        {
            TAS2013Entities db = new TAS2013Entities();
            List<UserLocation> ulocs = new List<UserLocation>();
            List<string> _CriteriaForOrLoc = new List<string>();
            ulocs = db.UserLocations.Where(aa => aa.UserID == LoggedInUser.UserID).ToList();
            String query = "";
            foreach (var uloc in ulocs)
            {
                _CriteriaForOrLoc.Add(" LocID = " + uloc.LocationID + " ");
            }
            for (int i = 0; i < _CriteriaForOrLoc.Count - 1; i++)
            {
                query = query + _CriteriaForOrLoc[i] + " or ";
            }
            query = query + _CriteriaForOrLoc[_CriteriaForOrLoc.Count - 1];
            return query;
        }

        internal string QueryForRegionFromCitiesForLinq(IEnumerable<City> cities)
        {

            String query = "";
            int d = 1;
            foreach (var city in cities)
            {
                if (d < cities.Count())
                    query = query + "RegionID=" + city.RegionID + " or ";
                d++;
            }
            query = query + "RegionID=" + cities.Last().RegionID;
            return query;
        }
        public string QueryForCitySegration(User _user)
        {
            TAS2013Entities db = new TAS2013Entities();
            List<UserLocation> ulocs = new List<UserLocation>();
            List<string> _CriteriaForOrLoc = new List<string>();
            ulocs = db.UserLocations.Where(aa => aa.UserID == _user.UserID).ToList();
            string query = " where ";
            foreach (var uloc in ulocs)
            {
                Location addloc = db.Locations.Where(aa => aa.LocID == uloc.LocationID).First();
                _CriteriaForOrLoc.Add(" CityID = " + addloc.CityID + " ");

            }
            for (int i = 0; i < _CriteriaForOrLoc.Count - 1; i++)
            {
                query = query + _CriteriaForOrLoc[i] + " or ";
            }
            query = query + _CriteriaForOrLoc[_CriteriaForOrLoc.Count - 1];
            return query;

        }
        internal string QueryForShiftForLinq(User LoggedInUser)
        {
            TAS2013Entities db = new TAS2013Entities();
            List<UserLocation> ulocs = new List<UserLocation>();
            List<string> _CriteriaForOrLoc = new List<string>();
            ulocs = db.UserLocations.Where(aa => aa.UserID == LoggedInUser.UserID).ToList();
            string query = "";
            foreach (var uloc in ulocs)
            {
                _CriteriaForOrLoc.Add(" LocationID = " + uloc.LocationID + " ");
            }
            for (int i = 0; i < _CriteriaForOrLoc.Count - 1; i++)
            {
                query = query + _CriteriaForOrLoc[i] + " or ";
            }
            query = query + _CriteriaForOrLoc[_CriteriaForOrLoc.Count - 1];
            return query;
        }
        
    }
}