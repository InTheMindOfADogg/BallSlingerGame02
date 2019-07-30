using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using BallSlingerGame02NewLoop.GameControlCollection;

using BallSlingerGame02NewLoop.CollisionPartsFolder;

public enum Shape
{
    Pixel,
    Ball,
    Circle,
    Block,
    Rect
}

namespace BallSlingerGame02NewLoop.BasicDrawableObjects
{
    using static HelperFunctionsFolder.GeneralFunctionsv1;
    using static ArrayHelperFunctions;
    class BasicObjectController
    {
        //Level01 lvl1;
        Level02 lvl2;
        float screenWidth;
        float screenHeight;

        public BasicObjectController(float screenWidth, float screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            //lvl1 = new Level01(new SizeF(screenWidth, screenHeight));
            //lvl1.Load();

            lvl2 = new Level02(new SizeF(screenWidth, screenHeight));
            lvl2.Load();
        } // end of constructor BasicObjectController(float screenWidth, float screenHeight)
        #region building walls region

        //private void LoadTestWall()
        //{
        //    int blockWidth = 50;
        //    int blockHeight = 50;
        //    int wallLength = 500;
        //    int blockCount = wallLength / blockWidth;
        //    int startX = 300;
        //    int startY = 75;
        //    int x = startX;
        //    int y = startY;
        //    for (int i = 0; i < blockCount; i++)
        //    {
        //        Blockv0d1 r = new Blockv0d1();
        //        r.Load(new PointF(x, y), new SizeF(blockWidth, blockHeight));
        //        //blockList.Add(r);
        //        blockList2.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //        x += blockWidth;
        //    }
        //} // end of function LoadTestWall()
        //private void LoadWalls()
        //{
        //    int blockWidth = 50;
        //    int blockHeight = 50;
        //    //int blocksWide = (int)screenWidth / blockWidth;
        //    //int blocksHigh = (int)screenHeight / blockHeight;
        //    int blocksWide = (int)screenWidth / blockWidth;
        //    int blocksHigh = (int)screenHeight / blockHeight;
        //    float x, y;
        //    #region north wall
        //    x = y = 0;
        //    for (int i = 0; i < blocksWide; i++)
        //    {
        //        Blockv0d1 r = new Blockv0d1(new PointF(x, y), new SizeF(blockWidth, blockHeight));
        //        blockList2.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //        x += blockWidth;
        //    }
        //    #endregion
        //    #region test region 1
        //    x = blockWidth * 2;
        //    y = blockHeight * 2;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Blockv0d1 r = new Blockv0d1(new PointF(x, y), new SizeF(blockWidth, blockHeight));
        //        //blockList.Add(r);
        //        blockList2.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //        x += blockWidth;
        //    }
        //    x = blockWidth * 2;
        //    y = blockHeight * 8;
        //    for (int i = 0; i < 7; i++)
        //    {
        //        Blockv0d1 r = new Blockv0d1(new PointF(x, y), new SizeF(blockWidth, blockHeight));
        //        //blockList.Add(r);
        //        blockList2.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //        x += blockWidth;
        //    }
        //    #endregion test region 1

        //    #region south wall
        //    x = 0;
        //    y = screenHeight - blockHeight;
        //    for (int i = 0; i < blocksWide; i++)
        //    {
        //        Blockv0d1 r = new Blockv0d1(new PointF(x, y), new SizeF(blockWidth, blockHeight));
        //        //blockList.Add(r);
        //        blockList2.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //        x += blockWidth;
        //    }
        //    #endregion

        //    #region west wall
        //    x = y = 0;
        //    for (int i = 0; i < blocksHigh; i++)
        //    {
        //        Blockv0d1 r = new Blockv0d1(new PointF(x, y), new SizeF(blockWidth, blockHeight));
        //        //blockList.Add(r);
        //        blockList2.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //        y += blockHeight;
        //    }
        //    #endregion

        //    #region east wall
        //    x = screenWidth - blockWidth;
        //    y = 0;
        //    for (int i = 0; i < blocksHigh; i++)
        //    {
        //        Blockv0d1 r = new Blockv0d1(new PointF(x, y), new SizeF(blockWidth, blockHeight));
        //        //blockList.Add(r);
        //        blockList2.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //        y += blockHeight;
        //    }
        //    #endregion
        //} // end of function LoadWalls()
        //private void LoadTestBlocks()
        //{
        //    int blockWidth = 50;
        //    int blockHeight = 50;

        //    Blockv0d1 r = new Blockv0d1(new PointF(300, 600), new SizeF(blockWidth, blockHeight));
        //    blockList2.Add(new Blockv0d2(new PointF(300, 600), new SizeF(blockWidth, blockHeight)));
        //    //blockList.Add(r);

