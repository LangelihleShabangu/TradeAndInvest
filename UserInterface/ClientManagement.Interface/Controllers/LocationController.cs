using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using Rubix.UI.Models;
using System.Linq;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class LocationController : BaseController<LocationLogic> 
    {

        public ActionResult GetLocation(int companyId, string restrition = "South Africa", string submitbtn = "Add")
        {        
            var model = new ProjectLocationSelector();
            model.CompanyId = companyId;
            model.Restriction = restrition;
            model.submitbtn = submitbtn;
            return View(model);
        }

        public ActionResult GetAgencyLocation(int AgencyId, string restrition = "South Africa", string submitbtn = "Add")
        {
            var model = new ProjectLocationSelector();
            model.CompanyId = AgencyId;
            model.Restriction = restrition;
            model.submitbtn = submitbtn;
            return View(model);
        }

        public ActionResult GetBuyerLocation(int BuyerId, string restrition = "South Africa", string submitbtn = "Add")
        {
            var model = new ProjectLocationSelector();
            model.CompanyId = BuyerId;
            model.Restriction = restrition;
            model.submitbtn = submitbtn;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateLocation(ProjectLocationSelector model)
        {
            var jsonSerial = new System.Web.Script.Serialization.JavaScriptSerializer();

            var jobject = jsonSerial.Deserialize<GoogleModel[]>(model.CurrentResults);

            int wardid = 0;
            string restriction = string.Empty;
            string place = string.Empty;
            string placeLong = string.Empty;
            string placeLat = string.Empty;
            string county = string.Empty;
            Province province = null;
            Municipality munic = null;
            Region region = null;
            Location location = null;

            if (jobject.Count() > 0)
            {
                foreach (var address in jobject)
                {
                    if (address.types.Contains("country"))
                    {
                        if (string.IsNullOrEmpty(county))
                        {
                            county = GetLongName(address, "country");

                        }
                    }
                    else
                        if (address.types.Contains("administrative_area_level_1"))
                        {
                            if (province == null)
                            {

                                province = new Province()
                                {
                                    Name = GetLongName(address, "administrative_area_level_1")
                                };

                                var id = BusinessObject.GetProvince(province.Name);
                                if (id == 0)
                                {
                                    province.Latitude = address.geometry.location.GetLatitude();
                                    province.Longitude = address.geometry.location.GetLongitude();
                                    province = BusinessObject.ManageProvince(province);
                                }
                                else
                                {
                                    province = BusinessObject.GetProvince(id);
                                }
                            }
                        }
                        else if (address.types.Contains("administrative_area_level_3"))
                        {
                            if (munic == null)
                            {
                                munic = new Municipality()
                                {
                                    Name = GetLongName(address, "administrative_area_level_3")
                                };

                                var id = BusinessObject.GetMunicipality(munic.Name);
                                if (id == 0)
                                {
                                    munic.Latitude = address.geometry.location.GetLatitude();
                                    munic.Longitude = address.geometry.location.GetLongitude();
                                    munic.RegionId = 0;
                                }
                                else
                                {
                                    munic = BusinessObject.GetMunicipality(id);
                                }


                            }
                        }
                        else if (address.types.Contains("administrative_area_level_2"))
                        {
                            if (region == null)
                            {
                                region = new Region()
                                {
                                    Name = GetLongName(address, "administrative_area_level_2")
                                };

                                var id = BusinessObject.GetRegion(region.Name);
                                if (id == 0)
                                {
                                    region.Latitude = address.geometry.location.GetLatitude();
                                    region.Longitude = address.geometry.location.GetLongitude();
                                    region.ProvinceId = 0;
                                }
                                else
                                {
                                    region = BusinessObject.GetRegion(id);
                                }

                            }
                        }
                        else if (address.types.Contains("locality"))
                        {
                            if (munic == null)
                            {
                                munic = new Municipality()
                                {
                                    Name = GetLongName(address, "locality")
                                };

                                var id = BusinessObject.GetMunicipality(munic.Name);
                                if (id == 0)
                                {
                                    munic.Latitude = address.geometry.location.GetLatitude();
                                    munic.Longitude = address.geometry.location.GetLongitude();
                                    munic.RegionId = 0;
                                }
                                else
                                {
                                    munic = BusinessObject.GetMunicipality(id);
                                }


                            }
                        }
                        else if (!address.types.Contains("street"))
                        {
                            place = address.formatted_address;
                            placeLong = address.geometry.location.GetLatitude();
                            placeLat = address.geometry.location.GetLongitude();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(place))
                            {
                                place = GetLongName(address, address.types[0]);
                            }
                        }
                }


                if (province.ProvinceId == 0)
                    return JavaScript("alert('Error Could not find the selected province')");


                restriction = string.Format("{0}", county);
                restriction = string.Format("{0},{1}", restriction, province.Name);

                if (region.RegionId == 0)
                {
                    region.ProvinceId = province.ProvinceId;
                    region = BusinessObject.ManageRegion(region);
                }

                restriction = string.Format("{0},{1}", restriction, region.Name);
                if (munic.MunicipaltyId == 0)
                {
                    munic.RegionId = region.RegionId;
                    munic = BusinessObject.ManageMunicipality(munic);
                }

                restriction = string.Format("{0},{1}", restriction, munic.Name);

                wardid = BusinessObject.GetWard(place);
                if (wardid == 0)
                {
                    location = new Location();
                    location.MunicipalityId = munic.MunicipaltyId;
                    location.Name = place;
                    location.Longitude = placeLong;
                    location.Latitude = placeLat;
                    location = BusinessObject.ManageWard(location);
                    wardid = location.WardId;
                }
                else
                {
                    location = BusinessObject.GetWard(wardid);
                }

                restriction = string.Format("{0},{1}", restriction, location.Name);
            }

            return Json(new { id = model.CompanyId, locationid = munic.MunicipaltyId, restrictionArea = restriction, submitbtn = model.submitbtn, locationId = location.WardId }, JsonRequestBehavior.AllowGet);
        }

        private string GetLongName(GoogleModel model, string name)
        {
            var address = model.address_components.FirstOrDefault(p =>
               p.types.Contains(name)
            );

            return address.long_name;
        }

        private GoogleModel GetObjByMaxCount(GoogleModel[] jobject)
        {
            int max = 0;
            GoogleModel current = null;
            foreach (var item in jobject)
            {
                var count = item.address_components.Count();

                if (count > max)
                {
                    max = count;
                    current = item;
                }
            }

            return current;
        }

    }
}
