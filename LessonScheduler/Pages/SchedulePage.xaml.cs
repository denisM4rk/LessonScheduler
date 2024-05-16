using LessonScheduler.AppFiles;
using LessonScheduler.Windows;
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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        private Lessons selectedLessons;
        private Plans selectedPlans;

        public SchedulePage(int IdUser)
        {
            InitializeComponent();

            TxbIdUser.Text = Convert.ToString(IdUser);

            CmbClass.SelectedValuePath = "Id";
            CmbClass.DisplayMemberPath = "ClassNumber";
            CmbClass.ItemsSource = DbConnect.entObj.Class.ToList();

            GridPlans.ItemsSource = DbConnect.entObj.Plans.Where(p => p.LessonsPlans.Any(l => l.Lessons.IdTeacher == IdUser)).ToList();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void CmbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridMonday.ItemsSource = DbConnect.entObj.Lessons.ToList().Where(x => x.IdDay == 1 && x.IdClass == CmbClass.SelectedIndex + 1);
            GridTuesday.ItemsSource = DbConnect.entObj.Lessons.ToList().Where(x => x.IdDay == 2 && x.IdClass == CmbClass.SelectedIndex + 1);
            GridWednesday.ItemsSource = DbConnect.entObj.Lessons.ToList().Where(x => x.IdDay == 3 && x.IdClass == CmbClass.SelectedIndex + 1);
            GridThursday.ItemsSource = DbConnect.entObj.Lessons.ToList().Where(x => x.IdDay == 4 && x.IdClass == CmbClass.SelectedIndex + 1);
            GridFriday.ItemsSource = DbConnect.entObj.Lessons.ToList().Where(x => x.IdDay == 5 && x.IdClass == CmbClass.SelectedIndex + 1);
        }

        private void BtnEditFridayLesson_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);

            var userSubjects = DbConnect.entObj.Lessons.FirstOrDefault(x => x.IdTeacher == selectedLessons.IdTeacher && x.IdTeacher == IdUser);

            if (userSubjects == null)
            {
                MessageBox.Show("Вы не являетесь преподавателем данного предмета, чтобы изменять расписание",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                if (selectedLessons != null)
                {
                    EditLessonPage editPage = new EditLessonPage(selectedLessons);
                    FrameApp.frmObj.Navigate(editPage);
                }
            }
        }

        private void GridMonday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLessons = (Lessons)GridMonday.SelectedItem;
        }

        private void GridTuesday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLessons = (Lessons)GridTuesday.SelectedItem;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);

            var userSubjects = DbConnect.entObj.Lessons.FirstOrDefault(x => x.IdTeacher == selectedLessons.IdTeacher && x.IdTeacher == IdUser);

            if (userSubjects == null)
            {
                MessageBox.Show("Вы не являетесь преподавателем данного предмета",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                DataGridRow row = sender as DataGridRow;

                PlanWindow planWindow = new PlanWindow(selectedLessons);
                planWindow.Show();
            }
        }

        private void GridWednesday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLessons = (Lessons)GridWednesday.SelectedItem;
        }

        private void GridThursday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLessons = (Lessons)GridThursday.SelectedItem;
        }

        private void GridFriday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLessons = (Lessons)GridFriday.SelectedItem;
        }

        private void BtnPlan_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);

            var userSubjects = DbConnect.entObj.Lessons.FirstOrDefault(x => x.IdTeacher == selectedLessons.IdTeacher && x.IdTeacher == IdUser);

            if (userSubjects == null)
            {
                MessageBox.Show("Вы не являетесь преподавателем данного предмета",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                DataGridRow row = sender as DataGridRow;

                PlanWindow planWindow = new PlanWindow(selectedLessons);
                planWindow.Show();
            }
        }

        private void GridPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlans = (Plans)GridPlans.SelectedItem;
        }

        private void BtnEditPlan_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new EditPlanPage(selectedPlans));
        }
    }
}
