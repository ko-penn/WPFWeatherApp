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

namespace WPF1.View.UserControls
{
    /// <summary>
    /// Interaction logic for timeTempRow.xaml
    /// </summary>
    public partial class timeTempRow : UserControl
    {
        public timeTempRow()
        {
            InitializeComponent();
        }

        public void SetBox1Text(string value)
        {
            box1.Text = value;
        }
        public void SetBox2Text(string value)
        {
            box2.Text = value;
        }
        public void SetBox3Text(string value)
        {
            box3.Text = value;
        }
        public void SetBox4Text(string value)
        {
            box4.Text = value;
        }
        public void SetBox5Text(string value)
        {
            box5.Text = value;
        }
    }
}
