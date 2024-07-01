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
        public CreateToDo createToDo;
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

        private void CheckboxEnableToDo_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGridToDo.SelectedItem == null) return;

            int index = toDoList.IndexOf(dataGridToDo.SelectedItem as ToDo);
            toDoList[index].Doing = true;
            endToDo(sender,e);
            

        }

        private void CheckboxEnableToDo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (dataGridToDo.SelectedItem == null) return;

            int index = toDoList.IndexOf(dataGridToDo.SelectedItem as ToDo);
            toDoList[index].Doing = false;
            endToDo(sender, e);

        }



        public void RefreshToDoList()
        {
            dataGridToDo.ItemsSource = null;
            dataGridToDo.ItemsSource = toDoList;
            endToDo(null, null);
        }

        private void ButtonRemoveToDo_Click(object sender, RoutedEventArgs e)
        {
            toDoList.Remove(dataGridToDo.SelectedItem as ToDo);           
            RefreshToDoList();
        }

        private void ButtonAddToDo_Click(Object sender, RoutedEventArgs e)
        {
            createToDo = new CreateToDo();
            createToDo.Show();
            createToDo.Owner = this;
        }

        private void endToDo(object sender, RoutedEventArgs e)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = toDoList.Count;
            progressBar.Value = toDoList.Where(td => td.Doing == true).Count();
            
            
        }

    }
}