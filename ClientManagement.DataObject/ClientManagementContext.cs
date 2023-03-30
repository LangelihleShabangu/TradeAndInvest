using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClientManagement.DataObject
{
    public partial class ClientManagementContext : DbContext
    {
        public ClientManagementContext()
            : base("name=ClientManagementContext")
        {
        }

        public ClientManagementContext(string con)
            : base(con)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountingType> AccountingType { get; set; }
        public virtual DbSet<AccountStatus> AccountStatus { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressDetails> AddressDetails { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Assessment> Assessment { get; set; }
        public virtual DbSet<AssociatedProject> AssociatedProject { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<ContactInformation> ContactInformation { get; set; }
        public virtual DbSet<DocumentManagement> DocumentManagement { get; set; }
        public virtual DbSet<DocumentManagementFolder> DocumentManagementFolder { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<DocumentStatus> DocumentStatus { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Facilitation> Facilitation { get; set; }
        public virtual DbSet<FacilitationRequest> FacilitationRequest { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Industry> Industry { get; set; }
        public virtual DbSet<Leads> Leads { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleObject> ModuleObject { get; set; }
        public virtual DbSet<ModuleObjectItem> ModuleObjectItem { get; set; }
        public virtual DbSet<ModulePermission> ModulePermission { get; set; }
        public virtual DbSet<ModulePermissionItem> ModulePermissionItem { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleModule> RoleModule { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Subscriptions> Subscriptions { get; set; }
        public virtual DbSet<SystemParameter> SystemParameter { get; set; }
        public virtual DbSet<SystemParameterType> SystemParameterType { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserProfileUserProfileAddressDetails> UserProfileUserProfileAddressDetails { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.CountryRegion)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.CompanyContactPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Sector)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.MainCompany)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.SummaryProject)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.RequestInformation)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<AccountingType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AccountingType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AccountStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AccountStatus>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.AddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.AddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.AddressLine3)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.AddressLine4)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.ContactInformation)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressDetails>()
                .Property(e => e.AddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<AddressDetails>()
                .Property(e => e.AddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<AddressDetails>()
                .Property(e => e.AddressLine3)
                .IsUnicode(false);

            modelBuilder.Entity<AddressDetails>()
                .Property(e => e.AddressLineCity)
                .IsUnicode(false);

            modelBuilder.Entity<AddressDetails>()
                .Property(e => e.AddressLineState_Province)
                .IsUnicode(false);

            modelBuilder.Entity<AddressDetails>()
                .Property(e => e.AddressLinePostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<AddressDetails>()
                .Property(e => e.AddressLineCountry)
                .IsUnicode(false);

            modelBuilder.Entity<AddressDetails>()
                .HasMany(e => e.UserProfileUserProfileAddressDetails)
                .WithRequired(e => e.AddressDetails)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Area>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Area>()
                .HasMany(e => e.Address)
                .WithRequired(e => e.Area)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Specify_Company_Location)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Exporter_Region)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Exporter_Country)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Industry_Sector)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Export_products)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Export_markets)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Regional_export_markets_future_expand)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Special_technical_support)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Country_Expand)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Government_export_assistance)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.specify_location_Assistance)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Additional_Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Challenges_facing_Exporter)
                .IsUnicode(false);

            modelBuilder.Entity<Assessment>()
                .Property(e => e.Other_issues)
                .IsUnicode(false);

            modelBuilder.Entity<AssociatedProject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AssociatedProject>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.EventData)
                .IsUnicode(false);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.Table)
                .IsUnicode(false);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.Columns)
                .IsUnicode(false);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.IdNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.PassportNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.CellNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.KnownAs)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInformation>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentManagement>()
                .Property(e => e.DocumentName)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentManagement>()
                .Property(e => e.DocumentDescription)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentManagement>()
                .Property(e => e.DocumentPath)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentManagement>()
                .Property(e => e.ContentType)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentManagementFolder>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentManagementFolder>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Documents>()
                .Property(e => e.DocumentName)
                .IsUnicode(false);

            modelBuilder.Entity<Documents>()
                .Property(e => e.DocumentDescription)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentStatus>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.DocumentStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.DocumentManagement)
                .WithRequired(e => e.DocumentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.DocumentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.ProjectDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.ProjectChallangesConstraints)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.Expectations)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.ProjectStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.RelevantDepartments)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.FuturePlans)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.ProjectManagerName)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.CellNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Facilitation>()
                .HasMany(e => e.FacilitationRequest)
                .WithRequired(e => e.Facilitation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacilitationRequest>()
                .Property(e => e.NeededAssistance)
                .IsUnicode(false);

            modelBuilder.Entity<FacilitationRequest>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.ContactInformation)
                .WithRequired(e => e.Gender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Industry>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Industry>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.CrmLeadId)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.VendorNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.ContactNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.EstateName)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.CellNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.CrmUri)
                .IsUnicode(false);

            modelBuilder.Entity<Leads>()
                .Property(e => e.RejectionComment)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Controller)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.CssClass)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Area)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.ModuleObject)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.RoleModule)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModuleObject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObject>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObject>()
                .Property(e => e.Controller)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObject>()
                .Property(e => e.CssClass)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObject>()
                .HasMany(e => e.ModuleObjectItem)
                .WithRequired(e => e.ModuleObject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModuleObject>()
                .HasMany(e => e.ModulePermission)
                .WithRequired(e => e.ModuleObject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModuleObjectItem>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObjectItem>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObjectItem>()
                .Property(e => e.Controller)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObjectItem>()
                .Property(e => e.CssClass)
                .IsUnicode(false);

            modelBuilder.Entity<ModuleObjectItem>()
                .HasMany(e => e.ModulePermissionItem)
                .WithRequired(e => e.ModuleObjectItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModulePermission>()
                .HasMany(e => e.ModulePermissionItem)
                .WithRequired(e => e.ModulePermission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Province>()
                .Property(e => e.Latitude)
                .IsUnicode(false);

            modelBuilder.Entity<Province>()
                .Property(e => e.Longitude)
                .IsUnicode(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Leads)
                .WithRequired(e => e.Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RoleModule)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRole)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleModule>()
                .HasMany(e => e.ModulePermission)
                .WithRequired(e => e.RoleModule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Leads)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Woman_Owned_Business)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Exporter)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Planning_to_Export)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Which_Region)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Contact_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Email_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Address_Line1)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Address_Line2)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Address_Line3)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Address_Line4)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .Property(e => e.Company_Based)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriptions>()
                .HasMany(e => e.Assessment)
                .WithRequired(e => e.Subscriptions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SystemParameter>()
                .Property(e => e.SystemParameterName)
                .IsUnicode(false);

            modelBuilder.Entity<SystemParameter>()
                .Property(e => e.SystemParameterDescription)
                .IsUnicode(false);

            modelBuilder.Entity<SystemParameter>()
                .Property(e => e.SystemParameterValue)
                .IsUnicode(false);

            modelBuilder.Entity<SystemParameterType>()
                .Property(e => e.SystemParameterTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<SystemParameterType>()
                .Property(e => e.SystemParameterTypeCShardDataType)
                .IsUnicode(false);

            modelBuilder.Entity<SystemParameterType>()
                .Property(e => e.SystemParameterTypeSQLDataType)
                .IsUnicode(false);

            modelBuilder.Entity<SystemParameterType>()
                .HasMany(e => e.SystemParameter)
                .WithRequired(e => e.SystemParameterType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Title>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Title>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Title>()
                .HasMany(e => e.ContactInformation)
                .WithRequired(e => e.Title)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserDetails>()
                .Property(e => e.UserDetails1)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Cellphone)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Account)
                .WithRequired(e => e.UserProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.UserProfileUserProfileAddressDetails)
                .WithRequired(e => e.UserProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.UserRole)
                .WithRequired(e => e.UserProfile)
                .WillCascadeOnDelete(false);
        }
    }
}
