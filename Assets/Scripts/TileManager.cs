using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager{
    
    private readonly List<List<Tile>> grid = new List<List<Tile>>();
    private readonly List<Tile> seenTiles = new List<Tile>();
    private readonly List<Tile> visitedTiles = new List<Tile>();
    private Tile startTile;
    private Tile goalTile;


    
    public void AddTile(Tile tile, int column)
    {
        grid[column].Add(tile);
    }
    
    public void AddColumn()
    {
        grid.Add(new List<Tile>());
    }

    public List<List<Tile>> GetTiles()
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
                tile.SetColour(Color.blue);
                foreach (var neighbourTile in tile.GetNeighbours())
                {
                    neighbourTile.SetColour(Color.yellow);
                }
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
    }

    public Tile GetStartTile()
    {
        return startTile;
    }

    public Tile GetGoalTile()
    {
        return goalTile;
    }
    
}
