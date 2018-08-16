using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FiddlerPlugin
{
    public partial class TestView : UserControl
    {
        public TestView()
        {
            InitializeComponent();
        }

        public void setText(string text)
        {
            textBox1.Text = text;
        }
    }
}
