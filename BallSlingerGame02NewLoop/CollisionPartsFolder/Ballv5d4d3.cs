using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    class Ballv5d4d3 : Ballv4d2
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

        protected List<Blockv0d1> blockList = new List<Blockv0d1>();

        protected PointF velocity;

        protected float startingSpeed = 5;
        protected float maxSpeed = 5;
        protected float speed = 5;

        public bool IsLaunched = false;
        public bool initialLaunch = true;

        protected bool collisionDetected = false;
        public Ballv5d4d3()
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
            LoadCollisionCheckMarkers();

        } // end of function LocalLoadAndReset()
        
        public void LoadBlockList(List<Blockv0d1> bList)
        {
            blockList.Clear();
            //collisionPointList.Clear();
            for (int i = 0; i < bList.Count; i++)
            {
                blockList.Add(bList[i]);
            }
            //for (int i = 0; i < blockList.Count; i++)
            //{
            //    Blockv0d1 tempblock = blockList[i];
            //    for (int j = 0; j < tempblock.ccMarkerList.Count; j++)
            //    {
            //        collisionPointList.Add(tempblock.ccMarkerList[j].position);
            //    }
            //}
        }
        
        public void Update()
        {
            if (IsLaunched)
                speed = maxSpeed;
            else
                speed = 0;
            UpdateCollisionCheckMarkers();
            SetBallHeadingv2();

            CheckCollisionWithBlockList();

            velocity.X = Cos(angle) * speed;
            velocity.Y = Sin(angle) * speed;
            position.X += velocity.X;
            position.Y += velocity.Y;
        } // end of function Update()
        
        public override void Draw(Graphics g)
        {
            DrawBlockList(g);
            base.Draw(g);
            if (showDebugText)
                DrawDebugInfo(g, ref textPos);
            DrawCollisionCheckMarkers(g);
        } // end of function Draw(Graphics g)

        protected void DrawDebugInfo(Graphics g, ref PointF textPos)
        {
            SolidBrush sb = new SolidBrush(Color.Peru);
            DrawText(g, "Ballv5d4d3 Debug Info:", sb, ref textPos);
            DrawText(g, "position: " + position.ToString(), sb, ref textPos);
            DrawText(g, "velocity: " + velocity.ToString(), sb, ref textPos);
            DrawText(g, "angle: " + angle.ToString(), sb, ref textPos);
            DrawText(g, "degrees: " + degrees.ToString(), sb, ref textPos);
            DrawText(g, "ballHeading: " + ballHeading.ToString(), sb, ref textPos);
            DrawText(g, "blockList.Count: " + blockList.Count.ToString(), sb, ref textPos);
            DrawText(g, "collision check points on ball: " + ccMarkerList.Count.ToString(), sb, ref textPos);
            
        } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)

        private void CheckCollisionWithBlockList()
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                if (DistanceBetween(position, blockList[i].center) < blockList[i].size.Width * 2)
                {
                    for (int j = 0; j < ccMarkerList.Count; j++)
                    {
                        if (blockList[i].InRectBounds(ccMarkerList[j].position))
                        {
                            //IsLaunched = false;
                            SetRectangleCollisionSide(blockList[i]);
                            BounceV3();
                        }
                    }
                }
            }
        } // end of function CheckCollisionWithBlockList()
        
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
        }

        private void SetRectangleCollisionSide(Blockv0d1 b)
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
    } // end of class Ballv5d4d3 : Ballv4d2
}


#region good back up version
// First good working ball
//class Ballv5d4d3 : Ballv4d2
//{
//    protected enum CardinalDirections
//    {
//        NotSet,
//        East,
//        SouthEast,
//        South,
//        SouthWest,
//        West,
//        NorthWest,
//        North,
//        NorthEast
//    }
//    protected CardinalDirections rectCollisionSide;
//    CardinalDirections ballHeading = CardinalDirections.NotSet;
//    CardinalDirections lastBallHeading = CardinalDirections.NotSet;

//    public List<Blockv0d1> blockList = new List<Blockv0d1>();
//    //public List<PointF> collisionPointList = new List<PointF>();

//    protected PointF velocity;

//    protected float startingSpeed = 5;
//    protected float maxSpeed = 5;
//    protected float speed = 5;

//    public bool IsLaunched = false;
//    public bool initialLaunch = true;

//    protected bool collisionDetected = false;
//    public Ballv5d4d3()
//    {

//        showBallPositionRect = false;
//        showHeadingMarker = false;
//        visible = true;
//        rectCollisionSide = CardinalDirections.NotSet;
//    }

