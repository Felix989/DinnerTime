using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerTime.DTO
{
    public class MealDTO
    {

        public int ID { get; set; }
        public String MealType { get; set; }

        public MealDTO(int id, String mt) {
            ID = id;
            MealType = mt;
        }
    }
}
