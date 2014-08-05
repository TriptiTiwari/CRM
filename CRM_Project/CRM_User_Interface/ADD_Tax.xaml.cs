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

namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for ADD_Tax.xaml
    /// </summary>
    public partial class ADD_Tax : Window
    {
        public ADD_Tax()
        {
            InitializeComponent();
        }

        private void btnTaxMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
