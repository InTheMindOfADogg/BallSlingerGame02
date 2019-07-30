using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    class Ballv5d4d2 : Ballv4d2
    {
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
        CardinalDirections ballHeading = CardinalDirections.NotSet;
        CardinalDirections lastBallHeading = CardinalDirections.NotSet;
        
        public List<Blockv0d1> blockList = new List<Blockv0d1>();
        public List<PointF> collisionPointList = new List<PointF>();

        protected PointF velocity;

        protected float startingSpeed = 5;
        protected float maxSpeed = 5;
        protected float speed = 5;

        public bool IsLaunched = false;
        public bool initialLaunch = true;

        protected bool collisionDetected = false;
        public Ballv5d4d2()
        {
            showBallPositionRect = false;
            showHeadingMarker = false;
            visible = true;
            rectCollisionSide = CardinalDirections.NotSet;
        }

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

        } // end of function LocalLoadAndReset()
        public void LoadBlockList(List<Blockv0d1> bList)
        {
            blockList.Clear();
            collisionPointList.Clear();
            for (int i = 0; i < bList.Count; i++)
            {
                blockList.Add(bList[i]);
            }
            for (int i = 0; i < blockList.Count; i++)
            {
                Blockv0d1 tempblock = blockList[i];
                for (int j = 0; j < tempblock.ccMarkerList.Count; j++)
                {
                    collisionPointList.Add(tempblock.ccMarkerList[j].position);
                }
            }
        }

        float angleTorc = 0;
        PointF tempPos = new PointF();
        public void Update()
        {
            if (IsLaunched)
                speed = maxSpeed;
            else
                speed = 0;
            SetBallHeadingv2();

            CheckCollisionWithBlockList();

            velocity.X = Cos(angle) * speed;
            velocity.Y = Sin(angle) * speed;
            position.X += velocity.X;
            position.Y += velocity.Y;
        } // end of function Update()
        private void CheckCollisionWithBlockList()
        {
            PointF nextPos = new PointF();
            nextPos.X = position.X + velocity.X;
            nextPos.Y = position.Y + velocity.Y;

            #region works ok, but does overlap some
            for (int i = 0; i < blockList.Count; i++)
            {
                Blockv0d1 temp = blockList[i];
                if (DistanceBetween(temp.center, position) < temp.size.Width * 2.3 + radius)
                    for (int j = 0; j < blockList[i].ccMarkerList.Count; j++)
                    {
                        if (DistanceBetween(position, blockList[i].ccMarkerList[j].position) <= radius * 1.2)
                        {
                            //IsLaunched = false;
                            SetRectangleCollisionSide(blockList[i]);
                            BounceV3();
                        }
                    }
            }
            #endregion works ok, but does overlap some

            #region works pretty good, but leaves a lot of open space
            //for(int i = 0; i < blockList.Count; i++)
            //{
            //    Blockv0d1 temp = blockList[i];
            //    if(DistanceBetween(temp.center, nextPos) <= temp.size.Width + radius)
            //    {

            //        tempPos = temp.center;
            //        angleTorc = AngleBetween2(position, tempPos);
            //        modAngle = AngleBetween(position, tempPos);

            //        if(modAngle > 40/180*pi && modAngle < 50/180*pi)
            //        {
            //            if(DistanceBetween(temp.center, position) < temp.size.Width * 0.8f + radius)
            //            {
            //                IsLaunched = false;

            //            }
            //        }
            //        else if(DistanceBetween(temp.center, position) < temp.size.Width * 0.5f + radius )
            //        {
            //            IsLaunched = false;
            //        }
            //        SetRectangleCollisionSide(temp);
            //        BounceV3();
            //    }
            //}
            #endregion works pretty good, but leaves a lot of open space

        } // end of function CheckCollisionWithBlockList()
        float modAngle = 0;
        public override void Draw(Graphics g)
        {
            DrawBlockList(g);
            base.Draw(g);
            if (showDebugText)
                DrawDebugInfo(g, ref textPos);

        } // end of function Draw(Graphics g)

        protected virtual void DrawDebugInfo(Graphics g, ref PointF textPos)
        {
            SolidBrush sb = new SolidBrush(Color.Peru);
            DrawText(g, "Ballv5d4d2 Debug Info:", sb, ref textPos);
            DrawText(g, "position: " + position.ToString(), sb, ref textPos);
            DrawText(g, "velocity: " + velocity.ToString(), sb, ref textPos);
            DrawText(g, "angle: " + angle.ToString(), sb, ref textPos);
            DrawText(g, "degrees: " + degrees.ToString(), sb, ref textPos);
            DrawText(g, "ballHeading: " + ballHeading.ToString(), sb, ref textPos);
            DrawText(g, "blockList.Count: " + blockList.Count.ToString(), sb, ref textPos);
            DrawText(g, "collisionPointList.Count: " + collisionPointList.Count.ToString(), sb, ref textPos);
            DrawText(g, "block collision side: " + rectCollisionSide.ToString(), sb, ref textPos);
            DrawText(g, "angleTorc: " + (angleTorc * 180/pi).ToString(), sb, ref textPos);
            DrawText(g, "modAngle: " + (modAngle * 180 / pi).ToString(), sb, ref textPos);

            g.DrawLine(Pens.Black, position, tempPos);
        } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
        
        private void SetBallHeadingv2()
        {
            lastBallHeading = ballHeading;
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
            //if (velocity.X == 0 && velocity.Y == 0)
            //{
            //    ballHeading = CardinalDirections.NotSet;
            //}

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

        private void SetRectangleCollisionSide(Blockv0d1 b)
        {
            float angleToRectCenter = AngleBetween2(b.center, position);
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

        private void DrawBlockList(Graphics g)
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                blockList[i].Draw(g);
            }
        } // end of function DrawBlockList(Graphics g)

    } // end of class Ballv5d4d2 : Ballv4d2
}
