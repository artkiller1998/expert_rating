using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expert
{
    public partial class Form1 : Form
    {
        int count_experts;
        int count_objects;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                count_experts = Int32.Parse(textBox1.Text);
                count_objects = Int32.Parse(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Неверные входные данные!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        
            
            Form2 newForm = new Form2(count_experts, count_objects);
            newForm.Show();
        }
    }
}
