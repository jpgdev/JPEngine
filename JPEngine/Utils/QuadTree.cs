using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace JPEngine.Utils
{
    public class QuadTree
    {
        private const int MAX_OBJECTS = 10;
        private const int MAX_LEVELS = 5;

        private readonly int _level;
        private readonly QuadTree[] _nodes;
        private readonly List<Rectangle> _objects;
        private Rectangle _bounds;

        public QuadTree(int level, Rectangle bounds)
        {
            _level = level;
            _objects = new List<Rectangle>();
            _bounds = bounds;
            _nodes = new QuadTree[4];
        }

        public void Insert(Rectangle rectangle)
        {
            if (_nodes[0] != null)
            {
                int index = GetIndex(rectangle);
                if (index != -1)
                {
                    _nodes[index].Insert(rectangle);
                    return;
                }
            }

            _objects.Add(rectangle);

            if (_objects.Count > MAX_OBJECTS && _level < MAX_LEVELS)
            {
                if(_nodes[0] == null)
                    Split();

                int i = 0;
                while (i < _objects.Count)
                {
                    int index = GetIndex(_objects[i]);
                    if (index != -1)
                    {
                        _nodes[index].Insert(_objects[i]);
                         _objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<Rectangle> GetPossibleChoices(Rectangle rectangle)
        {
            return GetPossibleChoicesInternal(new List<Rectangle>(), rectangle);
        }

        private List<Rectangle> GetPossibleChoicesInternal(List<Rectangle> objects, Rectangle rectangle)
        {
            int index = GetIndex(rectangle);
            if (index != -1 && _nodes[0] != null)
            {
                _nodes[index].GetPossibleChoicesInternal(objects, rectangle);
            }

            objects.AddRange(objects);

            return objects;
        }

        public void Clear()
        {
            _objects.Clear();

            for (int i = 0; i < _nodes.Length; i++)
            {
                if (_nodes[i] != null)
                {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int subWidth = _bounds.Width/2;
            int subHeight = _bounds.Height/2;
            int x = _bounds.X;
            int y = _bounds.Y;

            _nodes[0] = new QuadTree(_level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _nodes[1] = new QuadTree(_level + 1, new Rectangle(x, y, subWidth, subHeight));
            _nodes[2] = new QuadTree(_level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _nodes[3] = new QuadTree(_level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int GetIndex(Rectangle rectangle)
        {
            int index = -1;
            double verticalMidPoint = _bounds.X + (_bounds.Width/2);
            double horizontalMidPoint = _bounds.Y + (_bounds.Height/2);

            bool topQuadrant = (rectangle.Y < horizontalMidPoint && (rectangle.Y + rectangle.Height) < horizontalMidPoint);
            bool bottomQuadrant = (rectangle.Y > horizontalMidPoint);

            if (rectangle.X < verticalMidPoint && rectangle.X + rectangle.Width < verticalMidPoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            else if (rectangle.X > verticalMidPoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }
    }
}