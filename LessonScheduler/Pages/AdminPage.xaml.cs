using LessonScheduler.AppFiles;
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
        private Plans selectedPlans;
        public AdminPage()
        {
            InitializeComponent();

            GridUsers.ItemsSource = DbConnect.entObj.Users.ToList();
            GridPlans.ItemsSource = DbConnect.entObj.Plans.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new EditUserPage(selectedUsers));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (GridUsers.SelectedItem != null)
            {
                FrameApp.frmObj.Navigate(new AddUserPage());
            }
            else if (GridPlans.SelectedItem != null)
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

            if (GridPlans.SelectedItem != null)
            {
                var filesForRemoving = GridPlans.SelectedItems.Cast<Plans>().ToList();
                try
                {
                    var result = MessageBox.Show("Вы уверены?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DbConnect.entObj.Plans.RemoveRange(filesForRemoving);
                        DbConnect.entObj.SaveChanges();

                        MessageBox.Show("Данные удалены!",
                                        "Уведомление",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                        GridPlans.ItemsSource = DbConnect.entObj.Plans.ToList();
                    }
                    else
                    {
                        GridPlans.ItemsSource = DbConnect.entObj.Plans.ToList();
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

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new WelcomePage());
        }

        private void GridLessonsPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlans = (Plans)GridPlans.SelectedItem;
        }
    }
}
