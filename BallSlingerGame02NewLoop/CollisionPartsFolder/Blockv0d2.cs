using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    using static HelperFunctionsFolder.GeneralFunctionsv1;
    public class Blockv0d2
    {
        public RectangleF rectangle = new RectangleF();
        public PointF position = new PointF();
        public PointF center = new PointF();
        public SizeF size = new SizeF();
        public Color defaultColor = Color.AntiqueWhite;
        //public Color color = Color.AntiqueWhite;
        public SolidBrush fillBrush;
        public Pen outlinePen;
        public bool visible = true;

        public bool hit = false;
        public int hitsToBreak = 1;
        public int startingHitsToBreak = 1;
        Color color;

        protected Font blockTextFont;

        public Blockv0d2()
        {

        }
        public Blockv0d2(PointF pos, SizeF size)
        {
            Load(pos, size);
        }
        public void Load(PointF pos, SizeF size)
        {
            blockTextFont = new Font("Arial", 14, FontStyle.Regular);
            fillBrush = new SolidBrush(defaultColor);
            outlinePen = new Pen(Color.Black);
            rectangle = new RectangleF(pos, size);
            this.position = pos;
            this.size = size;
            center.X = position.X + size.Width / 2;
            center.Y = position.Y + size.Height / 2;
            LocalLoadAndReset();
        } // end of function Load(PointF pos, SizeF size)
        public void Reset()
        {
            LocalLoadAndReset();
        }
        private void LocalLoadAndReset()
        {
            hitsToBreak = startingHitsToBreak;
        }
        public void Update()
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
            center.X = position.X + size.Width / 2;
            center.Y = position.Y + size.Height / 2;
            CheckForHits();
        }
        public void Update(PointF position)
        {
            this.position = position;
            CheckForHits();
        }

        public void Draw(Graphics g)
        {
            fillBrush.Color = SetColor();
            if (visible)
            {
                g.FillRectangle(fillBrush, new RectangleF(position, size));
                g.DrawRectangle(outlinePen, position.X, position.Y, size.Width, size.Height);
                DrawHitsToBreakOnBlock(g);
            }
            
        } // end function Draw(Graphics g)
        public int hitAmount = 0;
        //public bool processingHit = false;
        public void CheckForHits()
        {
            if (hit)
            {
                hit = false;
                hitsToBreak -= hitAmount;
                hitAmount = 0;
                if (hitsToBreak <= 0)
                    visible = false;
            }
        }
        //public void CheckForHits()
        //{
        //    if (processingHit)
        //    {
        //        if (hit)
        //        {
        //            hitsToBreak -= hitAmount;
        //            hitAmount = 0;
        //            if (hitsToBreak <= 0)
        //                visible = false;
        //            hit = false;
        //        }
        //        processingHit = false;
        //    }
        //}
        private Color SetColor()
        {
            //int r = (startingHitsToBreak - hitsToBreak) * 3;
            //int g = (startingHitsToBreak - hitsToBreak) * 3;
            //int b = (startingHitsToBreak - hitsToBreak) * 3;
            int r = 255 - hitsToBreak * 2;
            int g = (startingHitsToBreak - hitsToBreak) * 3;
            int b = 255 - g;

            if (r > 255)
                r = 255;
            if (r < 0)
                r = 0;

            if (g > 255)
                g = 255;
            if (g < 0)
                g = 0;

            if (b > 255)
                b = 255;
            if (b < 0)
                b = 0;
            return Color.FromArgb(255, r, g, b);

        }
        private void DrawHitsToBreakOnBlock(Graphics g)
        {
            SizeF textSize = g.MeasureString(hitsToBreak.ToString(), blockTextFont);
            PointF textPos = new PointF();
            textPos.X = center.X - textSize.Width / 2;
            textPos.Y = center.Y - textSize.Height / 2;
            g.DrawString(hitsToBreak.ToString(), blockTextFont, Brushes.Black, textPos);
        }
        public bool InRectBounds(PointF p)
        {
            if (p.X >= position.X && p.X <= position.X + size.Width
                && p.Y >= position.Y && p.Y <= position.Y + size.Height)
                return true;
            else
                return false;
        } // end of function InRectBounds(PointF p)
    }
}