        //    r = new Blockv0d1(new PointF(600, 200), new SizeF(blockWidth, blockHeight));
        //    blockList2.Add(new Blockv0d2(new PointF(600, 200), new SizeF(blockWidth, blockHeight)));
        //    //blockList.Add(r);

        //    r = new Blockv0d1(new PointF(400, 200), new SizeF(blockWidth, blockHeight));
        //    blockList2.Add(new Blockv0d2(new PointF(400, 200), new SizeF(blockWidth, blockHeight)));
        //    //blockList.Add(r);

        //    r = new Blockv0d1(new PointF(200, 200), new SizeF(blockWidth, blockHeight));
        //    blockList2.Add(new Blockv0d2(new PointF(200, 200), new SizeF(blockWidth, blockHeight)));
        //    //blockList.Add(r);

        //    r = new Blockv0d1(new PointF(200, 300), new SizeF(blockWidth, blockHeight));
        //    blockList2.Add(new Blockv0d2(new PointF(200, 300), new SizeF(blockWidth, blockHeight)));
        //    //blockList.Add(r);
        //} // end of function LoadTestBlocks()
        #endregion building walls region

        public void ProcessGameControls(MouseControls mc)
        {
            //HandleMouseControls(mc);
            //lvl1.HandleMouseControls(mc);
            lvl2.HandleMouseControls(mc);

        } // end of function ProcessGameControls(MouseControls mc)
        protected void HandleMouseControls(MouseControls mc)
        {
            //if (mc.LeftButton.State == UpDownState.Down && mc.LeftButton.LastState == UpDownState.Down)
            //{
            //    if (ball.IsLaunched == false)
            //    {
            //        ball.SetAngle(mc.Position);
            //    }
            //}
            //if (mc.LeftButton.State == UpDownState.Up && mc.LeftButton.LastState == UpDownState.Down)
            //{
            //    if (ball.IsLaunched == false)
            //    {
            //        ball.IsLaunched = true;
            //    }
            //    else
            //    {
            //        ball.IsLaunched = false;
            //    }
            //}
            //if (mc.RightButton.State == UpDownState.Down && mc.RightButton.LastState == UpDownState.Down)
            //{
            //    ball.Reset();
            //}
        } // end of function HandleMouseControls(MouseControls mc)

        Timer gameTimer = new Timer();
        public virtual void Update()
        {
            //lvl1.Update(gameTimer);
            lvl2.Update(gameTimer);
        } // end of function Update()

        public virtual void Draw(Graphics g)
        {
            //lvl1.Draw(g);
            lvl2.Draw(g);
        } // end of function Draw(Graphics g)
    } // end of class BasicObjectController

    public class Level01
    {
        SizeF gameSize = new SizeF();
        Ballv6d2d2 ball = new Ballv6d2d2();
        List<Blockv0d2> blockList = new List<Blockv0d2>();
        int blockWidth = 50;
        int blockHeight = 50;
        int maxBlocksWide;
        int minBlockX = 0;

        bool gameOver = false;
        int round = 1;
        int highestRound = 1;

        public Level01(SizeF GameSize)
        {
            gameSize = GameSize;
        }
        public void Load()
        {
            LocalLoadAndReset();

        }
        public void Reset()
        {
            blockList.Clear();
            LocalLoadAndReset();
        }
        private void LoadBallStartingPosition()
        {
            PointF ballStartingPos = new PointF();
            ballStartingPos.X = gameSize.Width / 2 - ball.radius;
            ballStartingPos.Y = gameSize.Height - ball.radius * 1.5f;
            ball.Load(ballStartingPos, (int)gameSize.Width, (int)gameSize.Height);
        }
        private void LocalLoadAndReset()
        {
            LoadBallStartingPosition();

            round = 1;
            //maxBlocksWide = (int)gameSize.Width / blockWidth;
            maxBlocksWide = 5;
            minBlockX = (int)(gameSize.Width / 2 - (maxBlocksWide * blockWidth) / 2);

            ball.minBallBoundsX = minBlockX;
            ball.maxBallBoundsX = minBlockX + maxBlocksWide * blockWidth;
            AddBlocksForRound(round);
        }
        //private void AddBlocks()
        //{
        //    if (blockList.Count > 0)
        //    {
        //        for (int i = 0; i < blockList.Count; i++)
        //        {
        //            blockList[i].position.Y += blockHeight;
        //        }
        //    }
        //    int y = 0;
        //    int x = 0;
        //    int numOfBlocks = r.Next(1, maxBlocksWide);
        //    int[] blockSpaces = GetRandomNumbersNoRepeats(numOfBlocks, 0, maxBlocksWide, true);
        //    for (int i = 0; i < blockSpaces.Length; i++)
        //    {
        //        x = blockSpaces[i] * blockWidth;
        //        blockList.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //    }
        //}
        private void AddBlocksForRound(int round)
        {
            if (blockList.Count > 0)
            {
                for (int i = 0; i < blockList.Count; i++)
                {
                    blockList[i].position.Y += blockHeight;
                }
            }
            int y = 0;
            int x = 0;

            int numOfBlocks = r.Next(1, maxBlocksWide);
            int[] blockSpaces = GetRandomNumbersNoRepeats(numOfBlocks, 0, maxBlocksWide, true);
            for (int i = 0; i < blockSpaces.Length; i++)
            {
                x = (int)ball.minBallBoundsX + blockSpaces[i] * blockWidth;
                AddBlock(round, new PointF(x, y));
            }
        }
        private void AddBlock(int round, PointF pos)
        {
            blockList.Add(new Blockv0d2(pos, new SizeF(blockWidth, blockHeight)));
            blockList[blockList.Count - 1].hitsToBreak = round;
        }

