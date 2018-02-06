using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for StudentProfile.xaml
    /// </summary>
    public partial class StudentProfile : UserControl
    {
        public StudentProfile()
        {
            InitializeComponent();
        }

        #region Event Members
        public event EventHandler Back;
        #endregion

        #region Events
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // If the user is not a teacher, do nothing
            if (SessionContext.UserRole != Role.Teacher)
            {
                return;
            }

            // If the user is a teacher, raise the Back event
            // The MainWindow page has a handler that catches this event and returns to the Students page
            if (Back != null)
            {
                Back(sender, e);

                new Random().Next();
            }
        }
        #endregion

        public void Refresh()
        {
            Match matchNames = Regex.Match(SessionContext.CurrentStudent, @"([^ ]+) ([^ ]+)");

            if (matchNames.Success)
            {
                string studentFirstName = matchNames.Groups[1].Value;       //Have to start at [1] because [0] gives the full name, for some reason. Anything after 2 results into a blank result.
                string studentLastName = matchNames.Groups[2].Value;

                ((TextBlock)studentName.Children[0]).Text = studentFirstName;
                ((TextBlock)studentName.Children[1]).Text = studentLastName;
            }

            if (SessionContext.UserRole == Role.Student)
            {
                btnBack.Visibility = Visibility.Collapsed;
            }
        }
    }
}
