using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


using BallSlingerGame02NewLoop.HelperFunctionsFolder;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    public class Ballv4d2
    {
        #region Summary
        // - This is the base calss for the game ball
        // - In this class:
        //      - Basic set up for ball and holds base variables 
        //      - Handles movement
        //      - Handles drawing
        //
        // - Used by Ballv5d4
        #endregion
        protected const float pi = (float)Math.PI;

        public PointF position;
        protected PointF startingPosition;
        //protected PointF velocity;

        //protected float startingSpeed = 5;
        //protected float speed = 5;
        public float radius = 10;
        public float angle = 0;
        protected float degrees = 0;      // rounded to whole number

        protected int gameWidth = 1000;
        protected int gameHeight = 700;
        

        #region Debug Options
        protected bool showDebugText = true;
        protected bool showHeadingMarker = true;
        protected bool showBallPositionRect = true;
        #endregion Debug Options

        public bool visible = true;

        protected PointF textPos;// = new PointF(25, 25);

        public Ballv4d2()
        {
            position = new PointF();
            startingPosition = new PointF();
        }
        public virtual void Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)
        {
            this.gameWidth = gameBoundsWidth;
            this.gameHeight = gameBoundsHeight;
            startingPosition = pos;
            LocalLoadAndReset();
        }
        private void LocalLoadAndReset()
        {
            position = startingPosition;
        }
        public virtual void Reset()
        {
            LocalLoadAndReset();
        }
        #region removed 07/14/2017
        //public virtual void Update()  // removed 07/14/2017
        //{
        //    //if (IsLaunched)
        //    //{
        //    //    CalculateBallMovement();
        //    //}
        //}
        //private void CalculateBallMovement()
        //{
        //    velocity.X = (float)Math.Cos(angle) * speed;
        //    velocity.Y = (float)Math.Sin(angle) * speed;
        //    position.X += velocity.X;
        //    position.Y += velocity.Y;
        //}
        #endregion removed 07/14/2017
        public virtual void Draw(Graphics g)
        {
            textPos = new PointF(25, 25);
            if (showDebugText)
                DrawDebugText(g, ref textPos);

            if (visible)
            {
                DrawBall(g);
                //g.FillEllipse(Brushes.Plum, position.X-radius, position.Y-radius, radius * 2, radius * 2);
            }
            if (showBallPositionRect)
                DrawBallPositionRectDebug(g);

            if (showHeadingMarker)
                DrawAngleMarker(g, angle);
            //DrawCurrentHeadingMarker(g);

        }
        private void DrawDebugText(Graphics g, ref PointF textPos)
        {
            DrawText(g, "Ballv4d2 Debug Info: ", ref textPos);
            DrawText(g, "Angle(not rounded, radians): " + angle.ToString(), ref textPos);
            DrawText(g, "Angle(not rounded, degrees): " + GeneralFunctionsv1.RadiansToDegrees(angle).ToString(), ref textPos);
            DrawText(g, "Angle(rounded, degrees): " + (Round(angle * 180 / pi)).ToString(), ref textPos);
        }
        private void DrawBallPositionRectDebug(Graphics g)
        {
            g.DrawRectangle(Pens.Black, position.X - radius, position.Y - radius, radius, radius);
            g.DrawRectangle(Pens.Black, position.X - radius, position.Y, radius, radius);
            g.DrawRectangle(Pens.Black, position.X, position.Y - radius, radius, radius);
            g.DrawRectangle(Pens.Black, position.X, position.Y, radius, radius);
            g.FillRectangle(Brushes.Wheat, position.X - 5 / 2, position.Y - 5 / 2, 5, 5);
        }

        protected void DrawBall(Graphics g)
        {
            for (int i = 0; i < 360; i++)
            {
                float x, y;
                x = position.X + (float)Math.Cos(i * Math.PI / 180) * radius;
                y = position.Y + (float)Math.Sin(i * Math.PI / 180) * radius;
                //g.FillRectangle(Brushes.Black, new RectangleF(x, y, 1, 1));
                g.DrawLine(Pens.Plum, position, new PointF(x, y));
            }
        }
        private void DrawCurrentHeadingMarker(Graphics g)
        {
            PointF currentHeadingMarker = new PointF();
            PointF endPt = new PointF();
            PointF rightPt = new PointF();
            PointF leftPt = new PointF();
            float angle = this.angle;
            currentHeadingMarker.X = position.X + (float)Math.Cos(angle) * radius;
            currentHeadingMarker.Y = position.Y + (float)Math.Sin(angle) * radius;

            #region arrow drawing part
            endPt.X = currentHeadingMarker.X + (float)Math.Cos(angle) * 15;
            endPt.Y = currentHeadingMarker.Y + (float)Math.Sin(angle) * 15;

            rightPt.X = currentHeadingMarker.X + (float)Math.Cos(angle + pi / 2) * 10;
            rightPt.Y = currentHeadingMarker.Y + (float)Math.Sin(angle + pi / 2) * 10;

            leftPt.X = currentHeadingMarker.X - (float)Math.Cos(angle + pi / 2) * 10;
            leftPt.Y = currentHeadingMarker.Y - (float)Math.Sin(angle + pi / 2) * 10;
            #endregion
            Pen p = new Pen(Color.Black, 3);
            g.DrawLine(p, currentHeadingMarker, endPt);
            g.DrawLine(p, rightPt, endPt);
            g.DrawLine(p, leftPt, endPt);
        } // end of function DrawCurrentHeadingMarker(Graphics g)
        private void DrawAngleMarker(Graphics g, float angle)
        {
            PointF currentHeadingMarker = new PointF();
            PointF endPt = new PointF();
            PointF rightPt = new PointF();
            PointF leftPt = new PointF();
            currentHeadingMarker.X = position.X + (float)Math.Cos(angle) * radius;
            currentHeadingMarker.Y = position.Y + (float)Math.Sin(angle) * radius;

            #region arrow drawing part
            endPt.X = currentHeadingMarker.X + (float)Math.Cos(angle) * 15;
            endPt.Y = currentHeadingMarker.Y + (float)Math.Sin(angle) * 15;

            rightPt.X = currentHeadingMarker.X + (float)Math.Cos(angle + pi / 2) * 10;
            rightPt.Y = currentHeadingMarker.Y + (float)Math.Sin(angle + pi / 2) * 10;

            leftPt.X = currentHeadingMarker.X - (float)Math.Cos(angle + pi / 2) * 10;
            leftPt.Y = currentHeadingMarker.Y - (float)Math.Sin(angle + pi / 2) * 10;
            #endregion
            Pen p = new Pen(Color.Black, 3);
            g.DrawLine(p, currentHeadingMarker, endPt);
            g.DrawLine(p, rightPt, endPt);
            g.DrawLine(p, leftPt, endPt);
        } // end of function DrawCurrentHeadingMarker(Graphics g)

        //public void SetAngle(PointF p)
        //{
        //    PointF rtAnglPos = new PointF();
        //    rtAnglPos.X = p.X;
        //    rtAnglPos.Y = position.Y;
        //    float hyp = DistanceBetween(position, p);
        //    float adj = DistanceBetween(position, rtAnglPos);
        //    float adjOverHyp = adj / hyp;
        //    angle = (float)Math.Acos(adjOverHyp);

        //    if (p.X < position.X)
        //    {
        //        angle = (float)Math.PI - angle;
        //    }
        //    if (p.Y < position.Y)
        //    {
        //        angle = (float)Math.PI * 2 - angle;
        //    }

        //    degrees = angle * (180 / (float)Math.PI);
        //    degrees = (float)Math.Round(degrees);
        //}


        #region local calls to general functions
        protected void DrawText(Graphics g, string msg, ref PointF pos)
        {
            GeneralFunctionsv1.DrawText(g, msg, ref pos);
        }
        protected void DrawText(Graphics g, string msg, Brush b, ref PointF pos)
        {
            GeneralFunctionsv1.DrawText(g, msg, b, ref pos);
        }
        protected float Difference(float f1, float f2)
        {
            return (f1 - f2);
        } // end of function Difference(float f1, float f2)
        protected float DistanceBetween(PointF p1, PointF p2)
        {
            return GeneralFunctionsv1.DistanceBetween(p1, p2);
        }
        protected float AngleBetween(PointF fromPoint, PointF toPoint)
        {
            return GeneralFunctionsv1.AngleBetween(fromPoint, toPoint);
        }
        protected float AngleBetween2(PointF fromPoint, PointF toPoint)
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
        } // end of function AngleBetween2(PointF fromPoint, PointF toPoint)

        #endregion

        #region math float converter helpers
        public float Cos(float angle)
        {
            return (float)Math.Cos(angle);
        }
        public float Sin(float angle)
        {
            return (float)Math.Sin(angle);
        }
        public float Tan(float angle)
        {
            return (float)Math.Tan(angle);
        }
        public float Acos(float angle)
        {
            return (float)Math.Acos(angle);
        }
        public float Asin(float angle)
        {
            return (float)Math.Asin(angle);
        }
        public float Atan(float angle)
        {
            return (float)Math.Atan(angle);
        }
        public float Round(float f)
        {
            return (float)Math.Round(f);
        }
        public float Ceiling(float f)
        {
            return (float)Math.Ceiling(f);
        }
        public float Floor(float f)
        {
            return (float)Math.Floor(f);
        }
        public float Abs(float f)
        {
            return (float)Math.Abs(f);
        }
        #endregion
    }
}
