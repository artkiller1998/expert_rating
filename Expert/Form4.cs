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
    public partial class Form4 : Form
    {

        DataTable dt2;
        int count__objects;
        int count__experts;
        DataTable dt4;

        public Form4(DataTable dt)
        {
            InitializeComponent();


            count__objects = dt.Rows.Count;
            count__experts = dt.Columns.Count;
            dt4 = new DataTable();
            dt4 = dt.Copy();
            string exp_j;

            dt2 = new DataTable();
            dt2.Columns.Add(" ");

            for (int i = 1; i < count__experts; i++)
            {
                exp_j = String.Format("Эксперт {0}", i);
                dt2.Rows.Add(exp_j);
            }

            for (int j = 1; j < count__experts; j++)
            {
                exp_j = String.Format("Эксперт {0}", j);
                dt2.Columns.Add(exp_j);
            }

            for (int j = 0; j < dt.Rows.Count; j++)
            { 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                   dt4.Rows[j][i] = dt.Rows[j][i];
                }
            }
            int count_compares = dt4.Columns.Count-2;
            int count_columns = dt4.Columns.Count;

            for (int i = 1; i < count_compares + 1; i++)
            {
                dt4.Columns.Add(String.Format("d{0}", i));
                dt4.Columns.Add(String.Format("d{0}^2", i));
            }

            int dr = dt4.Rows.Count;
            for (int i = 1; i < count__experts; i++)
            {

                for (int j = 1; j < count__experts; j++)
                {
                    dt2.Rows[i - 1][j] = Math.Round(spirmen_count(i, j),3);
                }
            }


            dataGridView1.DataSource = dt2;
            print_in_csv();
        }

        public double spirmen_count(int exp1, int exp2)
        {
            double spirmen;
            double d;
            double sum = 0 ;
            for (int row = 0; row < dt4.Rows.Count; row++)
            {
                d = Math.Pow(Int32.Parse(dt4.Rows[row][exp1].ToString()) - Int32.Parse(dt4.Rows[row][exp2].ToString()),2);
                sum += d;
            }
            spirmen = 1 - ((6 * sum) / (count__objects * (Math.Pow(Int32.Parse(count__objects.ToString()), 2) - 1)));
            return spirmen;
        }

        private void print_in_csv()
        {

            string writePath = @"output_spirmen.csv";

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
