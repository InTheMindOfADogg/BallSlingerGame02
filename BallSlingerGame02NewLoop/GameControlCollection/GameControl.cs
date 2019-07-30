using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace BallSlingerGame02NewLoop.GameControlCollection
{
    public enum UpDownState
    {
        Up,
        Down
    }


    // Might MouseButtons use for a more generic form
    //struct MouseButtons
    //{
    //    public string Name;
    //    public UpDownState State;
    //    public UpDownState LastState;
    //    public PointF DownPosition;
    //    public PointF UpPosition;
    //}
    public struct LeftMouseButton
    {
        public UpDownState State;
        public UpDownState LastState;
        public PointF DownPosition;
        public PointF UpPosition;
    }
    public struct RightMouseButton
    {
        public UpDownState State;
        public UpDownState LastState;
        public PointF DownPosition;
        public PointF UpPosition;
    }
    public struct MouseControls
    {
        public PointF Position;
        public LeftMouseButton LeftButton;
        public RightMouseButton RightButton;
    }

    public enum KeyboardControlKeys
    {
        LeftArrow,
        UpArrow,
        RightArrow,
        DownArrow,
        A,
        S,
        D,
        W,
        EndKeyboardControlKey
    }

    public struct KeyboardControl
    {
        public string Name;
        public KeyboardControlKeys Key;
        public UpDownState State;
        public UpDownState LastState;
    }
    public class GameControl
    {
        MouseControls mControls = new MouseControls();
        List<KeyboardControl> kbControlList = new List<KeyboardControl>();
        public MouseControls MControls { get { return mControls; } }
        public List<KeyboardControl> KeyboardControlList { get { return kbControlList; } }
        public GameControl()
        {
            SetUpKeyboardControlList();
            SetUpMouseControls();
        }
        private void SetUpKeyboardControlList()
        {
            int keyIndex = 0;
            KeyboardControlKeys key = (KeyboardControlKeys)keyIndex;
            do
            {
                KeyboardControl kc = new KeyboardControl();
                kc.Name = key.ToString();
                kc.Key = key;
                kc.State = UpDownState.Up;
                kc.LastState = UpDownState.Up;
                kbControlList.Add(kc);
                keyIndex++;
                key = (KeyboardControlKeys)keyIndex;
            } while (key != KeyboardControlKeys.EndKeyboardControlKey);
        }
        private void SetUpMouseControls()
        {
            mControls.Position = new PointF();
            mControls.LeftButton = new LeftMouseButton();
            mControls.RightButton = new RightMouseButton();
        }

        public void UpdateMousePosition(PointF position)
        {
            mControls.Position = position;
        }
        public void UpdateButtonDown(System.Windows.Forms.MouseEventArgs me)
        {
            if (me.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mControls.LeftButton.State = UpDownState.Down;
                mControls.LeftButton.DownPosition = me.Location;
            }
            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                mControls.RightButton.State = UpDownState.Down;
                mControls.RightButton.DownPosition = me.Location;
            }
        }
        public void UpdateButtonUp(System.Windows.Forms.MouseEventArgs me)
        {
            if (me.Button == System.Windows.Forms.MouseButtons.Left )
            {
                mControls.LeftButton.State = UpDownState.Up;
                mControls.LeftButton.UpPosition = me.Location;
            }
            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                mControls.RightButton.State = UpDownState.Up;
                mControls.RightButton.UpPosition = me.Location;
            }
        }
        public void UpdateMouseControlLastState()
        {
            mControls.LeftButton.LastState = mControls.LeftButton.State;
            mControls.RightButton.LastState = mControls.RightButton.State;
        }

        //public void UpdateKeyDown(System.Windows.Forms.KeyEventArgs ke)
        //{
        //    if(ke.KeyCode == System.Windows.Forms.Keys.Left)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.LeftArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Down;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if(ke.KeyCode == System.Windows.Forms.Keys.Right)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.RightArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Down;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.Up)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.UpArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Down;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.Down)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.DownArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Down;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if(ke.KeyCode == System.Windows.Forms.Keys.A)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.A, UpDownState.Down);
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.S)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.S, UpDownState.Down);
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.D)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.D, UpDownState.Down);
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.W)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.W, UpDownState.Down);
        //    }
        //}
        //public void UpdateKeyUp(System.Windows.Forms.KeyEventArgs ke)
        //{
        //    if (ke.KeyCode == System.Windows.Forms.Keys.Left)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.LeftArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Up;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.Right)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.RightArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Up;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.Up)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.UpArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Up;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.Down)
        //    {
        //        int keyIndex = (int)KeyboardControlKeys.DownArrow;
        //        KeyboardControl kbc = kbControlList[keyIndex];
        //        kbc.State = UpDownState.Up;
        //        kbControlList[keyIndex] = kbc;
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.A)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.A, UpDownState.Up);
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.S)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.S, UpDownState.Up);
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.D)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.D, UpDownState.Up);
        //    }
        //    if (ke.KeyCode == System.Windows.Forms.Keys.W)
        //    {
        //        UpdateKbControl(KeyboardControlKeys.W, UpDownState.Up);
        //    }
        //}
        public bool ExitProgram = false;
        public void UpdateKeyDown(System.Windows.Forms.KeyEventArgs ke)
        {
            if(ke.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                ExitProgram = true;
            }
            //if (ke.KeyCode == System.Windows.Forms.Keys.A)
            //{
            //    UpdateKbControl(KeyboardControlKeys.A, UpDownState.Down);
            //}
            //if (ke.KeyCode == System.Windows.Forms.Keys.S)
            //{
            //    UpdateKbControl(KeyboardControlKeys.S, UpDownState.Down);
            //}
            //if (ke.KeyCode == System.Windows.Forms.Keys.D)
            //{
            //    UpdateKbControl(KeyboardControlKeys.D, UpDownState.Down);
            //}
            //if (ke.KeyCode == System.Windows.Forms.Keys.W)
            //{
            //    UpdateKbControl(KeyboardControlKeys.W, UpDownState.Down);
            //}
        }
        public void UpdateKeyUp(System.Windows.Forms.KeyEventArgs ke)
        {
            
            //if (ke.KeyCode == System.Windows.Forms.Keys.A)
            //{
            //    UpdateKbControl(KeyboardControlKeys.A, UpDownState.Up);
            //}
            //if (ke.KeyCode == System.Windows.Forms.Keys.S)
            //{
            //    UpdateKbControl(KeyboardControlKeys.S, UpDownState.Up);
            //}
            //if (ke.KeyCode == System.Windows.Forms.Keys.D)
            //{
            //    UpdateKbControl(KeyboardControlKeys.D, UpDownState.Up);
            //}
            //if (ke.KeyCode == System.Windows.Forms.Keys.W)
            //{
            //    UpdateKbControl(KeyboardControlKeys.W, UpDownState.Up);
            //}
        }
        private void UpdateKbControl(KeyboardControlKeys key, UpDownState state)
        {
            int keyIndex = (int)key;
            KeyboardControl kbc = kbControlList[keyIndex];
            kbc.State = state;
            kbControlList[keyIndex] = kbc;
        }
        public void UpdateKeyboardControlLastState()
        {
            for(int i = 0; i < kbControlList.Count; i++)
            {
                KeyboardControl kbc = kbControlList[i];
                kbc.LastState = kbc.State;
                kbControlList[i] = kbc;
            }
        }
        
        
    }

}
