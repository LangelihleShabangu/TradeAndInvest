﻿using System;

namespace ClientManagement.DomainModel
{
    public class DocumentTypeModel
    {
        public int DocumentTypeId { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}