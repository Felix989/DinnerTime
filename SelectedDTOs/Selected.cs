using DinnerTime.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerTime.SelectedDTOs
{
    public static class Selected
    {
        public static MealDTO SelectedMeal { get; set; }

        public static void SetSelectedMeal(MealDTO meal) {
            SelectedMeal = meal;
        }
    }
}
