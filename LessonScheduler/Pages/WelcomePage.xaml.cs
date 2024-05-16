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
    /// Логика взаимодействия для WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = DbConnect.entObj.Users.FirstOrDefault(x => x.Login == TxbLogin.Text && x.Password == PsbPassword.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такой пользователь не найден.",
                                    "Уведомление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    switch (userObj.IdRole)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте, администратор " + userObj.Name,
                                            "Уведомление",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);

                            FrameApp.frmObj.Navigate(new AdminPage());
                            break;

                        case 2:
                            MessageBox.Show("Здравствуйте, учитель " + userObj.Name,
                                            "Уведомление",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);

                            FrameApp.frmObj.Navigate(new SchedulePage(userObj.Id));
                            break;
                    }
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
}
