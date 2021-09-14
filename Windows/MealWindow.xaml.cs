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
        public static FoodDTO ShallBeDeleted { get; set; }

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

            //An option needs to be implemented to list everything
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

            //Make sudden prompt about what happened -> food updated

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
            //are you sure, you want to remove?
            //make a new window
            //this function will call the window
            //and then "sure" will call the code below:
            if(FilteredMealListSelection.SelectedItem != null){
                try
                {
                    //Task t = Task.Run(() => {
                    //    ShallBeDeleted = (FoodDTO)FilteredMealListSelection.SelectedItem;
                    //    ConfirmationDialogBox window = new ConfirmationDialogBox();
                    //    window.Show();
                    //});

                    //valahogy meg kellene várni, hogy befejeze, majd utána végezzen függvényhívásokat


                    //ShowDialog MEGOLDJA!!!!!!!!!!
                    ShallBeDeleted = (FoodDTO)FilteredMealListSelection.SelectedItem;
                    ConfirmationDialogBox window = new ConfirmationDialogBox();
                    window.ShowDialog();
                    //ShallBeDeleted = new FoodDTO();


                    //if (meal != null)
                    //{
                    //    dbController.RemoveFoodItem(meal.ID);
                    //}
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

            //Make sudden prompt about what happened -> food added

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

        /// <summary>
        /// Cleans the input fields only -> sets their value to "null".
        /// </summary>
        private void clean() {
            FoodNameField.Text = null;
            FoodMaterialField.Text = null;
            FoodDescriptionField.Text = null;
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Collapsed;

            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
