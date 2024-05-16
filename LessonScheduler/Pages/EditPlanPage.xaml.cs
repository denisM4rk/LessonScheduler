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
        public EditPlanPage(Plans selectedPlans)
        {
            InitializeComponent();
            
            this.plans = selectedPlans;

            if (plans != null)
            {
                TxbMaterial.Text = Convert.ToString(plans.LessonMaterials);
                TxbStudent.Text = Convert.ToString(plans.StudentsActivity);
                TxbTeacher.Text = Convert.ToString(plans.TeacherActivity);
                TxbTopic.Text = Convert.ToString(plans.LessonTopic);
                CmbType.Text = Convert.ToString(plans.LessonType);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            
            if (plans != null)
            {
                plans.LessonTopic = TxbTopic.Text;
                plans.LessonType = CmbType.Text;
                plans.LessonMaterials = TxbMaterial.Text;
                plans.StudentsActivity = TxbStudent.Text;
                plans.TeacherActivity = TxbTeacher.Text;

                DbConnect.entObj.SaveChanges();
                DbConnect.entObj.Plans.ToList();

                MessageBox.Show("Изменения внесены успешно!",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                FrameApp.frmObj.GoBack();
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
    }
}
