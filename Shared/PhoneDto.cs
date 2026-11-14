using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// DTO для передачи данных о телефоне между слоями
    /// </summary>
    public class PhoneDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Memory { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
    }
}
