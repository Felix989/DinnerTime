using DinnerTime.Database;
using DinnerTime.DTO;
using DinnerTime.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DinnerTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MealDTO> MealHolder { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            dbController.connectDB();
            MealHolder = new List<MealDTO>();
            MealHolder = dbController.getMealTypes();
            DataContext = this;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetMealType(object sender, RoutedEventArgs e)
        {

            try
            {
                //SelectedDTOs.Selected.SetSelectedMeal(MealHolder[MealTypeCombobox.SelectedIndex]);
                if (MealTypeCombobox.SelectedItem != null) {
                    SelectedDTOs.Selected.SetSelectedMeal((MealDTO)MealTypeCombobox.SelectedItem);
                    //Console.WriteLine(SelectedDTOs.Selected.SelectedMeal);
                    SelectedDTOs.Selected.SetSelectedFoodList();
                    this.Visibility = Visibility.Hidden;
                    MealWindow window = new MealWindow();
                    //DataContext = window;
                    window.Show();
                }
                else
                {
                    ChooseAMealType.Text = "Meal cannot be empty!";
                    
                    //problem: it runs in multiple threads (thread.sleep is executed first)
                    //ChooseAMealType.Foreground = new SolidColorBrush(Colors.Orchid);
                    //Thread.Sleep(3000);
                    //ChooseAMealType.Foreground = new SolidColorBrush(Colors.White);
                }
            }
            catch
            {
                //do nothing
            }
        }
    }
}
