using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Phone
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Color { get; set; }
        public int Memory { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
        public override string ToString()
        {
            string status;
            if (Availability == true) { status = "В наличии"; }
            else { status = "Продан"; }
            return ($"{Id}. {Brand} {Model} ({Year}) - {Color}, {Memory} ГБ, {Price} руб. - {status}");
        }
    }
}