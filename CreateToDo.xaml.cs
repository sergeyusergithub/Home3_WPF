using Home3_WPF.Resource;
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

namespace Home3_WPF
{
    /// <summary>
    /// Interaction logic for CreateToDo.xaml
    /// </summary>
    public partial class CreateToDo : Window
    {
        
        MainWindow main;
        public CreateToDo()
        {
            InitializeComponent();
            descriptionToDo.Text = "Описания нет";
            dateToDo.SelectedDate = DateTime.Today;
            MainWindow main = (this.Owner as MainWindow);
            this.main = main;
        }

        public static RoutedCommand EnterAddToList1 = new RoutedCommand();

        private void ButtonSaveToDo_Click(Object sender, RoutedEventArgs e)
        {
            (this.Owner as MainWindow).todoList.Add(new ToDo(titleToDo.Text, descriptionToDo.Text, dateToDo.SelectedDate.Value));

            titleToDo.Text = "";
            descriptionToDo.Text = "Описания нет";
            dateToDo.SelectedDate = new DateTime(2024, 01, 10);

            (this.Owner as MainWindow).listToDo.Items.Refresh();
            (this.Owner as MainWindow).OnPropertyChanged();

            this.Close();
        }
    }
}
