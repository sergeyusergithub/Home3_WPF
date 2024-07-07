using Home3_WPF.Resource;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<ToDo> toDoList;
        public List<ToDo> todoList
        {
            get { return toDoList; }
            set
            {
                toDoList = value;
                OnPropertyChanged();

            }
        }

        public int CountDone { get; set; }

        public CreateToDo createToDo;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            toDoList = new List<ToDo>();
            

            todoList.Add(new ToDo("Родиться", new DateTime(2024, 01, 10), "Важно!",false));
            todoList.Add(new ToDo("Посадить сына", new DateTime(2024, 01, 11), "Важно!!",false));
            todoList.Add(new ToDo("Построить дерево", new DateTime(2024, 01, 12), "Важно!!!", false));
            todoList.Add(new ToDo("Вырастить дом", new DateTime(2024, 01, 13), "Важно!!!!!",false));
            todoList.Add(new ToDo("Умереть", new DateTime(2024, 01, 15), "Важно!!!!!!", false));

            RefreshToDoList();
           


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            CountDone = todoList.Where(e => e.Doing == true).ToList().Count;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("todoList"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountDone"));
        }

        private void CheckboxEnableToDo_Checked(object sender, RoutedEventArgs e)
        {
            if (listToDo.SelectedItem == null) return;

            int index = todoList.IndexOf(listToDo.SelectedItem as ToDo);
            todoList[index].Doing = true;
            OnPropertyChanged();


        }

        private void CheckboxEnableToDo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (listToDo.SelectedItem == null) return;

            int index = todoList.IndexOf(listToDo.SelectedItem as ToDo);
            todoList[index].Doing = false;
            OnPropertyChanged();

        }

        public void RefreshToDoList()
        {
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = todoList;
        }

        private void ButtonRemoveToDo_Click(object sender, RoutedEventArgs e)
        {
            todoList.Remove(listToDo.SelectedItem as ToDo);
            RefreshToDoList();
            OnPropertyChanged();
        }

        private void ButtonAddToDo_Click(Object sender, RoutedEventArgs e)
        {
            createToDo = new CreateToDo();
            createToDo.Show();
            createToDo.Owner = this;
            OnPropertyChanged();
        }

    }
}