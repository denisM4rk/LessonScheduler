using LessonScheduler.AppFiles;
using LessonScheduler.Pages;
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

namespace LessonScheduler.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        private Users users;
        public EditUserWindow(Users selectedUsers)
        {
            InitializeComponent();
            AdminPage.a = 1;

            this.users = selectedUsers;

            if (users != null)
            {
                TxbLogin.Text = Convert.ToString(users.Login);
                TxbName.Text = Convert.ToString(users.Name);
                PsbPassword.Password = Convert.ToString(users.Password);
                users.IdRole = CmbRole.SelectedIndex + 1;
            }

            CmbRole.ItemsSource = DbConnect.entObj.Role.ToList();
            CmbRole.SelectedValuePath = "Id";
            CmbRole.DisplayMemberPath = "Name";
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (users != null)
            {
                users.Name = TxbName.Text;
                users.Login = TxbLogin.Text;
                users.Password = PsbPassword.Password;
                users.IdRole = Convert.ToInt32(CmbRole.SelectedIndex+1);

                DbConnect.entObj.SaveChanges();
                DbConnect.entObj.Users.ToList();

                MessageBox.Show("Изменения внесены успешно!",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                
                this.Close();
            }
        }
    }
}
