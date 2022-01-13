using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrunoCampiol.Infra.Data.Models
{
    [Table("VISITORS", Schema = "dbo")]
    public class VISITORS
    {
        [Key]
        [Column("IP")]
        [StringLength(45)]
        public string IP { get; set; }

        [Column("COUNTRY")]
        [StringLength(2)]
        [Required(AllowEmptyStrings = false)]
        public string COUNTRY { get; set; }

        [Column("REGION")]
        public string REGION { get; set; }

        [Column("CITY")]
        public string CITY { get; set; }

        [Column("ISP")]
        public string ISP { get; set; }

        [Column("CLIENT_HEADERS")]
        public string CLIENT_HEADERS { get; set; }

        [Column("CLIENT_USER_AGENT")]
        public string CLIENT_USER_AGENT { get; set; }

        [Column("CLIENT_BROWSER")]
        public string CLIENT_BROWSER { get; set; }

        [Column("CLIENT_OS")]
        public string CLIENT_OS { get; set; }
        
        [Column("CREATED_ON_UTC")]
        public DateTime CREATED_ON_UTC { get; set; }
    }
}
