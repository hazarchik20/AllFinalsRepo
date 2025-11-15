using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.ModelsInfo
{
    [Table("SoldMusicRecordInfo")]
    public class SOLDmusicRecordInfo
    {
        public int ID { get; set; }
        [Required]
        public PeopleInfo People { get; set; }
        [Required]
        public DateTime DateSold { get; set; }
    }
}
