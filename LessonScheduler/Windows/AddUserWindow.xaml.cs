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
using System.Windows.Shapes;

namespace LessonScheduler.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();

            CmbRole.ItemsSource = DbConnect.entObj.Role.ToList();
            CmbRole.SelectedValuePath = "Id";
            CmbRole.DisplayMemberPath = "Name";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TxbLogin.Text == null)
            {
                MessageBox.Show("Введите логин",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else if (TxbName.Text == null)
            {
                MessageBox.Show("Введите имя",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else if (PsbPassword.Password == null)
            {
                MessageBox.Show("Введите пароль",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else if (CmbRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else
            {
                if (DbConnect.entObj.Users.Count(x => x.Login == TxbName.Text) > 0)
                {
                    MessageBox.Show("Такой ученик уже есть!",
                                    "Уведомление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    return;
                }
                else
                {
                    try
                    {
                        Users usersObj = new Users()
                        {
                            Name = TxbName.Text,
                            Login = TxbLogin.Text,
                            Password = PsbPassword.Password,
                            IdRole = CmbRole.SelectedIndex + 1
                        };

                        DbConnect.entObj.Users.Add(usersObj);
                        DbConnect.entObj.SaveChanges();

                        MessageBox.Show("Новый пользователь добавлен!",
                                        "Уведомление",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка работы приложения: " + ex.Message.ToString(),
                                        "Критический сбой работы приложения",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }
            }
        }
    }
}
