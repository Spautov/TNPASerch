using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class DocumentfInfo
    {
        [JsonProperty("IDGLOBAL")]
        public int? GlobalId { get; set; }

        [JsonProperty("RN_OND")]
        public int? AdditionalId { get; set; }

        [JsonProperty("Annot")]
        public string Anatation { get; set; }

        [JsonProperty("Annot_EN")]
        public object AnatationEN { get; set; }

        [JsonProperty("ytv")]
        public string ApprovalInformation { get; set; }

        [JsonProperty("DTTN")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("DTTK")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("DSMSOS")]
        public DateTime? AdditionaDate { get; set; }

        [JsonProperty("DREG")]
        public DateTime? RegistrationDate { get; set; }

        [JsonProperty("LINK")]
        public object Link { get; set; }

        [JsonProperty("ID_WEB")]
        public int? WebId { get; set; }

        [JsonProperty("OND_EN")]
        public string NumberEN { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("NND")]
        public string Name { get; set; }

        [JsonProperty("NND_EN")]
        public string NameEN { get; set; }

        [JsonProperty("IDNND")]
        public int? NndId { get; set; }

        [JsonProperty("FNAME")]
        public string Author { get; set; }

        [JsonProperty("FNAME_EN")]
        public object AuthorEN { get; set; }

        [JsonProperty("NKT")]
        public string Type { get; set; }

        [JsonProperty("NAME_EN")]
        public string TypeEN { get; set; }

        [JsonProperty("NAME")]
        public string CategoryName { get; set; }

        [JsonProperty("NKT_EN")]
        public string CategoryNameEN { get; set; }

        [JsonProperty("SOST")]
        public string Status { get; set; }

        [JsonProperty("NAIM_ENG")]
        public string StatusEN { get; set; }

        [JsonProperty("TP_ID")]
        public int? TpId { get; set; }


        public int? PRIZN_BD { get; set; }
        public int? PRIZN { get; set; }
        public object ORGDP { get; set; }
        public object primen { get; set; }
        public string KBD { get; set; }
        public object ORGADR { get; set; }
        public object ORGML { get; set; }
        public object REG_NCPI { get; set; }
        public object DREG_NCPISite { get; set; }
        public object REG { get; set; }
        public object PRIM_SOST231 { get; set; }
        public string IY_TNPA_LAST { get; set; }
        public object CODII_NCPI { get; set; }
        public string KOLSTR { get; set; }
        public string CODGOS { get; set; }
        public object PRIM_SOST232 { get; set; }
        public string KT { get; set; }
        public string NII { get; set; }
        public object OND1 { get; set; }
        public string sost_prim { get; set; }
    }
}
