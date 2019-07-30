using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    class Ballv6d1 : Ballv4d2
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
        protected CardinalDirections ballHeading = CardinalDirections.NotSet;
        protected CardinalDirections lastBallHeading = CardinalDirections.NotSet;

        protected List<CollisionCheckMarkerv0d1> ccMarkerList = new List<CollisionCheckMarkerv0d1>();

        protected List<Blockv0d2> blockList = new List<Blockv0d2>();

        protected PointF velocity;

        //protected float startingSpeed = 5;
        protected float maxSpeed = 10;
        protected float speed;// = 5;

        public bool IsLaunched = false;
        public bool initialLaunch = true;

        protected bool collisionDetected = false;

        public int ballPower = 5;      // amount of hit points taken off block per hit
        public Ballv6d1()
        {
            showBallPositionRect = false;
            showHeadingMarker = false;
            visible = true;
            rectCollisionSide = CardinalDirections.NotSet;
        }
        #region quick copy region
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
            //radius = 10;      // default = 10;
            collisionDetected = false;
            rectCollisionSide = CardinalDirections.NotSet;
            IsLaunched = false;
            initialLaunch = true;
            LoadCollisionCheckMarkers();

        } // end of function LocalLoadAndReset()
        public void LoadBlockList(List<Blockv0d2> bList)
        {
            blockList.Clear();
            for (int i = 0; i < bList.Count; i++)
            {
                blockList.Add(bList[i]);
            }
        }
        public void Update()
        {
            CheckInBounds();
            if (IsLaunched)
            {
                speed = maxSpeed;
            }
            else
            {
                speed = 0;
            }
            SetBallHeadingv2();
            UpdateCollisionCheckMarkers();
            CheckCollisionWithBlockList();

            velocity.X = Cos(angle) * speed;
            velocity.Y = Sin(angle) * speed;
            position.X += Round(velocity.X);
            position.Y += Round(velocity.Y);
        } // end of function Update()
        public override void Draw(Graphics g)
        {
            DrawBlockList(g);
            base.Draw(g);
            if (showDebugText)
                DrawDebugInfo(g, ref textPos);
            //DrawCollisionCheckMarkers(g);
        } // end of function Draw(Graphics g)
        protected void DrawDebugInfo(Graphics g, ref PointF textPos)
        {
            //base.DrawDebugInfo(g, ref textPos);
            SolidBrush sb = new SolidBrush(Color.Peru);
            DrawText(g, "Ballv6d1 Debug Info:", sb, ref textPos);
            DrawText(g, "GameWidth: " + gameWidth.ToString(), sb, ref textPos);
            DrawText(g, "GameHeight: " + gameHeight.ToString(), sb, ref textPos);
            DrawText(g, "position: " + position.ToString(), sb, ref textPos);
            DrawText(g, "velocity: " + velocity.ToString(), sb, ref textPos);
            DrawText(g, "angle: " + angle.ToString(), sb, ref textPos);
            DrawText(g, "degrees: " + degrees.ToString(), sb, ref textPos);
            DrawText(g, "ballHeading: " + ballHeading.ToString(), sb, ref textPos);
            DrawText(g, "blockList.Count: " + blockList.Count.ToString(), sb, ref textPos);
            DrawText(g, "collision check points on ball: " + ccMarkerList.Count.ToString(), sb, ref textPos);

        } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
        private void CheckInBounds()
        {
            if (position.X + velocity.X - radius < 0)
            {
                BounceV3(CardinalDirections.East);
            }
            if (position.X + velocity.X + radius > gameWidth)
            {
                BounceV3(CardinalDirections.West);
            }
            if (position.Y + velocity.Y - radius < 0)
            {
                BounceV3(CardinalDirections.South);
            }
            if (position.Y + velocity.Y + radius * 1.5f > gameHeight)
            {
                IsLaunched = false;
                //BounceV3(CardinalDirections.North);
            }

        }
        private void CheckCollisionWithBlockList()
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                if (DistanceBetween(position, blockList[i].center) < blockList[i].size.Width * 2
                    && blockList[i].visible)
                {
                    blockList[i].Update();
                    for (int j = 0; j < ccMarkerList.Count; j++)
                    {
                        if (blockList[i].InRectBounds(ccMarkerList[j].position))
                        {
                            //IsLaunched = false;
                            SetRectangleCollisionSide(blockList[i]);
                            BounceV3();
                           // BounceV3d2();
                            RegisterHitOnBlock(blockList[i], ballPower);

                        } // end if
                    } // end for (int j = 0; j < ccMarkerList.Count; j++)
                } // end if
            }// end for (int i = 0; i < blockList.Count; i++)


        } // end of function CheckCollisionWithBlockList()
        protected void RegisterHitOnBlock(Blockv0d2 b, int amount)
        {
            //b.processingHit = true;
            b.hitAmount = amount;
            b.hit = true;
            //b.ProcessHit();
            //b.RegisterHit(amount);
        }
        #region Collision Check marker set up region
        private void LoadCollisionCheckMarkers()
        {
            ccMarkerList.Clear();
            for (int i = 0; i < 8; i++)
            {
                ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, 0, radius, i * pi / 4));
                if (i == 0)
                {
                    ccMarkerList[0].c = Color.Green;
                }
            }
            //for (int i = 0; i < 6; i++)
            //{
            //    ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, 0, radius, i * pi / 3));
            //    if (i == 0)
            //    {
            //        ccMarkerList[0].c = Color.Green;
            //    }
            //}
        }
        private void UpdateCollisionCheckMarkers()
        {
            for (int i = 0; i < ccMarkerList.Count; i++)
            {
                ccMarkerList[i].Update(position, velocity);
                //ccMarkerList[i].Update(position, angle, radius, velocity);
            }
        }
        private void DrawCollisionCheckMarkers(Graphics g)
        {
            for (int i = 0; i < ccMarkerList.Count; i++)
            {
                ccMarkerList[i].Draw(g);
            }
        }
        #endregion Collision Check marker set up region
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
        } // end of function BounceV3()
        private void BounceV3(CardinalDirections bounceWall)
        {
            //CardinalDirections bounceWall = CardinalDirections.NotSet;
            if (ballHeading == CardinalDirections.NorthEast)
            {
                float tempAngle = angle - 3 * pi / 2;
                if (bounceWall == CardinalDirections.South)
                {
                    angle = pi / 2 - tempAngle;
                }
                if (bounceWall == CardinalDirections.West)
                {
                    angle = 3 * pi / 2 - tempAngle;
                }
            }
            if (ballHeading == CardinalDirections.SouthEast)
            {
                float tempAngle = angle;
                if (bounceWall == CardinalDirections.North)
                {
                    angle = 2 * pi - tempAngle;
                }
                if (bounceWall == CardinalDirections.West)
                {
                    angle = pi - tempAngle;
                }
            }
            if (ballHeading == CardinalDirections.SouthWest)
            {
                float tempAngle = angle - pi / 2;
                if (bounceWall == CardinalDirections.North)
                {
                    angle = 3 * pi / 2 - tempAngle;
                }
                if (bounceWall == CardinalDirections.East)
                {
                    angle = pi / 2 - tempAngle;
                }
            }
            if (ballHeading == CardinalDirections.NorthWest)
            {
                float tempAngle = angle - pi;
                if (bounceWall == CardinalDirections.South)
                {
                    angle = pi - tempAngle;
                }
                if (bounceWall == CardinalDirections.East)
                {
                    angle = 2 * pi - tempAngle;
                }
            }
        } // end of function BounceV3(CardinalDirections bounceWall)
        private void SetRectangleCollisionSide(Blockv0d2 b)
        {
            float angleToRectCenter = AngleBetween2(b.center, position);
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
        } // end of function SetRectangleCollisionSide(Blockv0d1 b)
        private void DrawBlockList(Graphics g)
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                blockList[i].Draw(g);
            }
        } // end of function DrawBlockList(Graphics g)
        #endregion quick copy region
    }
}