//    public override void Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)
//    {
//        base.Load(pos, gameBoundsWidth, gameBoundsHeight);
//        LocalLoadAndReset();
//    } // end of function Load(PointF pos, int gameBoundsWidth, int gameBoundsHeight)
//    public override void Reset()
//    {
//        base.Reset();
//        LocalLoadAndReset();
//    } // end of function Reset()

//    private void LocalLoadAndReset()
//    {
//        collisionDetected = false;
//        rectCollisionSide = CardinalDirections.NotSet;
//        IsLaunched = false;
//        initialLaunch = true;
//        LoadCollisionCheckMarkers();

//    } // end of function LocalLoadAndReset()
//    #region Collision Check marker set up region
//    List<CollisionCheckMarkerv0d1> ccMarkerList = new List<CollisionCheckMarkerv0d1>();
//    private void LoadCollisionCheckMarkers()
//    {
//        ccMarkerList.Clear();
//        //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, pi / 2));
//        //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, pi / 4));
//        //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, 0));
//        //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, -(pi / 4)));
//        //ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, angle, radius, -(pi / 2)));

//        for (int i = 0; i < 8; i++)
//        {
//            ccMarkerList.Add(new CollisionCheckMarkerv0d1(position, 0, radius, i * pi / 4));
//            if (i == 0)
//            {
//                ccMarkerList[0].c = Color.Green;
//            }
//        }
//    }
//    private void UpdateCollisionCheckMarkers()
//    {
//        for (int i = 0; i < ccMarkerList.Count; i++)
//        {
//            //ccMarkerList[i].Update(position, angle, radius, velocity);
//            ccMarkerList[i].Update(position, velocity);
//        }
//    }
//    private void DrawCollisionCheckMarkers(Graphics g)
//    {
//        for (int i = 0; i < ccMarkerList.Count; i++)
//        {
//            ccMarkerList[i].Draw(g);
//        }
//    }

//    #endregion Collision Check marker set up region
//    public void LoadBlockList(List<Blockv0d1> bList)
//    {
//        blockList.Clear();
//        //collisionPointList.Clear();
//        for (int i = 0; i < bList.Count; i++)
//        {
//            blockList.Add(bList[i]);
//        }
//        //for (int i = 0; i < blockList.Count; i++)
//        //{
//        //    Blockv0d1 tempblock = blockList[i];
//        //    for (int j = 0; j < tempblock.ccMarkerList.Count; j++)
//        //    {
//        //        collisionPointList.Add(tempblock.ccMarkerList[j].position);
//        //    }
//        //}
//    }

//    public void Update()
//    {

//        if (IsLaunched)
//            speed = maxSpeed;
//        else
//            speed = 0;
//        UpdateCollisionCheckMarkers();
//        SetBallHeadingv2();

//        CheckCollisionWithBlockList();

//        velocity.X = Cos(angle) * speed;
//        velocity.Y = Sin(angle) * speed;
//        position.X += velocity.X;
//        position.Y += velocity.Y;
//    } // end of function Update()
//    private void CheckCollisionWithBlockList()
//    {
//        for (int i = 0; i < blockList.Count; i++)
//        {
//            if (DistanceBetween(position, blockList[i].center) < blockList[i].size.Width * 2)
//            {
//                for (int j = 0; j < ccMarkerList.Count; j++)
//                {
//                    if (blockList[i].InRectBounds(ccMarkerList[j].position))
//                    {
//                        //IsLaunched = false;
//                        SetRectangleCollisionSide(blockList[i]);
//                        BounceV3();
//                    }
//                }
//            }
//        }
//    } // end of function CheckCollisionWithBlockList()

//    private void CheckInBounds()
//    {
//        if (position.X < 0 || position.X > gameWidth || position.Y < 0 || position.Y > gameWidth)
//        {
//            IsLaunched = false;
//            Reset();
//        }

//    }
//    public override void Draw(Graphics g)
//    {
//        DrawBlockList(g);
//        base.Draw(g);
//        if (showDebugText)
//            DrawDebugInfo(g, ref textPos);
//        DrawCollisionCheckMarkers(g);
//    } // end of function Draw(Graphics g)

//    protected virtual void DrawDebugInfo(Graphics g, ref PointF textPos)
//    {
//        SolidBrush sb = new SolidBrush(Color.Peru);
//        DrawText(g, "Ballv5d4d3 Debug Info:", sb, ref textPos);
//        DrawText(g, "position: " + position.ToString(), sb, ref textPos);
//        DrawText(g, "velocity: " + velocity.ToString(), sb, ref textPos);
//        DrawText(g, "angle: " + angle.ToString(), sb, ref textPos);
//        DrawText(g, "degrees: " + degrees.ToString(), sb, ref textPos);
//        DrawText(g, "ballHeading: " + ballHeading.ToString(), sb, ref textPos);
//        DrawText(g, "blockList.Count: " + blockList.Count.ToString(), sb, ref textPos);
//        DrawText(g, "collision check points on ball: " + ccMarkerList.Count.ToString(), sb, ref textPos);

