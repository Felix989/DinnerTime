using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerTime.DTO
{
    public class FoodDTO
    {
        public int ID { get; set; }
        public int MealTypeID { get; set; }
        public String MealName { get; set; }
        public String MealDescription { get; set; }//split with ','
        public String MealMaterial { get; set; }
        public bool MealisFavourite { get; set; }
        public String MealType { get; set; }
        public FoodDTO(int id,
            int mealtypeid,
            String mealname,
            String mealdescription,
            string mealmaterial,
            bool isfavourite,
            String mealtype) {
            ID = id;
            MealTypeID = mealtypeid;
            MealName = mealname;
            MealDescription = mealdescription;
            MealMaterial = mealmaterial;
            MealisFavourite = isfavourite;
            MealType = mealtype;
        }
    }
}
