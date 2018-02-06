using System;
using System.Collections;
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
using GradesPrototype.Data;
using GradesPrototype.Services;
using GradesPrototype.Views;

namespace GradesPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GotoLogon();
        }

        #region Navigation
        public void GotoLogon()
        {
            logonPage.Visibility = Visibility.Visible;
            studentsPage.Visibility = Visibility.Collapsed;
            studentProfile.Visibility = Visibility.Collapsed;
        }

        private void GotoStudentsPage()
        {
            studentsPage.Visibility = Visibility.Visible;
            studentProfile.Visibility = Visibility.Collapsed;
            studentsPage.Refresh();
        }

        public void GotoStudentProfile()
        {
            studentProfile.Visibility = Visibility.Visible;
            studentsPage.Visibility = Visibility.Collapsed;
            studentProfile.Refresh();
        }
        #endregion

        #region Event Handlers
        private void Logon_Success(object sender, EventArgs e)
        {
            logonPage.Visibility = Visibility.Collapsed;
            gridLoggedIn.Visibility = Visibility.Visible;
            Refresh();
        }

        // Handle logoff
        private void Logoff_Click(object sender, RoutedEventArgs e)
        {
            // Hide all views apart from the logon page
            gridLoggedIn.Visibility = Visibility.Collapsed;
            studentsPage.Visibility = Visibility.Collapsed;
            studentProfile.Visibility = Visibility.Collapsed;
            logonPage.Visibility = Visibility.Visible;
        }

        // Handle the Back button on the Student page
        private void studentPage_Back(object sender, EventArgs e)
        {
            GotoStudentsPage();
        }

        private void studentsPage_StudentSelected(object sender, StudentEventArgs e)
        {
            SessionContext.CurrentStudent = e.Child;
            GotoStudentProfile();
        }
        #endregion

        #region Display Logic

        private void Refresh()
        {
            if (SessionContext.UserRole == Role.Student)
            {
                GotoStudentProfile();
            }
            else if (SessionContext.UserRole == Role.Teacher)
            {
                GotoStudentsPage();
            }
        }
        #endregion
    }
}
