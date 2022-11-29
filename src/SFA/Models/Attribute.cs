using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.Models
{
    public class AttributeModel
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public int AttributeTypeId { get; set; }
        public string AttributeTypeName { get; set; }
        public string AttributeNotes { get; set; }

        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public DateTime? UpdateDatetime { get; set; }
    }

    public class AttributeTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<AttributeModel> Attributes { get; set; }
    }

    public class AttributeModelQuery : Query
    {
        public string Name { get; set; }
    }
}