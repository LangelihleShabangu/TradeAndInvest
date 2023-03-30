using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ClientManagement.DomainModel;
using ClientManagement.DataObject;
using System;

namespace ClientManagement.BusinessLogic
{
    public class GlobalAmin
    {     

        public static List<ListItem> GetUserProfileInfo(List<UserProfileModel> UserProfileList)
        {
            List<ListItem> _items = new List<ListItem>();
            if (UserProfileList != null)
            {
                _items = UserProfileList.Select(x => new ListItem()
                {
                    Text = x.UserName,
                    Value = x.UserProfileId.ToString()
                }).ToList();
            }
            return _items;
        }

        public static List<ListItem> GetNeededAssistance()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "Rezoning", Value = "Rezoning" });
            _items.Add(new ListItem() { Text = "Business License", Value = "Business License" });
            _items.Add(new ListItem() { Text = "Building Plan", Value = "Building Plan" });
            _items.Add(new ListItem() { Text = "Bulk Services", Value = "Bulk Services" });
            _items.Add(new ListItem() { Text = "Other", Value = "Other" });
            return _items;
        }

        public static List<ListItem> GetCountry()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "None", Value = "None" });
            _items.Add(new ListItem() { Text = "South Africa", Value = "South Africa" });
            _items.Add(new ListItem() { Text = "Zambia", Value = "Zambia" });
            _items.Add(new ListItem() { Text = "Angola", Value = "Angola" });
            _items.Add(new ListItem() { Text = "Other", Value = "Other" }); 
            return _items;
        }

        public static List<ListItem> GetContinent()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "None", Value = "None" });
            _items.Add(new ListItem() { Text = "Africa", Value = "Africa" });
            _items.Add(new ListItem() { Text = "Europe", Value = "Europe" });
            _items.Add(new ListItem() { Text = "Antarctica", Value = "Antarctica" });
            _items.Add(new ListItem() { Text = "Australia", Value = "Australia" });
            _items.Add(new ListItem() { Text = "North America", Value = "North America" });
            _items.Add(new ListItem() { Text = "South America", Value = "South America" });                        
            _items.Add(new ListItem() { Text = "Asia", Value = "Asia" });
            _items.Add(new ListItem() { Text = "Global", Value = "Global" });
            return _items;
        }

        public static List<ListItem> YesNoOnly()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "No", Value = "No" });
            _items.Add(new ListItem() { Text = "Yes", Value = "Yes" });            
            return _items;
        }

        public static List<ListItem> YesNo()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "None", Value = "None" });
            _items.Add(new ListItem() { Text = "Yes", Value = "Yes" });
            _items.Add(new ListItem() { Text = "No", Value = "No" });
            return _items;
        }

      
        public static List<ListItem> GetTitleDataInfo(List<TitleModel> TitleList)
        {
            List<ListItem> _items = new List<ListItem>();
            if (TitleList != null)
            {
                _items = TitleList.Select(x => new ListItem()
                {
                    Text = x.Name,
                    Value = x.TitleId.ToString()
                }).ToList();
            }
            return _items;
        }

        public static List<ListItem> GetIndustryDataInfo(List<IndustryModel> IndustryList)
        {
            List<ListItem> _items = new List<ListItem>();
            if (IndustryList != null)
            {
                _items = IndustryList.Select(x => new ListItem()
                {
                    Text = x.Name,
                    Value = x.Name.ToString()
                }).ToList();
            }
            return _items;
        }

        public static List<ListItem> GetGenderDataInfo(List<GenderModel> GenderList)
        {
            List<ListItem> _items = new List<ListItem>();
            if (GenderList != null)
            {
                _items = GenderList.Select(x => new ListItem()
                {
                    Text = x.Name,
                    Value = x.GenderId.ToString()
                }).ToList();
            }
            return _items;
        }        

        public static List<ListItem> GetDocumentTypeDataInfo(List<DocumentTypeModel> DocumentTypeList)
        {
            List<ListItem> _items = new List<ListItem>();
            if (DocumentTypeList != null)
            {
                _items = DocumentTypeList.Select(x => new ListItem()
                {
                    Text = x.Name,
                    Value = x.DocumentTypeId.ToString()
                }).ToList();
            }
            return _items;
        }       

        public static List<ListItem> GetCategoryTypeData()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "Investor", Value = "Investor"});
            _items.Add(new ListItem() { Text = "Trade", Value = "Trade" });
            _items.Add(new ListItem() { Text = "Investor and Trade", Value = "Investor and Trade" });
            return _items;
        }

        public static List<ListItem> GetContryCodeData()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "South Africa +27", Value = "South Africa +27" });
            _items.Add(new ListItem() { Text = "Zambia +28", Value = "Zambia +28" });
            _items.Add(new ListItem() { Text = "Egypt +29", Value = "Egypt +29" });           
            return _items;
        }

        public static List<ListItem> GetIndustryData()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "Agro- processing (Agriculture)", Value = "Agro- processing (Agriculture)" });
            _items.Add(new ListItem() { Text = "Boatbuilding and Marine", Value = "Boatbuilding and Marine" });
            _items.Add(new ListItem() { Text = "Forestry, Timber, Paper, Pulp and Furniture", Value = "Forestry, Timber, Paper, Pulp and Furniture" });
            _items.Add(new ListItem() { Text = "Automotive", Value = "Automotive" });
            _items.Add(new ListItem() { Text = "Clothing, Textiles, Footwear and Leather", Value = "Clothing, Textiles, Footwear and Leather" });
            return _items;       
        }

        public static List<ListItem> GetContinentData()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "Africa", Value = "Africa" });
            _items.Add(new ListItem() { Text = "Europe", Value = "Europe" });
            _items.Add(new ListItem() { Text = "Asia", Value = "Asia" });
            _items.Add(new ListItem() { Text = "America(North and South)", Value = "America(North and South)" });            
            return _items;
        }

        public static List<ListItem> GetNumberEmployeesData()
        {
            List<ListItem> _items = new List<ListItem>();
            _items.Add(new ListItem() { Text = "Less than 10", Value = "Less than 10" });
            _items.Add(new ListItem() { Text = "10 to 50", Value = "10 to 50" });
            _items.Add(new ListItem() { Text = "50 to 100", Value = "50 to 100" });
            _items.Add(new ListItem() { Text = "100 to 500", Value = "100 to 500" });
            _items.Add(new ListItem() { Text = "More than 500", Value = "More than 500" });
            return _items;
        }

        public static List<ListItem> GetAreaDataInfo(List<AreaModel> AreaList)
        {
            List<ListItem> _items = new List<ListItem>();
            _items = AreaList.Select(x => new ListItem()
            {
                Text = x.Name,
                Value = x.AreaId.ToString()
            })
                       .ToList();
            return _items;
        }
      

        public static List<ListItem> GetAllMonthsData(bool includeNone = true)
        {
            List<ListItem> _items = new List<ListItem>();
            var List = new Dictionary<int, string>();

            for (int i = 1; i <= 12; i++)
            {
                List.Add(i, DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            }

            _items = List.Select(x => new ListItem()
            {
                Text = x.Value,
                Value = x.Key.ToString()
            }).ToList();
            return _items;
        }
    }

    public class ListItem
    {
        public string Text { get; internal set; }
        public string Value { get; internal set; }
    }
}
