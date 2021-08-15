using DinnerTime.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerTime.Database
{
    public static class dbController
    {

        public static SqlConnection connection = new SqlConnection();

        public static void connectDB() {
            string cn_String = Properties.Settings.Default.ConnectionString;

            try
            {
                dbController.connection = new SqlConnection(cn_String);
                dbController.connection.Open();
                Console.WriteLine("Connection was successful!");
            }
            catch (Exception)
            {
                Console.WriteLine("Connection Error!");
                throw;
            }
        }

        public static List<MealDTO> getMealTypes() {

            List<MealDTO> mealHolder = new List<MealDTO>();

            try
            {
                string readString = "select * from [Mealtype]";
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MealDTO meal = new MealDTO(int.Parse(reader["ID"].ToString()), reader["MealType"].ToString());
                            mealHolder.Add(meal);
                        }
                    }
                }
                return mealHolder;
            }
            catch
            {
                throw;
            }
        }




        public static List<FoodDTO> getEveryFood()
        {

            List<FoodDTO> foodHolder = new List<FoodDTO>();

            try
            {
                string readString = "select * from Meal join Mealtype ON Meal.MealTypeID = Mealtype.ID";
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool tempbool;

                            if (int.Parse(reader["MealisFavourite"].ToString()) == 1) {
                                tempbool = true;
                            } else
                            {
                                tempbool = false;
                            }

                            FoodDTO meal = new FoodDTO(int.Parse(reader["ID"].ToString()),
                                                       int.Parse(reader["MealTypeID"].ToString()),
                                                       reader["MealName"].ToString(),
                                                       reader["MealDescription"].ToString(),
                                                       reader["MealMaterials"].ToString(),
                                                       tempbool,
                                                       reader["MealType"].ToString());

                            foodHolder.Add(meal);
                        }
                    }
                }
                return foodHolder;
            }
            catch
            {
                throw;
            }
        }

    }
}
