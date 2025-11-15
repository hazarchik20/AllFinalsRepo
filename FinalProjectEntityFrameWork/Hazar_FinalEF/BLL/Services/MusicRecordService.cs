using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DLL.Interfaces;
using DLL.ModelsInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BLL.Services
{
    public class MusicRecordService : IServices<MusicRecord>
    {
        private IRepository<MusicRecordInfo> _contactRepository;
        private List<MusicRecord> _musicRecord;
        public MusicRecordService(IRepository<MusicRecordInfo> repository)
        {
            _contactRepository = repository;
            _musicRecord = new List<MusicRecord>();
        }
        private MusicRecord TranslateMusicInfoToMusic(MusicRecordInfo value)
        {
            List<REALmusicRecord> listReal = new List<REALmusicRecord>();
            List<SOLDmusicRecord> listSold = new List<SOLDmusicRecord>();
            List<VIDKLADmusicRecord> listVidklad = new List<VIDKLADmusicRecord>();
            if (value.RealmusicRecordInfo!=null) 
                foreach (var Values in value.RealmusicRecordInfo)
            {
                REALmusicRecord newReal = new REALmusicRecord()
                {
                    ID = Values.ID,
                };
                listReal.Add(newReal);
            }
            if (value.SoldmusicRecordInfo != null)
                foreach (var Values in value.SoldmusicRecordInfo)
            {
                SOLDmusicRecord newSold = new SOLDmusicRecord()
                {
                    ID = Values.ID,
                    DateSold = Values.DateSold,
                    //People = new People
                    //{
                    //    Name = Values.People.Name,
                    //    LastName = Values.People.LastName,
                    //    Phone = Values.People.Phone,
                    //    Birthday = Values.People.Birthday,
                    //}
                };
                listSold.Add(newSold);
            }
            if (value.VidkladmusicRecordInfo != null)
                foreach (var Values in value.VidkladmusicRecordInfo)
            {
                VIDKLADmusicRecord newVidlklad = new VIDKLADmusicRecord()
                {
                    ID = Values.ID,
                    DateVidklad = Values.DateVidklad,
                    //People = new People
                    //{
                    //    Name = Values.People.Name,
                    //    LastName = Values.People.LastName,
                    //    Phone = Values.People.Phone,
                    //    Birthday = Values.People.Birthday,
                    //}
                };
                listVidklad.Add(newVidlklad);
            }
            BLL.Models.Genre_MusicRecord myGenre= new Models.Genre_MusicRecord();
            switch (Convert.ToString(value.genre_MusicRecord))
            {
                case "Classical_music": { myGenre = BLL.Models.Genre_MusicRecord.Classical_music; } break;
                case "Country": { myGenre = BLL.Models.Genre_MusicRecord.Country; } break;
                case "Electronic_dance_music": { myGenre = BLL.Models.Genre_MusicRecord.Electronic_dance_music; } break;
                case "Hip_hop": { myGenre = BLL.Models.Genre_MusicRecord.Hip_hop; } break;
                case "Jazz": { myGenre = BLL.Models.Genre_MusicRecord.Jazz; } break;
                case "Rock": { myGenre = BLL.Models.Genre_MusicRecord.Rock; } break;
                case "K_pop": { myGenre = BLL.Models.Genre_MusicRecord.K_pop; } break;
                case "Latin_music": { myGenre = BLL.Models.Genre_MusicRecord.Latin_music; } break;
                case "Pop": { myGenre = BLL.Models.Genre_MusicRecord.Pop; } break;
            }
            return new MusicRecord()
            { 
                Id = value.Id,
                NameRecord = value.NameRecord,
                NameGroup = value.NameGroup,
                Birthday = value.Birthday,
                Cost = value.Cost,
                Cost_for_sale = value.Cost_for_sale,
                CountSong = value.CountSong,
                NamePublisher = value.NamePublisher,
                genre_MusicRecord = myGenre,
                RealmusicRecord = listReal,
                SoldmusicRecord= listSold,
                VidkladmusicRecord= listVidklad,
            };
        }
        private MusicRecordInfo TranslateMusicToMusicInfo(MusicRecord value)
        {
            List<REALmusicRecordInfo> listRealInfo = new List<REALmusicRecordInfo>();
            List<SOLDmusicRecordInfo> listSoldInfo = new List<SOLDmusicRecordInfo>();
            List<VIDKLADmusicRecordInfo> listVidkladInfo = new List<VIDKLADmusicRecordInfo>();
            if (value.RealmusicRecord != null)
                foreach (var Values in value.RealmusicRecord)
            {
                REALmusicRecordInfo newRealInfo = new REALmusicRecordInfo()
                {
                    ID = Values.ID,
                };
                listRealInfo.Add(newRealInfo);
            }
            if (value.SoldmusicRecord != null)
                foreach (var Values in value.SoldmusicRecord)
            {
                SOLDmusicRecordInfo newSoldInfo = new SOLDmusicRecordInfo()
                {
                    ID = Values.ID,
                    DateSold = Values.DateSold,
                    //People=new PeopleInfo
                    //{
                    //    Name= Values.People.Name,
                    //    LastName = Values.People.LastName,
                    //    Phone = Values.People.Phone,
                    //    Birthday = Values.People.Birthday,
                    //} 
                };
                listSoldInfo.Add(newSoldInfo);
            }
            if (value.VidkladmusicRecord != null)
                foreach (var Values in value.VidkladmusicRecord)
            {
                VIDKLADmusicRecordInfo newVidlkladInfo = new VIDKLADmusicRecordInfo()
                {
                    ID = Values.ID,
                    DateVidklad = Values.DateVidklad,
                    //People = new PeopleInfo
                    //{
                    //    Name = Values.People.Name,
                    //    LastName = Values.People.LastName,
                    //    Phone = Values.People.Phone,
                    //    Birthday = Values.People.Birthday,
                    //}
                };
                listVidkladInfo.Add(newVidlkladInfo);
            }
            DLL.ModelsInfo.Genre_MusicRecord myGenre = new DLL.ModelsInfo.Genre_MusicRecord();
            switch (Convert.ToString(value.genre_MusicRecord))
            {
                case "Classical_music": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Classical_music; } break;
                case "Country": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Country; } break;
                case "Electronic_dance_music": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Electronic_dance_music; } break;
                case "Hip_hop": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Hip_hop; } break;
                case "Jazz": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Jazz; } break;
                case "Rock": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Rock; } break;
                case "K_pop": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.K_pop; } break;
                case "Latin_music": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Latin_music; } break;
                case "Pop": { myGenre = DLL.ModelsInfo.Genre_MusicRecord.Pop; } break;
            }
            return new MusicRecordInfo()
            {
                Id = value.Id,
                NameRecord = value.NameRecord,
                NameGroup = value.NameGroup,
                Birthday = value.Birthday,
                Cost = value.Cost,
                Cost_for_sale = value.Cost_for_sale,
                CountSong = value.CountSong,
                NamePublisher = value.NamePublisher,
                genre_MusicRecord = myGenre,
                RealmusicRecordInfo = listRealInfo,
                SoldmusicRecordInfo = listSoldInfo,
                VidkladmusicRecordInfo= listVidkladInfo,

            };
        }
        public void Add(MusicRecord value)
        {
            _contactRepository.Add(TranslateMusicToMusicInfo(value));
        }

        public void Delete(MusicRecord value)
        {
            _contactRepository.Delete(TranslateMusicToMusicInfo(value));
        }

        public IEnumerable<MusicRecord> GetAll()
        {
            _musicRecord.Clear();
            foreach (MusicRecordInfo musicRecordInfo in _contactRepository.GetAll())
            {  
                _musicRecord.Add(TranslateMusicInfoToMusic(musicRecordInfo));
            }
            return _musicRecord;
        }

        public void Update(MusicRecord value)
        {
            _contactRepository.Update(TranslateMusicToMusicInfo(value));
        }
    }
}