        public void HandleMouseControls(MouseControls mc)
        {
            if (mc.LeftButton.State == UpDownState.Down && mc.LeftButton.LastState == UpDownState.Down)
            {
                if (ball.IsLaunched == false)
                {
                    ball.SetAngle(mc.Position);
                }
            }
            if (mc.LeftButton.State == UpDownState.Up && mc.LeftButton.LastState == UpDownState.Down)
            {
                if (ball.IsLaunched == false)
                {
                    ball.IsLaunched = true;
                }
                else
                {
                    ball.IsLaunched = false;
                }
            }
            if (mc.RightButton.State == UpDownState.Down && mc.RightButton.LastState == UpDownState.Down)
            {
                ball.Reset();
            }
        }

        public void Update(Timer gameTimer)
        {
            if (gameOver == false)
            {
                for (int i = 0; i < blockList.Count; i++)
                {
                    if (blockList[i].visible == false)
                    {
                        blockList.RemoveAt(i);
                    }
                    else
                    {
                        blockList[i].Update();
                        if (blockList[i].position.Y + blockList[i].size.Height > gameSize.Height - blockHeight * 4)
                            gameOver = true;
                    }
                }
                ball.Update(ref blockList);
            }
            if (ball.roundOver)
            {
                round++;
                if (highestRound < round)
                    highestRound = round;
                AddBlocksForRound(round);
                ball.roundOver = false;
            }
            if (gameOver)
            {
                gameOver = false;
                Reset();
            }
        }
        public void RemoveBrokenBlocks()
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                if (blockList[i].visible == false)
                {
                    blockList.RemoveAt(i);
                }
            }
        }
        public void Draw(Graphics g)
        {
            ball.Draw(g);
            for (int i = 0; i < blockList.Count; i++)
            {
                blockList[i].Draw(g);
            }
            DrawRoundText(g);
        }
        private void DrawRoundText(Graphics g)
        {
            Font f = new Font("Arial", 14, FontStyle.Bold);
            PointF textPos = new PointF();

            string s = "Round: " + round.ToString() + "(Highest: " + highestRound.ToString() + ")";
            SizeF strSize = g.MeasureString(s, f);
            textPos.X = gameSize.Width / 2 - strSize.Width / 2;
            textPos.Y = strSize.Height / 2 + 20;
            g.DrawString(s, f, Brushes.Black, textPos);
        }
    } // end of class Level01

    public class Level02
    {
        SizeF gameSize = new SizeF();
        Ballv6d2d2 ball = new Ballv6d2d2();
        BlockController bc;
        int blockWidth = 50;
        int blockHeight = 50;
        int maxBlocksWide;
        int minBlockX = 0;

        bool gameOver = false;
        int round = 1;
        int highestRound = 1;

        public Level02(SizeF GameSize)
        {
            bc = new BlockController(GameSize);
            gameSize = GameSize;
        }
        public void Load()
        {
            LocalLoadAndReset();
        }
        public void Reset()
        {
            LocalLoadAndReset();
        }
        private void LoadBallStartingPosition()
        {
            PointF ballStartingPos = new PointF();
            ballStartingPos.X = gameSize.Width / 2 - ball.radius;
            ballStartingPos.Y = gameSize.Height - ball.radius * 1.5f;
            ball.Load(ballStartingPos, (int)gameSize.Width, (int)gameSize.Height);

        }
        private void LocalLoadAndReset()
        {
            LoadBallStartingPosition();
            round = 1;
            maxBlocksWide = 5;
            minBlockX = (int)(gameSize.Width / 2 - (maxBlocksWide * blockWidth) / 2);

            ball.minBallBoundsX = minBlockX;
            ball.maxBallBoundsX = minBlockX + maxBlocksWide * blockWidth;
            bc.Load(maxBlocksWide, 0, (int)gameSize.Height);
            bc.AddBlocksForRound(round);
        }
        //private void AddBlocks()
        //{
        //    if (blockList.Count > 0)
        //    {
        //        for (int i = 0; i < blockList.Count; i++)
        //        {
        //            blockList[i].position.Y += blockHeight;
        //        }
        //    }
        //    int y = 0;
        //    int x = 0;
        //    int numOfBlocks = r.Next(1, maxBlocksWide);
        //    int[] blockSpaces = GetRandomNumbersNoRepeats(numOfBlocks, 0, maxBlocksWide, true);
        //    for (int i = 0; i < blockSpaces.Length; i++)
        //    {
        //        x = blockSpaces[i] * blockWidth;
        //        blockList.Add(new Blockv0d2(new PointF(x, y), new SizeF(blockWidth, blockHeight)));
        //    }
        //}
        //private void AddBlocksForRound(int round)
        //{
        //    if (blockList.Count > 0)
        //    {
        //        for (int i = 0; i < blockList.Count; i++)
        //        {
        //            blockList[i].position.Y += blockHeight;
        //        }
        //    }
        //    int y = 0;
        //    int x = 0;

        //    int numOfBlocks = r.Next(1, maxBlocksWide);
        //    int[] blockSpaces = GetRandomNumbersNoRepeats(numOfBlocks, 0, maxBlocksWide, true);
        //    for (int i = 0; i < blockSpaces.Length; i++)
        //    {
        //        x = (int)ball.minBallBoundsX + blockSpaces[i] * blockWidth;
        //        AddBlock(round, new PointF(x, y));
        //    }
        //}
        //private void AddBlock(int round, PointF pos)
        //{
        //    blockList.Add(new Blockv0d2(pos, new SizeF(blockWidth, blockHeight)));
        //    blockList[blockList.Count - 1].hitsToBreak = round;
        //}

        public void HandleMouseControls(MouseControls mc)
        {
            if (mc.LeftButton.State == UpDownState.Down && mc.LeftButton.LastState == UpDownState.Down)
            {
                if (ball.IsLaunched == false)
                {
                    ball.SetAngle(mc.Position);
                }
            }
            if (mc.LeftButton.State == UpDownState.Up && mc.LeftButton.LastState == UpDownState.Down)
            {
                if (ball.IsLaunched == false)
                {
                    ball.IsLaunched = true;
                }
                else
                {
                    ball.IsLaunched = false;
                }
            }
            if (mc.RightButton.State == UpDownState.Down && mc.RightButton.LastState == UpDownState.Down)
            {
                ball.Reset();
            }
        }

        public void Update(Timer gameTimer)
        {
            if (gameOver == false)
            {
                bc.Update(gameTimer);
                ball.Update(ref bc.blockList);
            }
            if (ball.roundOver)
            {
                gameOver = CheckGameOver(bc.blockList);     // added 9/15/2017
                if(!gameOver)
                {
                    round++;
                    if (highestRound < round)
                        highestRound = round;
                    bc.AddBlocksForRound(round);
                    ball.roundOver = false;
                }
                
            }
            if (gameOver)
            {
                gameOver = false;
                Reset();
            }
        } // end of function Update(Timer gameTimer)
        private bool CheckGameOver(List<Blockv0d2> blist)
        {
            bool gameOver = false;
            for(int i = 0; i < blist.Count; i++)
            {
                Blockv0d2 b = blist[i];
                if(b.position.Y > gameSize.Height - (b.size.Height * 4))
                {
                    gameOver = true;
                }
            }
            return gameOver;

        }
        public void Draw(Graphics g)
        {
            ball.Draw(g);
            bc.Draw(g);
            //for (int i = 0; i < blockList.Count; i++)
            //{
            //    blockList[i].Draw(g);
            //}
            DrawRoundText(g);
        }
        private void DrawRoundText(Graphics g)
        {
            Font f = new Font("Arial", 14, FontStyle.Bold);
            PointF textPos = new PointF();

            string s = "Round: " + round.ToString() + "(Highest: " + highestRound.ToString() + ")";
            SizeF strSize = g.MeasureString(s, f);
            textPos.X = gameSize.Width / 2 - strSize.Width / 2;
            textPos.Y = strSize.Height / 2 + 20;
            g.DrawString(s, f, Brushes.Black, textPos);
        }
    } // end of class Level02
    public static class ArrayHelperFunctions
    {
        public static Random r = new Random();
        public static int[] GetRandomNumbers(int numberOfRandom, int start, int totalElements, bool sortArray = false)
        {
            if (start > totalElements)
            {
                int temp = start;
                start = totalElements;
                totalElements = temp;
            }
            int[] randoms = new int[numberOfRandom];
            for (int i = 0; i < randoms.Length; i++)
            {
                randoms[i] = r.Next(start, totalElements);
            }
            if (sortArray)
                Array.Sort(randoms);
            return randoms;
        }
        public static int[] GetRandomNumbersNoRepeats(int numberOfRandom, int start, int totalElements, bool sortArray = false)
        {

            if (start > totalElements)
            {
                int temp2 = start;
                start = totalElements;
                totalElements = temp2;
            }

            int temp = 0;
            int[] randoms = new int[numberOfRandom];
            SetArrayToValue(randoms, -1);
            if (numberOfRandom == totalElements)
            {
                for (int i = 0; i < randoms.Length; i++)
                {
                    randoms[i] = start + i;
                }
                return randoms;
            }
            else if (numberOfRandom <= totalElements - start)
            {
                for (int i = 0; i < randoms.Length; i++)
                {
                    temp = r.Next(start, totalElements);
                    while (NumberFound(randoms, temp))
                    {
                        temp = r.Next(start, totalElements);
                    }
                    randoms[i] = temp;
                }
                if (sortArray)
                    Array.Sort(randoms);
                return randoms;
            }
            else
            {
                randoms = GetRandomNumbers(numberOfRandom, start, totalElements, sortArray);
                return randoms;
            }
            #region working, no error checking though
            //int[] randoms = new int[numberOfRandom];
            //int temp = -1;
            //for (int i = 0; i < numberOfRandom; i++)
            //{
            //    temp = r.Next(min, max);
            //    while (NumberFound(randoms, temp))
            //    {
            //        temp = r.Next(min, max);
            //    }
            //    randoms[i] = temp;
            //}
            //if (sortArray)
            //    Array.Sort(randoms);
            //return randoms;
            #endregion
        }
        public static int[] GetRandomNumbersTrueRange(int numberOfRandom, int minRandVal, int maxRandVal, bool sortArray = false)
        {
            if (minRandVal > maxRandVal)
            {
                int temp2 = minRandVal;
                minRandVal = maxRandVal;
                maxRandVal = temp2;
            }
            int maxVal = maxRandVal + minRandVal - 1;
            int[] randoms = new int[numberOfRandom];
            for (int i = 0; i < randoms.Length; i++)
            {
                randoms[i] = r.Next(minRandVal, maxVal);
            }
            if (sortArray)
                Array.Sort(randoms);
            return randoms;
        }

        public static bool NumberFound(int[] arr, int num)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == num)
                    return true;
            }
            return false;
        }
        public static bool NumberFound(int[] arr, int num, out int indexOfRepeat)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == num)
                {
                    indexOfRepeat = i;
                    return true;
                }
            }
            indexOfRepeat = -1;
            return false;
        }
        public static bool NumberFoundInRange(int[] arr, int num, int startRead, int endRead)
        {
            int end = arr.Length;
            if (endRead < end)
                end = endRead;
            for (int i = startRead; i < end; i++)
            {
                if (arr[i] == num)
                    return true;
            }
            return false;
        }
        public static void SetArrayToValue(int[] arr, int val)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = val;
        }


    } // end of public static class ArrayHelperFunctions

    public class BlockController
    {
        SizeF gameSize = new SizeF();
        public List<Blockv0d2> blockList = new List<Blockv0d2>();
        public PointF lowestBlockPos;

        int blockWidth = 50;
        int blockHeight = 50;
        int maxBlocksWide;
        int minBlockX = 0;
        int maxBlockX = 0;
        int minBlockY = 0;
        int maxBlockY = 0;
        public BlockController(SizeF GameSize)
        {
            gameSize = GameSize;
        }
        public void Load(int minBlockX, int maxBlockX, int minBlockY, int maxBlockY)
        {
            this.minBlockX = minBlockX;
            this.maxBlockX = maxBlockX;
            this.minBlockY = minBlockY;
            this.maxBlockY = maxBlockY;
            maxBlocksWide = (maxBlockX - minBlockX) / blockWidth;
            LocalLoadAndReset();
        }
        public void Load(int maxBlocksWide, int minBlockY, int maxBlockY)
        {
            this.maxBlocksWide = maxBlocksWide;
            minBlockX = (int)gameSize.Width / 2 - (maxBlocksWide * blockWidth / 2);
            this.minBlockY = minBlockY;
            this.maxBlockY = maxBlockY;
            LocalLoadAndReset();
        }
        public void Reset()
        {
            LocalLoadAndReset();
        }
        private void LocalLoadAndReset()
        {
            blockList.Clear();
            //maxBlocksWide = 5;
            //minBlockX = (int)(gameSize.Width / 2 - (maxBlocksWide * blockWidth) / 2);
        }
        
        public void Update(Timer gameTimer)
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                if (blockList[i].visible == false)
                {
                    blockList.RemoveAt(i);
                }
                else
                    blockList[i].Update();
            }
        } // end of function Update(Timer gameTimer)
        public void Draw(Graphics g)
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                blockList[i].Draw(g);
            }
        }

        public void AddBlock(int hitsToBreak, PointF pos)
        {
            blockList.Add(new Blockv0d2(pos, new SizeF(blockWidth, blockHeight)));
            blockList[blockList.Count - 1].hitsToBreak = hitsToBreak;
        }

        public void AddBlocksForRound(int hitsToBreak)
        {
            if (blockList.Count > 0)
            {
                for (int i = 0; i < blockList.Count; i++)
                {
                    blockList[i].position.Y += blockHeight;
                }
            }
            int y = 0;
            int x = 0;

            int numOfBlocks = r.Next(1, maxBlocksWide);
            int[] blockSpaces = GetRandomNumbersNoRepeats(numOfBlocks, 0, maxBlocksWide, true);
            for (int i = 0; i < blockSpaces.Length; i++)
            {
                x = minBlockX + blockSpaces[i] * blockWidth;
                AddBlock(hitsToBreak, new PointF(x, y));
            }
        } // end of function AddBlocksForRound(int round)

        #region older updates
        #region original update
        //public void Update(Timer gameTimer, ref Ballv6d2d2 ball)
        //{
        //    if (gameOver == false)
        //    {
        //        for (int i = 0; i < blockList.Count; i++)
        //        {
        //            if (blockList[i].visible == false)
        //            {
        //                blockList.RemoveAt(i);
        //            }
        //            else
        //            {
        //                blockList[i].Update();
        //                if (blockList[i].position.Y + blockList[i].size.Height > gameSize.Height - blockHeight * 4)
        //                    gameOver = true;
        //            }
        //        }
        //        ball.Update(ref blockList);
        //    }
        //    if (ball.roundOver)
        //    {
        //        round++;
        //        if (highestRound < round)
        //            highestRound = round;
        //        AddBlocksForRound(round);
        //        ball.roundOver = false;
        //    }
        //    if (gameOver)
        //    {
        //        gameOver = false;
        //        Reset();
        //    }
        //}
        #endregion original update
        //public void Update(Timer gameTimer)
        //{
        //    for (int i = 0; i < blockList.Count; i++)
        //    {
        //        if (blockList[i].visible == false)
        //        {
        //            blockList.RemoveAt(i);
        //        }
        //        else
        //        {
        //            blockList[i].Update();
        //            if (blockList[i].position.Y + blockList[i].size.Height > gameSize.Height - blockHeight * 4)
        //                gameOver = true;
        //        }
        //    }
        //} // end of function Update(Timer gameTimer)
        #endregion older updates
    } // end of class BlockController



}


