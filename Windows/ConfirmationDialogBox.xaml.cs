using DinnerTime.Database;
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
    /// <summary>
    /// Interaction logic for ConfirmationDialogBox.xaml
    /// </summary>
    public partial class ConfirmationDialogBox : Window
    {
        public ConfirmationDialogBox()
        {
            InitializeComponent();
        }


        private void deleteButtonPressed(object sender, RoutedEventArgs e)
        {
            //nem frissít automatikusan, mert ugye nem éri el a másik ablak metódusait és adattagjait
            if (MealWindow.ShallBeDeleted != null)
            {
                dbController.RemoveFoodItem(MealWindow.ShallBeDeleted.ID);
            }

            this.Close();
        }

        private void cancelButtonPressed(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            this.Close();
        }
    }
}
