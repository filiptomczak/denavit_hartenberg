using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenavitHartenberg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void Button_calc_Click(object sender, EventArgs e)
        {
            string theta1 = textBox_t1.Text;
            string theta2 = textBox_t2.Text;
            string theta3 = textBox_t3.Text;
            string lenght1 = textBox_l1.Text;
            string lenght2 = textBox_l2.Text;
            string lenght3 = textBox_l3.Text;
            
            double[,]dh= result(D_H(theta1, lenght1), D_H(theta2, lenght2), D_H(theta3, lenght3));

            label1.Text = dh[0, 0].ToString("f2");
            label2.Text = dh[0, 1].ToString("f2");
            label3.Text = dh[0, 2].ToString("f2");
            label4.Text = dh[0, 3].ToString("f2");
            label5.Text = dh[1, 0].ToString("f2");
            label6.Text = dh[1, 1].ToString("f2");
            label7.Text = dh[1, 2].ToString("f2");
            label8.Text = dh[1, 3].ToString("f2");
            label9.Text = dh[2, 0].ToString("f2");
            label10.Text = dh[2, 1].ToString("f2");
            label11.Text = dh[2, 2].ToString("f2");
            label12.Text = dh[2, 3].ToString("f2");
            label13.Text = dh[3, 0].ToString("f2");
            label14.Text = dh[3, 1].ToString("f2");
            label15.Text = dh[3, 2].ToString("f2");
            label16.Text = dh[3, 3].ToString("f2");
        }

        public double[,] D_H(string theta, string length)
        {
            int t = Int16.Parse(theta);
            int l = Int16.Parse(length);

            double[,] rotX = new double[4, 4]
            {
                {Math.Cos(deg2rad(t)),-Math.Sin(deg2rad(t)),0,0 },
                {Math.Sin(deg2rad(t)),Math.Cos(deg2rad(t)),0,0 },
                { 0,0,1,0 },
                { 0,0,0,1 },
            };
            double[,] trans = new double[4, 4]
            {
                { 1,0,0,l },
                { 0,1,0,0 },
                { 0,0,1,0 },
                { 0,0,0,1 },
            };
            double[,] result = new double[4, 4];
            result = multiplyMatrix(rotX, trans);      
            return result;
        }
        private static double[,]multiplyMatrix(double[,] A, double[,] B)
        {
            double[,] C;
            if (A.GetLength(1) == B.GetLength(0))
            {
                C = new double[A.GetLength(1), B.GetLength(0)];
                for (int i = 0; i < A.GetLength(1); i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        double temp = 0;
                        for (int k = 0; k < B.GetLength(0); k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        C[i, j] = temp;
                    }
                }
            }
            else C = null;
            return C;
        }
        private static double deg2rad(int theta)
        {
            return theta * Math.PI / 180;
        }
        private static double[,] result(double[,] A1, double[,] A2, double[,] A3)
        {
            double[,] A = multiplyMatrix(A1, A2);
            double[,] B = multiplyMatrix(A, A3);

            return B;
        }
    }
}
