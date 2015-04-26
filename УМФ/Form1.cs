using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace УМФ
{
    public partial class Form1 : Form
    {
        // параметры установки (названия)
        List<String> params1;
        // параметры установки (значения)
        List<Double> params1_values;
        // параметры материала (названия)
        List<String> params2;
        //параметры материала (значения)
        List<Double> params2_values;

        task2 task2_obj;

        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            task2_obj = new task2();

            params1 = new List<string>();
            params1_values = new List<double>();
            params2 = new List<string>();
            params2_values = new List<double>();
            params1.Add("Длина");
            params1.Add("Количество частей");
            params1.Add("Температура 1");
            params1.Add("Температура 2");
            params1.Add("Точность");
            params1.Add("Время");
            params1.Add("Шаг по длине");
            params1.Add("Шаг по времени");
            params2.Add("Теплопроводность");
            params2.Add("Теплоемкость");
            params2.Add("Плотность");
            params1_values.Add(10);
            params1_values.Add(5);
            params1_values.Add(100);
            params1_values.Add(60);
            params1_values.Add(0.5);
            params1_values.Add(100);
            params1_values.Add(params1_values[0]/params1_values[1]);
            params1_values.Add(1);
            params2_values.Add(203.5);
            params2_values.Add(0.93);
            params2_values.Add(2700);

            //dataGridView1.Rows.Clear();
            //dataGridView2.Rows.Clear();
            //dataGridView3.Rows.Clear();

            dataGridView2.Rows.Add(params1_values.Count);
            dataGridView8.Rows.Add(params1_values.Count);
            for (int i = 0; i < params1.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = params1[i];
                dataGridView2.Rows[i].Cells[1].Value = params1_values[i];
                dataGridView8.Rows[i].Cells[0].Value = params1[i];
                dataGridView8.Rows[i].Cells[1].Value = params1_values[i];
            }
            matParam_Refresh();
            for (int i = 0; i < params1_values[1] + 2; i++)
            {
                dataGridView3.Columns.Add("dgv3_" + Convert.ToString(i), Convert.ToString(i));
                dataGridView10.Columns.Add("dgv10_" + Convert.ToString(i), Convert.ToString(i));
            }
            method1_calc();
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 1;
            method2_calc();
            //dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);
            //dataGridView2.Rows.Remove(dataGridView1.Rows[dataGridView2.Rows.Count - 1]);
            //dataGridView3.Rows.Remove(dataGridView1.Rows[dataGridView3.Rows.Count - 1]);

            trackBar1.Maximum = dataGridView3.Rows.Count - 2;
            trackBar3.Maximum = dataGridView10.Rows.Count - 3;
            //this.Text = dataGridView3.Rows.Count.ToString();
            dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = "АЛЬФА";
            dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = params2_values[0] / params2_values[1] / params2_values[2];

            task3_fillData();
        }

        private void matParam_Refresh() {
            dataGridView1.Rows.Clear();
            if (params2.Count > params2_values.Count){
                dataGridView1.Rows.Add(params2.Count);
                dataGridView9.Rows.Add(params2.Count);
            }else{
                dataGridView1.Rows.Add(params2_values.Count);
                dataGridView9.Rows.Add(params2_values.Count);
            }
            for (int i = 0; i < params2.Count; i++) {
                dataGridView1.Rows[i].Cells[0].Value = params2[i];
                dataGridView9.Rows[i].Cells[0].Value = params2[i];
            }
            for (int i = 0; i < params2_values.Count; i++) {
                dataGridView1.Rows[i].Cells[1].Value = params2_values[i];
                dataGridView9.Rows[i].Cells[1].Value = params2_values[i];
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            matParam_Refresh();
        }

        private void method1_calc() {
            /*
            do
            {
                j++;
                //отрезок
                for (i = 1; i < n + 1; i++)
                {
                    temp[i][j] = koef * dt / dx * (temp[i - 1][j - 1] - 2 * temp[i][j - 1] + temp[i + 1][j - 1]) + temp[i][j - 1];
                }
            } while ((j < t_max) && (temp[1][j] - temp[1][j - 1] > e));
            */
            int j = 0;
            dataGridView3.Rows.Add();
            for(int i=0; i<=Convert.ToInt32(params1_values[1]); i++) dataGridView3.Rows[j].Cells[i].Value = Convert.ToString(20);
            dataGridView3.Rows[j].Cells[0].Value = Convert.ToString(params1_values[2]);
            dataGridView3.Rows[j].Cells[Convert.ToInt32(params1_values[1])+1].Value = Convert.ToString(params1_values[3]);
            do{
                j++;
                dataGridView3.Rows.Add();
                dataGridView3.Rows[j].Cells[0].Value = Convert.ToString(params1_values[2]);
                dataGridView3.Rows[j].Cells[Convert.ToInt32(params1_values[1]) + 1].Value = Convert.ToString(params1_values[3]);
                for (int i = 1; i <= Convert.ToInt32(params1_values[1]); i++)
                    dataGridView3.Rows[j].Cells[i].Value = Convert.ToString(
                        params2_values[0]/params2_values[1]/params2_values[2]*(
                        Convert.ToDouble(dataGridView3.Rows[j - 1].Cells[i - 1].Value) -
                        2 * (Convert.ToDouble(dataGridView3.Rows[j - 1].Cells[i].Value)) +
                        Convert.ToDouble(dataGridView3.Rows[j - 1].Cells[i + 1].Value))+
                        Convert.ToDouble(dataGridView3.Rows[j - 1].Cells[i].Value));
            } while ((j < 1000) && (Convert.ToDouble(dataGridView3.Rows[j].Cells[1].Value) - Convert.ToDouble(dataGridView3.Rows[j - 1].Cells[1].Value) > params1_values[4]));

        }

        private void method2_calc() {
            switch (comboBox4.SelectedIndex) { 
                // точное решение
                case 0: { 
                    int j = 0;
                    int a=1, q=1, p=2;
                    double[] alpha = new double[1000];
                    for (j = 0; j< 1000; j++) {
                        alpha[j] = ((a * (q - p)) / (Math.PI * j)) * Math.Cos(0.5 * Math.PI * j)
                            + ((2 * a * (q + p)) / (Math.PI * Math.PI * j* j)) * Math.Sin(0.5 * Math.PI *j);
                    }
                    j = 0;
                    dataGridView10.Rows.Add();
                    for (int i = 0; i <= Convert.ToInt32(params1_values[1]); i++) dataGridView10.Rows[j].Cells[i].Value = Convert.ToString(20);
                    dataGridView10.Rows[j].Cells[0].Value = Convert.ToString(params1_values[2]);
                    dataGridView10.Rows[j].Cells[Convert.ToInt32(params1_values[1]) + 1].Value = Convert.ToString(params1_values[3]);
                    do{
                        j++;
                        dataGridView10.Rows[j].Cells[0].Value = Convert.ToString(params1_values[2]);
                        dataGridView10.Rows[j].Cells[Convert.ToInt32(params1_values[1]) + 1].Value = Convert.ToString(params1_values[3]);
                        for (int i = 1; i <= Convert.ToInt32(params1_values[1]); i++)
                            //dataGridView10.Rows[j].Cells[i].Value = Convert.ToString(
                            //    params2_values[0]/params2_values[1]/params2_values[2]*(
                            //    Convert.ToDouble(dataGridView10.Rows[j - 1].Cells[i - 1].Value) -
                            //    2 * (Convert.ToDouble(dataGridView10.Rows[j - 1].Cells[i].Value)) +
                            //    Convert.ToDouble(dataGridView10.Rows[j - 1].Cells[i + 1].Value))+
                            //    Convert.ToDouble(dataGridView10.Rows[j - 1].Cells[i].Value));
                            dataGridView10.Rows[j].Cells[i].Value = Convert.ToString(
                                alpha[j]*Math.Exp(-Math.PI * Math.PI * j * j * Convert.ToDouble(params2_values[0])) * Math.Sin(Math.PI * j * i)
                                );
                    }while ((j<1000) && (Convert.ToDouble(dataGridView10.Rows[j].Cells[1].Value) - Convert.ToDouble(dataGridView10.Rows[j - 1].Cells[1].Value) > params1_values[4]/1000));
                    break;
                }
                // неявная схема
                case 1: {
                    double[] a = new double[6]; // { 1, 1, 1, 1, 1, 1 };
                    double[] b = new double[7]; // { -3, -3, -3, -3, -3, -3, -3, };
                    double[] c = new double[6]; // { 1, 1, 1, 1, 1, 1 };
                    double cent = 1 / 1;
                    for(int i=0; i<7;i++) b[i]= -(1+1/1);
                    for(int i=0; i<6;i++){
                        a[i] = 1 / 1; 
                        c[i] = 1 / 1;
                    }


                    dataGridView10.Rows.Add();
                    for (int i = 0; i <= Convert.ToInt32(params1_values[1]); i++) dataGridView10.Rows[0].Cells[i].Value = Convert.ToString(20);
                    dataGridView10.Rows[0].Cells[0].Value = Convert.ToString(params1_values[2]);
                    dataGridView10.Rows[0].Cells[Convert.ToInt32(params1_values[1]) + 1].Value = Convert.ToString(params1_values[3]);   
                    int j=0;
                    do{ 
                        j++;
                        // F
                        dataGridView10.Rows.Add();
                        dataGridView10.Rows[j].Cells[0].Value = Convert.ToString(params1_values[2]);
                        dataGridView10.Rows[j].Cells[Convert.ToInt32(params1_values[1]) + 1].Value = Convert.ToString(params1_values[3]);
                        double[] d = new double[7]; // { 100, 20, 20, 20, 20, 20, 80 };
                        for (int i = 0; i < 7; i++) d[i] = Convert.ToDouble(dataGridView10.Rows[j-1].Cells[i].Value);

                        double[] p = new double[10];
                        double[] q = new double[10];

                        p[2] = c[1] / b[1];
                        q[2] = -d[1] / b[1];
                        int n = 6;
                        for (int i = 2; i < n; i++)
                        {
                            p[i + 1] = c[i] / b[i] - a[i] * p[i];
                            q[i + 1] = a[i] * q[i] - d[i] / b[i] - a[i] * p[i];
                        }
                        double[] x = new double[n - 1];
                        for (int i = n; i > 1; i--)
                        {
                            //x[n + 1] = 0;
                            x[n - i] = q[i];
                            //cout << x[n] << " ";
                        }
                        for (int i = 0; i < 5; i++) dataGridView10.Rows[j].Cells[i + 1].Value = x[i];
                    }while((j<1000) && (Convert.ToDouble(dataGridView10.Rows[j].Cells[0].Value) > Convert.ToDouble(dataGridView10.Rows[j].Cells[1].Value)));
                    break; }
                // явная схема
                case 2: { break; }
            }
        
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //params2_values[e.RowIndex] = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            //else
            //    params2[e.RowIndex] = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }      

        private void drawGrid() {
            Graphics graphics = pictureBox1.CreateGraphics();
            int grid_size = 15;
            // width = 474
            // height = 195
            this.Text = Convert.ToString(pictureBox1.Height.ToString() + " " + pictureBox1.Width.ToString() + " " + graphics.DpiX.ToString() + " " + graphics.DpiY.ToString());
            //graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), pictureBox1.Width / 100, 2 * pictureBox1.Height / 100 * 99, pictureBox1.Width / 100 * 99, pictureBox1.Height / 100 * 99);
            //graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), pictureBox1.Width / 100, 2 * pictureBox1.Height / 100 * 99, pictureBox1.Width / 100, pictureBox1.Height / 100);
            //graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), pictureBox1.Width / 100, pictureBox1.Height / 100, pictureBox1.Width / 100 * (float)0.5, pictureBox1.Height / 100 * (float)7);
            //graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), pictureBox1.Width / 100, pictureBox1.Height / 100, pictureBox1.Width / 100 * (float)1.5, pictureBox1.Height / 100 * (float)7);
            //graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), pictureBox1.Width / 100 * 99, pictureBox1.Height / 100 * 99, pictureBox1.Width / 100 * (float)93, pictureBox1.Height / 100 * (float)96.5);
            //graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), pictureBox1.Width / 100 * 99, pictureBox1.Height / 100 * 99, pictureBox1.Width / 100 * (float)93, pictureBox1.Height / 100 * (float)99.5);


            graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), new Point(Convert.ToInt32(pictureBox1.Width / 100), Convert.ToInt32(pictureBox1.Height / 100 * 99)), new Point(Convert.ToInt32(pictureBox1.Width / 100 * 99), Convert.ToInt32(pictureBox1.Height / 100 * 99)));
            graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), new Point(Convert.ToInt32(pictureBox1.Width / 100), Convert.ToInt32(pictureBox1.Height / 100 * 99)), new Point(Convert.ToInt32(pictureBox1.Width / 100), Convert.ToInt32(pictureBox1.Height / 100)));
            graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), new Point(Convert.ToInt32(pictureBox1.Width / 100), Convert.ToInt32(pictureBox1.Height / 100)), new Point(Convert.ToInt32(pictureBox1.Width / 100 * (float)0.5), Convert.ToInt32(pictureBox1.Height / 100 * (float)7)));
            graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), new Point(Convert.ToInt32(pictureBox1.Width / 100), Convert.ToInt32(pictureBox1.Height / 100)), new Point(Convert.ToInt32(pictureBox1.Width / 100 * (float)1.5), Convert.ToInt32(pictureBox1.Height / 100 * (float)7)));
            graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), new Point(Convert.ToInt32(pictureBox1.Width / 100 * 99), Convert.ToInt32(pictureBox1.Height / 100 * 99)), new Point(Convert.ToInt32(pictureBox1.Width / 100 * (float)93), Convert.ToInt32(pictureBox1.Height / 100 * (float)96.5)));
            graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), new Point(Convert.ToInt32(pictureBox1.Width / 100 * 99), Convert.ToInt32(pictureBox1.Height / 100 * 99)), new Point(Convert.ToInt32(pictureBox1.Width / 100 * (float)93), Convert.ToInt32(pictureBox1.Height / 100 * (float)99.5)));
            //for (int i = 0; i <= pictureBox1.Height / grid_size; i++)
            //{
            //    graphics.DrawLine(new Pen(Color.FromArgb(100,100,100), (float)0.1), 0, pictureBox1.Height - grid_size * i, pictureBox1.Width, pictureBox1.Height-grid_size * i);
            //}
            //for (int i = 0; i <= pictureBox1.Width / grid_size; i++)
            //{
            //    graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), grid_size * i, 0, grid_size * i, pictureBox1.Height);
            //}
        }

        private void drawGraphic() {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            //// x = width / rows.count * currentrow.index
            //// y = height / max(T1, T2) * currentrow.T

            //int count = dataGridView3.Rows.Count-1;
            //Point[] points = new Point[count];
            double max_temperature = Math.Max(params1_values[2], params1_values[3]);
            //for (int i = 0; i < count; i++)
            //{
            //    points[i] = new Point(Convert.ToInt32(pictureBox1.Width / count * i),
            //        pictureBox1.Height - 
            //        Convert.ToInt32(pictureBox1.Height / max_temperature * 
            //        Convert.ToDouble(dataGridView3.Rows[i].Cells[1].Value)));
            //    //graphics.DrawCurve
            //}
            //graphics.DrawCurve(new Pen(Brushes.Red, 2), points);

            //float  width   = pictureBox1.Width / 100 * 80,
            //       heightofone  = pictureBox1.Height / 100 * 80 / (dataGridView3.Rows.Count - 1);

            //graphics.DrawRectangle(new Pen(Brushes.Transparent, (float)(0.5)), 0, 0, width, heightofone);
            //fill_rectangle();
            int count = (int)params1_values[1] + 2;
            Point[] points = new Point[count];
            points[0] = new Point(0, pictureBox1.Height -
                    Convert.ToInt32(pictureBox1.Height / max_temperature *
                    Convert.ToDouble(dataGridView3.Rows[trackBar1.Value].Cells[0].Value)));
            points[count - 1] = new Point(Convert.ToInt32(pictureBox1.Width / count * (count - 1)), pictureBox1.Height -
                    Convert.ToInt32(pictureBox1.Height / max_temperature *
                    Convert.ToDouble(dataGridView3.Rows[trackBar1.Value].Cells[count - 1].Value)));
            for (int i = 0; i < count-1; i++)
            {
                points[i] = new Point(Convert.ToInt32(pictureBox1.Width / count * i),
                    pictureBox1.Height -
                    Convert.ToInt32(pictureBox1.Height / max_temperature *
                    Convert.ToDouble(dataGridView3.Rows[trackBar1.Value].Cells[i].Value)));
            }
            graphics.DrawCurve(new Pen(Brushes.Red, 2), points);
        }

        private void drawGraphic2()
        {
            Graphics graphics = pictureBox2.CreateGraphics();
            graphics.Clear(Color.White);
            //// x = width / rows.count * currentrow.index
            //// y = height / max(T1, T2) * currentrow.T

            //int count = dataGridView3.Rows.Count-1;
            //Point[] points = new Point[count];
            double max_temperature = Math.Max(params1_values[2], params1_values[3]);
            //for (int i = 0; i < count; i++)
            //{
            //    points[i] = new Point(Convert.ToInt32(pictureBox1.Width / count * i),
            //        pictureBox1.Height - 
            //        Convert.ToInt32(pictureBox1.Height / max_temperature * 
            //        Convert.ToDouble(dataGridView3.Rows[i].Cells[1].Value)));
            //    //graphics.DrawCurve
            //}
            //graphics.DrawCurve(new Pen(Brushes.Red, 2), points);

            //float  width   = pictureBox1.Width / 100 * 80,
            //       heightofone  = pictureBox1.Height / 100 * 80 / (dataGridView3.Rows.Count - 1);

            //graphics.DrawRectangle(new Pen(Brushes.Transparent, (float)(0.5)), 0, 0, width, heightofone);
            //fill_rectangle();
            int count = (int)params1_values[1] + 2;
            Point[] points = new Point[count];
            points[0] = new Point(0, pictureBox2.Height -
                    Convert.ToInt32(pictureBox2.Height / max_temperature *
                    Convert.ToDouble(dataGridView10.Rows[trackBar3.Value].Cells[0].Value)));
            points[count - 1] = new Point(Convert.ToInt32(pictureBox1.Width / count * (count - 1)), 
                    pictureBox2.Height -
                    Convert.ToInt32(pictureBox2.Height / max_temperature *
                    Convert.ToDouble(dataGridView10.Rows[trackBar3.Value].Cells[count - 1].Value)));
            for (int i = 0; i < count - 1; i++)
            {
                points[i] = new Point(Convert.ToInt32(pictureBox2.Width / count * i),
                    pictureBox2.Height -
                    Convert.ToInt32(pictureBox2.Height / max_temperature *
                    Convert.ToDouble(dataGridView10.Rows[trackBar3.Value].Cells[i].Value)));
            }
            graphics.DrawCurve(new Pen(Brushes.Red, 2), points);
        }

        private void fill_rectangle() {
            Graphics graphics = pictureBox1.CreateGraphics();

            double min_temp = Math.Min(Math.Min(params1_values[2], params1_values[3]), 20);
            double max_temp = Math.Max(Math.Max(params1_values[2], params1_values[3]), 20);

            int count = (int)params1_values[1] + 2;

            Brush min = Brushes.Aquamarine;
            

            float  width   = pictureBox1.Width / 100 * 80,
                   heightofone  = pictureBox1.Height / 100 * 80 / (dataGridView3.Rows.Count - 1);

            graphics.FillRectangle(Brushes.AliceBlue, 0, 0, width, heightofone);
            

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //drawGrid();
            //pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //drawGrid();
            drawGraphic();
            //pictureBox1.Invalidate();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer4_SplitterMoved(object sender, SplitterEventArgs e)
        {
            dataGridView3.Width = splitContainer4.Panel1.Width;
            pictureBox1.Width = splitContainer4.Panel2.Width;
            drawGrid();
            drawGraphic();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            //this.Text = trackBar1.Value.ToString();
            drawGraphic();
        }

        private void task3_fillData() {
            dataGridView4.Rows.Clear();
            dataGridView4.Rows.Add(task2_obj.param_names.Count);
            dataGridView4.Rows[0].Cells[0].Value = task2_obj.param_names[0];
            dataGridView4.Rows[0].Cells[1].Value = task2_obj.param_values[0];
            for (int i = 1; i < task2_obj.param_names.Count; i++) {
                dataGridView4.Rows[i].Cells[0].Value = task2_obj.param_names[i];
                dataGridView4.Rows[i].Cells[1].Value = task2_obj.param_values[i];
            }

            dataGridView6.Rows.Clear();
            for (int i = 0; i < 3; i++) {
                dataGridView6.Rows.Add();
                for (int j = 0; j < 3; j++) dataGridView6.Rows[i].Cells[j].Value = 20;
            }
        }

        private void task2_iterCalc(int pIter) {
            dataGridView7.Rows.Clear();
            
            for(int i=0; i<3;i++) {
                dataGridView7.Rows.Add();
                for(int j=0; j<3; j++) dataGridView7.Rows[i].Cells[j].Value = dataGridView6.Rows[i].Cells[j].Value;
            }

            for(int i=0; i<pIter; i++){
                //угловые точки
                dataGridView7.Rows[0].Cells[0].Value = task2_obj.calc(
                    task2_obj.param_values[0], task2_obj.param_values[2], 
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[0].Value),
                    Convert.ToDouble(dataGridView7.Rows[0].Cells[1].Value));
                dataGridView7.Rows[0].Cells[2].Value = task2_obj.calc(
                    task2_obj.param_values[0], task2_obj.param_values[3],
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[2].Value),
                    Convert.ToDouble(dataGridView7.Rows[0].Cells[1].Value));
                dataGridView7.Rows[2].Cells[0].Value = task2_obj.calc(
                    task2_obj.param_values[1], task2_obj.param_values[2],
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[0].Value),
                    Convert.ToDouble(dataGridView7.Rows[2].Cells[1].Value));
                dataGridView7.Rows[2].Cells[2].Value = task2_obj.calc(
                    task2_obj.param_values[1], task2_obj.param_values[3],
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[2].Value),
                    Convert.ToDouble(dataGridView7.Rows[2].Cells[1].Value));

                //срединные по краям
                dataGridView7.Rows[0].Cells[1].Value = task2_obj.calc(
                    task2_obj.param_values[0],
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[1].Value),
                    Convert.ToDouble(dataGridView7.Rows[0].Cells[0].Value),
                    Convert.ToDouble(dataGridView7.Rows[0].Cells[2].Value));
                dataGridView7.Rows[2].Cells[1].Value = task2_obj.calc(
                    task2_obj.param_values[1],
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[1].Value),
                    Convert.ToDouble(dataGridView7.Rows[2].Cells[0].Value),
                    Convert.ToDouble(dataGridView7.Rows[2].Cells[2].Value));
                dataGridView7.Rows[1].Cells[0].Value = task2_obj.calc(
                    task2_obj.param_values[2],
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[1].Value),
                    Convert.ToDouble(dataGridView7.Rows[2].Cells[0].Value),
                    Convert.ToDouble(dataGridView7.Rows[0].Cells[0].Value));
                dataGridView7.Rows[1].Cells[2].Value = task2_obj.calc(
                    task2_obj.param_values[3],
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[1].Value),
                    Convert.ToDouble(dataGridView7.Rows[2].Cells[2].Value),
                    Convert.ToDouble(dataGridView7.Rows[0].Cells[2].Value));

                //центр
                dataGridView7.Rows[1].Cells[1].Value = task2_obj.calc(
                    Convert.ToDouble(dataGridView7.Rows[0].Cells[1].Value),
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[0].Value),
                    Convert.ToDouble(dataGridView7.Rows[2].Cells[1].Value),
                    Convert.ToDouble(dataGridView7.Rows[1].Cells[2].Value));
            }
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            task2_iterCalc(trackBar2.Value);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            drawGraphic2();
        }
    }
}
