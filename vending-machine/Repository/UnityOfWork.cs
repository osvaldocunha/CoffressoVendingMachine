using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vending_machine.Context;

namespace vending_machine.Repository
{
    public class UnityOfWork : IUnityOfWork
    {

        private DrinkRepository _drinkRepository;
        public AppDbContext _context;


        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IDrinkRepository  DrinkRepository
        {
            get
            {
                return _drinkRepository = _drinkRepository ?? new DrinkRepository(_context);
            }
        }
       
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void IDispose()
        {
            _context.Dispose();
        }
    }
}
