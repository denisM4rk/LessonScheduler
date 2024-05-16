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
using System.Windows.Shapes;

namespace LessonScheduler.Windows
{
    /// <summary>
    /// Логика взаимодействия для PlanWindow.xaml
    /// </summary>
    public partial class PlanWindow : Window
    {

        private Lessons lessons;
        public PlanWindow(Lessons selectedLessons)
        {
            InitializeComponent();

            //определяем выбранный в таблице урок
            this.lessons = selectedLessons;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //создание плана на урок
                Plans planObj = new Plans()
                {
                    LessonTopic = TxbTopic.Text,
                    LessonType = CmbType.Text,
                    TeacherActivity = TxbTeacher.Text,
                    StudentsActivity = TxbStudent.Text,
                    LessonMaterials = TxbMaterial.Text
                };

                //сохранение плана на урок
                DbConnect.entObj.Plans.Add(planObj);
                DbConnect.entObj.SaveChanges();

                //берем Id созданного плана
                int PlanId = planObj.Id;
                
                if (lessons != null)
                {
                    LessonsPlans lessonsPlansObj = new LessonsPlans()
                    {
                        IdLesson = lessons.Id,
                        IdPlan = PlanId
                    };

                    DbConnect.entObj.LessonsPlans.Add(lessonsPlansObj);
                    DbConnect.entObj.SaveChanges();
                }

                MessageBox.Show("План добавлен!",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка работы приложения: " + ex.Message.ToString(),
                                "Критический сбой работы приложения",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void BtnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All files (*.*)|*.*";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    byte[] fileData = File.ReadAllBytes(filePath);
                    string fileName = System.IO.Path.GetFileName(filePath);

                    LbMaterialFiles.Items.Add(System.IO.Path.GetFileName(fileName));
                    string combinedString = string.Join(", ", LbMaterialFiles.Items.Cast<string>().ToArray());
                    TxbMaterial.Text = combinedString;
                }
            }
            MessageBox.Show("Материалы добавлены!",
                            "Уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}