//    } // end of function DrawDebugInfo(Graphics g, ref PointF textPos)

//    private void SetBallHeadingv2()
//    {
//        lastBallHeading = ballHeading;
//        if (velocity.X > 0 && velocity.Y > 0)
//            ballHeading = CardinalDirections.SouthEast;
//        if (velocity.X < 0 && velocity.Y > 0)
//            ballHeading = CardinalDirections.SouthWest;
//        if (velocity.X < 0 && velocity.Y < 0)
//            ballHeading = CardinalDirections.NorthWest;
//        if (velocity.X > 0 && velocity.Y < 0)
//            ballHeading = CardinalDirections.NorthEast;
//        if (velocity.X == 0 && velocity.Y < 0)
//            ballHeading = CardinalDirections.North;
//        if (velocity.X == 0 && velocity.Y > 0)
//            ballHeading = CardinalDirections.South;
//        if (velocity.X < 0 && velocity.Y == 0)
//            ballHeading = CardinalDirections.West;
//        if (velocity.X > 0 && velocity.Y == 0)
//            ballHeading = CardinalDirections.East;
//        //if (velocity.X == 0 && velocity.Y == 0)
//        //{
//        //    ballHeading = CardinalDirections.NotSet;
//        //}

//    } // end of function SetBallHeading()
//    private void BounceV3()
//    {
//        if (ballHeading == CardinalDirections.NorthEast)
//        {
//            float tempAngle = angle - 3 * pi / 2;
//            if (rectCollisionSide == CardinalDirections.South)
//            {
//                angle = pi / 2 - tempAngle;
//            }
//            if (rectCollisionSide == CardinalDirections.West)
//            {
//                angle = 3 * pi / 2 - tempAngle;
//            }
//        }
//        if (ballHeading == CardinalDirections.SouthEast)
//        {
//            float tempAngle = angle;
//            if (rectCollisionSide == CardinalDirections.North)
//            {
//                angle = 2 * pi - tempAngle;
//            }
//            if (rectCollisionSide == CardinalDirections.West)
//            {
//                angle = pi - tempAngle;
//            }
//        }
//        if (ballHeading == CardinalDirections.SouthWest)
//        {
//            float tempAngle = angle - pi / 2;
//            if (rectCollisionSide == CardinalDirections.North)
//            {
//                angle = 3 * pi / 2 - tempAngle;
//            }
//            if (rectCollisionSide == CardinalDirections.East)
//            {
//                angle = pi / 2 - tempAngle;
//            }
//        }
//        if (ballHeading == CardinalDirections.NorthWest)
//        {
//            float tempAngle = angle - pi;
//            if (rectCollisionSide == CardinalDirections.South)
//            {
//                angle = pi - tempAngle;
//            }
//            if (rectCollisionSide == CardinalDirections.East)
//            {
//                angle = 2 * pi - tempAngle;
//            }
//        }
//    }

//    private void SetRectangleCollisionSide(Blockv0d1 b)
//    {
//        float angleToRectCenter = AngleBetween2(b.center, position);
//        #region attempt1
//        if (angleToRectCenter <= pi / 4 || angleToRectCenter >= (2 * pi) - pi / 4)
//        {
//            rectCollisionSide = CardinalDirections.East;
//        }
//        if (angleToRectCenter > pi / 4 && angleToRectCenter <= pi - pi / 4)
//        {
//            rectCollisionSide = CardinalDirections.South;
//        }
//        if (angleToRectCenter > pi - pi / 4 && angleToRectCenter <= pi + pi / 4)
//        {
//            rectCollisionSide = CardinalDirections.West;
//        }
//        if (angleToRectCenter > pi + pi / 4 && angleToRectCenter < 2 * pi - pi / 4)
//        {
//            rectCollisionSide = CardinalDirections.North;
//        }
//        #endregion
//    } // end of function SetRectangleCollisionSide(Blockv0d1 b, PointF cp)

//    private void DrawBlockList(Graphics g)
//    {
//        for (int i = 0; i < blockList.Count; i++)
//        {
//            blockList[i].Draw(g);
//        }
//    } // end of function DrawBlockList(Graphics g)
//} // end of class Ballv5d4d3 : Ballv4d2
#endregion
