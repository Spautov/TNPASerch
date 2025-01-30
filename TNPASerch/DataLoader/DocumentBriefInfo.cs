using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class DocumentBriefInfo
    {
        [JsonProperty("KL")]
        public string StructuredNumber { get; set; }

        [JsonProperty("NND")]
        public string Name { get; set; }

        [JsonProperty("NAIM")]
        public string Status { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("DTTN")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("DTTK")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("DSMSOS")]
        public DateTime? AdditionaDate { get; set; }

        [JsonProperty("IDGLOBAL")]
        public int? GlobalId { get; set; }

        [JsonProperty("RN")]
        public int? AdditionalId { get; set; }

        public string OND1 { get; set; }
        public string PRIZN { get; set; }
        public string PRIZN_BD { get; set; }
        public string PRIMEN { get; set; }
    }
}
