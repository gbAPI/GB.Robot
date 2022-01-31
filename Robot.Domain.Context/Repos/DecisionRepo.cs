using Microsoft.EntityFrameworkCore;
using Robot.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Robot.DAL.Repos
{
    public class DecisionRepo : BaseRepo<Decision>
    {
        public override List<Decision> GetAll()
        {
            return _table.Include(x => x.RequiredFields).ToList();
        }

        public override Decision Get(int id)
        {
            return _table.Include(x => x.RequiredFields).FirstOrDefault(x=>x.ID == id);
        }
    }
}
