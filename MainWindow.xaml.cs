using Home3_WPF.Resource;
using System.ComponentModel;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            todoList = new List<ToDo>();
            

            todoList.Add(new ToDo("Родиться", "Важно!", new DateTime(2024, 01, 10)));
            todoList.Add(new ToDo("Посадить сына", "Важно!", new DateTime(2024, 01, 11)));
            todoList.Add(new ToDo("Построить дерево", "Важно!", new DateTime(2024, 01, 12)));
            todoList.Add(new ToDo("Вырастить дом", "Важно!", new DateTime(2024, 01, 13)));
            todoList.Add(new ToDo("Умереть", "Важно!", new DateTime(2024, 01, 15)));

            listToDo.ItemsSource = todoList;
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
            ((sender as CheckBox).DataContext as ToDo).Doing = true;
            OnPropertyChanged();
        }

        private void CheckboxEnableToDo_Unchecked(object sender, RoutedEventArgs e)
        {
            ((sender as CheckBox).DataContext as ToDo).Doing = false;
            OnPropertyChanged();

        }

        private void ButtonRemoveToDo_Click(object sender, RoutedEventArgs e)
        {
            todoList.Remove((sender as Button).DataContext as ToDo);
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = toDoList;
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