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
using System.Windows.Shapes;
using Parakeet.ViewModel.TaskWindow;

namespace Parakeet.View.TaskWindow
{
    /// <summary>
    /// Logique d'interaction pour TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private TaskViewModel taskViewModel;

        public TaskWindow()
        {
            InitializeComponent();

            taskViewModel = new TaskViewModel(this);

            this.DataContext = taskViewModel;
        }
    }
}
