using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.ModelsInfo;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class MusicRecorContext : DbContext
    {
        public MusicRecorContext(DbContextOptions<MusicRecorContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<PeopleInfo> PeoplesInfo { get; set; }
        public DbSet<MusicRecordInfo> MusicRecordsInfo { get; set; }
        public DbSet<VIDKLADmusicRecordInfo> VIDKLADmusicRecordInfo { get; set; }
        public DbSet<REALmusicRecordInfo> REALmusicRecordInfo { get; set; }
        public DbSet<SOLDmusicRecordInfo> SOLDmusicRecordInfo { get; set; }
    }
}
