using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



using BallSlingerGame02NewLoop.BasicDrawableObjects;
using BallSlingerGame02NewLoop.GameControlCollection;

namespace BallSlingerGame02NewLoop.GamePlayScreens
{
    class GamePlayScreen01
    {
        public static GameControl gameControl;
        int screenWidth;
        int screenHeight;
        Bitmap backBuffer;
        Graphics bbg;
        public int ScreenWidth { set { screenWidth = value; } }
        public int ScreenHeight { set { screenHeight = value; } }

        bool exitProram = false;
        public bool ExitProgram { get { return exitProram; }set { exitProram = value; } }
        BasicObjectController gameObjs;

        Random r = new Random();


        public GamePlayScreen01()
        {
            gameControl = new GameControl();
        }

        public void Load()
        {
            backBuffer = new Bitmap(screenWidth, screenHeight);
            bbg = Graphics.FromImage(backBuffer);
            gameObjs = new BasicObjectController(screenWidth, screenHeight);
        }

        
        #region updateing game controls
        public void UpdateKeyboardEventArgs(System.Windows.Forms.KeyEventArgs ke, UpDownState state)
        {
            if (state == UpDownState.Down)
            {
                gameControl.UpdateKeyDown(ke);
            }
            if (state == UpDownState.Up)
            {
                gameControl.UpdateKeyUp(ke);
            }
        }
        public void UpdateMousePosition(System.Windows.Forms.MouseEventArgs me)
        {
            gameControl.UpdateMousePosition(new PointF(me.X, me.Y));
        }
        public void UpdateMouseButtonsDown(System.Windows.Forms.MouseEventArgs me)
        {
            gameControl.UpdateButtonDown(me);
        }
        public void UpdateMouseButtonsUp(System.Windows.Forms.MouseEventArgs me)
        {
            gameControl.UpdateButtonUp(me);
        }
        #endregion

        Timer gameTime = new Timer();
        int seconds = 0;
        bool exitProgram = false;
        public void Update()
        {
            exitProram = gameControl.ExitProgram;
            gameObjs.ProcessGameControls(gameControl.MControls);
            gameObjs.Update();

            DrawToBackBuffer();
            gameControl.UpdateKeyboardControlLastState();
            gameControl.UpdateMouseControlLastState();
            if(gameTime.GetTicks() >= 1000)
            {
                seconds++;
                gameTime.Reset();
            }
        }
        
        protected virtual void DrawToBackBuffer()
        {
            bbg.Clear(Color.Aqua);
            gameObjs.Draw(bbg);
            DrawText("Seconds: " + seconds.ToString(), new PointF(10, 0));
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(backBuffer, 0, 0);
        }

        
        private void BallControls()
        {
            
            
            //MouseControls mc = gameControl.MControls;
            //if (mc.LeftButton.State == UpDownState.Down && mc.LeftButton.LastState == UpDownState.Down &&
            //    ball.IsLaunched == false)
            //{
            //    if (mc.Position.Y > ball.Position.Y - (ball.ObjectSize.Height * 2))
            //    {
            //        ball.Misfire = true;
            //    }
            //    else
            //    {
            //        ball.SettingLaunchAngle = true;
            //        ball.SetLaunchAngle(mc.Position);
            //    }
            //}
            //if (mc.LeftButton.State == UpDownState.Up && mc.LeftButton.LastState == UpDownState.Down &&
            //    ball.IsLaunched == false)
            //{
            //    ball.SettingLaunchAngle = false;
            //    ball.Launch(ball.AngleBetweenPoints(mc.Position), mc.Position);
            //}
        }
        private void BallControlsWithMidAirClicks()
        {
            MouseControls mc = gameControl.MControls;
            //if (mc.LeftButton.State == UpDownState.Down && mc.LeftButton.LastState == UpDownState.Down &&
            //    ball.IsLaunched == false)
            //{
            //    if (mc.Position.Y > ball.Position.Y - (ball.ObjectSize.Height * 2))
            //    {
            //        ball.Misfire = true;
            //    }
            //    else
            //    {
            //        ball.SettingLaunchAngle = true;
            //        ball.SetLaunchAngle(mc.Position);
            //    }
            //}
            //if (mc.LeftButton.State == UpDownState.Up && mc.LeftButton.LastState == UpDownState.Down)
            //{
            //    ball.SettingLaunchAngle = false;
            //    //ball.Launch();
            //    ball.Launch(ball.AngleBetweenPoints(mc.Position), mc.Position);
            //}
        }


        #region text drawing parts
        Font font = new Font("Arial", 10, FontStyle.Regular);
        
        private void DrawDebugText(PointF position)
        {
            PointF textPos = position;
            PointF nextTextPosition;
            nextTextPosition = DrawKeyboardDebugText(textPos);
            DrawMouseDebugText(nextTextPosition);
        }
        private PointF DrawKeyboardDebugText(PointF position)
        {
            KeyboardControl kbc;

            string text;
            float textHeight;
            for (int i = 0; i < gameControl.KeyboardControlList.Count; i++)
            {

                kbc = gameControl.KeyboardControlList[i];
                text = kbc.Key.ToString() + ": ";
                text += kbc.State.ToString();
                textHeight = bbg.MeasureString(text, font).Height;
                DrawText(text, position);
                position.Y += textHeight;
            }
            return position;
        }
        private void DrawMouseDebugText(PointF position)
        {

            MouseControls mc = gameControl.MControls;
            string mousePosition = "Mouse Position: " + mc.Position.ToString();
            float textHeight = bbg.MeasureString(mousePosition, font).Height;
            DrawText(mousePosition, position);
            position.Y += textHeight;

            string leftButtonStatus = "Left Button: " + mc.LeftButton.State.ToString();
            string leftButtonDownPos = "Down: " + mc.LeftButton.DownPosition.ToString();
            string leftButtonUpPos = "Up: " + mc.LeftButton.UpPosition.ToString();
            DrawText(leftButtonStatus, position);
            position.Y += textHeight;
            DrawText(leftButtonDownPos, position);
            position.Y += textHeight;
            DrawText(leftButtonUpPos, position);
            position.Y += textHeight;


            string rightButtonStatus = "Right Button: " + mc.RightButton.State.ToString();
            string rightButtonDownPos = "Down: " + mc.RightButton.DownPosition.ToString();
            string rightButtonUpPos = "Up: " + mc.RightButton.UpPosition.ToString();
            DrawText(rightButtonStatus, position);
            position.Y += textHeight;
            DrawText(rightButtonDownPos, position);
            position.Y += textHeight;
            DrawText(rightButtonUpPos, position);
            position.Y += textHeight;
        }
        
        #region text drawing methods
        private void DrawText(string text, PointF position)
        {
            bbg.DrawString(text, font, Brushes.Black, position);
        }
        #endregion
        #endregion


    }
}
