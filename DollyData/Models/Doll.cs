using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollyData.Models
{
    public class Doll : DbObject
    {
        public string name { get; set; }
        public string description { get; set; }
        public string company { get; set; }
        public string lineName { get; set; }
        public int amount { get; set; }
        public bool isFavorite {  get; set; }


        public Doll()
        {
            
        }
    }
}
