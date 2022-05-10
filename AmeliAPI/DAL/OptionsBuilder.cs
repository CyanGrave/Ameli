using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class OptionsBuilder
    {
        public abstract DbContextOptions GetOptions<TContext>(string connectionString) where TContext : Context;

    }
}
