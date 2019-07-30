using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    class Ballv5d5 : Ballv5d4
    {
        #region Summary
        // 07/12/2017
        // - Moved fields to top
        // - Added bool collisionDetected to base class and changed occurances of
        //      IsLaunched to collisionDetected in CheckCollisionWithRectangle(RectangleF r)
        //      and base call to function.
        // - This version of Ball detects collision on corners of the ball. The previous
        //      collision detection(in version 5.4) would detect collision on the sides, but there
        //      would be overlapping of the ball and the corner often times.
        // - This version should also adds some addition detection to the 
        //      base collision detection.(Not confirmed with testing) 
        //
        // - Used by Ballv6d0
        #endregion Summary
        //float distanceFromClosestCorner = 0;
        float angleToClosestCorner = 0;
        //float ballcornerAngleDif = 0;
        float tempDist = 0;

        PointF rtAnglePt = new PointF();//(position.X, closestCornerToBall.Y);
        PointF endp = new PointF();

        protected bool showCollisionLine = true;
        #region Debug Options
        // Default values from v4.2
        //      showDebugText = true;
        //      showHeadingMarker = true;
        //      showBallPositionRect = true;
        //
        // Default values from v5.5
        //      showCollisionLine = true;
        //
        #endregion Debug Options
        public Ballv5d5()
        {
        }
        public new void Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)
        {
            base.Load(pos, gameBoundsWidth, gameBoundsHeight);
            LocalLoadAndReset();
        } // end of function Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)

        public new void Reset()
        {
            base.Reset();
            LocalLoadAndReset();
        } // end of function Reset()

        private void LocalLoadAndReset()
        {
            //speed = 4;
            rtAnglePt = position;
            closestCornerToBall = position;

        } // end of function LocalLoadAndReset()

        #region Original build 1
        //public override void CheckCollisionWithRectangle(RectangleF r)
        //{
        //    base.CheckCollisionWithRectangle(r);
        //    #region previous closest attempt at collision detection
        //    distanceFromClosestCorner = DistanceBetween(position, closestCornerToBall);
        //    angleToClosestCorner = AngleBetween2(position, closestCornerToBall);

        //    #endregion previous closest attempt at collision detection
        //    #region GIGGITY GIGGITY!!
        //    PointF tempd = new PointF();
        //    tempd.X = position.X + velocity.X;
        //    tempd.Y = position.Y + velocity.Y;
        //    if (UseHypLenAsTempDist())
        //    {
        //        tempDist = DistanceBetween(tempd, closestCornerToBall);
        //        //tempDist = DistanceBetween(position, closestCornerToBall);
        //    }
        //    else
        //    {
        //        if ((position.X < tempRect.X || position.X > tempRect.X + tempRect.Width)
        //            && position.Y > tempRect.Y && position.Y < tempRect.Y + tempRect.Height)
        //        {
        //            tempDist = (DistanceBetween(tempd, rc) - tempRect.Width / 2);
        //            //tempDist = (DistanceBetween(position, rc) - tempRect.Width / 2);
        //        }
        //        if (position.X > tempRect.X && position.X < tempRect.X + tempRect.Width
        //            && (position.Y < tempRect.Y || position.Y > tempRect.Y + tempRect.Height))
        //        {
        //            tempDist = (DistanceBetween(tempd, rc) - tempRect.Height / 2);
        //            //tempDist = (DistanceBetween(position, rc) - tempRect.Height / 2);
        //        }
        //    }
        //    if (tempDist < radius)
        //    {
        //        //IsLaunched = false;
        //        collisionDetected = true;
        //    }
        //    #endregion GIGGITY GIGGITY!!
        //    //base.CheckCollisionWithRectangle(r);
        //} // end of function CheckCollisionWithRectangle(RectangleF r)
        #endregion Original build 1

        #region attempting rebuild
        public new void CheckCollisionWithRectangle(Blockv0d1 b)
        {
            base.CheckCollisionWithRectangle(b);

        } // end of function CheckCollisionWithRectangle(RectangleF r)
        #endregion

        protected bool BetweenRectBoundsX(RectangleF rect)
        {
            if (position.X > rect.X && position.X < rect.X + rect.Width
                    && (position.Y < rect.Y || position.Y > rect.Y + rect.Height))
                return true;
            else
                return false;
        } // end of function BetweenRectBoundsX()
        protected bool BetweenRectBoundsY(RectangleF rect)
        {
            if ((position.X < rect.X || position.X > rect.X + rect.Width)
                    && position.Y > rect.Y && position.Y < rect.Y + rect.Height)
                return true;
            else
                return false;
        } // end of function BetweenRectBoundsY()
        protected bool BetweenRectBoundsX()
        {
            if (position.X > tempRect.X && position.X < tempRect.X + tempRect.Width
                    && (position.Y < tempRect.Y || position.Y > tempRect.Y + tempRect.Height))
                return true;
            else
                return false;
        } // end of function BetweenRectBoundsX()
        protected bool BetweenRectBoundsY()
        {
            if ((position.X < tempRect.X || position.X > tempRect.X + tempRect.Width)
                    && position.Y > tempRect.Y && position.Y < tempRect.Y + tempRect.Height)
                return true;
            else
                return false;
        } // end of function BetweenRectBoundsY()

        protected bool UseHypLenAsTempDist()
        {
            if ((position.X < tempRect.X || position.X > tempRect.X + tempRect.Width)
                && (position.Y < tempRect.Y || position.Y > tempRect.Y + tempRect.Height))
                return true;
            else
                return false;
        } // end of function UseHypLenAsTempDist()

        
        protected void DrawCollisionLine(Graphics g)
        {
            if (UseHypLenAsTempDist())
            {
                endp.X = position.X + Cos(angleToClosestCorner) * tempDist;
                endp.Y = position.Y + Sin(angleToClosestCorner) * tempDist;
            }
            else
            {
                if (BetweenRectBoundsX())
                {
                    endp.X = position.X;
                    endp.Y = rc.Y;
                }
                if (BetweenRectBoundsY())
                {
                    endp.X = rc.X;
                    endp.Y = position.Y;
                }
            }
            g.DrawLine(new Pen(Color.Black, 2), position, endp);
        } // end of function DrawCollisionLine(Graphics g)

        public new void Draw(Graphics g)
        {
            base.Draw(g);
            if (showCollisionLine)
                DrawCollisionLine(g);
        } // end of function Draw(Graphics g)

        #region original DrawDebugInfo(Graphics g, ref PointF textPos)
        //protected override void DrawDebugInfo(Graphics g, ref PointF textPos)
        //{
        //    base.DrawDebugInfo(g, ref textPos);
        //    SolidBrush sb = new SolidBrush(Color.Green);
        //    DrawText(g, "Ballv5d5 Debug Info: ", sb, ref textPos);
        //    DrawText(g, "distance to closest corner: " + distanceFromClosestCorner.ToString(), sb, ref textPos);
        //    DrawText(g, "angle to closest corner: " + (angleToClosestCorner * 180 / pi).ToString(), sb, ref textPos);
        //    DrawText(g, "tempDist: " + tempDist.ToString(), sb, ref textPos);
        //    DrawText(g, "ballCornerAngleDif: " + (ballcornerAngleDif * 180 / pi).ToString(), sb, ref textPos);
        //    DrawText(g, "useHypAsLen: " + UseHypLenAsTempDist().ToString(), sb, ref textPos);
        //} // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
        #endregion original DrawDebugInfo(Graphics g, ref PointF textPos)
        protected override void DrawDebugInfo(Graphics g, ref PointF textPos)
        {
            base.DrawDebugInfo(g, ref textPos);
            SolidBrush sb = new SolidBrush(Color.Green);
            DrawText(g, "Ballv5d5 Debug Info: ", sb, ref textPos);
            
        } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
    }
}

