using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    public class Ballv6d2d2 : Ballv6d2
    {

        public Ballv6d2d2()
        {
        }
        public void Update(ref List<Blockv0d2> blockList)
        {
            CheckInBounds();
            SetBallHeadingv2();
            UpdateCollisionCheckMarkers();
            CheckCollisionWithBlockList(ref blockList);
            if (IsLaunched)
            {
                speed = maxSpeed;
            }
            else
            {
                speed = 0;
            }

            //SetBallHeadingv2();
            //UpdateCollisionCheckMarkers();
            //CheckCollisionWithBlockList(ref blockList);

            velocity.X = Cos(angle) * speed;
            velocity.Y = Sin(angle) * speed;
            position.X += Round(velocity.X);
            position.Y += Round(velocity.Y);
        } // end of function Update()
        public void CheckCollisionWithBlockList(ref List<Blockv0d2> blockList)
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                if (blockList[i].visible)
                {
                    for (int j = 0; j < ccMarkerList.Count; j++)
                    {
                        if (blockList[i].InRectBounds(ccMarkerList[j].position))
                        {
                            SetRectangleCollisionSide(blockList[i]);
                            BounceV3();
                            RegisterHitOnBlock(blockList[i], ballPower);
                        }
                    }
                }
            }

        } // end of function CheckCollisionWithBlockList(ref List<Blockv0d2> blockList)

        protected new void CheckInBounds()
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
    }
}


#region older collision check function
//public void CheckCollisionWithBlockList(ref List<Blockv0d2> blockList)
//{
//    for (int i = 0; i < blockList.Count; i++)
//    {
//        if (DistanceBetween(position, blockList[i].center) < blockList[i].size.Width * 2
//            && blockList[i].visible)
//        {
//            blockList[i].Update();
//            for (int j = 0; j < ccMarkerList.Count; j++)
//            {
//                if (blockList[i].InRectBounds(ccMarkerList[j].position))
//                {
//                    SetRectangleCollisionSide(blockList[i]);
//                    BounceV3();
//                    RegisterHitOnBlock(blockList[i], ballPower);
//                } // end if
//            } // end for (int j = 0; j < ccMarkerList.Count; j++)
//        } // end if
//    }// end for (int i = 0; i < blockList.Count; i++)
//} // end of function CheckCollisionWithBlockList()
//public void CheckCollisionWithBlockList2(ref List<Blockv0d2> blockList)
//{
//    for (int i = 0; i < blockList.Count; i++)
//    {
//        if (DistanceBetween(position, blockList[i].center) < blockList[i].size.Width * 1.75
//            && blockList[i].visible)
//        {
//            blockList[i].Update();
//            for (int j = 0; j < ccMarkerList.Count; j++)
//            {
//                if (blockList[i].InRectBounds(ccMarkerList[j].position))
//                {
//                    SetRectangleCollisionSide(blockList[i]);
//                    BounceV3();
//                    RegisterHitOnBlock(blockList[i], ballPower);
//                } // end if 2
//            } // end for 2
//        } // end if 1
//        //else b.outlinePen.Color = Color.Black;
//    }// end for 1
//} // end of function CheckCollisionWithBlockList2(ref List<Blockv0d2> blockList)
#endregion older collision check functions