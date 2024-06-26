using Home3_WPF.Resource;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Home3_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ToDo> toDoList = new List<ToDo>();
        public MainWindow()
        {
            InitializeComponent();
            descriptionToDo.Text = "Описания нет";
            dateToDo.SelectedDate = DateTime.Today.AddDays(1);

            toDoList.Add(new ToDo("Родиться", new DateTime(2024, 01, 10), "Важно!"));
            toDoList.Add(new ToDo("Посадить сына", new DateTime(2024, 01, 11), "Важно!!"));
            toDoList.Add(new ToDo("Построить дерево", new DateTime(2024, 01, 12), "Важно!!!"));
            toDoList.Add(new ToDo("Вырастить дом", new DateTime(2024, 01, 13), "Важно!!!!!"));
            toDoList.Add(new ToDo("Умереть", new DateTime(2024, 01, 15), "Важно!!!!!!"));

            RefreshToDoList();
            CheckboxEnableToDo_Unchecked(Owner, new RoutedEventArgs()); // Hide right panel


        }

        private void RefreshToDoList()
        {
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = toDoList;
        }

        private void CheckboxEnableToDo_Checked(object sender, RoutedEventArgs e)
        {
            if (groupBoxToDo == null || buttonAdd == null) return;
            groupBoxToDo.Visibility = Visibility.Visible;
            buttonAdd.Visibility = Visibility.Visible;
        }

        private void CheckboxEnableToDo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (groupBoxToDo == null || buttonAdd == null) return;
            groupBoxToDo.Visibility = Visibility.Hidden;
            buttonAdd.Visibility = Visibility.Hidden;
        }

        private void ButtonRemoveToDo_Click(object sender, RoutedEventArgs e)
        {
            toDoList.Remove(listToDo.SelectedItem as ToDo);
            RefreshToDoList();
        }

        private void ButtonAddToDo_Click(Object sender, RoutedEventArgs e)
        {
            if (!dateToDo.SelectedDate.HasValue)
            {
                MessageBox.Show("Дата повесилась", Name = "ашыпка");
                return;
            }

            toDoList.Add(new ToDo(titleToDo.Text, (DateTime)dateToDo.SelectedDate, descriptionToDo.Text));
            titleToDo.Text = null;
            descriptionToDo.Text = "Описания нет";
            RefreshToDoList();
        }

    }
}