#region old code
//class TempRect
//{
//    public PointF position = new PointF();
//    public PointF center = new PointF();
//    public SizeF size = new SizeF();
//    List<PointF> testPoints = new List<PointF>();

//    public TempRect(PointF pos, SizeF size)
//    {
//        this.position = pos;
//        this.size = size;
//        center.X = position.X + size.Width / 2;
//        center.Y = position.Y + size.Height / 2;
//        //SetTestPoints();

//    } // end constructor TempRect(PointF pos, SizeF size)
//    float angleToBall = 0;
//    float distanceToBall = 0;
//    PointF bCenter = new PointF();
//    PointF rtPoint = new PointF();
//    float hyp = 0;
//    float adj = 0;
//    float opp = 0;
//    //public void CalculateAngleToBall(PointF ballCenter)
//    //{
//    //    this.bCenter = ballCenter;
//    //    distanceToBall = HelperFunctions.DistanceBetween(center, ballCenter);
//    //    rtPoint.X = center.X;
//    //    rtPoint.Y = bCenter.Y;
//    //    hyp = distanceToBall;
//    //    opp = HelperFunctions.DistanceBetween(center, rtPoint);
//    //    adj = HelperFunctions.DistanceBetween(bCenter, rtPoint);
//    //    angleToBall = (float)Math.Atan((opp / adj));

