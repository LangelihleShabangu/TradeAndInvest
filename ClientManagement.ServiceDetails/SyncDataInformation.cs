using ClientManagement.ServiceDetails.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ClientManagement.ServiceDetails
{
    public class SyncDataInformation
    {
        public void NewAccountInformation(
            string Firstname,
            string Lastname,
            string EmailAddress,
            Guid CRMContactId,
            Guid CRMUserProfileId,
            string CompanyName,
            string Address_Line1,
            string Address_Line2,
            string Address_Line3,
            string AddressLineCity,
            string AddressLineState_Province,
            string AddressLinePostalCode,
            string AddressLineCountry,
            int UserProfileId,
            string Category)
        {
            var queueService = new QueueService();

            JObject crmContact = JObject.FromObject(new
            {
                firstname = Firstname,
                lastname = Lastname,
                jobtitle = "New",
                emailaddress1 = EmailAddress,
                rif_dedcontactid = UserProfileId.ToString()
            });

            // Contact Id for updating - ONLY NEED THIS IF ITS AN UPDATE
            if (CRMContactId != Guid.Empty)
            {
                crmContact.Add("contactid", CRMContactId);
            }

            JObject crmAccount = JObject.FromObject(new
            {
                rif_dedaccountid = UserProfileId.ToString(),
                name = CompanyName,
                address1_line1 = Address_Line1,
                address1_line2 = Address_Line2,
                address1_line3 = Address_Line3,
                address1_city = AddressLineCity,
                address1_stateorprovince = AddressLineState_Province,
                address1_postalcode = AddressLinePostalCode,
                address1_country = AddressLineCountry,
                rif_businesscategory = Category == "Investor" ? "908510000" : "908510001"
            });

            // AccountId for updating
            if (CRMUserProfileId != Guid.Empty)
            {
                crmAccount.Add("accountid", CRMUserProfileId);
            }

            var account = new
            {
                account = crmAccount.ToString(),
                contact = crmContact.ToString()
            };

            queueService.SendMessage(account, "account-queue");
        }

        public void NewAssessmentDetails(
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
            bool Government_export_assistance,
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
            bool chkOther
            )
        {
            var queueService = new QueueService();

            JObject crmContact = JObject.FromObject(new
            {
                firstname = Firstname,
                lastname = Lastname,
                jobtitle = "New",
                emailaddress1 = EmailAddress,
                rif_dedcontactid = UserProfileId.ToString()
            });

            // Contact Id for updating - ONLY NEED THIS IF ITS AN UPDATE
            if (CRMContactId != Guid.Empty)
            {
                crmContact.Add("contactid", CRMContactId);
            }

            JObject crmAccount = JObject.FromObject(new
            {
                rif_dedaccountid = UserProfileId.ToString(),
                name = CompanyName,
                address1_line1 = Address_Line1,
                address1_line2 = Address_Line2,
                address1_line3 = Address_Line3,
                address1_city = AddressLineCity,
                address1_stateorprovince = AddressLineState_Province,
                address1_postalcode = AddressLinePostalCode,
                address1_country = AddressLineCountry,
                rif_businesscategory = "908510000"
            });

            // AccountId for updating
            if (CRMUserProfileId != Guid.Empty)
            {
                crmAccount.Add("accountid", CRMUserProfileId);
            }

            if (chkExporter_Education_Seminar)
                crmAccount.Add(new JProperty("rif_exportereducationseminar", "908510000"));

            if (chkParticipate_at_international_Trade)
                crmAccount.Add(new JProperty("rif_participate_at_international_Trade", "908510001"));

            if (chkMarket_Research)
                crmAccount.Add(new JProperty("rif_marketresearch", "908510002"));

            if (chkGuidance_on_export)
                crmAccount.Add(new JProperty("rif_guidanceonexport", "908510003"));

            if (chkFinancial_Assistance)
                crmAccount.Add(new JProperty("rif_financialassistance", "908510004"));

            if (chkConsultation_with_trade_specialist)
                crmAccount.Add(new JProperty("rif_consultationwithtradespecialist", chkConsultation_with_trade_specialist));

            if (chkInternational_Trade_Missions)
                crmAccount.Add(new JProperty("rif_internationaltrade_missions", "908510006"));

            if (chkTrade_Leads)
                crmAccount.Add(new JProperty("rif_tradeleads", "908510007"));

            if (chkShipping_Logistics_consulting)
                crmAccount.Add(new JProperty("rif_Shippinglogisticsconsulting", "908510008"));

            if (chkOther)
                crmAccount.Add(new JProperty("rif_other", "908510009"));

            crmAccount.Add(new JProperty("rif_noofyearsasanexporter", Number_of_years_you_have_exported));
            crmAccount.Add(new JProperty("rif_noofemployees", Number_of_Employees));
            crmAccount.Add(new JProperty("rif_annualturnover", Companys_annual_turnover));
            crmAccount.Add(new JProperty("rif_mainexportproducts", Export_products));
            crmAccount.Add(new JProperty("rif_mainexportmarkets", Export_markets));

            crmAccount.Add(new JProperty("rif_plannedexportregionalmarkets", Exporter_Region));
            crmAccount.Add(new JProperty("rif_plannedexportcountry", Exporter_Country));
            crmAccount.Add(new JProperty("rif_receivinggvtexportassistance", Government_export_assistance));
            crmAccount.Add(new JProperty("rif_exportassistancelocation", Specify_Company_Location));

            crmAccount.Add(new JProperty("rif_desiredservices", "908510000, 908510001"));
            crmAccount.Add(new JProperty("rif_additionalcomments", Additional_Comments));
            crmAccount.Add(new JProperty("rif_currentchallenges", Challenges_facing_Exporter));
            crmAccount.Add(new JProperty("rif_additionalareasofconcern", Other_issues));                       

            var account = new
            {
                account = crmAccount.ToString(),
                contact = crmContact.ToString()
            };

            if (CRMUserProfileId != Guid.Empty)
            {
                queueService.SendMessage(crmAccount.ToString(), "account-update-queue");
            }
            else
            {
                queueService.SendMessage(account, "account-queue");
            }
        }
        
        public void NewAccount(
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
            string DocumentLocationURL)
        {
            var queueService = new QueueService();
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

            // Contact Id for updating - ONLY NEED THIS IF ITS AN UPDATE
            if (CRMContactId != Guid.Empty)
            {
                crmContact.Add("contactid", CRMContactId);
            }

            JObject crmAccount = JObject.FromObject(new
            {
                rif_dedaccountid = UserProfileId.ToString(),
                name = CompanyName,
                address1_line1 = Address_Line1,
                address1_line2 = Address_Line2,
                address1_line3 = Address_Line3,
                address1_city = AddressLineCity,
                address1_stateorprovince = AddressLineState_Province,
                address1_postalcode = AddressLinePostalCode,
                address1_country = AddressLineCountry,
                rif_businesscategory = "908510000"
            });

            //// AccountId for updating
            if (CRMUserProfileId != Guid.Empty)
            {
                crmAccount.Add("accountid", CRMUserProfileId);
            }

            if (Exporter)//Exporter
            {
                crmAccount.Add(new JProperty("rif_womanowendbusiness", Woman_Owned_Business));
                crmAccount.Add(new JProperty("industrycode", Industrycode));
                crmAccount.Add(new JProperty("rif_planningtoexport", Planning_to_Export));
                crmAccount.Add(new JProperty("rif_joburgbasedcompany", IJHBCompany_Based));
                crmAccount.Add(new JProperty("rif_companydocument", DocumentLocationURL));
                crmAccount.Add(new JProperty("rif_exporterregion", Export_Which_Region));
                crmAccount.Add(new JProperty("rif_exportercountry", Export_Which_Country));
            }
            // Exporter Assessment
            else
            {
                crmAccount.Add(new JProperty("rif_noofyearsasanexporter", Number_of_years_you_have_exported));
                crmAccount.Add(new JProperty("rif_noofemployees", Number_of_Employees));
                crmAccount.Add(new JProperty("rif_annualturnover", Companys_annual_turnover));
                crmAccount.Add(new JProperty("rif_mainexportproducts", Export_products));
                crmAccount.Add(new JProperty("rif_mainexportmarkets", Export_markets));

                crmAccount.Add(new JProperty("rif_plannedexportregionalmarkets", Exporter_Region));
                crmAccount.Add(new JProperty("rif_plannedexportcountry", Exporter_Country));
                crmAccount.Add(new JProperty("rif_receivinggvtexportassistance", Government_export_assistance));
                crmAccount.Add(new JProperty("rif_exportassistancelocation", Specify_Company_Location));

                crmAccount.Add(new JProperty("rif_desiredservices", "908510000, 908510001"));
                crmAccount.Add(new JProperty("rif_additionalcomments", Additional_Comments));
                crmAccount.Add(new JProperty("rif_currentchallenges", Challenges_facing_Exporter));
                crmAccount.Add(new JProperty("rif_additionalareasofconcern", Other_issues));
            }

            var account = new
            {
                account = crmAccount.ToString(),
                contact = crmContact.ToString()
            };

            if (CRMUserProfileId != Guid.Empty)
            {
                queueService.SendMessage(crmAccount.ToString(), "account-update-queue");
            }
            else
            {
                queueService.SendMessage(account, "account-queue");
            }
        }

        public void NewFacilitationRequest(string Title, string Description, Guid CRMAccountId, Guid CRMOpportunityId, Guid CRMContactId, int FacilitationRequestId)
        {
            var queueService = new QueueService();

            JObject crmFacilitationRequest = JObject.FromObject(new
            {
                title = Title,
                description = Description,
                rif_dedfacilitaionreqid = FacilitationRequestId.ToString()
            });

            // OpportunityId
            if (CRMOpportunityId != Guid.Empty)
            {
                crmFacilitationRequest.Add("rif_opportunityid@odata.bind", string.Format("/opportunities({0})", CRMOpportunityId.ToString()));
            }

            // AccountId
            crmFacilitationRequest.Add("customerid_account@odata.bind", string.Format("/accounts({0})", CRMAccountId.ToString()));

            //ContactId
            crmFacilitationRequest.Add("primarycontactid@odata.bind", string.Format("/contacts({0})", CRMContactId.ToString()));

            queueService.SendMessage(crmFacilitationRequest.ToString(), "facilitationrequest-queue");
        }

        public void NewLead(
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
            var queueService = new QueueService();
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

            // LeadId for updating
            if (CRMLeadId != Guid.Empty)
            {
                crmLead.Add("leadid", CRMLeadId.ToString());
            }

            if (CRMOpportunityId != Guid.Empty)
            {
                crmLead.Add("opportunityid", CRMOpportunityId.ToString());
            }

            //contactId
            crmLead.Add("parentcontactid@odata.bind", string.Format("/contacts({0})", CRMContactId.ToString()));

            // AccountId
            crmLead.Add("parentaccountid@odata.bind", string.Format("/accounts({0})", CRMAccountId.ToString()));

            queueService.SendMessage(crmLead.ToString(), "lead-queue");
        }

        public void NewOpportunity(
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
            var queueService = new QueueService();
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

            queueService.SendMessage(crmOppotunity.ToString(), "lead-queue");
        }

        public void NewFacilitationDocument(string DocumentName, string DocumentDescription, string DocumentLocationURL, Guid CRMDocumentManagementId, Guid CRMOpportunityId, Guid CRMLeadId, Guid CrmContactId, int DocumentManagementId, string DocumentTypeInfo)
        {
            var queueService = new QueueService();
            JObject crmFacilitationDocument = JObject.FromObject(new
            {
                rif_name = DocumentName,
                rif_description = DocumentDescription,
                rif_documenttype = DocumentTypeInfo,
                rif_documenturl = DocumentLocationURL,
                rif_deddocid = DocumentManagementId.ToString()
            });

            //if (CRMDocumentManagementId != Guid.Empty)
            //{                
            //     crmFacilitationDocument.Add("rif_facilitationdocumentid", CRMDocumentManagementId.ToString());
            //}
            // provide leadId or crmLeadId
            if (CRMOpportunityId != Guid.Empty)
            {
                crmFacilitationDocument.Add("rif_opportunity@odata.bind", string.Format("/opportunities({0})", CRMOpportunityId.ToString()));
            }
            else if (CRMLeadId != Guid.Empty)
            {
                crmFacilitationDocument.Add("rif_leadid@odata.bind", string.Format("/leads({0})", CRMLeadId.ToString()));
            }

            queueService.SendMessage(crmFacilitationDocument.ToString(), "document-queue");
        }

    }
}
