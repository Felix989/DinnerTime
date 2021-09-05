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

        public static void AddFoodItem(String FoodNameField, String FoodMaterialField, String FoodDescriptionField, int MealTypeID){
            try
            {
                string sql = "insert into Meal(MealName, MealDescription, MealMaterials, MealTypeID, MealisFavourite) " +
                    "VALUES(@FoodNameField, @FoodMaterialField, @FoodDescriptionField, @MealTypeID, 0)";


                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FoodNameField", FoodNameField);
                    command.Parameters.AddWithValue("@FoodMaterialField", FoodMaterialField);
                    command.Parameters.AddWithValue("@FoodDescriptionField", FoodDescriptionField);
                    command.Parameters.AddWithValue("@MealTypeID", MealTypeID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                //
            }
        }

        public static void UpdateFoodItem(int ID, String FoodNameField, String FoodMaterialField, String FoodDescriptionField)
        {
            try
            {
                string sql = "update Meal SET MealName = @FoodNameField, MealDescription = @FoodDescriptionField, MealMaterials = @FoodMaterialField where ID = @ID";


                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FoodNameField", FoodNameField);
                    command.Parameters.AddWithValue("@FoodMaterialField", FoodMaterialField);
                    command.Parameters.AddWithValue("@FoodDescriptionField", FoodDescriptionField);
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                //
            }
        }

        public static void RemoveFoodItem(int ID)
        {
            if (ID != null)
            {
                try
                {
                    string sql = "DELETE FROM Meal WHERE ID = @ID;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    //
                }
            }

        }

    }
}
