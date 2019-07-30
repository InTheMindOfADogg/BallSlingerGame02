using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

using BallSlingerGame02NewLoop.GamePlayScreens;
using BallSlingerGame02NewLoop.BasicDrawableObjects;
using BallSlingerGame02NewLoop.GameControlCollection;

namespace BallSlingerGame02NewLoop
{
    // Game Form and control parts
    public class GameForm01 : Form
    {
        Timer timer = new Timer();

        GamePlayScreen01 gps;
        int screenOffSetWidth = 15;
        int screenOffSetHeight = 40;
        public GameForm01()
        {
            this.Text = "BallSlingerGame With new game loop";
            //Width = 1000 - screenOffSetWidth;
            //Height = 700 - screenOffSetHeight;
            Width = 1000 + screenOffSetWidth;
            Height = 700 + screenOffSetHeight;
            StartPosition = FormStartPosition.CenterScreen;

            //mainGame = LoadMainGame02WithDoubleDrawing();
            //gps = LoadGamePlayScreen();
            gps = new GamePlayScreen01();
            gps.ScreenWidth = Width - screenOffSetWidth;
            gps.ScreenHeight = Height - screenOffSetHeight; 

            KeyDown += GameForm01_KeyDown;
            KeyUp += GameForm01_KeyUp;
            MouseMove += GameForm01_MouseMove;
            MouseDown += GameForm01_MouseDown;
            MouseUp += GameForm01_MouseUp;
        }

        private void GameForm01_MouseUp(object sender, MouseEventArgs e)
        {
            //mainGame.UpdateMouseButtonsUp(e);
            gps.UpdateMouseButtonsUp(e);
        }
        private void GameForm01_MouseDown(object sender, MouseEventArgs e)
        {
            //mainGame.UpdateMouseButtonsDown(e);
            gps.UpdateMouseButtonsDown(e);
        }
        private void GameForm01_MouseMove(object sender, MouseEventArgs e)
        {
            //mainGame.UpdateMousePosition(e);
            gps.UpdateMousePosition(e);
        }
        private void GameForm01_KeyUp(object sender, KeyEventArgs e)
        {
            //mainGame.UpdateKeyboardEventArgs(e, UpDownState.Up);
            gps.UpdateKeyboardEventArgs(e, UpDownState.Up);
        }
        private void GameForm01_KeyDown(object sender, KeyEventArgs e)
        {
            //mainGame.UpdateKeyboardEventArgs(e, UpDownState.Down);
            gps.UpdateKeyboardEventArgs(e, UpDownState.Down);
        }

        



        public void LoadGame()
        {
            //mainGame.Load();
            gps.Load();
        }
        public void GameLoop()
        {
            while (this.Created)
            {
                timer.Reset();
                GameLogic();
                RenderScene();
                Application.DoEvents();
                if(gps.ExitProgram)
                    Application.Exit();
                while (timer.GetTicks() < 31) ;
            }
            
        }
        private void GameLogic()
        {
            //mainGame.Update();
            gps.Update();
        }
        private void RenderScene()
        {
            //mainGame.Draw(this.CreateGraphics());
            gps.Draw(this.CreateGraphics());
        }

    }
    
    
    static class BallSlingers01
    {

        static void Main()
        {
            //long startTick = 0;
            GameForm01 gameForm = new GameForm01();
            gameForm.Show();
            gameForm.LoadGame();
            gameForm.GameLoop();

        }
    }


    public class Timer
    {
        private long StartTick = 0;
        public Timer()
        {
            Reset();
        }
        public long GetTicks()
        {
            long currentTick = 0;
            currentTick = GetTickCount();
            return currentTick - StartTick;
        }
        public void Reset()
        {
            StartTick = GetTickCount();
        }
        [DllImport("kernel32.dll")]
        private static extern long GetTickCount();
    }
}


#region previous main game versions
#region version 01
//class MainGame01
//{
//    int screenWidth;
//    int screenHeight;
//    public int ScreenWidth { set { screenWidth = value; } }
//    public int ScreenHeight { set { screenHeight = value; } }
//    BaseDrawableObject01 obj;
//    public MainGame01()
//    {
//        obj = new BaseDrawableObject01();

//    }
//    public void SetProperties()
//    {
//        obj.ScreenWidth = screenWidth;
//        obj.ScreenHeight = screenHeight;


