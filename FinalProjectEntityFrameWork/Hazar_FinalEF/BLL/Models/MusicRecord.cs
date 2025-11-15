using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class MusicRecord
    {
        public int Id { get; set; }
        public string NameRecord { get; set; }
        public string NameGroup { get; set; }
        public int CountSong { get; set; }
        public string NamePublisher { get; set; }
        public Genre_MusicRecord genre_MusicRecord { get; set; }
        public DateTime Birthday { get; set; }
        public int Cost { get; set; }
        public int Cost_for_sale { get; set; }
        public List<REALmusicRecord> RealmusicRecord { get; set; }
        public List<SOLDmusicRecord> SoldmusicRecord { get; set; }
        public List<VIDKLADmusicRecord> VidkladmusicRecord { get; set; }

    }
    public enum Genre_MusicRecord
    {
        Classical_music = 1,
        Country = 2,
        Electronic_dance_music = 3,
        Hip_hop = 4,
        Jazz = 5,
        Rock = 6,
        K_pop = 7,
        Latin_music = 8,
        Pop = 9,
    }
}
