using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Expert
{
    public partial class Form2 : Form
    {
        DataTable dt;
        public Form2(int count__experts, int count__objects)
        {
            InitializeComponent();

            string exp_j;
            string obj_i;
            HashSet<int> set = new HashSet<int>();
            for (int k = 1; k < count__objects + 1; k++)
            {
                set.Add(k);
            }

            dt = new DataTable();
            dt.Columns.Add(" ");
           

            for (int i = 1; i < count__objects + 1; i++)
            {
                obj_i = String.Format("Объект {0}", i);
                dt.Rows.Add(obj_i);
            }

            for (int j = 1; j < count__experts + 1; j++)
            {
                exp_j = String.Format("Эксперт {0}", j);
                dt.Columns.Add(exp_j);
            }
            dataGridView1.DataSource = dt;
            var rand = new Random();
            int value;
            int ts_count = set.Count + 1;
            for (int j = 1; j < count__experts + 1; j++)
            {
                exp_j = String.Format("Эксперт {0}", j);
                HashSet<int> temp_set = new HashSet<int>();
                temp_set.UnionWith(set);
                for (int i = 0; i < count__objects ; i++)
                {
                    do
                    {
                        value = rand.Next(1, ts_count);
                    }
                    while (temp_set.Contains(value) != true);
                    temp_set.Remove(value);
                    dataGridView1.Rows[i].Cells[j].Value = value;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            print_in_csv();

            Form4 newForm42 = new Form4(dt);
            newForm42.Show();

            Form3 newForm32 = new Form3(dt);
            newForm32.Show();

            Form5 newForm52 = new Form5(dt);
            newForm52.Show();

        }

        private void print_in_csv()
        {

            string writePath = @"input_data.csv";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.Write(';');
                    for (int j = 1; j < dataGridView1.ColumnCount; j++)
                    {
                        sw.Write(String.Format("Эксперт {0};", j));
                    }
                    sw.WriteLine();
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            sw.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + ';'); 
                        }
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
