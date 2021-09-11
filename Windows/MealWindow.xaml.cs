using DinnerTime.Database;
using DinnerTime.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DinnerTime.Windows
{
    public partial class MealWindow : Window
    {
        public List<FoodDTO> FoodList { get; set; }
        public List<FoodDTO> FilteredFoodList { get; set; }

        public MealWindow()
        {
            InitializeComponent();
            FoodList = new List<FoodDTO>();
            FoodList = dbController.getEveryFood();
            FilterFood();
            DataContext = this;
        }


        private List<FoodDTO> FilterFood()
        {
            FilteredFoodList = new List<FoodDTO>();
            FoodList = new List<FoodDTO>();


            FoodList = dbController.getEveryFood();
            foreach (var food in FoodList)
            {
                if (food.MealTypeID == SelectedDTOs.Selected.SelectedMeal.ID) {
                    FilteredFoodList.Add(food);
                }
            }

            return FilteredFoodList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void MealSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (FilteredMealListSelection.SelectedItem != null) {
                FoodDTO meal = (FoodDTO)FilteredMealListSelection.SelectedItem;
                //MessageBox.Show("Name: " + meal.MealName + "\n" + 
                //                "Description: " + meal.MealDescription + "\n" +
                //                "Materials: " + meal.MealMaterial);
                FoodNameField.Text = meal.MealName;
                FoodMaterialField.Text = meal.MealMaterial;
                FoodDescriptionField.Text = meal.MealDescription;
            }
        }

        private void UpdateFoodItem(object sender, RoutedEventArgs e)
        {
            FoodDTO meal = (FoodDTO)FilteredMealListSelection.SelectedItem;

            if (FoodNameField.Text.ToString() != null &&
                FoodMaterialField.Text.ToString() != null &&
                FoodDescriptionField.Text.ToString() != null &&
                meal != null)
            {
                dbController.UpdateFoodItem(meal.ID,
                    FoodNameField.Text.ToString(),
                    FoodMaterialField.Text.ToString(),
                    FoodDescriptionField.Text.ToString());

                refresh();
                clean();
            }
        }

        private void RemoveFoodItem(object sender, RoutedEventArgs e)
        {
            if(FilteredMealListSelection.SelectedItem != null){

                try
                {
                    FoodDTO meal = (FoodDTO)FilteredMealListSelection.SelectedItem;

                    if (meal != null)
                    {
                        dbController.RemoveFoodItem(meal.ID);
                    }
                }
                catch
                {
                    //pass
                }

            }
            refresh();
            clean();
        }

        private void AddFoodItem(object sender, RoutedEventArgs e)
        {

            if (FoodNameField.Text.ToString() != null &&
                FoodMaterialField.Text.ToString() != null &&
                SelectedDTOs.Selected.SelectedMeal.ID.ToString() != null) 
            {
                dbController.AddFoodItem(FoodNameField.Text.ToString(),
                    FoodMaterialField.Text.ToString(),
                    FoodDescriptionField.Text.ToString(),
                    int.Parse(SelectedDTOs.Selected.SelectedMeal.ID.ToString()));

                refresh();
                clean();
            }
        }

        private void refresh() {
            FilterFood();
            FilteredMealListSelection.ItemsSource = FilteredFoodList;
            FilteredMealListSelection.InvalidateArrange();
            FilteredMealListSelection.Items.Refresh();
            FilteredMealListSelection.UpdateLayout();
        }

        private void clean() {
            FoodNameField.Text = null;
            FoodMaterialField.Text = null;
            FoodDescriptionField.Text = null;
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
