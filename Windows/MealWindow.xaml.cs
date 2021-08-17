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

        public MealWindow()
        {
            InitializeComponent();
            FoodList = new List<FoodDTO>();
            FoodList = dbController.getEveryFood();
            DataContext = this;
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
    }
}