//    //    if (bCenter.X < center.X && bCenter.Y > center.Y)
//    //    {
//    //        angleToBall = (float)Math.PI - angleToBall;
//    //    }
//    //    if (bCenter.X < center.X && bCenter.Y < center.Y)
//    //    {
//    //        angleToBall = (float)Math.PI + angleToBall;
//    //    }
//    //    if (bCenter.X > center.X && bCenter.Y < center.Y)
//    //    {
//    //        angleToBall = ((2 * (float)Math.PI)) - angleToBall;
//    //    }

//    //} // end function CalculateAngleToBall(PointF ballCenter)

//    public void Draw(Graphics g)
//    {
//        PointF textPos = new PointF(500, 500);
//        g.FillRectangle(Brushes.AntiqueWhite, new RectangleF(position, size));
//        g.DrawRectangle(Pens.Black, position.X, position.Y, size.Width, size.Height);
//    } // end function Draw(Graphics g)

//    private void DrawTestPoints(Graphics g)
//    {
//        float w = 3;
//        float h = 3;
//        for (int i = 0; i < testPoints.Count; i++)
//        {
//            g.FillRectangle(Brushes.Red, testPoints[i].X - w / 2, testPoints[i].Y - h / 2, w, h);
//        }
//    } // end function DrawTestPoints(Graphics g)

