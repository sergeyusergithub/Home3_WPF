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
        public static List<ToDo> toDoList = new List<ToDo>();
        public CreateToDo createToDo = new CreateToDo();
        public MainWindow()
        {
            InitializeComponent();
            

            toDoList.Add(new ToDo("Родиться", new DateTime(2024, 01, 10), "Важно!",false));
            toDoList.Add(new ToDo("Посадить сына", new DateTime(2024, 01, 11), "Важно!!",false));
            toDoList.Add(new ToDo("Построить дерево", new DateTime(2024, 01, 12), "Важно!!!", false));
            toDoList.Add(new ToDo("Вырастить дом", new DateTime(2024, 01, 13), "Важно!!!!!",false));
            toDoList.Add(new ToDo("Умереть", new DateTime(2024, 01, 15), "Важно!!!!!!", false));

            RefreshToDoList();


        }

        public void RefreshToDoList()
        {
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = toDoList;
        }

        private void ButtonRemoveToDo_Click(object sender, RoutedEventArgs e)
        {
            toDoList.Remove(listToDo.SelectedItem as ToDo);
            RefreshToDoList();
        }

        private void ButtonAddToDo_Click(Object sender, RoutedEventArgs e)
        {
            createToDo.Show();
            //this.Owner = createToDo;
            createToDo.Owner = this;
        }

    }
}