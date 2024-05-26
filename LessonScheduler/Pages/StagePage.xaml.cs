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
    /// Логика взаимодействия для StagePage.xaml
    /// </summary>
    public partial class StagePage : Page
    {
        public StagePage(int planId, int IdUser)
        {
            InitializeComponent();

            TxbIdUser.Text = Convert.ToString(IdUser);

            CmbPlan.SelectedValuePath = "Id";
            CmbPlan.DisplayMemberPath = "LessonTopic";
            CmbPlan.ItemsSource = DbConnect.entObj.Plans.ToList();

            CmbPlan.SelectedValue = planId;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
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

                    LstMaterial.Items.Add(System.IO.Path.GetFileName(fileName));
                    string combinedString = string.Join(", ", LstMaterial.Items.Cast<string>().ToArray());
                    TxbMaterial.Text = combinedString;
                }
            }
            MessageBox.Show("Материалы добавлены!", "Уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void BtnAddStage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stages stageObj = new Stages()
                {
                    StageNumber = Convert.ToInt32(TxbNumber.Text),
                    TimeDuration = Convert.ToDecimal(TxbDuration.Text),
                    Material = TxbMaterial.Text,
                    TeacherActivity = TxbTeacher.Text,
                    StudentActivity = TxbStudent.Text,
                    IdPlan = CmbPlan.SelectedIndex + 1
                };

                DbConnect.entObj.Stages.Add(stageObj);
                DbConnect.entObj.SaveChanges();

                MessageBox.Show("Этап добавлен!",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                GridStages.ItemsSource = DbConnect.entObj.Stages.Where(x => x.IdPlan == stageObj.IdPlan).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка работы приложения: " + ex.Message.ToString(),
                                "Критический сбой работы приложения",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);
            FrameApp.frmObj.Navigate(new SchedulePage(IdUser));
        }
    }
}
