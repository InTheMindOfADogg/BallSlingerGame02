using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    using static HelperFunctionsFolder.GeneralFunctionsv1;
    class Ballv5d4 : Ballv4d2
    {
        #region Summary
        // 07/12/2017
        // -Moved fields to top
        // -Added bool collisionDetected and changed occurances of
        //      IsLaunched to collisionDetected in CheckCollisionWithRectangle(RectangleF r)
        // -This version of Ball detects collision on the sides of the rectangle if the
        //      balls' position(center of ball) falls within the x bounds or y bounds of the rectangle.
        //
        // - Used by Ballv5d5
        //
        #endregion Summary

        protected enum CardinalDirections
        {
            NotSet,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest,
            North,
            NorthEast
        }
        protected CardinalDirections rectCollisionSide;

        protected float remainingDistancex = 0;
        protected float remainingDistancey = 0;
        protected float xOverhang = 0;
        protected float yOverhang = 0;
        protected PointF xdist = new PointF();
        protected PointF ydist = new PointF();
        protected RectangleF tempRect = new RectangleF();
        protected PointF rc = new PointF();
        protected PointF closestCornerToBall = new PointF();

        protected PointF velocity;


        protected float startingSpeed = 5;
        protected float maxSpeed = 5;
        protected float speed = 5;

        public bool IsLaunched = false;
        public bool initialLaunch = true;

        protected bool collisionDetected = false;

        #region Debug Options
        // Default values from v4.2
        //      showDebugText = true;
        //      showHeadingMarker = true;
        //      showBallPositionRect = true;
        #endregion Debug Options
        public Ballv5d4()
        {
            showBallPositionRect = false;
            showHeadingMarker = false;
            visible = true;
            rectCollisionSide = CardinalDirections.NotSet;
        } // end of constructor Ballv5d4()

        public override void Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)
        {
            base.Load(pos, gameBoundsWidth, gameBoundsHeight);
            LocalLoadAndReset();
        } // end of function Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)
        public override void Reset()
        {
            base.Reset();
            LocalLoadAndReset();
        } // end of function Reset()
        private void LocalLoadAndReset()
        {
            collisionDetected = false;
            rectCollisionSide = CardinalDirections.NotSet;
            IsLaunched = false;
            initialLaunch = true;
            ccIndex = -1;

            LoadCollisionCheckMarkers();
        } // end of function LocalLoadAndReset()
        public void Update()
        {
            if (IsLaunched)
                speed = maxSpeed;
            else
                speed = 0;
            SetBallHeadingv2();
            SetCCIndexRange(ballHeading);
            UpdateCollisionCheckMarkers();

            velocity.X = Cos(angle) * speed;
            velocity.Y = Sin(angle) * speed;
            position.X += velocity.X;
            position.Y += velocity.Y;
            //UpdateCollisionCheckMarkers();


        } // end of function Update()


        public Blockv0d1 block;

        public int ccIndex = -1;
        int[] ccIRange = new int[3];
        private void SetCCIndexRange(CardinalDirections ballHeading)
        {

            if (ballHeading == CardinalDirections.SouthEast)
            {
                ccIRange[0] = 0;
                ccIRange[1] = 1;
                ccIRange[2] = 2;
            }
            if (ballHeading == CardinalDirections.SouthWest)
            {
                ccIRange[0] = 2;
                ccIRange[1] = 3;
                ccIRange[2] = 4;
            }
            if (ballHeading == CardinalDirections.NorthWest)
            {
                ccIRange[0] = 4;
                ccIRange[1] = 5;
                ccIRange[2] = 6;
            }
            if (ballHeading == CardinalDirections.NorthEast)
            {
                ccIRange[0] = 6;
                ccIRange[1] = 7;
                ccIRange[2] = 0;
            }
            if (ballHeading == CardinalDirections.East)
            {
                ccIRange[0] = 0;
                ccIRange[1] = 0;
                ccIRange[2] = 0;
            }
            if (ballHeading == CardinalDirections.South)
            {
                ccIRange[0] = 2;
                ccIRange[1] = 2;
                ccIRange[2] = 2;
            }
            if (ballHeading == CardinalDirections.West)
            {
                ccIRange[0] = 4;
                ccIRange[1] = 4;
                ccIRange[2] = 4;
            }
            if (ballHeading == CardinalDirections.North)
            {
                ccIRange[0] = 6;
                ccIRange[1] = 6;
                ccIRange[2] = 6;
            }
        }
        List<Blockv0d1> recentCollisionList = new List<Blockv0d1>();
        public void CheckCollisionWithRectangle(Blockv0d1 b)
        {
            // might add b.collisionLastFrame to prevent double bounce. 07/18/2017
            collisionDetected = false;
            block = b;

            if (b.collisionLastFrame)
            {
                b.collisionLastFrame = false;       // In testing 07/18/2017
                //b.sb.Color = b.defaultColor;
            }
            else
            {
                for (int i = 0; i < b.ccMarkerList.Count; i++)
                {
                    if (DistanceBetween(position, b.ccMarkerList[i].position) <= radius)
                    {
                        b.collisionLastFrame = true;        // In testing 07/18/2017
                        //b.sb.Color = Color.Gold;
                        //b.collisionMarkerIndex = i;
                        for (int j = 0; j < ccMarkerList.Count; j++)
                        {
                            if (ccMarkerList[j].IsCollisionWithRect(b.rectangle))
                            {
                                ccIndex = j;
                                cp = ccMarkerList[j].position;
                                SetRectangleCollisionSide(b, cp);
                                collisionDetected = true;
                            }
                        }
                    }
                }
                if (collisionDetected)
                {
                    BounceV3();
                    //IsLaunched = false;
                }

            }

            //for (int i = 0; i < b.ccMarkerList.Count; i++)
            //{
            //    if (DistanceBetween(position, b.ccMarkerList[i].position) <= radius
            //        /*&& b.collisionLastFrame == false*/)
            //    {
            //        for (int j = 0; j < ccMarkerList.Count; j++)
            //        {
            //            if (ccMarkerList[j].IsCollisionWithRect(b.rectangle))
            //            {
            //                ccIndex = j;
            //                cp = ccMarkerList[j].position;
            //                SetRectangleCollisionSide(b, cp);
            //                collisionDetected = true;
            //            }
            //        }
            //    }
            //}
            //if(collisionDetected)
            //{
            //    BounceV3();
            //    //IsLaunched = false;
            //}
        } // end of function CheckCollisionWithRectangle(RectangleF r)

        #region testing functions from 6.0
        CardinalDirections ballHeading = CardinalDirections.NotSet;

        private void SetBallHeadingv2()
        {
            if (velocity.X > 0 && velocity.Y > 0)
                ballHeading = CardinalDirections.SouthEast;
            if (velocity.X < 0 && velocity.Y > 0)
                ballHeading = CardinalDirections.SouthWest;
            if (velocity.X < 0 && velocity.Y < 0)
                ballHeading = CardinalDirections.NorthWest;
            if (velocity.X > 0 && velocity.Y < 0)
                ballHeading = CardinalDirections.NorthEast;
            if (velocity.X == 0 && velocity.Y < 0)
                ballHeading = CardinalDirections.North;
            if (velocity.X == 0 && velocity.Y > 0)
                ballHeading = CardinalDirections.South;
            if (velocity.X < 0 && velocity.Y == 0)
                ballHeading = CardinalDirections.West;
            if (velocity.X > 0 && velocity.Y == 0)
                ballHeading = CardinalDirections.East;
        } // end of function SetBallHeading()
        private void BounceV3()
        {
            if (ballHeading == CardinalDirections.NorthEast)
            {
                float tempAngle = angle - 3 * pi / 2;
                if (rectCollisionSide == CardinalDirections.South)
                {
                    angle = pi / 2 - tempAngle;
                }
                if (rectCollisionSide == CardinalDirections.West)
                {
                    angle = 3 * pi / 2 - tempAngle;
                }
            }
            if (ballHeading == CardinalDirections.SouthEast)
            {
                float tempAngle = angle;
                if (rectCollisionSide == CardinalDirections.North)
                {
                    angle = 2 * pi - tempAngle;
                }
                if (rectCollisionSide == CardinalDirections.West)
                {
                    angle = pi - tempAngle;
                }
            }
            if (ballHeading == CardinalDirections.SouthWest)
            {
                float tempAngle = angle - pi / 2;
                if (rectCollisionSide == CardinalDirections.North)
                {
                    angle = 3 * pi / 2 - tempAngle;
                }
                if (rectCollisionSide == CardinalDirections.East)
                {
                    angle = pi / 2 - tempAngle;
                }
            }
            if (ballHeading == CardinalDirections.NorthWest)
            {
                float tempAngle = angle - pi;
                if (rectCollisionSide == CardinalDirections.South)
                {
                    angle = pi - tempAngle;
                }
                if (rectCollisionSide == CardinalDirections.East)
                {
                    angle = 2 * pi - tempAngle;
                }
            }
        }
        #endregion testing functions from 6.0

        public float angleToRectCenter = 0;
        public PointF cp = new PointF();
        public PointF rectCenter = new PointF();
        private void SetRectangleCollisionSide(Blockv0d1 b, PointF cp)
        {

            rectCenter = b.center;
            //cp = ccMarkerList[ccIndex].position;      // moved to check collision function 07/18/2017
            angleToRectCenter = AngleBetween2(rectCenter, cp);
            #region attempt1
            if (angleToRectCenter <= pi / 4 || angleToRectCenter >= (2 * pi) - pi / 4)
            {
                rectCollisionSide = CardinalDirections.East;
            }
            if (angleToRectCenter > pi / 4 && angleToRectCenter <= pi - pi / 4)
            {
                rectCollisionSide = CardinalDirections.South;
            }
            if (angleToRectCenter > pi - pi / 4 && angleToRectCenter <= pi + pi / 4)
            {
                rectCollisionSide = CardinalDirections.West;
            }
            if (angleToRectCenter > pi + pi / 4 && angleToRectCenter < 2 * pi - pi / 4)
            {
                rectCollisionSide = CardinalDirections.North;
            }
            #endregion
        } // end of function SetRectangleCollisionSide(Blockv0d1 b, PointF cp)


        public override void Draw(Graphics g)
        {
            base.Draw(g);
            if (showDebugText)
                DrawDebugInfo(g, ref textPos);
            //DrawDebugGraphics(g);
            DrawCollisionCheckMarkers(g);

        } // end of function Draw(Graphics g)

        protected virtual void DrawDebugInfo(Graphics g, ref PointF textPos)
        {
            SolidBrush sb = new SolidBrush(Color.Peru);
            DrawText(g, "Ballv5d4 Debug Info:", sb, ref textPos);
            DrawText(g, "position: " + position.ToString(), sb, ref textPos);
            DrawText(g, "velocity: " + velocity.ToString(), sb, ref textPos);
            DrawText(g, "angle: " + angle.ToString(), sb, ref textPos);
            DrawText(g, "degrees: " + degrees.ToString(), sb, ref textPos);
            DrawText(g, "ballHeading: " + ballHeading.ToString(), sb, ref textPos);
            DrawText(g, "index of collision marker making collision: " + ccIndex.ToString(), sb, ref textPos);
            DrawText(g, "angleToRectCenter: " + (angleToRectCenter * 180 / pi).ToString(), sb, ref textPos);
            DrawText(g, "rectCollisionSide: " + rectCollisionSide.ToString(), sb, ref textPos);
            DrawText(g, "cp: " + cp.ToString(), sb, ref textPos);
            //DrawText(g, "ccIndexRange: {" + ccIRange[0] + "," + ccIRange[1] + "," + ccIRange[2] + "}", sb, ref textPos);

            g.DrawLine(Pens.Black, cp, rectCenter);

        } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)

        #region Collision Check marker set up region
        private void LoadCollisionCheckMarkers()
        {
            ccMarkerList.Clear();
            //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, pi / 2));
            //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, pi / 4));
            //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, 0));
            //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, -(pi / 4)));
            //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, -(pi / 2)));

            for (int i = 0; i < 8; i++)
            {
                ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, 0, radius, i * pi / 4));
                if (i == 0)
                {
                    ccMarkerList[0].c = Color.Green;
                }
            }
        }
        private void UpdateCollisionCheckMarkers()
        {
            for (int i = 0; i < ccMarkerList.Count; i++)
            {
                //ccMarkerList[i].Update(position, angle, radius, velocity);
                ccMarkerList[i].Update(position, velocity);
            }
        }
        private void DrawCollisionCheckMarkers(Graphics g)
        {
            for (int i = 0; i < ccMarkerList.Count; i++)
            {
                ccMarkerList[i].Draw(g);
            }
        }
        List<CollisionCheckMarkerv0d1> ccMarkerList = new List<CollisionCheckMarkerv0d1>();
        #endregion Collision Check marker set up region

        protected void DrawAngleMarker(Graphics g, float angle)
        {
            PointF directionMarker = new PointF();
            PointF endPt = new PointF();
            PointF rightPt = new PointF();
            PointF leftPt = new PointF();
            directionMarker.X = position.X + (float)Math.Cos(angle) * radius;
            directionMarker.Y = position.Y + (float)Math.Sin(angle) * radius;

            #region arrow drawing part
            endPt.X = directionMarker.X + (float)Math.Cos(angle) * 15;
            endPt.Y = directionMarker.Y + (float)Math.Sin(angle) * 15;

            rightPt.X = directionMarker.X + (float)Math.Cos(angle + Math.PI / 2) * 10;
            rightPt.Y = directionMarker.Y + (float)Math.Sin(angle + Math.PI / 2) * 10;

            leftPt.X = directionMarker.X - (float)Math.Cos(angle + Math.PI / 2) * 10;
            leftPt.Y = directionMarker.Y - (float)Math.Sin(angle + Math.PI / 2) * 10;
            #endregion
            Pen p = new Pen(Color.Green, 3);
            g.DrawLine(p, directionMarker, endPt);
            g.DrawLine(p, rightPt, endPt);
            g.DrawLine(p, leftPt, endPt);
        } // end of function DrawPredictedBounceHeadingMarker(Graphics g)


        #region Original build 1
        //public virtual void CheckCollisionWithRectangle(RectangleF r)
        //{
        //    collisionDetected = false;
        //    tempRect = r;
        //    rc = new PointF(r.X + r.Width / 2, r.Y + r.Height / 2);

        //    SetXYDistClosestCornerAndXYOverhang(r);
        //    if (xOverhang <= 0)
        //    //if (xOverhang <= 0 + (Abs(velocity.X)))
        //    {
        //        if (yOverhang - Abs(velocity.Y) <= radius)
        //        {
        //            //IsLaunched = false;
        //            collisionDetected = true;
        //            remainingDistancey = radius - (yOverhang + velocity.Y);
        //        }
        //    }
        //    if (yOverhang <= 0)
        //    //if (yOverhang <= 0 + (Abs(velocity.Y)))
        //    {
        //        if (xOverhang - Abs(velocity.X) <= radius)
        //        {
        //            //IsLaunched = false;
        //            collisionDetected = true;
        //            remainingDistancex = radius - (yOverhang + velocity.X);
        //        }
        //    }
        //} // end of function CheckCollisionWithRectangle(RectangleF r)
        #endregion Original build 1
        #region original DrawDebugInfo(Graphics g, ref PointF textPos)
        //protected virtual void DrawDebugInfo(Graphics g, ref PointF textPos)
        //{
        //    SolidBrush sb = new SolidBrush(Color.Peru);
        //    DrawText(g, "Ballv5d4 Debug Info:", sb, ref textPos);
        //    DrawText(g, "position: " + position.ToString(), sb, ref textPos);
        //    DrawText(g, "velocity: " + velocity.ToString(), sb, ref textPos);
        //    DrawText(g, "angle: " + angle.ToString(), sb, ref textPos);
        //    DrawText(g, "degrees: " + degrees.ToString(), sb, ref textPos);
        //    DrawText(g, "xOverhang: " + xOverhang.ToString(), sb, ref textPos);
        //    DrawText(g, "yOverhang: " + yOverhang.ToString(), sb, ref textPos);
        //    DrawText(g, "remainingDistancex: " + remainingDistancex.ToString(), sb, ref textPos);
        //    DrawText(g, "remainingDistancey: " + remainingDistancey.ToString(), sb, ref textPos);
        //    float tempa = AngleBetween(position, new PointF(0, 0));
        //    DrawText(g, "AngleBetween ball center and top left: " + (tempa * 180 / pi).ToString(), sb, ref textPos);

        //    Color xydistColor = Color.Red;
        //    DrawXYDist(g, xydistColor);
        //    Color innerRectColor = Color.Black;
        //    DrawPoint(g, innerRectColor, rc);
        //    DrawPoint(g, innerRectColor, closestCornerToBall);
        //} // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
        #endregion original DrawDebugInfo(Graphics g, ref PointF textPos)
        #region older code not used at moment 07/18/2017
        //private void SetXYDistClosestCornerAndXYOverhang(RectangleF r)
        //{
        //    // called in CheckCollisionWithRectangle(RectangleF r)
        //    xdist.Y = rc.Y;
        //    ydist.X = rc.X;
        //    if (position.X < rc.X)
        //    {
        //        //xdist.X = rc.X - Abs(rc.X - (position.X));
        //        xdist.X = rc.X - Abs(rc.X - (position.X - velocity.X));
        //        closestCornerToBall.X = r.X;
        //    }
        //    else
        //    {
        //        //xdist.X = rc.X + Abs(rc.X - position.X);
        //        xdist.X = rc.X + Abs(rc.X - (position.X + velocity.X));
        //        closestCornerToBall.X = r.X + r.Width;
        //    }

        //    if (position.Y < rc.Y)
        //    {
        //        //ydist.Y = rc.Y - Abs(rc.Y - position.Y);
        //        ydist.Y = rc.Y - Abs(rc.Y - (position.Y - velocity.Y));
        //        closestCornerToBall.Y = r.Y;
        //    }
        //    else
        //    {
        //        //ydist.Y = rc.Y + Abs(rc.Y - position.Y);
        //        ydist.Y = rc.Y + Abs(rc.Y - (position.Y + velocity.Y));
        //        closestCornerToBall.Y = r.Y + r.Height;
        //    }
        //    SetXYOverhang(r);

        //} // end function SetXYDistAndClosestCorner(RectangleF r)
        //private void SetXYOverhang(RectangleF r)
        //{
        //    // called in SetXYDistClosestCornerAndXYOverhang(RectangleF r)
        //    if (DistanceBetween(rc, xdist) >= r.Width / 2)
        //    {
        //        xOverhang = DistanceBetween(rc, xdist) - r.Width / 2;
        //    }
        //    else
        //    {
        //        xOverhang = 0;
        //    }
        //    if (DistanceBetween(rc, ydist) >= r.Height / 2)
        //    {
        //        yOverhang = DistanceBetween(rc, ydist) - r.Height / 2;
        //    }
        //    else
        //    {
        //        yOverhang = 0;
        //    }
        //} // end function SetXYOverhang(RectangleF r)
        //private void DrawXYDist(Graphics g, Color c)
        //{
        //    Pen pen = new Pen(c);
        //    g.DrawLine(pen, rc, xdist);
        //    g.DrawLine(pen, rc, ydist);
        //} // end of function  DrawXYDist(Graphics g, Color c)
        #endregion older code not used at moment 07/18/2017
    } // end of class Ballv5d4 : Ballv4d2


}


