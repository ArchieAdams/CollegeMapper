using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager{
    
    private readonly List<List<Tile>> grid = new List<List<Tile>>();
    private readonly List<Tile> seenTiles = new List<Tile>();
    private readonly List<Tile> visitedTiles = new List<Tile>();
    private Tile startTile;
    private Tile goalTile;
    private Tile lastTile;


    
    public void AddTile(Tile tile, int column)
    {
        grid[column].Add(tile);
    }
    
    public void AddColumn()
    {
        grid.Add(new List<Tile>());
    }
    
    private void AddSeenTiles(Tile visitedTile)
    {
        Debug.Log("Tile "+visitedTile.GetCoordinates());
        foreach (var neighbourTile in visitedTile.GetNeighbours())
        {
            if (!(visitedTiles.Contains(neighbourTile)))
            {
                neighbourTile.SetColour(Color.yellow);
            }

            seenTiles.Add(neighbourTile);
            Debug.Log("neig :" + neighbourTile.GetCoordinates());
            AddCosts(neighbourTile);
        }
    }
    
    public void AddVisitedTiles(Tile visitedTile)
    {
        lastTile = visitedTile;
        visitedTiles.Add(visitedTile);
        if (!(visitedTile.Equals(goalTile)))
        {
            visitedTile.SetColour(Color.magenta);
        }

        seenTiles.Remove(visitedTile);
        AddSeenTiles(visitedTile);
    }

    public List<List<Tile>> GetGrid()
    {
        return grid;
    }

    
    public void SetStartTile(string coordinates)
    {
        foreach (var column in grid)
        {
            foreach (var tile in column.Where(tile => tile.GetCoordinates().Equals(coordinates)))
            {
                startTile = tile;
                AddVisitedTiles(tile);
                tile.SetColour(Color.blue);
            }
        }
    }
    
    public void SetGoalTile(string coordinates)
    {
        foreach (var column in grid)
        {
            foreach (var tile in column.Where(tile => tile.GetCoordinates().Equals(coordinates)))
            {
                goalTile = tile;
                tile.SetColour(Color.cyan);
            }
        }
        visitedTiles.Add(goalTile);
    }

    public Tile GetStartTile()
    {
        return startTile;
    }

    public Tile GetGoalTile()
    {
        return goalTile;
    }
    
    public List<Tile> GetSeenTiles()
    {
        return seenTiles;
    }
    
    public List<Tile> GetVisitedTiles()
    {
        return visitedTiles;
    }
    
    public Tile GetLastTile()
    {
        return lastTile;
    }
    
    
    private void AddCosts(Tile tile)
    {
        
        tile.SetGCost(CalculateDistance(tile, startTile));
        tile.SetHCost(CalculateDistance(tile, goalTile));
        tile.SetFCost(tile.GetGCost() + tile.GetHCost());
        //Debug.Log(tile.GetCoordinates() + " G:" + tile.GetGCost() + " + H:" + tile.GetHCost() + " = F:" +
          //        tile.GetFCost()); 
    }

    /// <summary>
    /// Finds the distance between 2 points.
    /// </summary>
    /// <param name="tileA">The first tile you want to find the distance between.</param>
    /// <param name="tileB">The second tile you want to find the distance between.</param>
    /// <returns>The distance between the points as an int this number has been x10 to make it easier to interpret.</returns>
    private static int CalculateDistance(Tile tileA, Tile tileB)
    {
        return (int)(Math.Sqrt(Math.Pow(tileA.GetX() - tileB.GetX(), 2) + Math.Pow(tileA.GetY() - tileB.GetY(), 2)) * 10);
    }
}