//    }
//    public void Load()
//    {
//        SetStartingPositions();
//    }
//    private void SetStartingPositions()
//    {
//        PointF p = new PointF();
//        p.X = screenWidth / 2 - obj.ObjectSize.Width / 2;
//        p.Y = screenHeight / 2 - obj.ObjectSize.Height / 2;
//        obj.Load(p);
//    }
//    public void Update()
//    {
//        obj.Update();
//    }
//    public void Draw(Graphics g)
//    {
//        g.Clear(Color.Aqua);
//        obj.Draw(g);
//    }
//}
#endregion
#region version 2 - moved to GamePlayScreen01
//class MainGame02WithDoubleDrawing01
//{
//    public static GameControl gameControl;
//    int screenWidth;
//    int screenHeight;
//    Bitmap backBuffer;
//    Graphics bbg;
//    public int ScreenWidth { set { screenWidth = value; } }
//    public int ScreenHeight { set { screenHeight = value; } }

//    //BasicCircle01 circle01;
//    // replacing circle01 with Ball02
//    Ball02 ball;
//    BasicRectangle01 block;
//    public MainGame02WithDoubleDrawing01()
//    {
//        gameControl = new GameControl();
//        //circle01 = new BasicCircle01();
//        ball = new Ball02();
//        block = new BasicRectangle01();
//    }
//    public void SetProperties()
//    {
//        //circle01.ScreenWidth = screenWidth;
//        //circle01.ScreenHeight = screenHeight;

//        ball.ScreenWidth = screenWidth;
//        ball.ScreenHeight = screenHeight;
//        block.ScreenWidth = screenWidth;
//        block.ScreenHeight = screenHeight;
//    }
//    public void Load()
//    {
//        backBuffer = new Bitmap(screenWidth, screenHeight);
//        bbg = Graphics.FromImage(backBuffer);
//        LoadBall();
//    }
//    private void LoadBall()
//    {
//        float circleRadius = 10;
//        PointF c1Pos = new PointF();
//        c1Pos.X = screenWidth / 2 - circleRadius * 2;
//        c1Pos.Y = screenHeight - circleRadius * 3;

//        ball.Load(c1Pos, circleRadius);
//        ball.SetDegrees(270);
//    }
//    private void LoadBlocks()
//    {
//        // working placing block
//    }

//    public void UpdateKeyboardEventArgs(KeyEventArgs ke, UpDownState state)
//    {
//        if (state == UpDownState.Down)
//        {
//            gameControl.UpdateKeyDown(ke);
//        }
//        if (state == UpDownState.Up)
//        {
//            gameControl.UpdateKeyUp(ke);
//        }
//    }
//    public void UpdateMousePosition(MouseEventArgs me)
//    {
//        gameControl.UpdateMousePosition(new PointF(me.X, me.Y));
//    }
//    public void UpdateMouseButtonsDown(MouseEventArgs me)
//    {
//        gameControl.UpdateButtonDown(me);
//    }
//    public void UpdateMouseButtonsUp(MouseEventArgs me)
//    {
//        gameControl.UpdateButtonUp(me);

//    }


//    private float DistanceBetweenPoints(PointF p1, PointF p2)
//    {
//        float distance = 0;
//        float deltaX = (p2.X - p1.X);
//        float deltaY = (p2.Y - p1.X);
//        deltaX = deltaX * deltaX;
//        deltaY = deltaY * deltaY;
//        float dXdYsum = deltaX + deltaY;
//        distance = (float)Math.Sqrt(dXdYsum);
//        return distance;
//    }
//    private void ProcessGameControls()
//    {
//        BallControls();
//    }
//    private void BallControls()
//    {
//        // start setting aiming when left button clicked down
//        // launch ball when released
//        MouseControls mc = gameControl.MControls;
//        if (mc.LeftButton.State == UpDownState.Down && mc.LeftButton.LastState == UpDownState.Down &&
//            ball.IsLaunched == false)
//        {
//            if(mc.Position.Y > ball.Position.Y - (ball.ObjectSize.Height * 2))
//            {
//                ball.Misfire = true;
//            }
//            else
//            {
//                ball.SettingLaunchAngle = true;
//                ball.SetLaunchAngle(mc.Position);
//            }
//        }
//        if (mc.LeftButton.State == UpDownState.Up && mc.LeftButton.LastState == UpDownState.Down)
//        {
//            ball.SettingLaunchAngle = false;
//            //ball.Launch();
//            ball.Launch(ball.AngleBetweenPoints(mc.Position), mc.Position);
//        }
//    }
//    public void Update()
//    {
//        ProcessGameControls();
//        //circle01.Update();
//        ball.Update();
//        DrawToBackBuffer();
//        gameControl.UpdateKeyboardControlLastState();
//        gameControl.UpdateMouseControlLastState();
//    }
//    private void DrawToBackBuffer()
//    {
//        bbg.Clear(Color.Aqua);
//        ball.Draw(bbg);

