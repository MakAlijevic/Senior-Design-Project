using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Repository
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly DataContext context;
        public ConditionRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Condition>> GetConditions()
        {
            return await context.Conditions.ToListAsync();
        }

        public async Task<Condition> GetCondition(int id)
        {
            var condition = await context.Conditions.FindAsync(id);
            if (condition == null)
            {
                throw new Exception("Condition doesn't exist");
            }
            return condition;
        }
    }
}