#region old SetBallHeading(), rebuilt based on velocit 07/18/2017
//private void SetBallHeading()
//{
//    if (angle < pi / 2)
//        ballHeading = CardinalDirections.SouthEast;
//    if (angle < pi && angle >= pi / 2)
//        ballHeading = CardinalDirections.SouthWest;
//    if (angle < 3 * pi / 2 && angle >= pi)
//        ballHeading = CardinalDirections.NorthWest;
//    if (angle < 2 * pi && angle >= 3 * pi / 2)
//        ballHeading = CardinalDirections.NorthEast;
//    if (angle == 2 * pi || angle == 0)
//        ballHeading = CardinalDirections.East;
//    if (angle == pi / 2)
//        ballHeading = CardinalDirections.South;
//    if (angle == pi)
//        ballHeading = CardinalDirections.West;
//    if (angle == 3 * pi / 2)
//        ballHeading = CardinalDirections.North;
//} // end of function SetBallHeading()
#endregion old SetBallHeading(), rebuilt based on velocit 07/18/2017
#region moved to CollisionCheckMarkerv0d1 07/18/2017
//public class CollisionCheckMarker
//{
//    public PointF center = new PointF();
//    public PointF position = new PointF();
//    public float angle = 0;
//    public float radius = 0;
//    public float rotation = 0;