//    private void DrawLineToBall(Graphics g)
//    {
//        PointF endPoint = new PointF();
//        endPoint.X = center.X + (float)Math.Cos(angleToBall) * distanceToBall;
//        endPoint.Y = center.Y + (float)Math.Sin(angleToBall) * distanceToBall;
//        g.DrawLine(Pens.Red, center, endPoint);
//        //g.DrawLine(Pens.Black, center, bCenter);
//    }
//    private void DrawTriangleWithBall(Graphics g)
//    {
//        Pen p = new Pen(Color.Chocolate);
//        g.DrawLine(p, bCenter, rtPoint);
//        g.DrawLine(p, rtPoint, center);
//    }

//    public bool PositionInRect(PointF pos)
//    {
//        if (pos.X > position.X && pos.X < position.X + size.Width
//            && pos.Y > position.Y && pos.Y < position.Y + size.Height)
//        {
//            return true;
//        }
//        return false;
//    }

//} // end of class TempRect




//public static class HelperFunctions
//{
//    static SolidBrush fontBrush = new SolidBrush(Color.Black);
//    static Font font = new Font("Arial", 15, FontStyle.Regular, GraphicsUnit.Pixel);

//    public static float DistanceBetween(PointF p1, PointF p2)
//    {
//        float d = 0;
//        float dx = p1.X - p2.X;
//        float dy = p1.Y - p2.Y;
//        float dxs = dx * dx;
//        float dys = dy * dy;
//        float dsum = dxs + dys;
//        d = (float)Math.Sqrt(dsum);
//        return d;
//    }
//    public static float AngleToPoint(Point fromPoint, PointF toPoint)
//    {
//        PointF p1 = fromPoint;
//        PointF p2 = toPoint;
//        float a = 0;
//        PointF rtAngle = new PointF(fromPoint.X, toPoint.Y);
//        float adjLen = 0;
//        float oppLen = 0;
//        adjLen = DistanceBetween(p1, rtAngle);
//        oppLen = DistanceBetween(p2, rtAngle);
//        a = (float)Math.Acos(oppLen / adjLen);
//        return a;
//    }

