using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager{
    
    private readonly List<Tile> tiles = new List<Tile>();
    private readonly List<Tile> seenTiles = new List<Tile>();
    private readonly List<Tile> visitedTiles = new List<Tile>();
    private Tile startTile;
    private Tile goalTile;

    public void AddTile(Tile tile)
    {

        tiles.Add(tile);
    }

    public List<Tile> GetTiles()
    {
        return tiles;
    }

    
    public void SetStartTile(string coordinates)
    {
        foreach (var tile in tiles.Where(tile => tile.GetCoordinates().Equals(coordinates)))
        {
            startTile = tile;
            tile.SetColour(Color.blue);
            foreach (var neighbourTile in tile.GetNeighbours())
            {
                neighbourTile.SetColour(Color.yellow);
            }
        }
    }
    
    public void SetGoalTile(string coordinates)
    {
        foreach (var tile in tiles.Where(tile => tile.GetCoordinates().Equals(coordinates)))
        {
            goalTile = tile;
            tile.SetColour(Color.cyan);
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
