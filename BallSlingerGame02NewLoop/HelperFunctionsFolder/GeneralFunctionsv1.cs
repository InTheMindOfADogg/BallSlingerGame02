using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.HelperFunctionsFolder
{

    public static class GeneralFunctionsv1
    {
        public const float pi = (float)Math.PI;
        static SolidBrush fontBrush = new SolidBrush(Color.Black);
        static Font font = new Font("Arial", 15, FontStyle.Regular, GraphicsUnit.Pixel);

        public static float DistanceBetween(PointF p1, PointF p2)
        {
            float d = 0;
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            float dxs = dx * dx;
            float dys = dy * dy;
            float dsum = dxs + dys;
            d = (float)Math.Sqrt(dsum);
            return d;
        }
        public static float AngleBetween(PointF fromPoint, PointF toPoint)
        {
            PointF p1 = fromPoint;
            PointF p2 = toPoint;
            float a = 0;
            PointF rtAngle = new PointF(fromPoint.X, toPoint.Y);
            float adjLen = 0;
            float oppLen = 0;
            //float hypLen = 0;
            adjLen = DistanceBetween(p1, rtAngle);
            oppLen = DistanceBetween(p2, rtAngle);
            //hypLen = DistanceBetween(p1, p2);
            //a = Acos(adjLen / hypLen);
            a = Atan(oppLen / adjLen);
            return a;
        }
        public static float AngleBetween2(PointF fromPoint, PointF toPoint)
        {
            float a = 0;
            PointF fp = fromPoint;
            PointF tp = toPoint;
            float xdif = Difference(tp.X, fp.X);
            float ydif = Difference(tp.Y, fp.Y);
            a = (float)Math.Atan2(ydif, xdif);
            if (a < 0)
            {
                a = 2 * pi + a;
            }
            return a;
        }
        public static float DegreesToRadians(float degrees)
        {
            return (degrees * (float)Math.PI / 180);
        }
        public static float RadiansToDegrees(float radians)
        {
            return (radians * 180 / (float)Math.PI);
        }

        public static float Difference(float f1, float f2)
        {
            return (f1 - f2);
        }
        #region math float converter helpers
        public static float Cos(float angle)
        {
            return (float)Math.Cos(angle);
        }
        public static float Sin(float angle)
        {
            return (float)Math.Sin(angle);
        }
        public static float Tan(float angle)
        {
            return (float)Math.Tan(angle);
        }
        public static float Acos(float angle)
        {
            return (float)Math.Acos(angle);
        }
        public static float Asin(float angle)
        {
            return (float)Math.Asin(angle);
        }
        public static float Atan(float angle)
        {
            return (float)Math.Atan(angle);
        }
        public static float Round(float f)
        {
            return (float)Math.Round(f);
        }
        public static float Ceiling(float f)
        {
            return (float)Math.Ceiling(f);
        }
        public static float Floor(float f)
        {
            return (float)Math.Floor(f);
        }
        public static float Abs(float f)
        {
            return (float)Math.Abs(f);
        }
        #endregion
        #region math int converter helpers
        public static int Abs(int i)
        {
            return Math.Abs(i);
        }
        #endregion math int converter helpers
        public static void DrawText(Graphics g, string msg, PointF position)
        {
            g.DrawString(msg, font, fontBrush, position);
        }
        public static void DrawText(Graphics g, string msg, ref PointF position)
        {
            g.DrawString(msg, font, fontBrush, position);
            position.Y += g.MeasureString(msg, font).Height;
        }
        public static void DrawText(Graphics g, string msg, Brush b, ref PointF position)
        {
            g.DrawString(msg, font, b, position);
            position.Y += g.MeasureString(msg, font).Height;
        }

        public static void DrawPoint(Graphics g, Color c, PointF p)
        {
            SolidBrush sb = new SolidBrush(c);
            float w = 3;
            float h = 3;
            g.FillRectangle(sb, p.X - w / 2, p.Y - h / 2, w, h);
        } // end of function DrawPoint(Graphics g, Color c, PointF p)
        public static void DrawPoint(Graphics g, Color c, PointF p, float width, float height)
        {
            SolidBrush sb = new SolidBrush(c);
            //float w = width;
            //float h = height;
            g.FillRectangle(sb, p.X - width / 2, p.Y - height / 2, width, height);
        } // end of function DrawPoint(Graphics g, Color c, PointF p, float width, float height)
        public static void DrawTriangle(Graphics g, Color c, PointF p1, PointF p2, PointF p3)
        {
            Pen p = new Pen(c);
            g.DrawLine(p, p1, p2);
            g.DrawLine(p, p2, p3);
            g.DrawLine(p, p1, p3);
        } // end of function DrawTriangle(Graphics g, Color c, PointF p1, PointF p2, PointF p3)
    } 
}
