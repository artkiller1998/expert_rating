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
    public partial class Form3 : Form
    {
        List<DataTable> ldt;
        DataTable dt;
        DataTable dt2;
        DataTable dt3;
        float sum = 0;
        float kendall = 0;
        int count__objects;
        int count__experts;

        public Form3(DataTable d_t)
        {
            dt = d_t;
            InitializeComponent();

            ldt = new List<DataTable>();
            dt2 = new DataTable();
            dt2.Columns.Add(" ");

            dt3 = new DataTable();
            dt3.Columns.Add(" ");
            count__objects = dt.Rows.Count;
            count__experts = dt.Columns.Count;
            string obj_i;
            string exp_j;

            for (int i = 1; i < count__objects + 1; i++)
            {
                obj_i = String.Format("Объект {0}", i);
                dt3.Rows.Add(obj_i);
                dt3.Columns.Add(obj_i);
            }
            DataTable dat1 = new DataTable();
            dat1 = dt3.Copy();
            DataTable dat2 = new DataTable();
            dat2 = dt3.Copy();
            ldt.Add(dat1);
            ldt.Add(dat2);

            for (int i = 1; i < count__experts ; i++)
            {
                exp_j = String.Format("Эксперт {0}", i);
                dt2.Rows.Add(exp_j);
            }

            for (int j = 1; j < count__experts ; j++)
            {
                exp_j = String.Format("Эксперт {0}", j);
                dt2.Columns.Add(exp_j);
            }

            //dt2.Rows[1][1] = kedall_count_pair(2, 2);

            for (int i = 1; i < count__experts ; i++)
            {

                for (int j = 1; j < count__experts ; j++)
                {
                    dt2.Rows[i-1][j] = Math.Round(kedall_count_pair(i, j),3);
                }
            }
            dataGridView1.DataSource = dt2;
            print_in_csv();

        }

        public float kedall_count_pair(int exp1, int exp2)
        {
            sum = 0;
            int result = 0;
            

            for (int s = 0; s < 2; s++)
            {
                int j = 0;
                if (s == 0)
                    j = exp1;
                else
                    j = exp2;

                for (int i = 0; i < count__objects; i++)
                {
                    for (int l = 0; l < count__objects; l++)
                    {
                        if (Int32.Parse(dt.Rows[i][j].ToString()) < Int32.Parse(dt.Rows[l][j].ToString()))
                        {
                            result = 1;
                        }
                        else if (Int32.Parse(dt.Rows[i][j].ToString()) > Int32.Parse(dt.Rows[l][j].ToString()))
                        {
                            result = -1;
                        }
                        else
                        {
                            result = 0;
                        }
                        ldt[s].Rows[i][l + 1] = result;
                    }
                }
            }

            for (int i = 0; i < count__objects; i++)
            {

                for (int l = 0; l < count__objects; l++)
                {
                    int mult = 1;
                    for (int s = 0; s < 2; s++)
                    {
                        mult = mult * Int32.Parse(ldt[s].Rows[i][l + 1].ToString());
                    }
                    dt3.Rows[i][l + 1] = mult;
                    sum += mult;
                }

            }
            if (sum != 0)
            {
                kendall =  sum / (count__objects * (count__objects - 1));
            }
            return kendall;
        }

        private void print_in_csv()
        {

            string writePath = @"output_kendall.csv";

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

