using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VS_1
{
    public partial class Graph_form : Form
    {

        public Graph_form()
        {
            InitializeComponent();
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();



            List<count_time> mas_time = new List<count_time>();
            JarvanIV metod = new JarvanIV();
            myGraphic Exemp = new myGraphic();
            Exemp.DrawOs(pictureBox1, max_point);

            int m_point = Convert.ToInt32(max_point.Text);
            int count_c = Convert.ToInt32(count_usred.Text);
            myStopwatch.Start(); //запуск
            metod.DzharvisRand(1);
            myStopwatch.Stop(); //остановить
            count_time ct = new count_time();
            ct.c_point = 1;
            ct.time = Convert.ToDouble(myStopwatch.Elapsed.TotalSeconds);
            //label3.Text += myStopwatch.Elapsed.TotalSeconds.ToString() + "\n";
            mas_time.Add(ct);

            int cur_point = 0;
            for (int i = 1; i < count_c; i++)
            {

                myStopwatch.Start(); //запуск
                cur_point = i * (m_point / count_c);
                metod.DzharvisRand(cur_point);
                myStopwatch.Stop(); //остановить
                // label3.Text += myStopwatch.Elapsed.TotalSeconds.ToString() + "\n";
                ct.c_point = cur_point;
                ct.time = Convert.ToDouble(myStopwatch.Elapsed.TotalSeconds);
                //  label3.Text = myStopwatch.Elapsed.TotalSeconds.ToString() + "\n";
                mas_time.Add(ct);
            }
            Exemp.DrawGraph(pictureBox1, mas_time, max_point);
        }


    }

    public class JarvanIV
    {

        public void DzharvisRand(int count_point)
        {
            List<myPoint> mas_myPoint = new List<myPoint>();
            
            mas_myPoint.Clear();
            Random r = new Random();
            for (int i = 0; i < count_point; i++)
            {
                myPoint p = new myPoint(r.Next(10, 490), r.Next(10, 490));
                mas_myPoint.Add(p);
            }


            if (mas_myPoint.Count > 0)
            {

                int maxy = -1;
                int min_myPoint = -1;
                for (int i = 0; i < mas_myPoint.Count; i++)
                {
                    if (mas_myPoint[i].y > maxy)
                    {
                        maxy = mas_myPoint[i].y;
                        min_myPoint = i;
                    }
                }
                for (int i = 0; i < mas_myPoint.Count; i++)
                {
                    if (mas_myPoint[i].y == mas_myPoint[min_myPoint].y)
                        if (mas_myPoint[i].x < mas_myPoint[min_myPoint].x)
                        {
                            min_myPoint = i;
                        }
                }
                mas_myPoint[min_myPoint].use = true;

                int myPoint_min_ugol = -1;
                for (int i = 0; i < mas_myPoint.Count; i++)
                {
                    mas_myPoint[i].searchNext(mas_myPoint[min_myPoint].x, mas_myPoint[min_myPoint].y, mas_myPoint[min_myPoint].x + 10, mas_myPoint[min_myPoint].y);
                }
                double min_ugol = 500.0;
                myPoint_min_ugol = -1;
                for (int i = 0; i < mas_myPoint.Count; i++)
                {
                    if (mas_myPoint[i].ugol <= min_ugol && mas_myPoint[i].use == false)
                    {
                        if (mas_myPoint[i].ugol == min_ugol)
                        {
                            double r1 = 0, r2 = 0;
                            r1 = Math.Sqrt((mas_myPoint[min_myPoint].x - mas_myPoint[myPoint_min_ugol].x) * (mas_myPoint[min_myPoint].x - mas_myPoint[myPoint_min_ugol].x) + (mas_myPoint[min_myPoint].y - mas_myPoint[myPoint_min_ugol].y) * (mas_myPoint[min_myPoint].y - mas_myPoint[myPoint_min_ugol].y));
                            r2 = Math.Sqrt((mas_myPoint[min_myPoint].x - mas_myPoint[i].x) * (mas_myPoint[min_myPoint].x - mas_myPoint[i].x) + (mas_myPoint[min_myPoint].y - mas_myPoint[i].y) * (mas_myPoint[min_myPoint].y - mas_myPoint[i].y));
                            if (r2 > r1)
                            {
                                min_ugol = mas_myPoint[i].ugol;
                                myPoint_min_ugol = i;
                            }
                        }
                        else
                        {
                            min_ugol = mas_myPoint[i].ugol;
                            myPoint_min_ugol = i;
                        }

                    }

                }
                //Pen pn=new Pen(Color.Green,3);

               // g.DrawLine(Pens.Red, mas_myPoint[min_myPoint].xcentr, mas_myPoint[min_myPoint].ycentr, mas_myPoint[myPoint_min_ugol].xcentr, mas_myPoint[myPoint_min_ugol].ycentr);
              //  mas_myPoint[myPoint_min_ugol].Repaint(g, pb);


                int pred_myPoint = min_myPoint;
                min_myPoint = myPoint_min_ugol;
                int end_myPoint = min_myPoint;
                myPoint_min_ugol = -1;

                while (myPoint_min_ugol != end_myPoint)
                {
                    for (int i = 0; i < mas_myPoint.Count; i++)
                    {
                        mas_myPoint[i].searchNext(mas_myPoint[min_myPoint].x, mas_myPoint[min_myPoint].y, mas_myPoint[pred_myPoint].x, mas_myPoint[pred_myPoint].y);
                    }
                    min_ugol = 500.0;
                    myPoint_min_ugol = -1;
                    for (int i = 0; i < mas_myPoint.Count; i++)
                    {
                        if (mas_myPoint[i].ugol <= min_ugol)
                        {
                            if (mas_myPoint[i].ugol == min_ugol)
                            {
                                double r1 = 0, r2 = 0;
                                r1 = Math.Sqrt((mas_myPoint[min_myPoint].x - mas_myPoint[myPoint_min_ugol].x) * (mas_myPoint[min_myPoint].x - mas_myPoint[myPoint_min_ugol].x) + (mas_myPoint[min_myPoint].y - mas_myPoint[myPoint_min_ugol].y) * (mas_myPoint[min_myPoint].y - mas_myPoint[myPoint_min_ugol].y));
                                r2 = Math.Sqrt((mas_myPoint[min_myPoint].x - mas_myPoint[i].x) * (mas_myPoint[min_myPoint].x - mas_myPoint[i].x) + (mas_myPoint[min_myPoint].y - mas_myPoint[i].y) * (mas_myPoint[min_myPoint].y - mas_myPoint[i].y));
                                if (r2 > r1)
                                {
                                    min_ugol = mas_myPoint[i].ugol;
                                    myPoint_min_ugol = i;
                                }
                            }
                            else
                            {
                                min_ugol = mas_myPoint[i].ugol;
                                myPoint_min_ugol = i;
                            }

                        }


                    }
                    if ((myPoint_min_ugol != end_myPoint))
                    {
                   //     g.DrawLine(Pens.Red, mas_myPoint[min_myPoint].xcentr, mas_myPoint[min_myPoint].ycentr, mas_myPoint[myPoint_min_ugol].xcentr, mas_myPoint[myPoint_min_ugol].ycentr);
                   //     mas_myPoint[myPoint_min_ugol].Repaint(g, pb);
                   //     pb.Refresh();
                    }
                    pred_myPoint = min_myPoint;
                    min_myPoint = myPoint_min_ugol;

                }

            }

        }
    }


    public class count_time
    {
       public double time;
       public int c_point;
    
    }


    class myGraphic
    {


        public void DrawOs(PictureBox pb1, TextBox t)
        {
            int centrX, centrY;
            //Инициализация объекта Graphics
            Graphics g =pb1.CreateGraphics();
            //Инициализация объекта "перо", цвет - черный
            Pen BlackPen;
            BlackPen = new Pen(Color.Black);
            //Определение координат центра
            centrX = pb1.ClientSize.Width; //Значение ширины
            centrY = pb1.ClientSize.Height; //Значение высоты
            //Прорисовка осей
            //Ось X
            Point KX1, KX2;
            KX1 = new Point(20,0);
            KX2 = new Point(20, centrY );
            g.DrawLine(BlackPen, KX1, KX2);
            //Ось Y
            Point KY1, KY2;
            KY1 = new Point(0 , centrY - 20);
            KY2 = new Point(centrX , centrY-20);
            g.DrawLine(BlackPen, KY1, KY2);
            System.Drawing.Font drawFont = new
            System.Drawing.Font("Arial", 8);
            System.Drawing.SolidBrush drawBrush = new
            System.Drawing.SolidBrush(System.Drawing.Color.Black);
            g.DrawString(0.ToString(), drawFont, drawBrush, 0, 580);
            double y_pixel = Convert.ToInt32(t.Text) / (double)600;

            for (int i=1;i<=11;i++)
            {
                double num = Convert.ToInt32(t.Text)/10;
                g.DrawString((num*i).ToString(), drawFont, drawBrush, (int)(num * i / y_pixel), 580);
                g.DrawEllipse(Pens.Black, 20+(int)(num*i / y_pixel), 580 , 2, 2);
            }

            for (int i = 1; i <= 10; i++)
            {
                double num = (double)i / 10;
                g.DrawString(num.ToString(), drawFont, drawBrush, 0, 580-58*i);
                g.DrawEllipse(Pens.Black,20, 580 - 58 * i, 2, 2);
            }
            g.Dispose();
        }
        public void DrawGraph(PictureBox pb1, List<count_time> pts,TextBox t)
        {
           
           // Random r=new Random();
            Graphics g = pb1.CreateGraphics();
            
            //int xmin = 20;
            //int ymin = 20;
            int xmax = pb1.ClientSize.Width; //Значение ширины
            int ymax = pb1.ClientSize.Height; //Значение высоты
            
            double y_pixel = Convert.ToInt32(t.Text) / (double)600;
            Point oldp = new Point(20,580);
            foreach (var p in pts)
                {
                     Point npt = new Point();
                     npt.X = (int)(p.c_point / y_pixel) + 120;
                     npt.Y =580-(int)(p.time/0.0016);
                     g.DrawLine(new Pen(Color.Red, 1f), oldp, npt);
                     oldp = npt;
                }
        
        }
    }
}
