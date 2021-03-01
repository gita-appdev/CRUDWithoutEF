using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUDWithoutEF.Data
{
    public class CRUDWithoutEFContext : DbContext
    {
        public CRUDWithoutEFContext (DbContextOptions<CRUDWithoutEFContext> options)
            : base(options)
        {
        }

        public DbSet<CRUD.Models.Personnel> Personnel { get; set; }
    }
}
