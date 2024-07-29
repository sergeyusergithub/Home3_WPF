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
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
            SaveToJSON();
        }

        private void CheckboxEnableToDo_Unchecked(object sender, RoutedEventArgs e)
        {
            ((sender as CheckBox).DataContext as ToDo).Doing = false;
            OnPropertyChanged();
            SaveToJSON();

        }

        private void ButtonRemoveToDo_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить дело?", "Удаление дела",MessageBoxButton.YesNo,MessageBoxImage.None,MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {

                todoList.Remove((sender as Button).DataContext as ToDo);
                listToDo.ItemsSource = null;
                listToDo.ItemsSource = toDoList;
                OnPropertyChanged();
                SaveToJSON();
            }
        }

        private void CommandRemoveToDo(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить дело?", "Удаление дела", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {

                todoList.Remove(todoList[(sender as ListBox).SelectedIndex]);
                listToDo.ItemsSource = null;
                listToDo.ItemsSource = toDoList;
                OnPropertyChanged();
                SaveToJSON();
            }
           
        }

        private void ButtonAddToDo_Click(Object sender, RoutedEventArgs e)
        {
            createToDo = new CreateToDo();
            createToDo.Show();
            createToDo.Owner = this;
            OnPropertyChanged();
            SaveToJSON();
        }


        private void SaveTxtFile_Click(Object sender, RoutedEventArgs e)
        {
            if(toDoList.Count != 0)
            {
                string path;
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "Сохранить список дел";
                saveFile.InitialDirectory = Directory.GetCurrentDirectory();
                saveFile.Filter = "Текстовый файл(*.txt)| *.txt";
                saveFile.OverwritePrompt = false;
                if (saveFile.ShowDialog() == true)
                {
                    path = saveFile.FileName;

                    StringBuilder str = new StringBuilder();

                    foreach (var item in todoList)
                    {
                        if (item.Doing)
                        {
                            str.Append("\u2714");
                        }
                        else
                        {
                            str.Append(" ");
                        }

                        str.AppendLine(item.Title + "\n");
                        str.AppendLine(item.Description + "\n");
                        str.AppendLine(item.Date.ToString("dd.MM.yyyy") + "\n\n");

                    }
                    File.WriteAllText(path, str.ToString());


                }
                

            }
            else
            {
                MessageBox.Show("В списке нет дел", "", MessageBoxButton.OK, MessageBoxImage.None);
            }


        }

        private void SaveToJSON()
        {
            string path = Directory.GetCurrentDirectory()  + "\\File\\todolist.json";
            string strJson = JsonConvert.SerializeObject(todoList, Newtonsoft.Json.Formatting.Indented);
            Directory.CreateDirectory("File");
            File.WriteAllText(path,strJson);

        }

        private void LoadToJSON()
        {
            string path = Directory.GetCurrentDirectory() + "\\File\\todolist.json";
            string jsonStr;
            jsonStr = File.ReadAllText(path);
            toDoList = JsonConvert.DeserializeObject<List<ToDo>>(jsonStr);

        }
             
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadToJSON();
            OnPropertyChanged();
            listToDo.ItemsSource = todoList;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveToJSON();
        }
    }
}