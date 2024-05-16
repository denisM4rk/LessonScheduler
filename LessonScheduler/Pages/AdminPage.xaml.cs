using LessonScheduler.AppFiles;
using LessonScheduler.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace LessonScheduler.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private Users selectedUsers;
        private LessonsPlans selectedLessonsPlans;
        public AdminPage()
        {
            InitializeComponent();

            GridUsers.ItemsSource = DbConnect.entObj.Users.ToList();
            GridLessonsPlans.ItemsSource = DbConnect.entObj.LessonsPlans.ToList();
        }

        // метод обновления таблиц
        static public int a = 0;
        private void TableWindowUpdate()
        {
            while (true)
            {
                if (a != 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        GridUsers.ItemsSource = DbConnect.entObj.Users.ToList();
                    });

                    a = 0;
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUsers != null)
            {
                EditUserWindow editWindow = new EditUserWindow(selectedUsers);
                editWindow.Show();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (GridUsers.SelectedItem != null)
            {
                AddUserWindow addWindow = new AddUserWindow();
                addWindow.Show();
            }
            else if (GridLessonsPlans.SelectedItem != null)
            {
                BtnAdd.IsEnabled = true;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridUsers.SelectedItem != null)
            {
                var filesForRemoving = GridUsers.SelectedItems.Cast<Users>().ToList();
                try
                {
                    var result = MessageBox.Show("Вы уверены?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DbConnect.entObj.Users.RemoveRange(filesForRemoving);
                        DbConnect.entObj.SaveChanges();

                        MessageBox.Show("Данные удалены!",
                                        "Уведомление",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                        GridUsers.ItemsSource = DbConnect.entObj.Users.ToList();
                    }
                    else
                    {
                        GridUsers.ItemsSource = DbConnect.entObj.Users.ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Критический сбой в работе приложения: " + ex.Message.ToString(),
                                    "Уведомление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }
            }

            if (GridLessonsPlans.SelectedItem != null)
            {
                var filesForRemoving = GridLessonsPlans.SelectedItems.Cast<LessonsPlans>().ToList();
                try
                {
                    var result = MessageBox.Show("Вы уверены?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DbConnect.entObj.LessonsPlans.RemoveRange(filesForRemoving);
                        DbConnect.entObj.SaveChanges();

                        MessageBox.Show("Данные удалены!",
                                        "Уведомление",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                        GridLessonsPlans.ItemsSource = DbConnect.entObj.LessonsPlans.ToList();
                    }
                    else
                    {
                        GridLessonsPlans.ItemsSource = DbConnect.entObj.LessonsPlans.ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Критический сбой в работе приложения: " + ex.Message.ToString(),
                                    "Уведомление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }
            }
        }

        private void GridUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUsers = (Users)GridUsers.SelectedItem;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            new Thread(TableWindowUpdate).Start();

            MessageBox.Show("Данные обновлены!",
                            "Уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new WelcomePage());
        }

        private void GridLessonsPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLessonsPlans = (LessonsPlans)GridLessonsPlans.SelectedItem;
        }
    }
}
