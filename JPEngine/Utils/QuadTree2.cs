//using System.Collections.Generic;
//using Microsoft.Xna.Framework;

//namespace JPEngine.Utils
//{
//    //TODO: Does not support a rectangle in multiple quad (I think? need testing)

//    //Look at this implementation
//    // source: https://gist.github.com/ismyhc/4747262

//    public class QuadTree2
//    {

//        private int MAX_OBJECTS = 1;
//        private int MAX_LEVELS = 3;

//        private int level;
//        private List<Rectangle> objects;
//        private Rectangle bounds;
//        private QuadTree2[] nodes;

//        public QuadTree2(int pLevel, Rectangle pBounds)
//        {
//            level = pLevel;
//            objects = new List<Rectangle>();
//            bounds = pBounds;
//            nodes = new QuadTree2[4];
//            //DebugLineDraw.drawRectHitBox(pBounds, Color.cyan);
//        }

//        // Clear quadtree
//        public void Clear()
//        {
//            objects.Clear();

//            for (int i = 0; i < nodes.Length; i++)
//            {
//                if (nodes[i] != null)
//                {
//                    nodes[i].Clear();
//                    nodes[i] = null;
//                }
//            }
//        }

//        // Split the node into 4 subnodes
//        private void Split()
//        {
//            int subWidth = (int)(bounds.Width / 2);
//            int subHeight = (int)(bounds.Height / 2);
//            int x = (int)bounds.X;
//            int y = (int)bounds.Y;

//            nodes[0] = new QuadTree2(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
//            nodes[1] = new QuadTree2(level + 1, new Rectangle(x, y, subWidth, subHeight));
//            nodes[2] = new QuadTree2(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
//            nodes[3] = new QuadTree2(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
//        }

//        private List<int> GetIndexes(Rectangle pRect)
//        {

//            List<int> indexes = new List<int>();

//            double verticalMidpoint = bounds.X + (bounds.Width / 2);
//            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

//            bool topQuadrant = pRect.Y >= horizontalMidpoint;
//            bool bottomQuadrant = (pRect.Y - pRect.Height) <= horizontalMidpoint;
//            bool topAndBottomQuadrant = pRect.Y + pRect.Height + 1 >= horizontalMidpoint && pRect.Y + 1 <= horizontalMidpoint;

//            if (topAndBottomQuadrant)
//            {
//                topQuadrant = false;
//                bottomQuadrant = false;
//            }

//            // Check if object is in left and right quad
//            if (pRect.X + pRect.Width + 1 >= verticalMidpoint && pRect.X - 1 <= verticalMidpoint)
//            {
//                if (topQuadrant)
//                {
//                    indexes.Add(2);
//                    indexes.Add(3);
//                }
//                else if (bottomQuadrant)
//                {
//                    indexes.Add(0);
//                    indexes.Add(1);
//                }
//                else if (topAndBottomQuadrant)
//                {
//                    indexes.Add(0);
//                    indexes.Add(1);
//                    indexes.Add(2);
//                    indexes.Add(3);
//                }
//            }

//            // Check if object is in just right quad
//            else if (pRect.X + 1 >= verticalMidpoint)
//            {
//                if (topQuadrant)
//                {
//                    indexes.Add(3);
//                }
//                else if (bottomQuadrant)
//                {
//                    indexes.Add(0);
//                }
//                else if (topAndBottomQuadrant)
//                {
//                    indexes.Add(3);
//                    indexes.Add(0);
//                }
//            }
//            // Check if object is in just left quad
//            else if (pRect.X - pRect.Width <= verticalMidpoint)
//            {
//                if (topQuadrant)
//                {
//                    indexes.Add(2);
//                }
//                else if (bottomQuadrant)
//                {
//                    indexes.Add(1);
//                }
//                else if (topAndBottomQuadrant)
//                {
//                    indexes.Add(2);
//                    indexes.Add(1);
//                }
//            }
//            else
//            {
//                indexes.Add(-1);
//            }

//            return indexes;
//        }

//        public void Insert(Rectangle sprite)
//        {
//            Rectangle fSprite = sprite;
//            Rectangle pRect = fSprite.GetTextureRectRelativeToContainer();

//            if (nodes[0] != null)
//            {
//                List<int> indexes = GetIndexes(pRect);
//                for (int ii = 0; ii < indexes.Count; ii++)
//                {
//                    int index = indexes[ii];
//                    if (index != -1)
//                    {
//                        nodes[index].Insert(fSprite);
//                        return;
//                    }
//                }

//            }

//            objects.Add(fSprite);

//            if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
//            {
//                if (nodes[0] == null)
//                {
//                    Split();
//                }

//                int i = 0;
//                while (i < objects.Count)
//                {
//                    Rectangle squareOne = objects[i];
//                    Rectangle oRect = squareOne.GetTextureRectRelativeToContainer();
//                    List<int> indexes = GetIndexes(oRect);
//                    for (int ii = 0; ii < indexes.Count; ii++)
//                    {
//                        int index = indexes[ii];
//                        if (index != -1)
//                        {
//                            nodes[index].Insert(squareOne);
//                            objects.Remove(squareOne);
//                        }
//                        else
//                        {
//                            i++;
//                        }
//                    }
//                }
//            }
//        }

//        public List<Rectangle> Get(List<Rectangle> fSpriteList, Rectangle pRect)
//        {
//            return Retrieve(fSpriteList, pRect);
//        }

//        private List<Rectangle> Retrieve(List<Rectangle> fSpriteList, Rectangle pRect)
//        {
//            List<int> indexes = GetIndexes(pRect);
//            for (int ii = 0; ii < indexes.Count; ii++)
//            {
//                int index = indexes[ii];
//                if (index != -1 && nodes[0] != null)
//                {
//                    nodes[index].Retrieve(fSpriteList, pRect);
//                }

//                fSpriteList.AddRange(objects);
//            }

//            return fSpriteList;
//        }
//    }
//}