#region Commented section

#region Moved to Ballv4d2 07/13/2017
//protected float AngleBetween2(PointF fromPoint, PointF toPoint)
//{
//    float a = 0;
//    PointF fp = fromPoint;
//    PointF tp = toPoint;
//    float xdif = Difference(tp.X, fp.X);
//    float ydif = Difference(tp.Y, fp.Y);
//    a = (float)Math.Atan2(ydif, xdif);
//    if (a < 0)
//    {
//        a = 2 * pi + a;
//    }
//    return a;
//} // end of function AngleBetween2(PointF fromPoint, PointF toPoint)
//protected float Difference(float f1, float f2)
//{
//    return (f1 - f2);
//} // end of function Difference(float f1, float f2)
#endregion Moved to Ballv4d2  07/13/2017

//private void DrawTestTriangeWithRect(Graphics g)
//{
//    Pen tdpen = new Pen(Color.Red);
//    Pen regpen = new Pen(Color.Green);

//    //if (position.X < tempRect.X || position.X > tempRect.X + tempRect.Width
//    //    && position.Y < tempRect.Y || position.Y > tempRect.Y + tempRect.Height)
//    //{
//    //    g.DrawLine(regpen, position, rtAnglePt);
//    //    g.DrawLine(tdpen, position, closestCornerToBall);
//    //    g.DrawLine(regpen, rtAnglePt, closestCornerToBall);
//    //}
//    //else
//    //{
//    //    g.DrawLine(tdpen, position, rtAnglePt);
//    //    g.DrawLine(regpen, position, closestCornerToBall);
//    //    g.DrawLine(regpen, rtAnglePt, closestCornerToBall);
//    //}
//    g.DrawLine(tdpen, position, rtAnglePt);
//    g.DrawLine(regpen, position, closestCornerToBall);
//    g.DrawLine(regpen, rtAnglePt, closestCornerToBall);