//    public static float DegreesToRadians(float degrees)
//    {
//        return (degrees * (float)Math.PI / 180);
//    }
//    public static float RadiansToDegrees(float radians)
//    {
//        return (radians * 180 / (float)Math.PI);
//    }
//    public static void DrawText(Graphics g, string msg, PointF position)
//    {
//        g.DrawString(msg, font, fontBrush, position);
//    }
//    public static void DrawText(Graphics g, string msg, ref PointF position)
//    {
//        g.DrawString(msg, font, fontBrush, position);
//        position.Y += g.MeasureString(msg, font).Height;
//    }
//} // end of static class HelperFunctions
#endregion

#region moved to public static class ArrayHelperFunctions 07/21/2017
//private int[] GetRandomNumbers(int numberOfRandom, int min, int max)
//{
//    int[] randoms = new int[numberOfRandom];
//    for(int i = 0; i < randoms.Length; i++)
//    {
//        randoms[i] = r.Next(min, max);
//    }
//    return randoms;
//}
//private int[] GetRandomNumbersNoRepeats(int numberOfRandom, int min, int max)
//{
//    int[] randoms = new int[numberOfRandom];
//    if (numberOfRandom > (max - min - 1))
//        randoms = GetRandomNumbers(numberOfRandom, min, max);
//    else
//    {
//        int temp = -1;
//        for (int i = 0; i < numberOfRandom; i++)
//        {
//            temp = r.Next(min, max);
//            while (NumberFound(randoms, temp))
//            {
//                temp = r.Next(min, max);
//            }
//            randoms[i] = temp;
//        }
//    }
//    Array.Sort(randoms);
//    return randoms;
//}
//private bool NumberFound(int[] arr, int num)
//{
//    for(int i = 0; i < arr.Length; i++)
//    {
//        if (arr[i] == num)
//            return true;
//    }
//    return false;
//}
//private bool NumberFound(int[] arr, int num, out int indexOfRepeat)
//{
//    for (int i = 0; i < arr.Length; i++)
//    {
//        if (arr[i] == num)
//        {
//            indexOfRepeat = i;
//            return true;
//        }
//    }
//    indexOfRepeat = -1;
//    return false;
//}
//private bool NumberFoundInRange(int[] arr, int num, int startRead, int endRead)
//{
//    int end = arr.Length;
//    if (endRead < end )
//        end = endRead;
//    for (int i = startRead; i < end; i++)
//    {
//        if (arr[i] == num)
//            return true;
//    }
//    return false;
//}
//void SetArrayToValue(int[] arr, int val)
//{
//    for (int i = 0; i < arr.Length; i++)
//        arr[i] = val;
//}
#endregion moved to public static class ArrayHelperFunctions 07/21/2017

