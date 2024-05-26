using LessonScheduler.AppFiles;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LessonScheduler.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        private Users users;
        public EditUserPage(Users selectedUsers)
        {
            InitializeComponent();

            CmbRole.ItemsSource = DbConnect.entObj.Role.ToList();
            CmbRole.SelectedValuePath = "Id";
            CmbRole.DisplayMemberPath = "Name";

            this.users = selectedUsers;

            if (users != null)
            {
                TxbLogin.Text = Convert.ToString(users.Login);
                TxbName.Text = Convert.ToString(users.Name);
                PsbPassword.Password = Convert.ToString(users.Password);
                CmbRole.SelectedValue = users.IdRole;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (users != null)
            {
                users.Name = TxbName.Text;
                users.Login = TxbLogin.Text;
                users.Password = PsbPassword.Password;
                users.IdRole = Convert.ToInt32(CmbRole.SelectedIndex + 1);

                DbConnect.entObj.SaveChanges();
                DbConnect.entObj.Users.ToList();

                MessageBox.Show("Изменения внесены успешно!",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                FrameApp.frmObj.Navigate(new AdminPage());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new AdminPage());
        }
    }
}