//    //DrawTriangle(g, Color.Green, position, rtAnglePt, closestCornerToBall);
//} // end of function DrawTestTriangeWithRect(Graphics g)

#region older attempt at collision detection
//if (angleToClosestCorner < pi)
//{
//    rtAnglePt = new PointF(closestCornerToBall.X, position.Y);
//    tempDist = DistanceBetween(position, rtAnglePt);
//}

//if (angleToClosestCorner >= pi)
//{

//    if(angle > pi && angle < 3*pi/2)
//    {
//        rtAnglePt.X = closestCornerToBall.X;
//        rtAnglePt.Y = position.Y;
//    }
//    //rtAnglePt = new PointF(position.X, closestCornerToBall.Y);

//    rtAnglePt = new PointF(closestCornerToBall.X, position.Y);
//    //rtAnglePt = new PointF(closestCornerToBall.X, closestCornerToBall.Y + tempRect.Height/2);
//    tempDist = DistanceBetween(position, rtAnglePt);
//}
//if(UseHypLenAsTempDist())
//{
//    rtAnglePt = closestCornerToBall;
//    tempDist = DistanceBetween(position, rtAnglePt);
//}
//if (tempDist < radius)
//    IsLaunched = false;
#endregion  older attempt at collision detection

#region Moved to DrawCollisionLine(Graphics g)
//if (UseHypLenAsTempDist())
//{
//    endp.X = position.X + Cos(angleToClosestCorner) * tempDist;
//    endp.Y = position.Y + Sin(angleToClosestCorner) * tempDist;
//}
//else
//{
//    if(BetweenRectBoundsX())
//    {
//        endp.X = position.X;
//        endp.Y = rc.Y;
//    }
//    if(BetweenRectBoundsY())
//    {
//        endp.X = rc.X;
//        endp.Y = position.Y;
//    }
//}
//g.DrawLine(new Pen(Color.Black, 2), position, endp);
#endregion Moved to DrawCollisionLine(Graphics g)

#endregion Commented section