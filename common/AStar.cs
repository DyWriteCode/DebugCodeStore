using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarPoint
{
    public int RowIndex;
    public int ColumnIndex;
    public int G;
    public int H;
    public int F;
    public AStarPoint Parent;
    public AStarPoint(int row,int column)
    {
        this.RowIndex = row;
        this.ColumnIndex = column;
        Parent = null;
    }
    public AStarPoint(int row, int column, AStarPoint parent)
    {
        this.RowIndex = row;
        this.ColumnIndex =column; 
        this.Parent = parent;
    }

    public int GetG()
    {
        int _g = 0;
        AStarPoint parent = this.Parent;
        while(parent != null)
        {
            _g++;
            parent = parent.Parent;
        }
        return _g;
    }
    
    public int GetH(AStarPoint end)
    {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColumnIndex - end.ColumnIndex);
    }
}

public class AStar 
{
    private int rowCount;
    private int columnCount;
    private List<AStarPoint> open;
    private Dictionary<string,AStarPoint> close;
    private AStarPoint start;
    private AStarPoint end;

    public AStar(int rowCount,int columnCount)
    {
        this.rowCount = rowCount;
        this.columnCount = columnCount;
        open=new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();
    }

    public AStarPoint IsInOpen(int rowIndex,int columnIndex)
    {
        for(int i = 0; i < open.Count; i++)
        {
            if (open[i].RowIndex == rowIndex && open[i].ColumnIndex == columnIndex)
            {
                return open[i];
            }
        }
        return null;
    }

    public bool IsInClose(int rowIndex,int columnIndex)
    {
        if (close.ContainsKey($"{rowIndex}_{columnIndex}"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public  bool  FindPath(AStarPoint start, AStarPoint end,System.Action<List<AStarPoint>> findCallBack)
    {
        this.start = start;
        this.end = end;
        open=new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();
        open.Add(start);
        while (true)
        {
            AStarPoint current = GetMinFFromInOpen();
            if (current == null) {
                return false;
            }
            else
            {
                open.Remove(current);
                close.Add($"{current.RowIndex}_{current.ColumnIndex}", current);
                AddAroundInOpen(current);
                AStarPoint endPoint=IsInOpen(end.RowIndex, end.ColumnIndex);
                if (endPoint != null)
                {
                    findCallBack(GetPath(endPoint));
                    return true;
                }
                open.Sort(OpenSort);
            }
        }
    }

    public List<AStarPoint> GetPath(AStarPoint point)
    {
        List<AStarPoint> paths = new List<AStarPoint>();
        paths.Add(point);
        AStarPoint parent = point.Parent;
        while (parent != null)
        {
            paths.Add(parent); 
            parent = parent.Parent;
        }
        paths.Reverse();
        return paths;
    }

    public int OpenSort(AStarPoint a,AStarPoint b)
    {
        return a.F-b.F;
    }

    public void AddAroundInOpen(AStarPoint current)
    {
        if (current.RowIndex - 1 >= 0)
        {
            AddOpen(current,current.RowIndex-1,current.ColumnIndex);
        }
        if (current.RowIndex + 1 <rowCount)
        {
            AddOpen(current, current.RowIndex + 1, current.ColumnIndex);

        }
        if (current.ColumnIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex , current.ColumnIndex- 1);

        }
        if (current.ColumnIndex + 1 < columnCount)
        {
            AddOpen(current, current.RowIndex, current.ColumnIndex + 1);

        }
    }

    public void AddOpen(AStarPoint current,int row,int col)
    {
        if(IsInOpen(row,col)==null && IsInClose(row,col)==false && GameApp.MapManager.GetBlockType(row,col)==BlockType.Null) {
            AStarPoint newPoint = new AStarPoint(row, col, current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);
            newPoint.F = newPoint.G + newPoint.H;
            open.Add(newPoint);
        }
    }
    public AStarPoint GetMinFFromInOpen()
    {
        if (open.Count == 0)
        {
            return null;
        }
        return open[0];
    }
}
