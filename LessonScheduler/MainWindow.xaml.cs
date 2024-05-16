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

namespace LessonScheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        public MainWindow()
        {
            InitializeComponent();

            DbConnect.entObj = new DbSchedulerEntities();

            FrameApp.frmObj = FrmMain;

            FrmMain.Navigate(new Pages.WelcomePage());
        }
    }
}
