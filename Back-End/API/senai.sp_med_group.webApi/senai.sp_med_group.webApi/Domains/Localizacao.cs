using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Domains
{
    public class Localizacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Nome { get; set; }

        [BsonRequired]
        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonRequired]
        public string Longitude { get; set; }
    }
}
