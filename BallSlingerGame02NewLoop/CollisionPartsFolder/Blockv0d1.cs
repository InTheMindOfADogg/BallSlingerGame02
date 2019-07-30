using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    using static HelperFunctionsFolder.GeneralFunctionsv1;
    class Blockv0d1
    {
        public bool collisionLastFrame = false;
        public bool restRound= false;
        public RectangleF rectangle = new RectangleF();
        public PointF position = new PointF();
        public PointF center = new PointF();
        public SizeF size = new SizeF();
        List<PointF> cornerPointList = new List<PointF>();
        public List<CollisionCheckMarkerv0d1> ccMarkerList = new List<CollisionCheckMarkerv0d1>();
        public Color defaultColor = Color.AntiqueWhite;
        //public Color color = Color.AntiqueWhite;
        public SolidBrush sb;


        //public float angleToBall = 0;
        

        public Blockv0d1()
        {

        }
        public Blockv0d1(PointF pos, SizeF size)
        {
            Load(pos, size);
        }
        public void Load(PointF pos, SizeF size)
        {
            sb = new SolidBrush(defaultColor);
            rectangle = new RectangleF(pos, size);
            this.position = pos;
            this.size = size;
            center.X = position.X + size.Width / 2;
            center.Y = position.Y + size.Height / 2;
            //SetCollisionPoints();

        } // end of function Load(PointF pos, SizeF size)

        public void Draw(Graphics g)
        {
            PointF textPos = new PointF(500, 500);
            //g.FillRectangle(Brushes.AntiqueWhite, new RectangleF(position, size));
            g.FillRectangle(sb, new RectangleF(position, size));
            g.DrawRectangle(Pens.Black, position.X, position.Y, size.Width, size.Height);
            //DrawCollisionPoints(g);
        } // end function Draw(Graphics g)

        public bool InRectBounds(PointF p)
        {
            if (p.X >= position.X && p.X <= position.X + size.Width
                && p.Y >= position.Y && p.Y <= position.Y + size.Height)
                return true;
            else
                return false;
        }

        private void SetCollisionPoints()
        {
            PointF centerMarker = new PointF(center.X, center.Y);
            float radius = 0;

            #region corner collision markers
            radius = size.Width * 0.7f;
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, pi / 4, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 3 * pi / 4, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 5 * pi / 4, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 7 * pi / 4, radius, 0));
            #endregion corner collision markers

            #region side collision markers
            radius = size.Width * 0.5f;
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 0, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 2 * pi / 4, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 4 * pi / 4, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 6 * pi / 4, radius, 0));
            #endregion side collision markers

            #region bottom between side center and corner
            radius = size.Width * 0.55f;
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, pi / 7, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 2.5f * pi / 7, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 4.5f * pi / 7, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 6 * pi / 7, radius, 0));
            #endregion bottom between side center and corner

            #region top between side center and corner
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 8 * pi / 7, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 9.5f * pi / 7, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 11.5f * pi / 7, radius, 0));
            ccMarkerList.Add(new CollisionCheckMarkerv0d1(centerMarker, 13 * pi / 7, radius, 0));
            #endregion top between side center and corner


        } // end of function SetTestPoints()
        private void DrawCollisionPoints(Graphics g)
        {
            for (int i = 0; i < ccMarkerList.Count; i++)
            {
                ccMarkerList[i].Draw(g);
            }
        } // end function DrawCornerPoints(Graphics g)

    } // end of class Blockv0d1


}
