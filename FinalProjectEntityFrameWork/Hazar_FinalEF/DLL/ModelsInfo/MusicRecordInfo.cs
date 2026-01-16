using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.ModelsInfo
{
   
    public class MusicRecordInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameRecord { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameGroup { get; set; }
        [Required]
        [MaxLength(50)]
        public string NamePublisher { get; set; }
        [Required]
        public int CountSong { get; set; }
        [Required]
        public Genre_MusicRecord genre_MusicRecord { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        public int Cost {  get; set; }
        [Required]
        public int Cost_for_sale { get; set; }
        public List<REALmusicRecordInfo> RealmusicRecordInfo { get; set; }
        public List<SOLDmusicRecordInfo> SoldmusicRecordInfo { get; set; }
        public List<VIDKLADmusicRecordInfo> VidkladmusicRecordInfo { get; set; }
    }
    public enum Genre_MusicRecord
    {
        Classical_music=1,
        Country=2,
        Electronic_dance_music=3,
        Hip_hop=4,
        Jazz=5,
        Rock=6,
        K_pop=7,
        Latin_music=8,
        Pop=9,
    }
}
