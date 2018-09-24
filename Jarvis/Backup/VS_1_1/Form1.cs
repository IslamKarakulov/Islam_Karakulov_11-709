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
    public partial class Form1 : Form
    {
        
        Bitmap btmBack=new Bitmap(500,500);      //изображение
        Bitmap btmFront=new Bitmap(500,500);     //фон
        Graphics grBack;
        Graphics grFront;
        List<myPoint> mas_myPoint = new List<myPoint>();

        public Form1()
        {
            InitializeComponent();
            
            grBack = Graphics.FromImage(btmBack);
            grFront = Graphics.FromImage(btmFront);  
            pictureBox1.Image = btmFront;
            pictureBox1.BackgroundImage = btmBack;
        }




//--------------------------------------ввод точек вручную------------------------------------------------------------------------------------------

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
           
                myPoint p = new myPoint(e.X, e.Y, Convert.ToInt32(tb_diametr.Text), grBack, pictureBox1); 
                mas_myPoint.Add(p);


                grBack.Clear(Color.White);
                
                Dzharvis(mas_myPoint, grBack, pictureBox1);
                pictureBox1.Refresh();
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grBack.Clear(Color.White);
            mas_myPoint.Clear();
            Random r=new Random();
            for (int i = 0; i < Convert.ToInt32(numbers_tb.Text); i++)
            {
                myPoint p = new myPoint(r.Next(10, 490), r.Next(10, 490), Convert.ToInt32(tb_diametr.Text), grBack, pictureBox1);
                mas_myPoint.Add(p);
              
            }
            Dzharvis(mas_myPoint, grBack, pictureBox1);
            pictureBox1.Refresh();
           // mas_myPoint[0].Repaint(grBack, pictureBox1);/
           
           
        }

      

        private void reset_Click(object sender, EventArgs e)
        {
            grBack.Clear(Color.White);
            mas_myPoint.Clear();
            pictureBox1.Refresh();
           
        }

    

        public void Dzharvis(List<myPoint> mas_myPoint, Graphics g, PictureBox pb)
        {

            if (mas_myPoint.Count > 1)
            {

                int maxy = -1;
                int min_myPoint = -1;
                for (int i = 0; i < mas_myPoint.Count; i++)
                {
                    mas_myPoint[i].PaintPoint(g,pb);
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
                mas_myPoint[min_myPoint].Repaint(g, pb);
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
                    if (mas_myPoint[i].ugol <= min_ugol && mas_myPoint[i].use==false)
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

                g.DrawLine(Pens.Red, mas_myPoint[min_myPoint].xcentr, mas_myPoint[min_myPoint].ycentr, mas_myPoint[myPoint_min_ugol].xcentr, mas_myPoint[myPoint_min_ugol].ycentr);
                mas_myPoint[myPoint_min_ugol].Repaint(g, pb);

                
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
                        if (mas_myPoint[i].ugol <= min_ugol )
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
                        g.DrawLine(Pens.Red, mas_myPoint[min_myPoint].xcentr, mas_myPoint[min_myPoint].ycentr, mas_myPoint[myPoint_min_ugol].xcentr, mas_myPoint[myPoint_min_ugol].ycentr);
                        mas_myPoint[myPoint_min_ugol].Repaint(g, pb);
                        pb.Refresh();
                    }
                    pred_myPoint = min_myPoint;
                    min_myPoint = myPoint_min_ugol;

                }
               
            }


        }

        private void reset_Click_1(object sender, EventArgs e)
        {
            grBack.Clear(Color.White);
            mas_myPoint.Clear();
            pictureBox1.Refresh();
        }

        private void but_graph_Click(object sender, EventArgs e)
        {
            Graph_form f2 = new Graph_form();
            f2.Show();
        }

    

      

        private void tb_diametr_SelectedIndexChanged(object sender, EventArgs e)
        {
            grBack.Clear(Color.White);
            for (int i = 0; i < mas_myPoint.Count; i++)
                mas_myPoint[i].d = Convert.ToInt32(tb_diametr.Text);
            Dzharvis(mas_myPoint, grBack, pictureBox1);
            pictureBox1.Refresh();
        }

 
 
        }
    
    }

    public class myPoint{
        public int nom;
        public int x;
        public int xcentr;
        public int ycentr;
        public int y;
        public int d;
        public bool min=false;
        public bool use=false;
        public double ugol=0.0;
        public myPoint(int mouse_x,int mouse_y,int diametr,Graphics g,PictureBox pb)
        {
            x = mouse_x;
            y = mouse_y;
            
            d = diametr;//присваиваем снаружи кооррдинаты мышки и поля для рисования нашему экземпляру класса точка
            xcentr = x + d / 2;
            ycentr = y + d / 2;
           g.DrawEllipse(Pens.Black,x,y,d,d);//рисуем точку
            
           //pb.Refresh();//обновить

          

        
        }

        public void PaintPoint(Graphics g, PictureBox pb)
        {
                   
            g.DrawEllipse(Pens.Black, x, y, d, d);//рисуем точку

        }
        public myPoint(int mouse_x, int mouse_y)
        {
            x = mouse_x;
            y = mouse_y;
        }
    

        public void Repaint(Graphics g,PictureBox pb)
        {
         g.DrawEllipse(Pens.Red,x-d/2,y-d/2,d*2,d*2);
       //  pb.Refresh();//обновтиь
        
        
        }
        public void searchNext(int kor_xB, int kor_yB, int kor_xC, int kor_yC)
        { 
            double dx1,dx2,dy1,dy2;
          
            double x1 = x, y1 = y;
            double x2 = kor_xB, y2 = kor_yB;
            double x3 = kor_xC, y3 = kor_yC;
         
            dx1 = x1 - x2;
            dy1 = y1 - y2;
            dx2 = x3 - x2;
            dy2 = y3 - y2;
            ugol =( Math.Atan2(dx1 * dy2 - dy1 * dx2, dx1 * dx2 + dy1 * dy2) * 180 / Math.PI);
        }

    
    }

