using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using Models;
using Models.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class FarmerController : BaseController<FarmerLogic>
    {
        public ActionResult Index()
        {
            var models = new FarmerModel();
            return View(BusinessObject.GetFarmerList());
        }

        public ActionResult FarmerAsserts(int FarmerId)
        {
            var models = new FarmerModel();
            return PartialView("_AddFarmerAsserts", BusinessObject.GetFarmerAssertList(FarmerId));
        }

        public decimal Tax { get; set; }

        public decimal Interest { get; set; }

        public decimal Depreciation { get; set; }

        public decimal BusinessRevenue { get; set; }

        public decimal ProductionCost { get; set; }

        public decimal GrossProfit { get; set; }

        public decimal GrossProfitTotal { get; set; }

        public decimal GrossProfitTotalMargin { get; set; }

        [HttpGet]
        public ActionResult UpdateFarmInfrastucture(
            int FarmInfrastuctureId,
            int FarmerId,
            string Shreds,
            string ShredsConditions,
            string ProcessingFacilities,
            string ProcessingFacilitiesConditions,
            string Packhouse,
            string StorageFacilities,
            string StorageFacilitiesConditions,
            string Office,
            string OfficeConditions,
            string Storage,
            string StorageConditions,
            string Fencing,
            string FencingConditions,
            string Livestock,
            string LivestockConditions,
            string LivestockInfrustructure,
            string LivestockInfrustructureCondition,
            string FishTunnels,
            string FishTunnelsConditions,
            string VegetableTunnels,
            string VegetableTunnelsConditions,
            string WaterTanks,
            string WaterTanksConditions,
            string FarmInfrastuctureOther,
            string FarmInfrastuctureOtherConditions,
            string AccesstoWater,
            string SourceofWater,
            string AccesstoPowerSupply,
            string SourceofPowerSupply,
            string NetworkCoverage,
            string NetworkCoverageCondition,
            string AvailabilityofSewerageServices,
            string SewerageServicesCondition
            )
        {
            FarmerModel model = new FarmerModel();
            model.FarmInfrastuctureId = FarmInfrastuctureId;
            model.FarmerId = FarmerId;
            model.Shreds = Shreds;
            model.ShredsConditions = ShredsConditions;
            model.ProcessingFacilities = ProcessingFacilities;
            model.ProcessingFacilitiesConditions = ProcessingFacilitiesConditions;
            model.Packhouse = Packhouse;
            model.StorageFacilities = StorageFacilities;
            model.StorageFacilitiesConditions = StorageFacilitiesConditions;
            model.Office = Office;
            model.OfficeConditions = OfficeConditions;
            model.Storage = Storage;
            model.StorageConditions = StorageConditions;
            model.Fencing = Fencing;
            model.FencingConditions = FencingConditions;
            model.Livestock = Livestock;
            model.LivestockConditions = LivestockConditions;
            model.LivestockInfrustructure = LivestockInfrustructure;
            model.LivestockInfrustructureCondition = LivestockInfrustructureCondition;
            model.FishTunnels = FishTunnels;
            model.FishTunnelsConditions = FishTunnelsConditions;
            model.VegetableTunnels = VegetableTunnels;
            model.VegetableTunnelsConditions = VegetableTunnelsConditions;
            model.WaterTanks = WaterTanks;
            model.WaterTanksConditions = WaterTanksConditions;
            model.FarmInfrastuctureOther = FarmInfrastuctureOther;
            model.FarmInfrastuctureOtherConditions = FarmInfrastuctureOtherConditions;
            model.AccesstoWater = AccesstoWater;
            model.SourceofWater = SourceofWater;
            model.AccesstoPowerSupply = AccesstoPowerSupply;
            model.SourceofPowerSupply = SourceofPowerSupply;
            model.NetworkCoverage = NetworkCoverage;
            model.NetworkCoverageCondition = NetworkCoverageCondition;
            model.AvailabilityofSewerageServices = AvailabilityofSewerageServices;
            model.SewerageServicesCondition = SewerageServicesCondition;

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFarmInfrustructure(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpdateTradingDetails(
            int TradingDetailsId,
            int FarmerId,
            string EntityName,
            int LegalEntityTypeId,
            int NumberofDirectors,
            string CoRegNo,
            string TaxRegNo,
            string VatRegNo,
            int Yearsinexistance)
        {
            FarmerModel model = new FarmerModel();
            model.TradingDetailsId = TradingDetailsId;
            model.FarmerId = FarmerId;
            model.EntityName = EntityName;
            model.LegalEntityTypeId = LegalEntityTypeId;
            model.NumberofDirectors = NumberofDirectors;
            model.CoRegNo = CoRegNo;
            model.TaxRegNo = TaxRegNo;
            model.VatRegNo = VatRegNo;
            model.Yearsinexistance = Yearsinexistance;

            if (ModelState.IsValid)
            {
                BusinessObject.SaveTradingDetails(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpdateFarmDescritionResource(
            int FarmDescritionResourceId,
            int FarmerId,
            int OwnershipTypeId,
            string Latitude,
            string Longitude,
            string Office,
            string AreaExtenstionOfficer,
            decimal LandSize,
            decimal LandUnderProduction,
            string LandSizeVerifiedbyaSurveyor,
            string FarmAccessibility,
            string SoilType,
            string SoilCondition,
            string SlopeGradiencyCondition
            )
        {
            FarmerModel model = new FarmerModel();
            model.FarmDescritionResourceId = FarmDescritionResourceId;
            model.FarmerId = FarmerId;
            model.OwnershipTypeId = OwnershipTypeId;
            model.Latitude = Latitude;
            model.Longitude = Longitude;
            model.Office = Office;
            model.AreaExtenstionOfficer = AreaExtenstionOfficer;
            model.LandSize = LandSize;
            model.LandUnderProduction = LandUnderProduction;
            model.LandSize = LandSize;
            model.LandSizeVerifiedbyaSurveyor = LandSizeVerifiedbyaSurveyor;
            model.FarmAccessibility = FarmAccessibility;
            model.SoilType = SoilType;
            model.SoilCondition = SoilCondition;
            model.SlopeGradiencyCondition = SlopeGradiencyCondition;

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFarmDescritionResource(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult UpdateFarmer(
            int CompanyId,
            int FarmerId,
            int AddressId,
            int ContactInformationId,
            string FirstName,
            string Surname,
            string IdNumber,
            string CellNumber,
            string PhoneNumber,
            string Email,
            string AddressLine1,
            string AddressLine2,
            string AddressLine3,
            string AddressLine4)
        {
            FarmerModel model = new FarmerModel();
            model.CompanyId = CompanyId;
            model.FarmerId = FarmerId;
            model.AddressId = AddressId;
            model.ContactInformationId = ContactInformationId;
            model.FirstName = FirstName;
            model.Surname = Surname;
            model.IdNumber = IdNumber;
            model.CellNumber = CellNumber;
            model.PhoneNumber = PhoneNumber;
            model.Email = Email;
            model.AddressLine1 = AddressLine1;
            model.AddressLine2 = AddressLine2;
            model.AddressLine3 = AddressLine3;
            model.AddressLine4 = AddressLine4;

            if (ModelState.IsValid)
            {
                BusinessObject.UpdateFarmer(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpdateEconomic(
            int FarmerSocioEconomicId,
            int FarmerId,
            int Permanent,
            int Seasonal,
            int NoofWomen,
            int NoofYouth,
            int EmployeeswithDisabilities,
            int MilitaryVeterans,
            string Notes)
        {
            FarmerModel model = new FarmerModel();
            model.FarmerSocioEconomicId = FarmerSocioEconomicId;
            model.FarmerId = FarmerId;
            model.Permanent = Convert.ToInt32(Permanent);
            model.Seasonal = Convert.ToInt32(Seasonal);
            model.NoofWomen = Convert.ToInt32(NoofWomen);
            model.NoofYouth = Convert.ToInt32(NoofYouth);
            model.EmployeeswithDisabilities = Convert.ToInt32(EmployeeswithDisabilities);
            model.MilitaryVeterans = Convert.ToInt32(MilitaryVeterans);
            model.Notes = Convert.ToString(Notes);

            if (ModelState.IsValid)
            {
                BusinessObject.SaveSocioEconomic(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SaveVegetable(
            int FarmerId,
            int ProductInfoId,
            string VegetableAvarageIncomeperAnnum,
            string Typeofproduction,
            string AverageIncome,
            string Government,
            string Informal,
            string Formal)
        {
            FarmerModel model = new FarmerModel();
            model.FarmerId = FarmerId;
            model.ProductInfoId = ProductInfoId;
            model.VegetableAvarageIncomeperAnnum = VegetableAvarageIncomeperAnnum;
            model.Typeofproduction = Typeofproduction;
            model.AverageIncome = AverageIncome;
            model.Government = Convert.ToBoolean(Government);
            model.Informal = Convert.ToBoolean(Informal);
            model.Formal = Convert.ToBoolean(Formal);

            if (ModelState.IsValid)
            {
                BusinessObject.SaveVegetable(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SaveDryCrop(
            int FarmerId,
            int ProductInfoId,
            string DryCropAvarageIncomeperAnnum,
            string Typeofproduction,
            string AverageIncome,
            string Government,
            string Informal,
            string Formal)
        {
            FarmerModel model = new FarmerModel();
            model.FarmerId = FarmerId;
            model.ProductInfoId = ProductInfoId;
            model.DryCropAvarageIncomeperAnnum = DryCropAvarageIncomeperAnnum;
            model.Typeofproduction = Typeofproduction;
            model.AverageIncome = AverageIncome;
            model.Government = Convert.ToBoolean(Government);
            model.Informal = Convert.ToBoolean(Informal);
            model.Formal = Convert.ToBoolean(Formal);

            if (ModelState.IsValid)
            {
                BusinessObject.SaveDryCrop(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SaveFruit(
            int FarmerId,
            int ProductInfoId,
            string FruitAvarageIncomeperAnnum,
            string Typeofproduction,
            string AverageIncome,
            string Government,
            string Informal,
            string Formal)
        {
            FarmerModel model = new FarmerModel();
            model.FarmerId = FarmerId;
            model.ProductInfoId = ProductInfoId;
            model.FruitAvarageIncomeperAnnum = FruitAvarageIncomeperAnnum;
            model.Typeofproduction = Typeofproduction;
            model.AverageIncome = AverageIncome;
            model.Government = Convert.ToBoolean(Government);
            model.Informal = Convert.ToBoolean(Informal);
            model.Formal = Convert.ToBoolean(Formal);

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFruit(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SaveLiveStock(
            int FarmerId,
            int ProductInfoId,
            string LiveStockAvarageIncomeperAnnum,
            string Typeofproduction,
            string AverageIncome,
            string Government,
            string Informal,
            string Formal)
        {
            FarmerModel model = new FarmerModel();
            model.FarmerId = FarmerId;
            model.ProductInfoId = ProductInfoId;
            model.LiveStockAvarageIncomeperAnnum = LiveStockAvarageIncomeperAnnum;
            model.Typeofproduction = Typeofproduction;
            model.AverageIncome = AverageIncome;
            model.Government = Convert.ToBoolean(Government);
            model.Informal = Convert.ToBoolean(Informal);
            model.Formal = Convert.ToBoolean(Formal);
                        
            if (ModelState.IsValid)
            {
                BusinessObject.SaveLiveStock(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult SaveFinancialPerformance(
            int FinancialPerformanceFarmerId,
            int FarmerId,
            string BusinessRevenue,
            string ProductionCost,
            string GrossProfitTotal,
            string Tax,
            string Interest,
            string Depreciation,
            string GrossProfitTotalMargin,
            string NetProfit,
            string EBITDA,
            string EBITDAMargin)
        {
            FarmerModel model = new FarmerModel();

            model.FinancialPerformanceFarmerId = FinancialPerformanceFarmerId;
            model.FarmerId = FarmerId;
            model.Tax = Convert.ToDecimal(Tax);
            model.Interest = Convert.ToDecimal(Interest);
            model.Depreciation = Convert.ToDecimal(Depreciation);
            model.BusinessRevenue = Convert.ToDecimal(BusinessRevenue);
            model.ProductionCost = Convert.ToDecimal(ProductionCost);
            model.GrossProfitTotal = Convert.ToDecimal(GrossProfitTotal);
            model.GrossProfitTotalMargin = Convert.ToDecimal(GrossProfitTotalMargin);
            model.EBITDA = Convert.ToDecimal(EBITDA);
            model.EBITDAMargin = Convert.ToDecimal(EBITDAMargin);

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFinancial(model);
                return PartialView("_FarmerFinancialPerformance", BusinessObject.GetFarmerFinancialList(model.FarmerId));
            }
            else
            {
                return PartialView("Farmer");
            }
        }

        public ActionResult FinancialPerformance(int FarmerId)
        {
            var models = new FarmerModel();
            return PartialView("_FarmerFinancialPerformance", BusinessObject.GetFarmerFinancialList(FarmerId));
        }

        public ActionResult FarmerPersonalDetails(int FarmerId)
        {
            var models = new FarmerModel();
            return PartialView("_AddFarmer", BusinessObject.GetFarmerList(FarmerId));
        }

        public ActionResult Grants(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateGrant(FarmerId);
            return PartialView("_FarrmerGrant", models);
        }

        public ActionResult DryCrop(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateDryCrop(FarmerId);
            return PartialView("_CommoditiesDryCrop", models);
        }

        public ActionResult Vegetable(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateVegetable(FarmerId);
            return PartialView("_CommoditiesVegetable", models);
        }

        public ActionResult Images(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateFarmerModelData(FarmerId);
            return PartialView("_AddFarmerImages", models);
        }
        
        public ActionResult Fruit(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateFruit(FarmerId);
            return PartialView("_CommoditiesFruit", models);
        }

        [HttpGet]
        public ActionResult DeleteVegetable(int CommodityVegetableId)
        {
            var models = new FarmerModel();
            BusinessObject.DeleteVegetable(CommodityVegetableId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult DeleteDryCrop(int CommodityDryCropId)
        {
            var models = new FarmerModel();
            BusinessObject.DeleteDryCrop(CommodityDryCropId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        } 

        [HttpGet]
        public ActionResult DeleteFruit(int CommodityFruitId)
        {
            var models = new FarmerModel();
            BusinessObject.DeleteFruit(CommodityFruitId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        } 

         [HttpGet]
        public ActionResult DeleteLivestock(int CommodityLivestockId)
        {
            var models = new FarmerModel();
            BusinessObject.DeleteLivestock(CommodityLivestockId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        } 

        public ActionResult Livestock(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateLivestock(FarmerId);
            return PartialView("_CommoditiesLivestock", models);
        }

        public ActionResult FarmerSocioEconomic(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateFarmerSocioEconomic(FarmerId);
            return PartialView("_AddFarmerSocioEconomic", models);
        }

        public ActionResult FarmerInfrastucture(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateFarmerInfrustucture(FarmerId);
            return PartialView("_AddFarmerInfrustructure", models);
        }


        public ActionResult TradingDetails(int FarmerbyId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateFarmerModelData(FarmerbyId);
            return PartialView("_AddFarmerTradingDetails", models);
        }

        public ActionResult FarmerDescriptionResources(int FarmerId)
        {
            var models = new FarmerModel();
            models = BusinessObject.CreateFarmerRecourcesModelData(FarmerId);
            return PartialView("_AddFarmerDescriptionResources", models);
        }

        public ActionResult GetFarmerbyId(string GetFarmerbyId)
        {
            var models = new FarmerModel();
            return PartialView("_AddFarmer", BusinessObject.GetFarmerList(GetFarmerbyId));
        }

        public ActionResult AddFarmer()
        {
            var models = new FarmerModel();
            return PartialView("_AddFarmer", models);
        }

        [HttpPost]
        public JsonResult UploadFile(string Id)
        {
            var model = new FarmerModel();
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];

                if (Request.Files != null && Request.Files.Count > 0)
                {
                    var files = Request.Files[0];
                    if (files != null && files.ContentLength > 0)
                    {
                        var content = new byte[files.ContentLength];
                        files.InputStream.Read(content, 0, files.ContentLength);
                        model.Image = content;
                        model.FarmerId = Convert.ToInt32(Id);
                        BusinessObject.SaveFarmerImage(model);
                    }
                }
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult SaveFarmer(string FirstName, string Surname, string EmailAddress, string BirthDate, string IdNumber, int CompanyId)
        {
            var models = new FarmerModel();
            models.FirstName = FirstName;
            models.Surname = Surname;
            models.Email = EmailAddress;
            models.InceptionDate = Convert.ToDateTime(BirthDate);
            models.IdNumber = IdNumber;
            models.CompanyId = CompanyId;

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFarmer(models);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("Farmer");
            }
        }
    }
}
