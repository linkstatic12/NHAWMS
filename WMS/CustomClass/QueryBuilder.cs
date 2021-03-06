﻿using System;
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
           TAS2013Entities db = new TAS2013Entities();
            List<UserAccess> ulocs = new List<UserAccess>();
            ulocs = db.UserAccesses.Where(aa => aa.UserID == _user.UserID).ToList();
            foreach (var uloc in ulocs)
            {      
                if(uloc.Criteria.Trim() == "Z")
                _CriteriaForOrLoc.Add(" ZoneID = " + uloc.CriteriaData + " ");
                if (uloc.Criteria.Trim() == "R")
                    _CriteriaForOrLoc.Add(" RegionID = " + uloc.CriteriaData + " ");
                if (uloc.Criteria.Trim() == "C")
                    _CriteriaForOrLoc.Add(" CityID = " + uloc.CriteriaData + " ");
                if (uloc.Criteria.Trim() == "L")
                    _CriteriaForOrLoc.Add(" LocID = " + uloc.CriteriaData + " ");
                if (uloc.Criteria.Trim() == "S")
                    query = "";
                if (uloc.Criteria.Trim() == "")
                { 
                    
                    query = "";
                return query;
                
                }
            }
           
           
            for (int i = 0; i < _CriteriaForOrLoc.Count - 1; i++)
            {
                subQueryLoc = subQueryLoc + _CriteriaForOrLoc[i] + " or ";
            }
            if(_CriteriaForOrLoc.Count>0)
            subQueryLoc = " ( " + subQueryLoc + _CriteriaForOrLoc[_CriteriaForOrLoc.Count - 1] + " ) ";
           
            //subQuery = " ( ";
            //for (int i = 0; i < _CriteriaForOr.Count - 1; i++)
            //{
            //    subQuery = subQuery + _CriteriaForOr[i] + " or ";
            //} if (_CriteriaForOr.Count > 0)
            //subQuery = subQuery + _CriteriaForOr[_CriteriaForOr.Count - 1];
            //subQuery = subQuery + " ) ";
            query = query + subQueryLoc;
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
                case "SuperUser": query = "CompanyID >0";
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
                        query = query + "ZoneID= " + region.ZoneID + " or ";


                    } query = query.Substring(0, query.Length - 4);
                    return query;
                case "SuperUser": query = "ZoneID > 0 ";
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


        public string CheckForUserRole(User user)
        {
            string val = "";
            using (var ctx = new TAS2013Entities())
            {
                string criteria = ctx.UserAccesses.Where(aa => aa.UserID == user.UserID).FirstOrDefault().Criteria;
                switch (criteria.Trim())
                {
                    case "Z":
                        val = "Zone";
                        break;

                    case "R":
                        val="Region";
                        break;
                    case "C":
                        val = "City";
                        break;
                    case "L":
                        val = "Location";
                        break;
                        case "S":
                        val = "SuperUser";
                        break;
                }
                ctx.Dispose();
            }
            return val;
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

        #region -- Reports Filters Data Seggregation according to User Role--
        internal string QueryForRegionInFilters(User LoggedInUser)
        {
            string query = " where ";
            TAS2013Entities db = new TAS2013Entities();
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = db.UserAccesses.Where(aa => aa.UserID == LoggedInUser.UserID).ToList();
            List<Region> regions = db.Regions.ToList();
            List<City> cities = db.Cities.ToList();
            List<Location> locs = db.Locations.ToList();
            List<string> queryList = new List<string>();
            foreach (var access in uAcc)
            {
                switch (LoggedInUser.RoleID)
                {
                    case 1://Super ADmin
                        query = "";
                        break;
                    case 4://Zone
                        queryList.Add(" ZoneID =" + access.CriteriaData.ToString());
                        break;
                    case 5://REgion
                        queryList.Add(" RegionID =" + access.CriteriaData.ToString());
                        break;
                    case 6://City
                        string regionID = cities.Where(aa => aa.CityID == access.CriteriaData).FirstOrDefault().RegionID.ToString();
                        queryList.Add(" RegionID =" + regionID);
                        break;
                    case 7://Location
                         string regionIDForLoc = locs.Where(aa => aa.LocID == access.CriteriaData).FirstOrDefault().City.Region.RegionID.ToString();
                         queryList.Add(" RegionID =" + regionIDForLoc);
                        break;
                }
            }
            if (queryList.Count == 1)
            {
                query = query + queryList[0];
            }
            else if(queryList.Count>1)
            {
                for (int i = 0; i < queryList.Count - 1; i++)
                {
                    query = query + queryList[i] + " or ";
                }
                query = query + queryList[queryList.Count - 1];
            }


            return query;
        }

        internal string QueryForReportsCity(User LoggedInUser, string p)
        {
            string query = " where ";
            TAS2013Entities db = new TAS2013Entities();
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = db.UserAccesses.Where(aa => aa.UserID == LoggedInUser.UserID).ToList();
            List<Region> regions = db.Regions.ToList();
            List<City> cities = db.Cities.ToList();
            List<Location> locs = db.Locations.ToList();
            List<string> queryList = new List<string>();
            foreach (var access in uAcc)
            {
                switch (LoggedInUser.RoleID)
                {
                    case 1://Super ADmin
                        query = "";
                        break;
                    case 4://Zone
                        List<City> city = db.Cities.Where(aa => aa.Region.ZoneID== access.CriteriaData).ToList();
                        foreach (var c in city)
                        {
                            queryList.Add(" CityID =" + c.CityID);
                        }

                        break;
                    case 5://REgion
                        city = db.Cities.Where(aa => aa.RegionID == access.CriteriaData).ToList();
                        foreach (var c in city)
                        {
                            queryList.Add(" CityID =" + c.CityID);
                        }
                        break;
                    case 6://City
                        string cityID = cities.Where(aa => aa.CityID == access.CriteriaData).FirstOrDefault().CityID.ToString();
                        queryList.Add(" CityID =" + cityID);
                        break;
                    case 7://Location
                        string cityIDForLoc = locs.Where(aa => aa.LocID == access.CriteriaData).FirstOrDefault().CityID.ToString();
                        queryList.Add(" CityID =" + cityIDForLoc);
                        break;
                }
            }
            if (queryList.Count == 1)
            {
                query = query + queryList[0];
            }
            else if (queryList.Count > 1)
            {
                for (int i = 0; i < queryList.Count - 1; i++)
                {
                    query = query + queryList[i] + " or ";
                }
                query = query + queryList[queryList.Count - 1];
            }


            return query;
        }

        internal string QueryForLocReport(User LoggedInUser)
        {
            string query = " where ";
            TAS2013Entities db = new TAS2013Entities();
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = db.UserAccesses.Where(aa => aa.UserID == LoggedInUser.UserID).ToList();
            List<Region> regions = db.Regions.ToList();
            List<City> cities = db.Cities.ToList();
            List<Location> locss = db.Locations.ToList();
            List<string> queryList = new List<string>();
            foreach (var access in uAcc)
            {
                switch (LoggedInUser.RoleID)
                {
                    case 1://Super ADmin
                        query = "";
                        break;
                    case 4://Zone
                        List<Location> locs = db.Locations.Where(aa => aa.City.Region.ZoneID == access.CriteriaData).ToList();
                        foreach (var c in locs)
                        {
                            queryList.Add(" LocID =" + c.LocID);
                        }

                        break;
                    case 5://REgion
                        locs = db.Locations.Where(aa => aa.City.RegionID == access.CriteriaData).ToList();
                        foreach (var c in locs)
                        {
                            queryList.Add(" LocID =" + c.LocID);
                        }
                        break;
                    case 6://City
                        locs = db.Locations.Where(aa => aa.CityID == access.CriteriaData).ToList();
                        foreach (var c in locs)
                        {
                            queryList.Add(" LocID =" + c.LocID);
                        }
                        break;
                    case 7://Location
                        string cityIDForLoc = locss.Where(aa => aa.LocID == access.CriteriaData).FirstOrDefault().LocID.ToString();
                        queryList.Add(" LocID =" + cityIDForLoc);
                        break;
                }
            }
            if (queryList.Count == 1)
            {
                query = query + queryList[0];
            }
            else if (queryList.Count > 1)
            {
                for (int i = 0; i < queryList.Count - 1; i++)
                {
                    query = query + queryList[i] + " or ";
                }
                query = query + queryList[queryList.Count - 1];
            }


            return query;
        }

        internal string QueryForEmployeeReports(User LoggedInUser)
        {
            string query = " where ";
            TAS2013Entities db = new TAS2013Entities();
            List<UserAccess> uAcc = new List<UserAccess>();
            uAcc = db.UserAccesses.Where(aa => aa.UserID == LoggedInUser.UserID).ToList();
            List<Region> regions = db.Regions.ToList();
            List<City> cities = db.Cities.ToList();
            List<Location> locs = db.Locations.ToList();
            List<string> queryList = new List<string>();
            foreach (var access in uAcc)
            {
                switch (LoggedInUser.RoleID)
                {
                    case 1://Super ADmin
                        query = "";
                        break;
                    case 4://Zone
                        queryList.Add(" ZoneID =" + access.CriteriaData.ToString());
                        break;
                    case 5://REgion
                        queryList.Add(" RegionID =" + access.CriteriaData.ToString());
                        break;
                    case 6://City

                        queryList.Add(" CityID =" + access.CriteriaData.ToString());
                        break;
                    case 7://Location
                        queryList.Add(" LocID =" + access.CriteriaData.ToString());
                        break;
                }
            }
            if (queryList.Count == 1)
            {
                query = query + queryList[0];
            }
            else if (queryList.Count > 1)
            {
                for (int i = 0; i < queryList.Count - 1; i++)
                {
                    query = query + queryList[i] + " or ";
                }
                query = query + queryList[queryList.Count - 1];
            }


            return query;
        }

        #endregion
    }
}