//        DrawDebugText(new PointF(10, 10));
//    }
//    public void Draw(Graphics g)
//    {
//        g.DrawImage(backBuffer, 0, 0);
//    }

//    #region text drawing parts
//    Font font = new Font("Arial", 10, FontStyle.Regular);
//    private void DrawText(string text, PointF position)
//    {
//        bbg.DrawString(text, font, Brushes.Black, position);
//    }
//    private void DrawDebugText(PointF position)
//    {
//        PointF textPos = position;
//        PointF nextTextPosition;
//        nextTextPosition = DrawKeyboardDebugText(textPos);
//        DrawMouseDebugText(nextTextPosition);
//        //DrawCircleInfo(new PointF(300, 20));
//        DrawBallInfo(new PointF(300, 20));
//    }
//    private PointF DrawKeyboardDebugText(PointF position)
//    {
//        KeyboardControl kbc;

//        string text;
//        float textHeight;
//        for (int i = 0; i < gameControl.KeyboardControlList.Count; i++)
//        {

//            kbc = gameControl.KeyboardControlList[i];
//            text = kbc.Key.ToString() + ": ";
//            text += kbc.State.ToString();
//            textHeight = bbg.MeasureString(text, font).Height;
//            DrawText(text, position);
//            position.Y += textHeight;
//        }
//        return position;
//    }
//    private void DrawMouseDebugText(PointF position)
//    {

//        MouseControls mc = gameControl.MControls;
//        string mousePosition = "Mouse Position: " + mc.Position.ToString();
//        float textHeight = bbg.MeasureString(mousePosition, font).Height;
//        DrawText(mousePosition, position);
//        position.Y += textHeight;

//        string leftButtonStatus = "Left Button: " + mc.LeftButton.State.ToString();
//        string leftButtonDownPos = "Down: " + mc.LeftButton.DownPosition.ToString();
//        string leftButtonUpPos = "Up: " + mc.LeftButton.UpPosition.ToString();
//        DrawText(leftButtonStatus, position);
//        position.Y += textHeight;
//        DrawText(leftButtonDownPos, position);
//        position.Y += textHeight;
//        DrawText(leftButtonUpPos, position);
//        position.Y += textHeight;


//        string rightButtonStatus = "Right Button: " + mc.RightButton.State.ToString();
//        string rightButtonDownPos = "Down: " + mc.RightButton.DownPosition.ToString();
//        string rightButtonUpPos = "Up: " + mc.RightButton.UpPosition.ToString();
//        DrawText(rightButtonStatus, position);
//        position.Y += textHeight;
//        DrawText(rightButtonDownPos, position);
//        position.Y += textHeight;
//        DrawText(rightButtonUpPos, position);
//        position.Y += textHeight;



//    }
//    //private void DrawCircleInfo(PointF position)
//    //{
//    //    DrawText("Circle Pos: " + circle01.Position.ToString(), position);
//    //    position.Y += 20;
//    //    DrawText("Circle angle (Deg: " + circle01.Degrees.ToString() + ") (Rad: " + circle01.Radians.ToString() + ")", position);
//    //}
//    private void DrawBallInfo(PointF position)
//    {
//        DrawText("Circle Pos: " + ball.Position.ToString(), position);
//        position.Y += 20;
//        DrawText("Circle angle (Deg: " + ball.Degrees.ToString() + ") (Rad: " + ball.Radians.ToString() + ")", position);
//        position.Y += 20;
//        DrawText("Setting Launch Angle: " + ball.SettingLaunchAngle.ToString(), position);
//        position.Y += 20;
//        DrawText("IsLaunched: " + ball.IsLaunched.ToString(), position);
//        position.Y += 20;
//        DrawText("Launch Angle: " + ball.LaunchAngle.ToString(), position);
//        position.Y += 20;
//        DrawText("Launch Angle: " + ball.AngleToMouse.ToString(), position);
//        position.Y += 20;
//        DrawText("Reset ball: " + ball.ResetBall.ToString(), position);
//    }
//    #endregion 
//}
#endregion
#endregion
