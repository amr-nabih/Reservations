using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Infrastructure.DataContext
{
    public class DbSeeder
    {
        public static void Initialize(TaskContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
