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

            this.lessons = selectedLessons;
            if (lessons != null)
            {
                TxbCabinet.Text = Convert.ToString(lessons.Cabinet);
                TxbLesson.Text = lessons.Lesson;
                TxbTeacher.Text = Convert.ToString(lessons.IdTeacher);
                TxbNumber.Text = Convert.ToString(lessons.NumberOfLesson);
                CmbClass.Text = Convert.ToString(lessons.IdClass);
                CmbDay.Text = Convert.ToString(lessons.IdDay);
            }

            CmbDay.ItemsSource = DbConnect.entObj.Days.ToList();
            CmbDay.SelectedValuePath = "Id";
            CmbDay.DisplayMemberPath = "DayName";

            CmbClass.ItemsSource = DbConnect.entObj.Class.ToList();
            CmbClass.SelectedValuePath = "Id";
            CmbClass.DisplayMemberPath = "ClassNumber";
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lessons != null)
            {
                lessons.NumberOfLesson = Convert.ToInt32(TxbNumber.Text);
                lessons.Lesson = TxbLesson.Text;
                lessons.Cabinet = Convert.ToInt32(TxbCabinet.Text);
                lessons.IdTeacher = Convert.ToInt32(TxbTeacher.Text);
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