#region previos version GetRandomNumbersNoRepeats
//public static int[] GetRandomNumbersNoRepeats(int numberOfRandom, int min, int max, bool sortArray = false)
//{
//    int range = max - min + 1;
//    if (numberOfRandom < range)
//    {
//        int[] randoms = new int[numberOfRandom];
//        int temp = -1;
//        for (int i = 0; i < randoms.Length; i++)
//        {
//            temp = r.Next(min, max);
//            while (NumberFound(randoms, temp))
//            {
//                temp = r.Next(min, max);
//            }
//            randoms[i] = temp;
//        }
//        if (sortArray)
//            Array.Sort(randoms);
//        return randoms;
//    }
//    else
//    {
//        int[] randoms = new int[numberOfRandom];
//        SetArrayToValue(randoms, -1);
//        return randoms;
//    }
//    #region working, no error checking though
//    //int[] randoms = new int[numberOfRandom];
//    //int temp = -1;
//    //for (int i = 0; i < numberOfRandom; i++)
//    //{
//    //    temp = r.Next(min, max);
//    //    while (NumberFound(randoms, temp))
//    //    {
//    //        temp = r.Next(min, max);
//    //    }
//    //    randoms[i] = temp;
//    //}
//    //if (sortArray)
//    //    Array.Sort(randoms);
//    //return randoms;
//    #endregion
//}
#endregion previos version GetRandomNumbersNoRepeats

#region older copies of GetRandomNumbersNoRepeats and GetRandomNumbersNoRepeatsTrueRange
//public static int[] GetRandomNumbersNoRepeats(int numberOfRandom, int start, int totalElemens, bool sortArray = false)
//{
//    int temp = 0;
//    int[] randoms = new int[numberOfRandom];
//    SetArrayToValue(randoms, -1);
//    //if (numberOfRandom == totalElemens)
//    //{
//    //    for(int i = 0; i < randoms.Length; i++)
//    //    {
//    //        randoms[i] = start + i;
//    //    }
//    //    return randoms;
//    //}
//    //else 
//    if (numberOfRandom < totalElemens)
//    {
//        for (int i = 0; i < randoms.Length; i++)
//        {
//            temp = r.Next(start, totalElemens);
//            while (NumberFound(randoms, temp))
//            {
//                temp = r.Next(start, totalElemens);
//            }
//            randoms[i] = temp;
//        }
//        if (sortArray)
//            Array.Sort(randoms);
//        return randoms;
//    }
//    else
//    {
//        randoms = GetRandomNumbers(numberOfRandom, start, totalElemens, sortArray);
//        return randoms;
//    }
//    #region working, no error checking though
//    //int[] randoms = new int[numberOfRandom];
//    //int temp = -1;
//    //for (int i = 0; i < numberOfRandom; i++)
//    //{
//    //    temp = r.Next(min, max);
//    //    while (NumberFound(randoms, temp))
//    //    {
//    //        temp = r.Next(min, max);
//    //    }
//    //    randoms[i] = temp;
//    //}
//    //if (sortArray)
//    //    Array.Sort(randoms);
//    //return randoms;
//    #endregion
//}
//public static int[] GetRandomNumbersNoRepeatsTrueRange(int numberOfRandom, int start, int totalElemens, bool sortArray = false)
//{
//    int maxVal = totalElemens + start - 1;
//    int temp = 0;
//    int[] randoms = new int[numberOfRandom];
//    SetArrayToValue(randoms, -1);
//    //if (numberOfRandom == totalElemens)
//    //{
//    //    for (int i = 0; i < randoms.Length; i++)
//    //    {
//    //        randoms[i] = start + i;
//    //    }
//    //    return randoms;
//    //}
//    //else 
//    if (numberOfRandom < totalElemens)
//    {
//        for (int i = 0; i < randoms.Length; i++)
//        {
//            //temp = r.Next(start, totalElemens);
//            temp = r.Next(start, maxVal);
//            while (NumberFound(randoms, temp))
//            {
//                //temp = r.Next(start, totalElemens);
//                temp = r.Next(start, maxVal);
//            }
//            randoms[i] = temp;
//        }
//        if (sortArray)
//            Array.Sort(randoms);
//        return randoms;
//    }
//    else
//    {
//        randoms = GetRandomNumbers(numberOfRandom, start, totalElemens, sortArray);
//        return randoms;
//    }
//}
#endregion