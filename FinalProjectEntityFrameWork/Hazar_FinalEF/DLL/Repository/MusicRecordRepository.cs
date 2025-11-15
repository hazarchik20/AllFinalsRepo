using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Interfaces;
using DLL.ModelsInfo;
using Microsoft.EntityFrameworkCore;


namespace DLL.Repository
{
    public class MusicRecordRepository : IRepository<MusicRecordInfo>
    {
        private MusicRecorContext _context;
        public MusicRecordRepository(MusicRecorContext context)
        {
            this._context = context;
        }
        public void Add(MusicRecordInfo value)
        {
            this._context.MusicRecordsInfo.Add(value);
            this._context.SaveChanges();
        }
        public void Delete(MusicRecordInfo value)
        {
            this._context.MusicRecordsInfo.Remove(value);
            this._context.SaveChanges();
        }
        public MusicRecordInfo FindElement(int id)
        {
            return _context.MusicRecordsInfo.Where(p => p.Id == id).FirstOrDefault();
        }
        public IEnumerable<MusicRecordInfo> GetAll()
        {
            return _context.MusicRecordsInfo.Include(R => R.RealmusicRecordInfo).Include(S => S.SoldmusicRecordInfo).Include(V => V.VidkladmusicRecordInfo);
        }
        public IEnumerable<SOLDmusicRecordInfo> GetAllSold()
        {
            return _context.SOLDmusicRecordInfo.Include(P => P.People);
        }
        public IEnumerable<VIDKLADmusicRecordInfo> GetAllVidklad()
        {
            return _context.VIDKLADmusicRecordInfo.Include(P => P.People); 
        }
        public void Update(MusicRecordInfo value)
        {
            _context.Update(value);
            _context.SaveChanges();
        }

        
    }

}
