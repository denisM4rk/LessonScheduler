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
    /// Логика взаимодействия для NewPlanPage.xaml
    /// </summary>
    public partial class NewPlanPage : Page
    {
        private Lessons lessons;
        public NewPlanPage(Lessons selectedLessons, int IdUser)
        {
            InitializeComponent();

            TxbIdUser.Text = Convert.ToString(IdUser);

            CmbLesson.SelectedValuePath = "Id";
            CmbLesson.DisplayMemberPath = "Lesson";
            CmbLesson.ItemsSource = DbConnect.entObj.Lessons.ToList();

            this.lessons = selectedLessons;

            if (lessons != null)
            {
                CmbLesson.SelectedValue = lessons.Id;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);

            try
            {
                Plans planObj = new Plans()
                {
                    LessonTopic = TxbTopic.Text,
                    LessonType = CmbType.Text,
                    LessonDate = DateLesson.SelectedDate,
                    IdLesson = CmbLesson.SelectedIndex + 1
                };

                DbConnect.entObj.Plans.Add(planObj);
                DbConnect.entObj.SaveChanges();
                
                int PlanId = planObj.Id;

                FrameApp.frmObj.Navigate(new StagePage(PlanId, IdUser));
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
