using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BallSlingerGame02NewLoop.CollisionPartsFolder
{
    using static HelperFunctionsFolder.GeneralFunctionsv1;
    public class CollisionCheckMarkerv0d1
    {
        PointF center = new PointF();
        public PointF position = new PointF();
        public float angle = 0;
        public float radius = 0;
        public float rotation = 0;
        public Color c = Color.Red;

        
        public CollisionCheckMarkerv0d1(PointF center, float angle, float radius, float rotation)
        {
            Load(center, angle, radius, rotation);
        }
        public void Load(PointF center, float angle, float radius, float rotation)
        {
            this.center = center;
            this.angle = angle;
            this.radius = radius;
            this.rotation = rotation;
            float adjustedAngle = angle + rotation;
            //position.X = center.X + Cos(adjustedAngle) * radius;
            //position.Y = center.Y + Sin(adjustedAngle) * radius;
            position.X = Round(center.X + Cos(adjustedAngle) * radius);
            position.Y = Round(center.Y + Sin(adjustedAngle) * radius);
        }
        public void Update(PointF center, float angle, float radius, PointF velocity)
        {
            float adjustedAngle = angle + rotation;
            //position.X = center.X + Cos(adjustedAngle) * radius + velocity.X;
            //position.Y = center.Y + Sin(adjustedAngle) * radius + velocity.Y;
            position.X = Round(center.X + Cos(adjustedAngle) * radius + velocity.X);
            position.Y = Round(center.Y + Sin(adjustedAngle) * radius + velocity.Y);
        }
        public void Update(PointF center, PointF velocity)
        {
            //position.X = center.X + Cos(rotation) * radius + velocity.X;
            //position.Y = center.Y + Sin(rotation) * radius + velocity.Y;
            position.X = Round(center.X + Cos(rotation) * radius + velocity.X);
            position.Y = Round(center.Y + Sin(rotation) * radius + velocity.Y);
        }

        public bool IsCollisionWithRect(RectangleF rect)
        {
            if (position.X >= rect.X && position.X <= rect.X + rect.Width
                && position.Y >= rect.Y && position.Y <= rect.Y + rect.Height)
                return true;
            else
                return false;
        }
        
        public void Draw(Graphics g)
        {
            DrawPoint(g, c, position);
        }
    }
}
