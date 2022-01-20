using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrunoCampiol.Infra.Data.Models
{
    [Table("POSTS", Schema = "dbo")]
    public class POSTS
    {
        [Key]
        [Column("ID")]
        public long ID { get; set; }

        [Column("USER_POST")]
        [StringLength(2000)]
        public string USER_POST { get; set; }

        [Column("USER_NAME")]
        [StringLength(300)]
        public string USER_NAME { get; set; }

        [Column("USER_MEDIA")]
        [StringLength(100)]
        public string USER_MEDIA { get; set; }

        [Column("USER_IP")]
        [StringLength(45)]
        public string USER_IP { get; set; }

        [Column("USER_COUNTRY")]
        [StringLength(2)]
        public string USER_COUNTRY { get; set; }

        [Column("USER_BROWSER")]
        [StringLength(100)]
        public string USER_BROWSER { get; set; }

        [Column("USER_OS")]
        [StringLength(100)]
        public string USER_OS { get; set; }

        [Column("CREATED_ON_UTC")]
        public DateTime CREATED_ON_UTC { get; set; }
    }
}
