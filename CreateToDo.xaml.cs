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
        public CreateToDo()
        {
            InitializeComponent();
            descriptionToDo.Text = "Описания нет";
            dateToDo.SelectedDate = DateTime.Today.AddDays(1);
        }

        private void ButtonSaveToDo_Click(Object sender, RoutedEventArgs e)
        {
                   
            if (!dateToDo.SelectedDate.HasValue)
            {
                MessageBox.Show("Дата повесилась", Name = "ашыпка");
                return;
            }

            (this.Owner as MainWindow).todoList.Add(new ToDo(titleToDo.Text, (DateTime)dateToDo.SelectedDate, descriptionToDo.Text,false));
            titleToDo.Text = null;
            descriptionToDo.Text = "Описания нет";
            (this.Owner as MainWindow).OnPropertyChanged();
            (this.Owner as MainWindow).RefreshToDoList();

            this.Close();
        }
    }
}
