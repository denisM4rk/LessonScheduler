using LessonScheduler.AppFiles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditPlanPage.xaml
    /// </summary>
    public partial class EditPlanPage : Page
    {
        private Plans plans;
        public EditPlanPage(Plans selectedPlans, int IdUser)
        {
            InitializeComponent();

            TxbIdUser.Text = Convert.ToString(IdUser);

            this.plans = selectedPlans;

            CmbLesson.SelectedValuePath = "Id";
            CmbLesson.DisplayMemberPath = "Lesson";
            CmbLesson.ItemsSource = DbConnect.entObj.Lessons.ToList();

            if (plans != null)
            {
                DateLesson.SelectedDate = plans.LessonDate;
                TxbTopic.Text = Convert.ToString(plans.LessonTopic);
                CmbType.Text = Convert.ToString(plans.LessonType);
                CmbLesson.SelectedValue = plans.IdLesson;
            }

            GridStages.ItemsSource = DbConnect.entObj.Stages.Where(x => x.IdPlan == plans.Id).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);

            if (plans != null)
            {
                plans.LessonTopic = TxbTopic.Text;
                plans.LessonType = CmbType.Text;
                plans.LessonDate = DateLesson.SelectedDate;
                plans.IdLesson = CmbLesson.SelectedIndex+1;

                DbConnect.entObj.SaveChanges();
                DbConnect.entObj.Plans.ToList();
                
                MessageBox.Show("Изменения внесены успешно!",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                int PlanId = plans.Id;
                FrameApp.frmObj.Navigate(new StagePage(PlanId, IdUser));
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);
            FrameApp.frmObj.Navigate(new SchedulePage(IdUser));
        }

        private void BtnStages_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);

            FrameApp.frmObj.Navigate(new StagePage(plans.Id, IdUser));
        }
    }
}
