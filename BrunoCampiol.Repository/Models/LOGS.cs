using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrunoCampiol.Repository.Models
{
    [Table("LOGS", Schema = "dbo")]
    public class LOGS
    {
        [Key]
        [Column("ID")]
        public long ID { get; set; }

        [Column("LEVEL")]
        [StringLength(10)]
        public string LEVEL { get; set; }

        [Column("MESSAGE")]
        public string MESSAGE { get; set; }

        [Column("STACK_TRACE")]
        public string STACK_TRACE { get; set; }

        [Column("FULL_EXCEPTION")]
        public string FULL_EXCEPTION { get; set; }

        [Column("CREATED_ON_UTC")]
        public DateTime CREATED_ON_UTC { get; set; }
    }
}
