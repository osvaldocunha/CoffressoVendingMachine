using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vending_machine.DTOs
{
    public class DrinkDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int DrinkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Stock { get; set; }


    }
}
