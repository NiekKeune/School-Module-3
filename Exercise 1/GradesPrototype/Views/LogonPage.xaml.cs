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
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Views
{
    /// <summary>
    /// Interaction logic for LogonPage.xaml
    /// </summary>
    public partial class LogonPage : UserControl
    {
        public LogonPage()
        {
            InitializeComponent();
        }

        #region Event Members
        public event EventHandler LogonSuccess;
        public event EventHandler LogonFailed;

        // TODO: Exercise 3: Task 1a: Define LogonFailed event

        #endregion

        #region Logon Validation

        // TODO: Exercise 3: Task 1b: Validate the username and password against the Users collection in the MainWindow window
        private void Logon_Click(object sender, RoutedEventArgs e)
        {
            var determineTeacher = (from Teacher t in DataSource.Teachers
                                    where String.Compare(t.UserName, username.Text) == 0
                                    && String.Compare(t.Password, password.Password) == 0
                                    select t).FirstOrDefault();

            if (!String.IsNullOrEmpty(determineTeacher.UserName))
            {
                SessionContext.UserID = determineTeacher.TeacherID;
                SessionContext.UserRole = Role.Teacher;
                SessionContext.UserName = determineTeacher.UserName;
                SessionContext.CurrentTeacher = determineTeacher;

                LogonSuccess(this, null);
                return;
            }


            var determineStudent = (from Student s in DataSource.Students
                                    where String.Compare(s.UserName, username.Text) == 0
                                    && String.Compare(s.Password, password.Password) == 0
                                    select s).FirstOrDefault();

            if (!String.IsNullOrEmpty(determineStudent.UserName))
            {
                SessionContext.UserID = determineStudent.StudentID;
                SessionContext.UserRole = Role.Student;
                SessionContext.UserName = determineStudent.UserName;
                SessionContext.CurrentStudent = determineStudent;

                LogonSuccess(this, null);
                return;
            }

            if (String.IsNullOrEmpty(determineTeacher.UserName) && String.IsNullOrEmpty(determineStudent.UserName))
            {
                LogonFailed(this, null);
                return;
            }
        }
        #endregion
    }
}
