using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ClientManagement.Interface
{
    public class CrmQuery
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Description { get; internal set; }
        public string address1_line1 { get; set; }
        public string address1_line2 { get; set; }
        public string address1_line3 { get; set; }
        public string addresslinecity { get; set; }
        public string addresslineprovince { get; set; }
        public string address1_postalcode { get; set; }
    }

    public class SaveBusinessLogic
    {
        public static string ClientId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ClientId"].ToString();
            }
        }//"7d92808b-3679-4879-abdf-41a58739db6c";

        public static string ClientSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ClientSecret"].ToString();
            }
        }//"eyQ7Q~c0Y4ZNOYxv2Io1po1m438vBEbui1wDF";

        public static string ResourceUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["resourceUrl"].ToString();
            }
        }//"https://informaltrader.crm4.dynamics.com";

        public static string AccessTokenGenerator()
        {
            string authority = "https://login.microsoftonline.com/0824d779-ab6e-4b2f-b001-cf2995fc7db6";
           
            ClientCredential credentials = new ClientCredential(ClientId, ClientSecret);
            var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3; // only allow TLSV1.2 and SSL3            

            var result = authContext.AcquireTokenAsync(ResourceUrl, credentials);
            return Convert.ToString(result.Result.AccessToken);
        }

        public Guid CreateCRMQueryAccount(CrmQuery query)
        {
            var contact = JObject.FromObject(new
            {
                firstname = query.FirstName,
                lastname = query.LastName,
                emailaddress1 = query.Email,
                mobilephone = query.ContactNumber
            });
            contact.Add(new JProperty("address1_line1", query.address1_line1));
            contact.Add(new JProperty("address1_line2", query.address1_line2));
            contact.Add(new JProperty("address1_line3", query.address1_line3));
            contact.Add(new JProperty("address1_postalcode", query.address1_postalcode));
            contact.Add(new JProperty("address1_city", query.addresslinecity));
            contact.Add(new JProperty("address1_stateorprovince", query.addresslineprovince));

            return InsertDynamics365Entity("contacts", "create", contact, AccessTokenGenerator());           
        }

        public Guid NewFacilitationDocument(string DocumentName, string DocumentDescription, string DocumentLocationURL, Guid CRMDocumentManagementId, Guid CRMOpportunityId, Guid CRMLeadId, Guid CrmContactId, int DocumentManagementId, string DocumentTypeInfo)
        {            
            JObject crmFacilitationDocument = JObject.FromObject(new
            {
                rif_name = DocumentName,
                rif_description = DocumentDescription,
                rif_documenttype = DocumentTypeInfo,
                rif_documenturl = DocumentLocationURL,
                rif_deddocid = DocumentManagementId.ToString()
            });

            // provide leadId or crmLeadId
            if (CRMOpportunityId != Guid.Empty)
            {
                crmFacilitationDocument.Add("rif_opportunity@odata.bind", string.Format("/opportunities({0})", CRMOpportunityId.ToString()));
            }
            else if (CRMLeadId != Guid.Empty)
            {
                crmFacilitationDocument.Add("rif_leadid@odata.bind", string.Format("/leads({0})", CRMLeadId.ToString()));
            }

            return InsertDynamics365Entity("document", "rif_facilitationdocuments", crmFacilitationDocument, AccessTokenGenerator());
        }

        public Guid NewFacilitationRequest(string Title, string Description,string Documenturl, Guid CRMAccountId, Guid CRMOpportunityId, Guid CRMContactId, int FacilitationRequestId, Guid CRMLeadId)
        {
            JObject crmFacilitationRequest = JObject.FromObject(new { });

            crmFacilitationRequest.Add(new JProperty("rif_name", Title));
            crmFacilitationRequest.Add(new JProperty("rif_documenttype", "908510001"));
            crmFacilitationRequest.Add(new JProperty("rif_description", Description));
            crmFacilitationRequest.Add(new JProperty("rif_documenturl", Documenturl));

            // OpportunityId
            if (CRMLeadId != Guid.Empty)
            {
                crmFacilitationRequest.Add("rif_leadid@odata.bind", string.Format("/leads({0})", CRMLeadId.ToString()));
            }
            if (CRMOpportunityId != Guid.Empty)
            {
                crmFacilitationRequest.Add("rif_opportunityid@odata.bind", string.Format("/opportunities({0})", CRMOpportunityId.ToString()));
            }
            //// AccountId
            //crmFacilitationRequest.Add("customerid_account@odata.bind", string.Format("/accounts({0})", CRMAccountId.ToString()));

            ////ContactId
            //crmFacilitationRequest.Add("primarycontactid@odata.bind", string.Format("/contacts({0})", CRMContactId.ToString()));

            return InsertDynamics365Entity("rif_facilitationdocuments", "create", crmFacilitationRequest, AccessTokenGenerator());
        }

        public Guid NewLead(
            string ProjectName,
            string Firstname,
            string Surname,
            string CellNumber,
            string PhoneNumber,
            string Email,
            string CompanyName,
            string Address_Line1,
            string Address_Line2,
            string Address_Line3,
            string AddressLineCity,
            string AddressLineState_Province,
            string AddressLinePostalCode,
            string AddressLineCountry,
            string ProjectDescription,
            string ProjectChallangesConstraints,
            string ProjectStatus,
            string Expectations,
            string FuturePlans,
            decimal Totals,
            int JobsOpportunities,
            int Temporary,
            int Permanent,
            Guid CRMLeadId,
            Guid CRMOpportunityId,
            Guid CRMAccountId,
            Guid CRMContactId,
            int FacilitationId)
        {
            
            JObject crmLead = JObject.FromObject(new
            {
                rif_dedleadid = FacilitationId.ToString(),
                subject = ProjectName,
                firstname = Firstname,
                lastname = Surname,
                jobtitle = "Director",
                telephone1 = CellNumber,
                mobilephone = PhoneNumber,
                emailaddress1 = Email,               

                companyname = CompanyName,
                websiteurl = "www.google.com",
                address1_line1 = Address_Line1,
                address1_line2 = Address_Line2,
                address1_line3 = Address_Line3,
                address1_city = AddressLineCity,
                address1_stateorprovince = AddressLineState_Province,
                address1_postalcode = AddressLinePostalCode,
                address1_country = AddressLineCountry,

                rif_projectdescription = ProjectDescription,
                rif_projectchallengesandconstraints = ProjectChallangesConstraints,
                rif_projectstatus = ProjectStatus,
                rif_projectexpectations = Expectations,
                rif_futureplans = FuturePlans,
                estimatedamount = Totals,
                rif_jobopportunities = JobsOpportunities,
                rif_temporaryjobs = Temporary,
                rif_permanentjobs = Permanent,
            });

            //// LeadId for updating
            //if (CRMLeadId != Guid.Empty)
            //{
            //    crmLead.Add("leadid", CRMLeadId.ToString());
            //}

            //if (CRMOpportunityId != Guid.Empty)
            //{
            //    crmLead.Add("opportunityid", CRMOpportunityId.ToString());
            //}
            ////contactId
            //crmLead.Add("parentcontactid@odata.bind", string.Format("/contacts({0})", CRMContactId.ToString()));

            //// AccountId
            //crmLead.Add("parentaccountid@odata.bind", string.Format("/accounts({0})", CRMAccountId.ToString()));

            return InsertDynamics365Entity("leads", "create", crmLead, AccessTokenGenerator());
        }

        public Guid NewOpportunity(
            string ProjectName,
            string ProjectDescription,
            string ProjectChallangesConstraints,
            string ProjectStatus,
            string Expectations,
            string FuturePlans,
            decimal Totals,
            int JobsOpportunities,
            int Temporary,
            int Permanent,
            Guid CRMOpportunityId,
            Guid CRMAccountId,
            Guid CRMContactId)
        {            
            JObject crmOppotunity = JObject.FromObject(new
            {
                subject = ProjectName,
                rif_projectdescription = ProjectDescription,
                rif_projectchallenges = ProjectChallangesConstraints,
                rif_projectstatus = ProjectStatus,
                rif_projectexpectations = Expectations,
                rif_futureplans = FuturePlans,
                estimatedvalue = Totals,
                rif_jobopportunities = JobsOpportunities,
                rif_temporaryjobs = Temporary,
                rif_permanantjobs = Permanent,
            });

            // CRMOpportunityId for updating
            if (CRMOpportunityId != Guid.Empty)
            {
                crmOppotunity.Add("opportunityid", CRMOpportunityId.ToString());
            }
            //contactId
            crmOppotunity.Add("parentcontactid@odata.bind", string.Format("/contacts({0})", CRMContactId.ToString()));

            //AccountId
            crmOppotunity.Add("customerid_account@odata.bind", string.Format("/accounts({0})", CRMAccountId.ToString()));
            
            return InsertDynamics365Entity("opportunities", "create", crmOppotunity, AccessTokenGenerator());
        }

        public Guid NewSubscription(
            string Firstname,
            string Lastname,
            string EmailAddress,
            Guid CRMContactId,
            string CompanyName,
            string Address_Line1,
            string Address_Line2,
            string Address_Line3,
            string AddressLineCity,
            string AddressLineState_Province,
            string AddressLinePostalCode,
            string AddressLineCountry,
            Guid CRMUserProfileId,
            bool Woman_Owned_Business,
            int Industrycode,
            bool Planning_to_Export,
            bool IJHBCompany_Based,
            string Which_Region,
            string Export_Which_Region,
            string Export_Which_Country,
            bool Exporter,
            int Number_of_years_you_have_exported,
            int Number_of_Employees,
            decimal Companys_annual_turnover,
            string Export_products,
            string Export_markets,
            string Exporter_Region,
            string Exporter_Country,
            bool Government_export_assistance,
            string Specify_Company_Location,
            string Additional_Comments,
            string Challenges_facing_Exporter,
            string Other_issues,
            int UserProfileId,
            string DocumentLocationURL,
            string Phone_Number,
            string Contact_Phone)
        {
            
            if (string.IsNullOrEmpty(Firstname))
            {
                var namearray = Firstname.Split(' ');

                Firstname = namearray[0];
                if (namearray.Length > 1)
                {
                    Lastname = namearray[namearray.Length - 1];
                }
            }

            JObject crmContact = JObject.FromObject(new
            {
                firstname = Firstname,

                lastname = Lastname,
                jobtitle = "New",
                emailaddress1 = EmailAddress,
                rif_dedcontactid = UserProfileId.ToString()
            });
           
            JObject crmAccount = JObject.FromObject(new {  });
            crmAccount.Add(new JProperty("rif_addressline1", Address_Line1));
            crmAccount.Add(new JProperty("rif_addressline2", Address_Line2));
            crmAccount.Add(new JProperty("rif_addressline3", Address_Line3));
            crmAccount.Add(new JProperty("rif_addressline4", AddressLineCity));
            crmAccount.Add(new JProperty("rif_areyouanexporter", Exporter ? "908510001" : "908510002"));
            crmAccount.Add(new JProperty("rif_areyouplanningtoexport", Planning_to_Export ? "908510001" : "908510002"));
            crmAccount.Add(new JProperty("rif_companyname", CompanyName));
            crmAccount.Add(new JProperty("rif_emailaddress", EmailAddress));
            crmAccount.Add(new JProperty("rif_ifyeswhichregion", "908510001"));
            crmAccount.Add(new JProperty("rif_firstname", Firstname));
            crmAccount.Add(new JProperty("rif_surname", Lastname)); 
            crmAccount.Add(new JProperty("rif_isyourcompanybasedwithinthecityofjohannesb", IJHBCompany_Based ? true : true));
            crmAccount.Add(new JProperty("rif_noofyearsinoperation", Number_of_years_you_have_exported));
            crmAccount.Add(new JProperty("rif_contactphone", Contact_Phone));
            crmAccount.Add(new JProperty("rif_phonenumber", Phone_Number)); 
            crmAccount.Add(new JProperty("rif_productservices", "Testing")); 
            crmAccount.Add(new JProperty("rif_whichcountry", "908510001"));
            crmAccount.Add(new JProperty("rif_womanownedbusiness", Woman_Owned_Business ? true : true));
            crmAccount.Add(new JProperty("rif_document", DocumentLocationURL));
            
            return InsertDynamics365Entity("rif_subscriptions", "create", crmAccount, AccessTokenGenerator());
        }

        internal Guid NewSubscription(string firstName, string surname, string email_Address, object cRMContactId, string companyName, string address_Line1, string address_Line2, string address_Line3, string address_Line4, object addressLineState_Province, object addressLinePostalCode, object addressLineCountry, object cRMAccountId, bool v1, int v2, bool v3, bool v4, string which_Region1, string which_Region2, string which_Region3, bool v5, int v6, int v7, decimal v8, string empty1, string empty2, string empty3, string empty4, bool v9, string empty5, string empty6, string empty7, string empty8, object userProfileId, object documentLocationURL)
        {
            throw new NotImplementedException();
        }

        public Guid NewAssessmentDetails(
            string Firstname,
            string Lastname,
            string EmailAddress,
            Guid CRMContactId,
            string CompanyName,
            string Address_Line1,
            string Address_Line2,
            string Address_Line3,
            string AddressLineCity,
            string AddressLineState_Province,
            string AddressLinePostalCode,
            string AddressLineCountry,
            Guid CRMUserProfileId,
            int Number_of_years_you_have_exported,
            int Number_of_Employees,
            decimal Companys_annual_turnover,
            string Export_products,
            string Export_markets,
            string Exporter_Region,
            string Exporter_Country,
            string Government_export_assistance,
            string Specify_Company_Location,
            string Additional_Comments,
            string Challenges_facing_Exporter,
            string Other_issues,
            int UserProfileId,
            string DocumentLocationURL,
            bool chkExporter_Education_Seminar,
            bool chkParticipate_at_international_Trade,
            bool chkMarket_Research,
            bool chkGuidance_on_export,
            bool chkFinancial_Assistance,
            bool chkConsultation_with_trade_specialist,
            bool chkInternational_Trade_Missions,
            bool chkTrade_Leads,
            bool chkShipping_Logistics_consulting,
            bool chkOther,
            string rif_subscriptionid
            )
        {
            JObject crmAccount = JObject.FromObject(new { });

            //// rif_subscriptionid for updating
            //if (!string.IsNullOrEmpty(rif_subscriptionid))
            //{
            //    //crmAccount.Add("rif_subscriptionid", rif_subscriptionid);
            //    crmAccount.Add(new JProperty("rif_subscriptionid", rif_subscriptionid));
            //}

            crmAccount.Add(new JProperty("rif_numberofyearsyouhaveexported", Number_of_years_you_have_exported));

            crmAccount.Add(new JProperty("rif_howmanyemployeesdoesyourcompanyhave", "908510000"));

            crmAccount.Add(new JProperty("rif_whatisyourcompanysannualturnover",Convert.ToDouble(Companys_annual_turnover)));

            crmAccount.Add(new JProperty("rif_whichindustrysectorisyourcompanyin", "908510003"));

            crmAccount.Add(new JProperty("rif_areyouanexporter", true ? "908510001" : "908510002"));

            crmAccount.Add(new JProperty("rif_areyouplanningtoexport", true ? "908510001" : "908510002"));

            crmAccount.Add(new JProperty("rif_companyname", CompanyName));

            crmAccount.Add(new JProperty("rif_whatareyourmainexportmarkets", Export_markets));

            crmAccount.Add(new JProperty("rif_ifyeswhichregion", "908510001"));

            crmAccount.Add(new JProperty("rif_firstname", Firstname));

            crmAccount.Add(new JProperty("rif_surname", Lastname));

            crmAccount.Add(new JProperty("rif_isyourcompanybasedwithinthecityofjohannesb", true ? true : false));

            crmAccount.Add(new JProperty("rif_hatareyourmainexportproducts", Export_products));

            crmAccount.Add(new JProperty("rif_whichregionalexportmarketsareyouplanningto", "908510000")); //Exporter_Region

            crmAccount.Add(new JProperty("rif_doesyourproductrequireanyspecialtechnicalsu", Export_products));

            crmAccount.Add(new JProperty("rif_isyourcompanycurrentlyreceivinganygovernment", "908510001"));

            crmAccount.Add(new JProperty("rif_ifyespleasespecifylocation", Specify_Company_Location));

            crmAccount.Add(new JProperty("rif_additionalcomments", Additional_Comments));

            crmAccount.Add(new JProperty("rif_whatchallengesareyoucurrentlyfacingasanexp", Challenges_facing_Exporter));

            crmAccount.Add(new JProperty("rif_emailaddress", EmailAddress));  

            crmAccount.Add(new JProperty("rif_anyotherissuesareasofconcernthathavenot", Other_issues));

            crmAccount.Add(new JProperty("rif_whichcountry", "908510001"));
           
            return InsertDynamics365Entity("rif_subscriptions", "create", crmAccount, AccessTokenGenerator());
        }       

        private Guid InsertDynamics365Entity(string entity, string operation, JObject crmEntity, string accessToken)
        {
            Guid result = Guid.Empty;
            using (var client = new HttpClient())
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["CrmUrl"].ToString();
                client.BaseAddress = new Uri(url);
                client.Timeout = new TimeSpan(0, 2, 0);
                client.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                client.DefaultRequestHeaders.Add("OData-Version", "4.0");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //set Create request and content 
                HttpRequestMessage createRequest = new HttpRequestMessage(HttpMethod.Post, entity)
                {
                    Content = new StringContent(crmEntity.ToString(), Encoding.UTF8, "application/json")
                };

                HttpResponseMessage createResponse = client.SendAsync(createRequest).GetAwaiter().GetResult();
                try
                {
                    //verify successful call 
                    createResponse.EnsureSuccessStatusCode();
                    //get result content 
                    string leadURL = createResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                    //extract GUID from URL
                    result = Guid.Parse(Regex.Match(leadURL, @"\(([^)]*)\)").Groups[1].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Something is wrong: {ex.Message}");
                }
                return result;
            }
        }

        public bool RegisterDatabaseUser(string Username)
        {
            try
            {
                SqlDataReader rdr = null;
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("CREATE USER [{0}] FOR LOGIN [{0}]; EXEC sp_addrolemember 'db_owner', '{0}'", Username); ;

                        var exc = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        return true;
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close();

                        if (Con != null)
                            Con.Close();
                    }
                    return true;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public bool RegisterUser(string Username, string Password)
        {
            try
            {
                SqlDataReader rdr = null;
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("CREATE LOGIN [{0}] WITH password='{1}'", Username, Password); ;

                        var exc = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        return true;
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close();

                        if (Con != null)
                            Con.Close();
                    }
                    return true;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public bool RegisterUserNew(int UserId, string Password)
        {
            try
            {
                SqlDataReader rdr = null;
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = string.Format("usp_CreateUserDetails"); ;

                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;

                        var exc = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        return true;
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close();

                        if (Con != null)
                            Con.Close();
                    }
                    return true;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public int SaveUserRole(int UserProfileID)
        {
            int newID = 0;
            try
            {
                SqlDataReader rdr = null;
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = string.Format("usp_UserRole");

                        cmd.Parameters.Add("@UserProfileID", SqlDbType.Int).Value = UserProfileID;

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return newID;
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close();

                        if (Con != null)
                            Con.Close();
                    }
                    return newID;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return newID;
                }
            }
            return newID;
        }

        public int RegisterCompany(int UserProfile, string CompanyName, string CountryRegion, string NumberofEmployees, string CompanyContactPhone, string CompanyContactPerson, string Sector, string MainCompany,
            string SummaryProject, string RequestInformation, string Password, string EmailAddress, string Category,
            string AddressLine1, string AddressLine2, string AddressLine3, string AddressLineCity, string AddressLineState_Province, string AddressLinePostalCode, string AddressLineCountry, Guid CRMUserProfileId, Guid CRMContactId)
        {
            int newID = 0;
            try
            {
                SqlDataReader rdr = null;
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = string.Format("usp_InsertAccount");

                        cmd.Parameters.Add("@UserProfileId", SqlDbType.Int).Value = UserProfile;
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 255).Value = CompanyName; ;
                        cmd.Parameters.Add("@CountryRegion", SqlDbType.VarChar, 255).Value = CountryRegion;
                        cmd.Parameters.Add("@NumberofEmployees", SqlDbType.VarChar, 255).Value = NumberofEmployees;
                        cmd.Parameters.Add("@CompanyContactPhone", SqlDbType.VarChar, 255).Value = CompanyContactPhone;
                        cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 255).Value = Sector;

                        cmd.Parameters.Add("@MainCompany", SqlDbType.VarChar, 255).Value = MainCompany;
                        cmd.Parameters.Add("@SummaryProject", SqlDbType.VarChar, 255).Value = SummaryProject;
                        cmd.Parameters.Add("@RequestInformation", SqlDbType.VarChar, 255).Value = RequestInformation;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = Password;
                        cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = EmailAddress;
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar, 255).Value = Category;

                        cmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 255).Value = AddressLine1;
                        cmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 255).Value = AddressLine2;
                        cmd.Parameters.Add("@AddressLine3", SqlDbType.VarChar, 255).Value = AddressLine3;
                        cmd.Parameters.Add("@AddressLineCity", SqlDbType.VarChar, 255).Value = AddressLineCity;
                        cmd.Parameters.Add("@AddressLineState_Province", SqlDbType.VarChar, 255).Value = AddressLineState_Province;
                        cmd.Parameters.Add("@AddressLinePostalCode", SqlDbType.VarChar, 255).Value = AddressLinePostalCode;
                        cmd.Parameters.Add("@AddressLineCountry", SqlDbType.VarChar, 255).Value = AddressLineCountry;
                        cmd.Parameters.Add("@CRMUserProfileId", SqlDbType.UniqueIdentifier, 255).Value = CRMUserProfileId;
                        cmd.Parameters.Add("@CRMContactId", SqlDbType.UniqueIdentifier, 255).Value = CRMContactId;

                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                            newID = (int)rdr["Id"];
                    }
                    catch (Exception ex)
                    {
                        if (!string.IsNullOrEmpty(ex.ToString()))
                        {
                            return newID;
                        }
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close();

                        if (Con != null)
                            Con.Close();
                    }
                    return newID;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return newID;
                }
            }
            return newID;
        }

        public int SaveUserDetails(string UserName, string Password, string FirstName, string LastName, string Telephone, string Cellphone, string Email)
        {
            int newID = 0;
            try
            {
                SqlDataReader rdr = null;
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = string.Format("usp_InsertUserProfile");

                        cmd.Parameters.Add("@Password", SqlDbType.VarChar, 300).Value = Password;
                        cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 255).Value = UserName; ;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 255).Value = FirstName;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 255).Value = LastName;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = Email;
                        cmd.Parameters.Add("@Cellphone", SqlDbType.VarChar, 255).Value = Cellphone;
                        cmd.Parameters.Add("@Telephone", SqlDbType.VarChar, 255).Value = Telephone;

                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                            newID = (int)rdr["Id"];
                    }
                    catch (Exception EXC)
                    {
                        string EXCS = EXC.ToString();
                        return newID;
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close();

                        if (Con != null)
                            Con.Close();
                    }
                    return newID;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return newID;
                }
            }
            return newID;
        }

        public int Addsrvrolemember(string UserName)
        {
            int rdr = 0;
            try
            {

                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("EXEC sp_addsrvrolemember '{0}', 'sysadmin'", UserName);

                        var rdrss = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        return rdr;
                    }
                    finally
                    {
                        if (Con != null)
                            Con.Close();
                    }
                    return rdr;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return rdr;
                }
            }
            return rdr;
        }

        public int GetUSERInformation(int UserId)
        {
            SqlDataReader rdrs = null;
            int rdr = 0;
            try
            {
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = string.Format("usp_GetUserInformationbyUserId");
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                        rdrs = cmd.ExecuteReader();

                        while (rdrs.Read())
                        {

                        }

                    }
                    catch (Exception)
                    {
                        return rdr;
                    }
                    finally
                    {
                        if (Con != null)
                            Con.Close();
                    }
                    return rdr;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return rdr;
                }
            }
            return rdr;
        }

        public void UpdateForgotPassword(int UserProfileID, string Password)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();

                    Con.Open();

                    cmd.Connection = Con;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = string.Format("usp_UpdateInsertAccount");

                    cmd.Parameters.Add("@UserProfileID", SqlDbType.Int).Value = UserProfileID;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = Techhill.Framework.Tools.Encode(Password);

                    cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                string exc = ex.ToString();
            }
        }
        public List<Techhill.Framework.DomainTypes.Account.UserProfileModel> usp_GetUserInformationbyUsernameOTP(string UserName)
        {
            List<Techhill.Framework.DomainTypes.Account.UserProfileModel> modeList = new List<Techhill.Framework.DomainTypes.Account.UserProfileModel>();
            SqlDataReader rdrs = null;

            try
            {
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = "usp_GetUserInformationbyUsernameOTP";
                        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;


                        rdrs = cmd.ExecuteReader();

                        while (rdrs.Read())
                        {
                            modeList.Add(new Techhill.Framework.DomainTypes.Account.UserProfileModel
                            {
                                UserProfileID = rdrs.GetInt32(0),
                                UserName = rdrs.GetString(1),
                                Email = rdrs.GetString(2),
                                Cellphone = rdrs.GetString(3),
                                FirstName = rdrs.GetString(4),
                                LastName = rdrs.GetString(5)
                            });
                        }
                    }
                    catch (Exception exc)
                    {
                        string ex = exc.ToString();
                        return modeList;
                    }
                    finally
                    {
                        if (Con != null)
                            Con.Close();
                    }
                    return modeList;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return modeList;
                }
            }
            return modeList;
        }

        public List<Techhill.Framework.DomainTypes.Account.UserProfileModel> GetUSERInformationDetails(string UserName)
        {
            List<Techhill.Framework.DomainTypes.Account.UserProfileModel> modeList = new List<Techhill.Framework.DomainTypes.Account.UserProfileModel>();
            SqlDataReader rdrs = null;

            try
            {
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = "usp_GetUserInformationbyUsername";
                        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;


                        rdrs = cmd.ExecuteReader();

                        while (rdrs.Read())
                        {
                            modeList.Add(new Techhill.Framework.DomainTypes.Account.UserProfileModel
                            {
                                UserProfileID = rdrs.GetInt32(0),
                                UserName = rdrs.GetString(1),
                                Email = rdrs.GetString(2),
                                Cellphone = rdrs.GetString(3),
                                FirstName = rdrs.GetString(4),
                                LastName = rdrs.GetString(5)
                            });
                        }
                    }
                    catch (Exception exc)
                    {
                        string ex = exc.ToString();
                        return modeList;
                    }
                    finally
                    {
                        if (Con != null)
                            Con.Close();
                    }
                    return modeList;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return modeList;
                }
            }
            return modeList;
        }
        public int GetUSER_ID(string UserName)
        {
            SqlDataReader rdrs = null;
            int rdr = 0;
            try
            {
                using (SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        Con.Open();

                        cmd.Connection = Con;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("SELECT CAST(USER_ID('{0}') AS INT) AS UserId", UserName);

                        rdrs = cmd.ExecuteReader();

                        while (rdrs.Read())
                            rdr = (int)rdrs["UserId"];


                    }
                    catch (Exception)
                    {
                        return rdr;
                    }
                    finally
                    {
                        if (Con != null)
                            Con.Close();
                    }
                    return rdr;
                }
            }

            catch (Exception ex)
            {
                if (ex.ToString() != string.Empty)
                {
                    return rdr;
                }
            }
            return rdr;
        }
    }
}