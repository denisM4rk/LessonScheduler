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
    /// Логика взаимодействия для EditLessonPage.xaml
    /// </summary>
    public partial class EditLessonPage : Page
    {
        private Lessons lessons;

        public EditLessonPage(Lessons selectedLessons)
        {
            InitializeComponent();

            CmbDay.ItemsSource = DbConnect.entObj.Days.ToList();
            CmbDay.SelectedValuePath = "Id";
            CmbDay.DisplayMemberPath = "DayName";

            CmbClass.ItemsSource = DbConnect.entObj.Classes.ToList();
            CmbClass.SelectedValuePath = "Id";
            CmbClass.DisplayMemberPath = "ClassNumber";

            CmbTeacher.ItemsSource = DbConnect.entObj.Users.ToList();
            CmbTeacher.SelectedValuePath = "Id";
            CmbTeacher.DisplayMemberPath = "Name";

            this.lessons = selectedLessons;

            if (lessons != null)
            {
                TxbCabinet.Text = Convert.ToString(lessons.Cabinet);
                TxbLesson.Text = lessons.Lesson;
                CmbTeacher.SelectedValue = lessons.IdUser;
                TxbNumber.Text = Convert.ToString(lessons.NumberOfLesson);
                CmbClass.SelectedValue = lessons.IdClass;
                CmbDay.SelectedValue = lessons.IdDay;
            }         
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lessons != null)
            {
                lessons.NumberOfLesson = Convert.ToInt32(TxbNumber.Text);
                lessons.Lesson = TxbLesson.Text;
                lessons.Cabinet = Convert.ToInt32(TxbCabinet.Text);
                lessons.IdUser = Convert.ToInt32(CmbTeacher.SelectedIndex + 1);
                lessons.IdClass = Convert.ToInt32(CmbClass.SelectedIndex + 1);
                lessons.IdDay = Convert.ToInt32(CmbDay.SelectedIndex + 1);

                DbConnect.entObj.SaveChanges();
                DbConnect.entObj.Lessons.ToList();

                MessageBox.Show("Изменения внесены успешно!",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                FrameApp.frmObj.GoBack();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
    }
}
