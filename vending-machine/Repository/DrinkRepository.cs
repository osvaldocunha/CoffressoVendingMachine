using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vending_machine.Context;
using vending_machine.Models;

namespace vending_machine.Repository
{
    public class DrinkRepository : Repository<Drink>, IDrinkRepository 
    {


        private new readonly AppDbContext _context;

        public DrinkRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Drink>> GetAll()
        {
            return await Get().ToListAsync();
        }


        public async Task<Drink> GetDrink(string drinkName, float money)
        {
            return await Get().Where(d => d.Name.Equals(drinkName)).FirstAsync();
        }

        public async Task<IEnumerable<Drink>> GetDrinkByPrice()
        {
            return await Get().OrderBy(c => c.Price).ToListAsync();
        }

        public async Task<IEnumerable<Drink>> GetAmountByDrink(int id)
        {
            return (IEnumerable<Drink>)await Get().Where(d => d.DrinkId.Equals(id)).FirstAsync();

        }




        //private new readonly AppDbContext _context;

        //public DrinkRepository(AppDbContext context) : base(context)
        //{
        //    _context = context;
        //}

        //public async Task<IEnumerable<Drink>> GetAll()
        //{
        //    return await Get().ToListAsync();
        //}

        //public async Task<IEnumerable<Drink>> GetDrinkByPrice()
        //{
        //    return await Get().OrderBy(c => c.Price).ToListAsync();
        //}

        //public async Task<Drink> GetDrink(string drinkName, float money)
        //{           
        //    return await Get().Where(d => d.Name.Equals(drinkName)).FirstAsync();    
        //}

        //public async Task<IEnumerable<Drink>> GetAmountByDrink(int id)
        //{
        //    return (IEnumerable<Drink>)await Get().Where(d => d.DrinkId.Equals(id) ).FirstAsync();

        //}


    }
}
