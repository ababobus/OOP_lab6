﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;

namespace WinFormsApp1.Shapes
{
    class CTriangle : Shape

    {
        private int x, y;
        private int width, height = 30;
        private Color color;
        bool Selected = false;

        public CTriangle()
        {
            this.x = 0;
            this.y = 0;
        }
        public CTriangle(int x, int y)
        {
            this.x = x;
            this.y = y;
            Selected = true;
        }
        private Point[] vertex = new Point[3];
        private void GetVertex()
        {
            //левая вершина
            Point a = new Point(this.x - this.width / 2, this.y + this.height / 2);
            this.vertex[0] = a;
            //верх
            Point b = new Point(this.x, this.y - this.height / 2);
            this.vertex[1] = b;
            //правая
            Point c = new Point(this.x + this.width / 2, this.y + this.height / 2);
            this.vertex[2] = c;
        }

        public void Draw(PaintEventArgs e)
        {
            Graphics graphic = e.Graphics;
            GetVertex();
            graphic.DrawPolygon((Selected ? Form1.PenCircleSelect : Form1.PenCircleNotSelect), this.vertex);
        }

        public bool InShape(int x, int y)
        {
            bool flag = false;
            GetVertex();
            
            int a = (this.vertex[0].X - x) * (this.vertex[1].Y - this.vertex[0].Y) - (this.vertex[1].X - this.vertex[0].X) * (this.vertex[0].Y - y);
            int b = (this.vertex[1].X - x) * (this.vertex[2].Y - this.vertex[1].Y) - (this.vertex[2].X - this.vertex[1].X) * (this.vertex[1].Y - y);
            int c = (this.vertex[2].X - x) * (this.vertex[0].Y - this.vertex[2].Y) - (this.vertex[0].X - this.vertex[2].X) * (this.vertex[2].Y - y);
            
            if ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0))
                flag = true;
            return flag;
        }

        public void SetSelect(bool select)
        {
            Selected = select;
        }
        public void ChangeSelect()
        {
            Selected = !Selected;
        }
        public bool GetSelect()
        {
            return Selected;
        }

        public void MoveX(int num, int start, int end)
        {
            GetVertex();
            if (this.vertex[0].X + num < start)
                this.x = start + this.width / 2;
            else if (this.vertex[2].X + num > end)
                this.x = end - this.width / 2;
            else
                this.x += num;
        }
        public void MoveY(int num, int start, int end)
        {
            GetVertex();
            if (this.vertex[1].Y + num < start)
                this.y = start + this.height / 2;
            else if (this.vertex[0].Y + num > end || this.vertex[2].Y + num > end)
                this.y = end - this.height / 2;
            else
                this.y += num;
        }
        public void ChangeSize(int num)
        {
            if (this.width + num > 0 && this.height + num > 0)
            {
                this.width = this.width + num;
                this.height = this.height + num;
            }
        }
        public void SetColor(Color color)
        {
            if (Selected)
                this.color = color;
        }
    }
}
