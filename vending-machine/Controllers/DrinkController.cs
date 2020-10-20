using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using vending_machine.Repository;
using AutoMapper.Configuration;
using System.Threading.Tasks;
using vending_machine.Models;
using vending_machine.DTOs;
using Microsoft.EntityFrameworkCore;

namespace coffee_machine_dotnet.Controllers
{
    [Route("vending")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        private readonly IUnityOfWork _uof;
        private readonly IMapper _mapper;
        public DrinkController(IUnityOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkDto>>> Get()
        {
            var drink = await _uof.DrinkRepository.Get().ToListAsync();

            var drinkDTO = _mapper.Map<List<DrinkDto>>(drink);
            return drinkDTO;
        }

        [HttpGet]
        [Route("drinks")]  
        public async Task<IEnumerable<DrinkDto>> GetAll()
        {
            var drink = await _uof.DrinkRepository.GetAll();

            return _mapper.Map<List<DrinkDto>>(drink);
        }

        [HttpGet("DrinkByPrice")]
        public async Task<IEnumerable<DrinkDto>> GetDrinkByPreco()
        {
            var drink = await _uof.DrinkRepository.GetDrinkByPrice();

            return _mapper.Map<List<DrinkDto>>(drink);
        }


        [HttpGet("{id}")]   
        public async Task<ActionResult<DrinkDto>> GetDrink(int Id, decimal money)
        {
            string drinkName = string.Empty;
           
            try
            {
                var drink = await _uof.DrinkRepository.GetById(x => x.DrinkId == Id);
                drinkName = drink.Name;
               
                _uof.DrinkRepository.Update(drink);

                if (drink.Price < 0)
                {

                    drink.Stock -= 1;

                    await Delete(drink.DrinkId);
                   // await AddDrinks((DrinkDto)drink); TODO
                   

                    return StatusCode(
                        503,
                        String.Format("There is no stock for {0}", drink.Name)
                    );
                }
                if (money >= drink.Price) 
                {
                  
                    return Ok("Your order has been processed correctly");
                }
                else
                {
                    return BadRequest(
                        String.Format("The inserted money is not enough for the given drink. {0} costs {1}", drink.Name, drink.Price)
                    );
                }

            }
            catch (Exception)
            {
                return StatusCode(
                    404,
                    String.Format("There is no {0} item", drinkName)
                );
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddDrinks([FromBody] DrinkDto drinkdto)
        {
            try 
            {
                _uof.DrinkRepository.Add(_mapper.Map<Drink>(drinkdto));
                await _uof.Commit();

                var drink = _mapper.Map<DrinkDto>(drinkdto);

                return new CreatedAtRouteResult("GetDrink",
                    new { id = drink.DrinkId }, drink);//return StatusCode(201);
             }
            catch (Exception)
            {
                return StatusCode(400,
                    String.Format("There is no {0} item", drinkdto.Name)
                );
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDrink(int id, [FromBody] DrinkDto drinkdto)
        {
            try
            {
                if (id != drinkdto.DrinkId)
                {
                    return BadRequest();
                }

                var actuaDrink = _uof.DrinkRepository.GetById(x => x.DrinkId == id);
                
                await Delete(actuaDrink.Id);

           
                var drink = _mapper.Map<Drink>(drinkdto); 

                _uof.DrinkRepository.Update(drink);
                
                await _uof.Commit();

                return StatusCode(200, drink);

            } catch(Exception)
            {
                return StatusCode(400,
                    String.Format("There is no {0} item", drinkdto.Name)
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DrinkDto>> Delete(int id)
        {
            var drink = await _uof.DrinkRepository.GetById(p => p.DrinkId == id);

            if (drink == null)
            {
                return NotFound();
            }

            _uof.DrinkRepository.Delete(drink);
            await _uof.Commit();

            var drinkDto = _mapper.Map<DrinkDto>(drink);

            return drinkDto;
        }


        [HttpGet("AmountByItem")]
        public async Task<IEnumerable<DrinkDto>> GetAmountByDrink(int id)
        {
            
            try
            {
                var itemAmount = (await _uof.DrinkRepository.GetAll()).Count();
              
                var drink = await _uof.DrinkRepository.GetById(x => x.DrinkId == id);
               

            
                if (drink.Price != 0 && drink.Stock !=0)
                {

                    var amount = drink.Price * itemAmount; //TODO:

                }
                if (drink.Price != 0 && drink.Stock != 0 && drink.Date == DateTime.Today && itemAmount >= 5)
                {
                    decimal price = drink.Price * (decimal)0.05; //TODO:

                }

                _uof.DrinkRepository.Update(drink);

                return (IEnumerable<DrinkDto>)StatusCode(200, drink); //TODO:
            }
            catch (Exception)
            {
                return null; //TODO:
            }

          
        }
     
    }
}
