using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.Entities.Concreate
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public int? Onhand { get; set; }
        public decimal? Price { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LastUpdatedTime { get; set; }
    }
}
