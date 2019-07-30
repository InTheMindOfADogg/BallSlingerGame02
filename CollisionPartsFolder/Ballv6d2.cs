using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    public class Ballv6d2 : Ballv4d2
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
        protected CardinalDirections rectCollisionSide = CardinalDirections.NotSet;
        protected CardinalDirections ballHeading = CardinalDirections.NotSet;
        protected CardinalDirections lastBallHeading = CardinalDirections.NotSet;
        protected List<CollisionCheckMarkerv0d1> ccMarkerList = new List<CollisionCheckMarkerv0d1>();
        protected List<Blockv0d2> blockList = new List<Blockv0d2>();
        protected PointF velocity;
        protected float maxSpeed = 15;
        protected float speed;// = 5;
        public bool IsLaunched = false;
        public bool initialLaunch = true;
        protected bool collisionDetected = false;
        public int ballPower = 1;      // amount of hit points taken off block per hit

        public bool roundOver = false;

        public float minBallBoundsX = 0;
        public float maxBallBoundsX = 0;
        public float minBallBoundsY = 0;
        public float maxBallBoundsY = 0;
        public Ballv6d2()
        {
            showBallPositionRect = false;
            showHeadingMarker = false;
            visible = true;
            //rectCollisionSide = CardinalDirections.NotSet;
        }

        #region quick copy region
        public override void Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)
        {
            minBallBoundsX = 0;
            minBallBoundsY = 0;
            maxBallBoundsX = gameBoundsWidth;
            maxBallBoundsY = gameBoundsHeight;
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
            IsLaunched = false;
            initialLaunch = true;
            LoadCollisionCheckMarkers();
            SetCollisionCheckArea();

        } // end of function LocalLoadAndReset()
        

        
        //public void Update()
        //{
        //    SetCollisionCheckArea();
        //    CheckInBounds();
        //    if (IsLaunched)
        //    {
        //        speed = maxSpeed;
        //    }
        //    else
        //    {
        //        speed = 0;
        //    }
        //    SetBallHeadingv2();
        //    UpdateCollisionCheckMarkers();
        //    //CheckCollisionWithBlockList();
        //    CheckCollisionWithBlockList2();

        //    //float roundedAngle = Round(angle * 180 / pi);
        //    //velocity.X = Cos(roundedAngle * pi / 180) * speed;
        //    //velocity.Y = Sin(roundedAngle * pi / 180) * speed;
        //    velocity.X = Cos(angle) * speed;
        //    velocity.Y = Sin(angle) * speed;
        //    position.X += Round(velocity.X);
        //    position.Y += Round(velocity.Y);
        //} // end of function Update()
        
        public override void Draw(Graphics g)
        {
            DrawBlockList(g);
            base.Draw(g);
            if (showDebugText)
                DrawDebugInfo(g, ref textPos);

            DrawBallBoundsWalls(g);
            //DrawCollisionCheckMarkers(g);
            //DrawCollisionCone(g);
        } // end of function Draw(Graphics g)

        PointF endpoint1 = new PointF();
        PointF endpoint2 = new PointF();
        PointF midpoint = new PointF();
        private void SetCollisionCheckArea()
        {
            float multiplier = 10;
            endpoint1.X = position.X + Cos(angle - pi / 4) * (radius * multiplier);
            endpoint1.Y = position.Y + Sin(angle - pi / 4) * (radius * multiplier);
            endpoint2.X = position.X + Cos(angle + pi / 4) * (radius * multiplier);
            endpoint2.Y = position.Y + Sin(angle + pi / 4) * (radius * multiplier);
            midpoint.X = position.X + Cos(angle) * (radius * multiplier);
            midpoint.Y = position.Y + Sin(angle) * (radius * multiplier);
        }

        private void DrawCollisionCone(Graphics g)
        {
            int a, r, gr, b;
            a = 150;
            r = 150;
            gr = 150;
            b = 150;
            //float multiplier = 20;
            Pen pen = new Pen(Color.FromArgb(a, r, gr, b));
            g.DrawLine(pen, position, endpoint1);
            g.DrawLine(pen, position, endpoint2);
            PointF[] points = { endpoint1, midpoint, endpoint2 };
            g.DrawCurve(pen, points);
        }

        protected void DrawDebugInfo(Graphics g, ref PointF textPos)
        {
            //base.DrawDebugInfo(g, ref textPos);
            SolidBrush sb = new SolidBrush(Color.Peru);
            DrawText(g, "Ballv6d1 Debug Info:", sb, ref textPos);
            DrawText(g, "ballPower: " + ballPower.ToString(), sb, ref textPos);
            DrawText(g, "position: " + position.ToString(), sb, ref textPos);
            DrawText(g, "velocity: " + velocity.ToString(), sb, ref textPos);
            DrawText(g, "angle: " + angle.ToString(), sb, ref textPos);
            DrawText(g, "degrees: " + (Round(angle * 180 / pi)).ToString(), sb, ref textPos);
            DrawText(g, "ballHeading: " + ballHeading.ToString(), sb, ref textPos);
            DrawText(g, "blockList.Count: " + blockList.Count.ToString(), sb, ref textPos);
            DrawText(g, "collision check points on ball: " + ccMarkerList.Count.ToString(), sb, ref textPos);

        } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)
        private void DrawBallBoundsWalls(Graphics g)
        {
            g.DrawLine(Pens.Black, new PointF(minBallBoundsX, minBallBoundsY), new PointF(minBallBoundsX, maxBallBoundsY));
            g.DrawLine(Pens.Black, new PointF(maxBallBoundsX, minBallBoundsY), new PointF(maxBallBoundsX, maxBallBoundsY));
            g.DrawLine(Pens.Black, new PointF(minBallBoundsX, minBallBoundsY), new PointF(maxBallBoundsX, minBallBoundsY));
            g.DrawLine(Pens.Black, new PointF(maxBallBoundsX, minBallBoundsY), new PointF(maxBallBoundsX, maxBallBoundsY));
        }
        protected void CheckInBounds()
        {
            if (position.X + velocity.X - radius < minBallBoundsX)
            {
                BounceV3(CardinalDirections.East);
            }
            if (position.X + velocity.X + radius > maxBallBoundsX)
            {
                BounceV3(CardinalDirections.West);
            }
            if (position.Y + velocity.Y - radius < minBallBoundsY)
            {
                BounceV3(CardinalDirections.South);
            }
            if (position.Y + velocity.Y + radius * 1.5f > maxBallBoundsY)
            {
                IsLaunched = false;
                roundOver = true;
                //BounceV3(CardinalDirections.North);
            }
        } // end of function CheckInBounds()
        //protected void CheckInBounds()
        //{
        //    if (position.X + velocity.X - radius < 0)
        //    {
        //        BounceV3(CardinalDirections.East);
        //    }
        //    if (position.X + velocity.X + radius > gameWidth)
        //    {
        //        BounceV3(CardinalDirections.West);
        //    }
        //    if (position.Y + velocity.Y - radius < 0)
        //    {
        //        BounceV3(CardinalDirections.South);
        //    }
        //    if (position.Y + velocity.Y + radius * 1.5f > gameHeight)
        //    {
        //        IsLaunched = false;
        //        roundOver = true;
        //        //BounceV3(CardinalDirections.North);
        //    }
        //} // end of function CheckInBounds()
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
                            SetRectangleCollisionSide(blockList[i]);
                            BounceV3();
                            RegisterHitOnBlock(blockList[i], ballPower);
                        } // end if
                    } // end for (int j = 0; j < ccMarkerList.Count; j++)
                } // end if
            }// end for (int i = 0; i < blockList.Count; i++)
        } // end of function CheckCollisionWithBlockList()
        private void CheckCollisionWithBlockList2()
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                if (DistanceBetween(position, blockList[i].center) < blockList[i].size.Width * 1.75
                    && blockList[i].visible)
                {
                    blockList[i].Update();
                    for (int j = 0; j < ccMarkerList.Count; j++)
                    {
                        if (blockList[i].InRectBounds(ccMarkerList[j].position))
                        {
                            SetRectangleCollisionSide(blockList[i]);
                            BounceV3();
                            RegisterHitOnBlock(blockList[i], ballPower);
                        } // end if 2
                    } // end for 2
                } // end if 1
                //else b.outlinePen.Color = Color.Black;
            }// end for 1
        } // end of function CheckCollisionWithBlockList2()
        
        protected void RegisterHitOnBlock(Blockv0d2 b, int amount)
        {
            b.hitAmount = amount;
            b.hit = true;
        } // end of function RegisterHitOnBlock(Blockv0d2 b, int amount)
        #region Collision Check marker set up region
        protected void LoadCollisionCheckMarkers()
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
        protected void UpdateCollisionCheckMarkers()
        {
            for (int i = 0; i < ccMarkerList.Count; i++)
            {
                ccMarkerList[i].Update(position, velocity);
                //ccMarkerList[i].Update(position, angle, radius, velocity);
            }
        }
        protected void DrawCollisionCheckMarkers(Graphics g)
        {
            for (int i = 0; i < ccMarkerList.Count; i++)
            {
                ccMarkerList[i].Draw(g);
            }
        }
        #endregion Collision Check marker set up region
        protected void SetBallHeadingv2()
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

        protected void BounceV3()
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
        protected void BounceV3(CardinalDirections bounceWall)
        {
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
        protected void BounceV4()
        {
            if (ballHeading == CardinalDirections.NorthEast)
            {
                if (rectCollisionSide == CardinalDirections.South)
                {
                    //angle = pi / 4;
                    angle -= pi / 4;
                }
                if (rectCollisionSide == CardinalDirections.West)
                {
                    //angle = 5 * pi / 4;
                    angle += pi / 4;
                }
            }
            if (ballHeading == CardinalDirections.SouthEast)
            {
                if (rectCollisionSide == CardinalDirections.North)
                {
                    //angle = 7 * pi / 4;
                    angle -= pi / 4;
                }
                if (rectCollisionSide == CardinalDirections.West)
                {
                    //angle = 3 * pi / 4;
                    angle += pi / 4;
                }
            }
            if (ballHeading == CardinalDirections.SouthWest)
            {
                if (rectCollisionSide == CardinalDirections.North)
                {
                    //angle = 5 * pi / 4;
                    angle += pi / 4;
                }
                if (rectCollisionSide == CardinalDirections.East)
                {
                    //angle = pi / 4;
                    angle -= pi / 4;
                }
            }
            if (ballHeading == CardinalDirections.NorthWest)
            {
                if (rectCollisionSide == CardinalDirections.South)
                {
                    //angle = 3 * pi / 4;
                    angle -= pi / 4;
                }
                if (rectCollisionSide == CardinalDirections.East)
                {
                    //angle = 7 * pi / 4;
                    angle += pi / 4;
                }
            }
        } // end of function BounceV4()
        protected void SetRectangleCollisionSide(Blockv0d2 b)
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
        private void SetRectangleCollisionSide(Blockv0d2 b, CollisionCheckMarkerv0d1 cc)
        {
            //float angleToRectCenter = AngleBetween2(b.center, cc.position);

            if (cc.position.X < b.position.X + b.size.Width && cc.position.X > b.center.X
                && cc.position.Y > b.position.Y && cc.position.Y < b.position.Y + b.size.Height)
            {
                rectCollisionSide = CardinalDirections.East;
            }
            if (cc.position.X > b.position.X && cc.position.X < b.position.X + b.size.Width
                && cc.position.Y < b.position.Y + b.size.Height && cc.position.Y > b.center.Y)
            {
                rectCollisionSide = CardinalDirections.South;
            }

            if (cc.position.X > b.position.X && cc.position.X < b.center.X
                && cc.position.Y > b.position.Y
                && cc.position.Y < b.position.Y + b.size.Height)
            {
                rectCollisionSide = CardinalDirections.West;
            }

            if (cc.position.X > b.position.X && cc.position.X < b.position.X + b.size.Width
                && cc.position.Y > b.position.Y && cc.position.Y < b.center.Y)
            {
                rectCollisionSide = CardinalDirections.North;
            }
        } // end of function SetRectangleCollisionSide(Blockv0d2 b, CollisionCheckMarkerv0d1 cc)
        private void DrawBlockList(Graphics g)
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                blockList[i].Draw(g);
            }
        } // end of function DrawBlockList(Graphics g)

        public void SetAngle(PointF p)
        {
            angle = AngleBetween2(position, p);
            //PointF rtAnglPos = new PointF();
            //rtAnglPos.X = p.X;
            //rtAnglPos.Y = position.Y;
            //float hyp = DistanceBetween(position, p);
            //float adj = DistanceBetween(position, rtAnglPos);
            //float adjOverHyp = adj / hyp;
            //angle = (float)Math.Acos(adjOverHyp);

            //if (p.X < position.X)
            //{
            //    angle = (float)Math.PI - angle;
            //}
            //if (p.Y < position.Y)
            //{
            //    angle = (float)Math.PI * 2 - angle;
            //}

            //degrees = angle * (180 / (float)Math.PI);
            //degrees = (float)Math.Round(degrees);
        }
        #endregion quick copy region
    } // end of class Ballv6d2
}

#region removed from class
//public void LoadBlockList(List<Blockv0d2> bList)
//{
//    blockList.Clear();
//    for (int i = 0; i < bList.Count; i++)
//    {
//        blockList.Add(bList[i]);
//    }
//}
//public void AddToBlockList(List<Blockv0d2> bList)
//{
//    for (int i = 0; i < bList.Count; i++)
//    {
//        blockList.Add(bList[i]);
//    }
//}
//public void RemoveBrokenBlocks()
//{
//    for(int i = 0; i < blockList.Count; i++)
//    {
//        if (blockList[i].visible == false)
//        {
//            blockList.RemoveAt(i);
//        }
//    }
//}
#endregion