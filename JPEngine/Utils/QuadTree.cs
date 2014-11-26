using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace JPEngine.Utils
{
    //TODO: Does not support a rectangle in multiple quad (I think? need testing)

    //Look at this implementation
    // source: https://gist.github.com/ismyhc/4747262
    /*
     * using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class QuadTree {
 
	private int MAX_OBJECTS = 1;
	private int MAX_LEVELS = 3;
	
	private int level;
	private List<SquareOne> objects;
	private Rect bounds;
	private QuadTree[] nodes;
 
	public QuadTree (int pLevel, Rect pBounds)
	{
		level = pLevel;
		objects = new List<SquareOne>();
		bounds = pBounds;
		nodes = new QuadTree[4];
		//DebugLineDraw.drawRectHitBox(pBounds, Color.cyan);
	}
 
	// Clear quadtree
	public void Clear()
	{
		objects.Clear();
		
		for(int i  = 0; i < nodes.Length; i++)
		{
			if(nodes[i] != null)
			{
				nodes[i].Clear();
				nodes[i] = null;
			}
		}
	}
	
	// Split the node into 4 subnodes
	private void Split()
	{
		int subWidth = (int)(bounds.width / 2);
		int subHeight = (int)(bounds.height / 2);
		int x = (int)bounds.x;
		int y = (int)bounds.y;
		
		nodes[0] = new QuadTree(level + 1, new Rect(x + subWidth, y, subWidth, subHeight));
		nodes[1] = new QuadTree(level + 1, new Rect(x, y, subWidth, subHeight));
		nodes[2] = new QuadTree(level + 1, new Rect(x, y + subHeight, subWidth, subHeight));
		nodes[3] = new QuadTree(level + 1, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));
	}
	
	private List<int> GetIndexes(Rect pRect)
	{
	
	    List<int> indexes = new List<int>();
	
	    double verticalMidpoint = bounds.x + (bounds.width / 2);
	    double horizontalMidpoint = bounds.y + (bounds.height / 2);
	
		bool topQuadrant = pRect.y >= horizontalMidpoint;
		bool bottomQuadrant = (pRect.y - pRect.height) <= horizontalMidpoint;
		bool topAndBottomQuadrant = pRect.y + pRect.height + 1 >= horizontalMidpoint && pRect.y + 1 <= horizontalMidpoint;
		
		if(topAndBottomQuadrant)
		{
			topQuadrant = false;
			bottomQuadrant = false;
		}
	
		// Check if object is in left and right quad
		if(pRect.x + pRect.width + 1 >= verticalMidpoint && pRect.x -1 <= verticalMidpoint)
		{
			if(topQuadrant)
			{
				indexes.Add(2);
				indexes.Add(3);
			}
			else if(bottomQuadrant)
			{
				indexes.Add(0);
				indexes.Add(1);
			}
			else if(topAndBottomQuadrant)
			{
				indexes.Add(0);
				indexes.Add(1);
				indexes.Add(2);
				indexes.Add(3);
			}
		}
		
		// Check if object is in just right quad
		else if(pRect.x + 1 >= verticalMidpoint)
		{
			if(topQuadrant)
			{
				indexes.Add(3);
			}
			else if(bottomQuadrant)
			{
				indexes.Add(0);
			}
			else if(topAndBottomQuadrant)
			{
				indexes.Add(3);
				indexes.Add(0);
			}
		}
		// Check if object is in just left quad
		else if(pRect.x - pRect.width <= verticalMidpoint)
		{
			if(topQuadrant)
			{
				indexes.Add(2);
			}
			else if(bottomQuadrant)
			{
				indexes.Add(1);
			}
			else if(topAndBottomQuadrant)
			{
				indexes.Add(2);
				indexes.Add(1);
			}
		}
		else
		{
			indexes.Add(-1);
		}
	
		return indexes;
	}
	
	public void Insert(SquareOne sprite)
	{
		SquareOne fSprite = sprite;
		Rect pRect = fSprite.GetTextureRectRelativeToContainer();
			
		if(nodes[0] != null)
		{
			List<int> indexes = GetIndexes(pRect);
			for(int ii = 0; ii < indexes.Count; ii++)
			{
				int index = indexes[ii];
				if(index != -1)
				{
					nodes[index].Insert(fSprite);
					return;
				}
			}
 
		}
		
		objects.Add(fSprite);
		
		if(objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
		{
			if(nodes[0] == null)
			{
				Split();
			}
			
			int i = 0;
			while(i < objects.Count)
			{
				SquareOne sqaureOne = objects[i];
				Rect oRect = sqaureOne.GetTextureRectRelativeToContainer();
				List<int> indexes = GetIndexes(oRect);
				for(int ii = 0; ii < indexes.Count; ii++)
				{
					int index = indexes[ii];
					if (index != -1)
					{
						nodes[index].Insert(sqaureOne);
						objects.Remove(sqaureOne);
					}
					else
					{
						i++;
					}
				}
			}
		}
	}
	
	public List<SquareOne> Get(List<SquareOne> fSpriteList, Rect pRect)
	{
		return Retrieve(fSpriteList, pRect);
	}
	
	private List<SquareOne> Retrieve(List<SquareOne> fSpriteList, Rect pRect)
	{
		List<int> indexes = GetIndexes(pRect);
		for(int ii = 0; ii < indexes.Count; ii++)
		{
			int index = indexes[ii];
			if(index != -1 && nodes[0] != null)
			{
				nodes[index].Retrieve(fSpriteList, pRect);
			}	
				
			fSpriteList.AddRange(objects);
		}
		
		return fSpriteList;
	}
}
     * 
     * 
     * 
     * 
     * 
     */

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
                _nodes[index].GetPossibleChoicesInternal(objects, rectangle);

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