using iTextSharp.text;
using iTextSharp.text.pdf;
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
            CmbClass.ItemsSource = DbConnect.entObj.Classes.ToList();

            GridPlans.ItemsSource = DbConnect.entObj.Plans.Where(l => l.Lessons.IdUser == IdUser).ToList();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new WelcomePage());
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

            var userSubjects = DbConnect.entObj.Lessons.FirstOrDefault(x => x.IdUser == selectedLessons.IdUser && x.IdUser == IdUser);

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

            var userSubjects = DbConnect.entObj.Lessons.FirstOrDefault(x => x.IdUser == selectedLessons.IdUser && x.IdUser == IdUser);

            if (userSubjects == null)
            {
                MessageBox.Show("Вы не являетесь преподавателем данного предмета",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                FrameApp.frmObj.Navigate(new NewPlanPage(selectedLessons, IdUser));
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

            var userSubjects = DbConnect.entObj.Lessons.FirstOrDefault(x => x.IdUser == selectedLessons.IdUser && x.IdUser == IdUser);

            if (userSubjects == null)
            {
                MessageBox.Show("Вы не являетесь преподавателем данного предмета",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                FrameApp.frmObj.Navigate(new NewPlanPage(selectedLessons, IdUser));
            }
        }
        private void GridPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlans = (Plans)GridPlans.SelectedItem;

            if (selectedPlans != null)
            {
                GridStages.ItemsSource = DbConnect.entObj.Stages.Where(s => s.IdPlan == selectedPlans.Id).ToList();
            }
        }

        private void BtnEditPlan_Click(object sender, RoutedEventArgs e)
        {
            int IdUser = int.Parse(TxbIdUser.Text);
            FrameApp.frmObj.Navigate(new EditPlanPage(selectedPlans, IdUser));
        }

        private void BtnDeletePlan_Click(object sender, RoutedEventArgs e)
        {
            if (GridPlans.SelectedItem != null)
            {
                var plansForRemoving = GridPlans.SelectedItems.Cast<Plans>().ToList();
                var stages = DbConnect.entObj.Stages.Where(s => s.IdPlan == selectedPlans.Id).ToList();
                try
                {
                    var result = MessageBox.Show("Вы уверены?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DbConnect.entObj.Plans.RemoveRange(plansForRemoving);
                        foreach (var stage in stages)
                        {
                            DbConnect.entObj.Stages.Remove(stage);
                        }
                        DbConnect.entObj.SaveChanges();

                        MessageBox.Show("Данные удалены!",
                                        "Уведомление",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                        GridPlans.ItemsSource = DbConnect.entObj.Plans.ToList();
                        GridStages.Items.Clear();
                    }
                    else
                    {
                        GridPlans.ItemsSource = DbConnect.entObj.Plans.ToList();
                        GridStages.Items.Clear();
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

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            selectedPlans = (Plans)GridPlans.SelectedItem;

            try
            {
                Document document = new Document();

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\План занятия.pdf", FileMode.Create));

                BaseFont baseFont = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", "Identity-H", true);
                Font font = new Font(baseFont, 12);

                document.Open();

                var plan = DbConnect.entObj.Stages.FirstOrDefault(s => s.IdPlan == selectedPlans.Id);

                iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(plan.Plans.LessonTopic, font);
                header.Alignment = Element.ALIGN_CENTER;
                header.SpacingBefore = 10f;
                header.SpacingAfter = 10f;
                document.Add(header);

                PdfPTable table = new PdfPTable(5);

                Font headerFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD);

                table.AddCell(new PdfPCell(new Phrase("№", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                table.AddCell(new PdfPCell(new Phrase("Длительность", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                table.AddCell(new PdfPCell(new Phrase("Материал", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                table.AddCell(new PdfPCell(new Phrase("Деятельность учителя", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                table.AddCell(new PdfPCell(new Phrase("Деятельность ученика", font)) { BackgroundColor = new BaseColor(240, 240, 240) });

                foreach (var row in GridStages.ItemsSource)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(((Stages)row).StageNumber.ToString(), font));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Stages)row).TimeDuration.ToString(), font));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Stages)row).Material.ToString(), font));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Stages)row).TeacherActivity.ToString(), font));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Stages)row).StudentActivity.ToString(), font));
                    table.AddCell(cell);
                }
                document.Add(table);

                document.Close();

                MessageBox.Show("План экспортирован!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка при сохранении" + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSchedulePDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Расписание.pdf", FileMode.Create));

                BaseFont baseFont = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", "Identity-H", true);
                Font font = new Font(baseFont, 12);

                document.Open();

                // Заголовок учебного класса
                var header = DbConnect.entObj.Classes.FirstOrDefault(s => s.Id == CmbClass.SelectedIndex + 1);
                iTextSharp.text.Paragraph headerClass = new iTextSharp.text.Paragraph(header.ClassNumber, font);
                headerClass.Alignment = Element.ALIGN_CENTER;
                headerClass.SpacingBefore = 10f;
                headerClass.SpacingAfter = 10f;
                document.Add(headerClass);

                // Понедельник
                var monday = DbConnect.entObj.Days.FirstOrDefault(s => s.Id == 1);

                iTextSharp.text.Paragraph header1 = new iTextSharp.text.Paragraph(monday.DayName, font);
                header1.Alignment = Element.ALIGN_CENTER;
                header1.SpacingBefore = 10f;
                header1.SpacingAfter = 10f;
                document.Add(header1);

                PdfPTable mondayTable = new PdfPTable(4);
                mondayTable.AddCell(new PdfPCell(new Phrase("№", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                mondayTable.AddCell(new PdfPCell(new Phrase("Урок", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                mondayTable.AddCell(new PdfPCell(new Phrase("Кабинет", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                mondayTable.AddCell(new PdfPCell(new Phrase("Учитель", font)) { BackgroundColor = new BaseColor(240, 240, 240) });

                foreach (var row in GridMonday.ItemsSource)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(((Lessons)row).NumberOfLesson.ToString(), font));
                    mondayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Lesson, font));
                    mondayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Cabinet.ToString(), font));
                    mondayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Users.Name.ToString(), font));
                    mondayTable.AddCell(cell);
                }

                document.Add(mondayTable);

                // Вторник
                var tuesday = DbConnect.entObj.Days.FirstOrDefault(s => s.Id == 2);

                iTextSharp.text.Paragraph header2 = new iTextSharp.text.Paragraph(tuesday.DayName, font);
                header2.Alignment = Element.ALIGN_CENTER;
                header2.SpacingBefore = 10f;
                header2.SpacingAfter = 10f;
                document.Add(header2);

                PdfPTable tuesdayTable = new PdfPTable(4);
                tuesdayTable.AddCell(new PdfPCell(new Phrase("№", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                tuesdayTable.AddCell(new PdfPCell(new Phrase("Урок", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                tuesdayTable.AddCell(new PdfPCell(new Phrase("Кабинет", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                tuesdayTable.AddCell(new PdfPCell(new Phrase("Учитель", font)) { BackgroundColor = new BaseColor(240, 240, 240) });

                foreach (var row in GridTuesday.ItemsSource)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(((Lessons)row).NumberOfLesson.ToString(), font));
                    tuesdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Lesson, font));
                    tuesdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Cabinet.ToString(), font));
                    tuesdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Users.Name.ToString(), font));
                    tuesdayTable.AddCell(cell);
                }

                document.Add(tuesdayTable);

                // Среда
                var wednesday = DbConnect.entObj.Days.FirstOrDefault(s => s.Id == 3);

                iTextSharp.text.Paragraph header3 = new iTextSharp.text.Paragraph(wednesday.DayName, font);
                header3.Alignment = Element.ALIGN_CENTER;
                header3.SpacingBefore = 10f;
                header3.SpacingAfter = 10f;
                document.Add(header3);

                PdfPTable wednesdayTable = new PdfPTable(4);
                wednesdayTable.AddCell(new PdfPCell(new Phrase("№", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                wednesdayTable.AddCell(new PdfPCell(new Phrase("Урок", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                wednesdayTable.AddCell(new PdfPCell(new Phrase("Кабинет", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                wednesdayTable.AddCell(new PdfPCell(new Phrase("Учитель", font)) { BackgroundColor = new BaseColor(240, 240, 240) });

                foreach (var row in GridWednesday.ItemsSource)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(((Lessons)row).NumberOfLesson.ToString(), font));
                    wednesdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Lesson, font));
                    wednesdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Cabinet.ToString(), font));
                    wednesdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Users.Name.ToString(), font));
                    wednesdayTable.AddCell(cell);
                }

                document.Add(wednesdayTable);

                // Четверг
                var thurday = DbConnect.entObj.Days.FirstOrDefault(s => s.Id == 4);

                iTextSharp.text.Paragraph header4 = new iTextSharp.text.Paragraph(thurday.DayName, font);
                header4.Alignment = Element.ALIGN_CENTER;
                header4.SpacingBefore = 10f;
                header4.SpacingAfter = 10f;
                document.Add(header4);

                PdfPTable thursdayTable = new PdfPTable(4);
                thursdayTable.AddCell(new PdfPCell(new Phrase("№", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                thursdayTable.AddCell(new PdfPCell(new Phrase("Урок", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                thursdayTable.AddCell(new PdfPCell(new Phrase("Кабинет", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                thursdayTable.AddCell(new PdfPCell(new Phrase("Учитель", font)) { BackgroundColor = new BaseColor(240, 240, 240) });

                foreach (var row in GridThursday.ItemsSource)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(((Lessons)row).NumberOfLesson.ToString(), font));
                    thursdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Lesson, font));
                    thursdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Cabinet.ToString(), font));
                    thursdayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Users.Name.ToString(), font));
                    thursdayTable.AddCell(cell);
                }

                document.Add(thursdayTable);

                // Пятница
                var friday = DbConnect.entObj.Days.FirstOrDefault(s => s.Id == 5);

                iTextSharp.text.Paragraph header5 = new iTextSharp.text.Paragraph(friday.DayName, font);
                header5.Alignment = Element.ALIGN_CENTER;
                header5.SpacingBefore = 10f;
                header5.SpacingAfter = 10f;
                document.Add(header5);

                PdfPTable fridayTable = new PdfPTable(4);
                fridayTable.AddCell(new PdfPCell(new Phrase("№", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                fridayTable.AddCell(new PdfPCell(new Phrase("Урок", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                fridayTable.AddCell(new PdfPCell(new Phrase("Кабинет", font)) { BackgroundColor = new BaseColor(240, 240, 240) });
                fridayTable.AddCell(new PdfPCell(new Phrase("Учитель", font)) { BackgroundColor = new BaseColor(240, 240, 240) });

                foreach (var row in GridFriday.ItemsSource)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(((Lessons)row).NumberOfLesson.ToString(), font));
                    fridayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Lesson, font));
                    fridayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Cabinet.ToString(), font));
                    fridayTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(((Lessons)row).Users.Name.ToString(), font));
                    fridayTable.AddCell(cell);
                }

                document.Add(fridayTable);

                document.Close();

                MessageBox.Show("Расписание экспортировано!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка при сохранении" + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
