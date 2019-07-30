using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    class Ballv6d0 : Ballv5d5
    {
        #region Summary
        // 07/12/2017
        //
        // - Created class
        // - Modified the previous versions to not stop the ball movement, 
        //      but to instead change collisionDetected, a varialbe added in v5.4, to true.

        // - Added bool showPredictedBounceMarker
        // - Added float predictedBounceAngle

        #region Function CheckCollisionWithRectangle(RectangleF r) Summary
        //
        //      This function calls all base versions (in v5d5 and v5d4), and if collision is detected
        //      IsLaunched is set to false here to stop ball movement(for testing purposes).
        //
        #endregion Function CheckCollisionWithRectangle(RectangleF r) Summary
        #endregion Summary
        #region Goals and possible improvments
        // 07/12/2017
        // - create a bounce function and call it in 
        //      CheckCollisionWithRectangle(RectangleF r) if collisionDetected == true,
        //      which is set in the base calls.
        //
        // 07/13/2017
        // Idea for Improvement:
        //  - Compare distance between rectangle center and ball position using PointF and velocity.
        //      Find x distance, y distance and add the velocity from each and then
        //      if the x is less than or the y is less than, collision occured(I am thinking.)
        #endregion Goals


        // Probably need to set where on the rectangle the collision occured,
        //      (side or corner) N, E, S, or W for side and NE, SE, SW, NW for corner.
        //
        // Might move this to earlier version to identify the position on the rect
        //      where the collision occured. (CardinalDirection rectCollisionPoint;)
        // 

        // Added 07/13/2017
        protected CardinalDirections ballHeading;
        protected bool showPredictedBounceMarker = true;
        float predictedBounceAngle = 0;
        //protected CardinalDirections rectCollisionSide;
        // end Added 07/13/2017

        public Ballv6d0()
        {
            #region Debug Options with default values
            #region v4.2
            //      showDebugText = true;
            //      showHeadingMarker = true;
            //      showBallPositionRect = true;
            #endregion v4.2
            #region v5.5
            //      showCollisionLine = true;
            #endregion v5.5
            #region v6.0
            //      showPredictedBounceMarker = true;
            #endregion v6.0
            #endregion Debug Options
            showHeadingMarker = false;
            showPredictedBounceMarker = false;
            showCollisionLine = false;
            ballHeading = CardinalDirections.NotSet;
            rectCollisionSide = CardinalDirections.NotSet;
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
            ballHeading = CardinalDirections.NotSet;
            rectCollisionSide = CardinalDirections.NotSet;
            //tempVarReset();
        } // end of function LocalLoadAndReset()
        float xthreshold = 20;
        float ythreshold = 20;
        public new void Update()
        {

            SetBallHeading();
            if (position.X < xthreshold * 2 || position.X > gameWidth - xthreshold * 2)
            {
                IsLaunched = false;
            }
            if (position.Y < ythreshold * 2 || position.Y > gameHeight - ythreshold * 2)
            {
                IsLaunched = false;
            }

            //collisionOnSide = false;
            //collisionOnCorner = false;
            base.Update();
        } // end of function Update()
        #region original CheckCollisionWithRectangle(RectangleF r)
        //public new void CheckCollisionWithRectangle(RectangleF r)
        //{
        //    // Probably need to set where on the rectangle the collision occured,
        //    //      (side or corner) N, E, S, or W for side and NE, SE, SW, NW for corner.
        //    #region code in Ballv5d5
        //    //speed = 1;
        //    //#region previous closest attempt at collision detection
        //    //distanceFromClosestCorner = DistanceBetween(position, closestCornerToBall);
        //    //angleToClosestCorner = AngleBetween2(position, closestCornerToBall);

        //    //#endregion previous closest attempt at collision detection
        //    //#region GIGGITY GIGGITY!!
        //    //PointF tempd = new PointF();
        //    //tempd.X = position.X + velocity.X;
        //    //tempd.Y = position.Y + velocity.Y;
        //    //if (UseHypLenAsTempDist())
        //    //{

        //    //    tempDist = DistanceBetween(tempd, closestCornerToBall);
        //    //    //tempDist = DistanceBetween(position, closestCornerToBall);
        //    //}
        //    //else
        //    //{
        //    //    if ((position.X < tempRect.X || position.X > tempRect.X + tempRect.Width)
        //    //        && position.Y > tempRect.Y && position.Y < tempRect.Y + tempRect.Height)
        //    //    {
        //    //        tempDist = (DistanceBetween(tempd, rc) - tempRect.Width / 2);
        //    //        //tempDist = (DistanceBetween(position, rc) - tempRect.Width / 2);
        //    //    }
        //    //    if (position.X > tempRect.X && position.X < tempRect.X + tempRect.Width
        //    //        && (position.Y < tempRect.Y || position.Y > tempRect.Y + tempRect.Height))
        //    //    {
        //    //        tempDist = (DistanceBetween(tempd, rc) - tempRect.Height / 2);
        //    //        //tempDist = (DistanceBetween(position, rc) - tempRect.Height / 2);
        //    //    }
        //    //}
        //    //if (tempDist < radius)
        //    //    IsLaunched = false;
        //    //#endregion GIGGITY GIGGITY!!
        //    #endregion code in Ballv5d5
        //    base.CheckCollisionWithRectangle(r);

        //    if (collisionDetected)
        //    {
        //        IsLaunched = false;

        //        //BounceV2();
        //        BounceV3();

        //        collisionDetected = false;
        //        //IsLaunched = true;
        //    }

        //} // end of function CheckCollisionWithRectangle(RectangleF r)
        #endregion original CheckCollisionWithRectangle(RectangleF r)
        public new void CheckCollisionWithRectangle(Blockv0d1 b)
        {
            base.CheckCollisionWithRectangle(b);

            if (collisionDetected)
            {
                IsLaunched = false;
                //BounceV2();
                //BounceV3();
                collisionDetected = false;

            }

        } // end of function CheckCollisionWithRectangle(RectangleF r)


        #region moved to v5.4 07/18/2017
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
                    angle = tempAngle;
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
        #endregion moved to v5.4 07/18/2017
        

        #region original BounceV2()
        private void BounceV2()
        {
            //collisionOnSide = false;
            //collisionOnCorner = false;
            //angleToRectCenter = AngleBetween2(position, rc);
            //modAngleToRectCenter = AngleBetween(position, rc);

            //float cornerDegrees = 8;
            //if ((modAngleToRectCenter * 180 / pi) < 45 - cornerDegrees
            //    || (modAngleToRectCenter * 180 / pi) > 45 + cornerDegrees)
            //{
            //    // collision is on a side
            //    collisionOnSide = true;
            //    if (modAngleToRectCenter * 180 / pi < 45 - cornerDegrees)
            //    {
            //        // collision is on north or south
            //        if (position.Y < rc.Y)
            //            rectCollisionSide = CardinalDirections.North;
            //        if (position.Y > rc.Y)
            //            rectCollisionSide = CardinalDirections.South;
            //    }
            //    if (modAngleToRectCenter * 180 / pi > 45 + cornerDegrees)
            //    {
            //        // collision is on east or west
            //        if (position.X < rc.X)
            //            rectCollisionSide = CardinalDirections.West;
            //        if (position.X > rc.X)
            //            rectCollisionSide = CardinalDirections.East;
            //    }
            //}
            //else
            //{
            //    // collision is on a corner
            //    collisionOnCorner = true;
            //    if (position.X < rc.X && position.Y < rc.Y)
            //        rectCollisionSide = CardinalDirections.NorthWest;
            //    if (position.X < rc.X && position.Y > rc.Y)
            //        rectCollisionSide = CardinalDirections.SouthWest;
            //    if (position.X > rc.X && position.Y > rc.Y)
            //        rectCollisionSide = CardinalDirections.SouthEast;
            //    if (position.X > rc.X && position.Y < rc.Y)
            //        rectCollisionSide = CardinalDirections.NorthEast;
            //}
            //SetPredictedBounce(ballHeading, rectCollisionSide);
        } // end of function BounceV2()
        #endregion original

        private void SetPredictedBounce(CardinalDirections ballHeading, CardinalDirections rectCollisionSide)
        {
            //switch (rectCollisionSide)
            //{
            //    case CardinalDirections.East:
            //        if (ballHeading == CardinalDirections.SouthWest)
            //        {
            //            float tempangle = angle - (pi / 2);
            //            predictedBounceAngle = (pi / 2) - tempangle;
            //        }
            //        if (ballHeading == CardinalDirections.West)
            //        {
            //            //predictedBounceAngle = bounceAxis;
            //        }
            //        if (ballHeading == CardinalDirections.NorthWest)
            //        {
            //            float tempangle = (3 * pi / 2) - angle;
            //            predictedBounceAngle = (3 * pi / 2) + tempangle;
            //        }
            //        break;
            //    case CardinalDirections.South:
            //        if (ballHeading == CardinalDirections.NorthEast)
            //        {
            //            float tempangle = 2 * pi - angle;
            //            predictedBounceAngle = tempangle;
            //        }
            //        if (ballHeading == CardinalDirections.North)
            //        {
            //            predictedBounceAngle = pi;
            //        }
            //        if (ballHeading == CardinalDirections.NorthWest)
            //        {
            //            float tempangle = angle - pi;
            //            predictedBounceAngle = pi + tempangle;
            //        }
            //        break;
            //    case CardinalDirections.West:
            //        if (ballHeading == CardinalDirections.NorthEast)
            //        {
            //            float tempangle = angle - (3 * pi / 2);
            //            predictedBounceAngle = (3 * pi / 2) - tempangle;

            //        }
            //        if (ballHeading == CardinalDirections.East)
            //        {
            //            predictedBounceAngle = pi;
            //        }
            //        if (ballHeading == CardinalDirections.SouthEast)
            //        {
            //            float tempangle = pi / 2 - angle;
            //            predictedBounceAngle = (pi / 2) + tempangle;
            //        }
            //        break;
            //    case CardinalDirections.North:
            //        if (ballHeading == CardinalDirections.SouthEast)
            //        {
            //            predictedBounceAngle = 2 * pi - angle;
            //        }
            //        if (ballHeading == CardinalDirections.South)
            //        {
            //            predictedBounceAngle = 3 * pi / 2;
            //        }
            //        if (ballHeading == CardinalDirections.SouthWest)
            //        {
            //            float tempangle = pi - angle;
            //            predictedBounceAngle = pi + tempangle;
            //        }
            //        break;
            //} // end of switch (rectCollisionSide)
        } // end of SetPredictedBounce(CardinalDirections ballHeading, CardinalDirections rectCollisionSide)

        #region BounceV1 Parts

        private void BounceV1()
        {
            float ballAngleScaledToPiOver2 = 0;
            float minBounceAngle = 0;
            // Could work off velocity instead of angles.. might do later.
            if (angle < pi / 2)
            {
                ballAngleScaledToPiOver2 = angle;
                ballHeading = CardinalDirections.SouthEast;
            }
            if (angle < pi && angle >= pi / 2)
            {
                ballAngleScaledToPiOver2 = angle - pi / 2;
                ballHeading = CardinalDirections.SouthWest;
            }
            if (angle < 3 * pi / 2 && angle >= pi)
            {
                ballAngleScaledToPiOver2 = angle - pi;
                ballHeading = CardinalDirections.NorthWest;
            }
            if (angle < 2 * pi && angle >= 3 * pi / 2)
            {
                ballAngleScaledToPiOver2 = angle - 3 * pi / 2;
                ballHeading = CardinalDirections.NorthEast;
            }
            if (angle == 2 * pi || angle == 0)
                ballHeading = CardinalDirections.East;
            if (angle == pi / 2)
                ballHeading = CardinalDirections.South;
            if (angle == pi)
                ballHeading = CardinalDirections.West;
            if (angle == 3 * pi / 2)
                ballHeading = CardinalDirections.North;

            minBounceAngle = SetMinBounceAngle(ballHeading, angle);
            //predictedBounceAngle = scaledDown;
        } // end of function BounceV1
        float SetMinBounceAngle(CardinalDirections gh, float angle)
        {
            float minangle = 0;
            switch (gh)
            {
                case CardinalDirections.East:
                    break;
                case CardinalDirections.SouthEast:
                    break;
                case CardinalDirections.SouthWest:
                    break;
                case CardinalDirections.West:
                    break;
                case CardinalDirections.NorthWest:
                    break;
                case CardinalDirections.North:
                    break;
                case CardinalDirections.NorthEast:
                    break;
                default:
                    break;
            }
            return minangle;
        }
        #endregion BounceV1 Parts

        public new void Draw(Graphics g)
        {
            base.Draw(g);
            if (showPredictedBounceMarker)
            {
                //if (predictedBounceAngle != angle)
                DrawAngleMarker(g, predictedBounceAngle);
            }
        } // end of function Draw(Graphics g)

        #region original DrawDebugInfo(Graphics g, ref PointF textPos)
        //protected override void DrawDebugInfo(Graphics g, ref PointF textPos)
        //{
        //    base.DrawDebugInfo(g, ref textPos);
        //    SolidBrush sb = new SolidBrush(Color.DarkCyan);
        //    DrawText(g, "Ballv6d0 Debug Info: ", sb, ref textPos);
        //    DrawText(g, " ball angle scaledTo90: " + (ballAngleScaledToPiOver2 * 180 / pi).ToString(), sb, ref textPos);
        //    DrawText(g, "predictedBounceAngle: " + (predictedBounceAngle * 180 / pi).ToString(), sb, ref textPos);
        //    DrawText(g, "angleToRectCenter: " + (angleToRectCenter * 180 / pi).ToString(), sb, ref textPos);
        //    DrawText(g, "modAngleToRectCenter: " + (modAngleToRectCenter * 180 / pi).ToString(), sb, ref textPos);
        //    //DrawText(g, "cornerCalcAngle: " + (cornerCalcAngle * 180/pi).ToString(), sb, ref textPos);
        //    DrawText(g, "collisionOnSide: " + collisionOnSide.ToString(), sb, ref textPos);
        //    DrawText(g, "collisionOnCorner: " + collisionOnCorner.ToString(), sb, ref textPos);
        //    DrawText(g, "ballHeading: " + genHeading.ToString(), sb, ref textPos);
        //    DrawText(g, "rectCollisionSide: " + rectCollisionSide.ToString(), sb, ref textPos);
        //    DrawText(g, "rcAngleDif: " + (rcAngleDif * 180 / pi).ToString(), sb, ref textPos);
        //} // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
        #endregion original DrawDebugInfo(Graphics g, ref PointF textPos)
        protected override void DrawDebugInfo(Graphics g, ref PointF textPos)
        {
            base.DrawDebugInfo(g, ref textPos);
            SolidBrush sb = new SolidBrush(Color.DarkCyan);
            DrawText(g, "Ballv6d0 Debug Info: ", sb, ref textPos);
            //DrawText(g, "angleToRectCenter: " + (angleToRectCenter * 180 / pi).ToString(), sb, ref textPos);
            DrawText(g, "ballHeading: " + ballHeading.ToString(), sb, ref textPos);
            DrawText(g, "predictedBounceAngle: " + (predictedBounceAngle * 180 / pi).ToString(), sb, ref textPos);
            DrawText(g, "rectCollisionSide: " + rectCollisionSide.ToString(), sb, ref textPos);
            //DrawText(g, "angleToRectCenter: " + angleToRectCenter.ToString(), sb, ref textPos);



        } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
        #region moved to v5.4 07/18/2017
        private void SetBallHeading()
        {
            if (angle < pi / 2)
                ballHeading = CardinalDirections.SouthEast;
            if (angle < pi && angle >= pi / 2)
                ballHeading = CardinalDirections.SouthWest;
            if (angle < 3 * pi / 2 && angle >= pi)
                ballHeading = CardinalDirections.NorthWest;
            if (angle < 2 * pi && angle >= 3 * pi / 2)
                ballHeading = CardinalDirections.NorthEast;
            if (angle == 2 * pi || angle == 0)
                ballHeading = CardinalDirections.East;
            if (angle == pi / 2)
                ballHeading = CardinalDirections.South;
            if (angle == pi)
                ballHeading = CardinalDirections.West;
            if (angle == 3 * pi / 2)
                ballHeading = CardinalDirections.North;
        } // end of function SetBallHeading()
        #endregion moved to v5.4 07/18/2017
    }
}

#region Commented

#region moved to v5d4 07/13/2017
//protected enum CardinalDirections
//{
//    NotSet,
//    East,
//    SouthEast,
//    South,
//    SouthWest,
//    West,
//    NorthWest,
//    North,
//    NorthEast
//}
#endregion moved to v5d4 07/13/2017

#endregion