//    public CollisionCheckMarker(PointF center, float angle, float radius, float rotation)
//    {
//        Load(center, angle, radius, rotation);
//    }
//    public void Load(PointF center, float angle, float radius, float rotation)
//    {
//        this.center = center;
//        this.angle = angle;
//        this.radius = radius;
//        this.rotation = rotation;
//        float adjustedAngle = angle + rotation;
//        position.X = center.X + Cos(adjustedAngle) * radius;
//        position.Y = center.Y + Sin(adjustedAngle) * radius;
//    }
//    public void Update(PointF center, float angle, float radius, PointF velocity)
//    {
//        float adjustedAngle = angle + rotation;
//        position.X = center.X + Cos(adjustedAngle) * radius + velocity.X;
//        position.Y = center.Y + Sin(adjustedAngle) * radius + velocity.Y;
//    }
//    public bool IsCollisionWithRect(RectangleF rect)
//    {
//        if (position.X >= rect.X && position.X <= rect.X + rect.Width
//            && position.Y >= rect.Y && position.Y <= rect.Y + rect.Height)
//            return true;
//        else
//            return false;
//    }
//    public void Draw(Graphics g)
//    {
//        DrawPoint(g, Color.Red, position);
//    }
//} // end of class CollisionCheckMarker
#endregion moved to CollisionCheckMarkerv0d1 07/18/2017
#region moved to SetXYDistAndClosestCorner(r) on 7/7/2017
// NOTES
// Moved from CheckCollisionWithRectangle(RectangleF r) and
// just called SetXYDistAndClosestCorner(r) in CheckCollisionWithRectangle(RectangleF r).
// END NOTES
//
//xdist.Y = rc.Y;
//ydist.X = rc.X;
//if (position.X < rc.X)
//{
//    xdist.X = rc.X - Abs(rc.X - position.X);
//    closestCornerToBall.X = r.X;
//}

//else
//{
//    xdist.X = rc.X + Abs(rc.X - position.X);
//    closestCornerToBall.X = r.X + r.Width;
//}

//if (position.Y < rc.Y)
//{
//    ydist.Y = rc.Y - Abs(rc.Y - position.Y);
//    closestCornerToBall.Y = r.Y;
//}
//else
//{
//    ydist.Y = rc.Y + Abs(rc.Y - position.Y);
//    closestCornerToBall.Y = r.Y + r.Height;
//}
#endregion moved to SetXYDistAndClosestCorner(r) on 7/7/2017