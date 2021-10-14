using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 16;
    [SerializeField]
    private int columns = 9;
    [SerializeField]
    private int tileSize = 1;

    private readonly TileManager tileManager = new TileManager();
    
    public InputField startInputField;
    public InputField goalInputField;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Tile"));
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Tile tile = new Tile(Instantiate(referenceTile, transform), column * tileSize, row * tileSize);
                tileManager.AddTile(tile);
                
            }
        }
        Destroy(referenceTile);
        AddNeighbours();
        float gridWidth = columns * tileSize;
        float gridHeight = rows * tileSize;
        transform.position = new Vector3(-(gridWidth- tileSize) / 2 , -(gridHeight-tileSize) / 2);
        
    }

    private void AddNeighbours()
    {
        List<Tile> tiles = tileManager.GetTiles();
        foreach (var tile in tiles)
        {
            int x = tile.GetX();
            int y = tile.GetY();
            for (int xDifference = -1; xDifference <= 1; xDifference++)
            {
                for (int yDifference = -1; yDifference <= 1; yDifference++)
                {
                    foreach (var neighbourTile in tiles.Where(neighbourTile =>
                        neighbourTile.GetX() == x + xDifference && neighbourTile.GetY() == y + yDifference &&
                        !(xDifference == 0 && yDifference == 0)))
                    {
                        tile.AddNeighbours(neighbourTile);
                    }
                }
            }
        }
    }

    public void SetLocations()
    {
        tileManager.SetStartTile(startInputField.text);
        tileManager.SetGoalTile(goalInputField.text);
        foreach (var neighbour in tileManager.GetStartTile().GetNeighbours())
        {
            neighbour.SetGCost(CalculateDistance(neighbour, tileManager.GetStartTile()));
            neighbour.SetHCost(CalculateDistance(neighbour, tileManager.GetGoalTile()));
            neighbour.SetFCost(neighbour.GetGCost() + neighbour.GetHCost());
            Debug.Log(neighbour.GetCoordinates() + " G:" + neighbour.GetGCost() + " + H:" + neighbour.GetHCost() + " = F:" +
                      neighbour.GetFCost());

        }

        Tile nextTile = GetLowestFCost();
        Debug.Log(nextTile.GetCoordinates());
        nextTile.SetColour(Color.magenta);
    }

    private int CalculateDistance(Tile tileA, Tile tileB)
    {
        return (int)(Math.Sqrt(Math.Pow(tileA.GetX() - tileB.GetX(), 2) + Math.Pow(tileA.GetY() - tileB.GetY(), 2)) * 10);
    }

    private Tile GetLowestFCost()
    {
        Tile lowestTile = null;
        int lowestFCost = -1;
        int lowestHCost = -1;
        foreach (var neighbour in tileManager.GetStartTile().GetNeighbours())
        {
            if (neighbour.GetFCost()<lowestFCost || lowestFCost == -1)
            {
                lowestTile = neighbour;
                lowestFCost = neighbour.GetFCost();
                lowestHCost = neighbour.GetHCost();
            }

            if (neighbour.GetFCost() == lowestHCost)
            {
                if (neighbour.GetFCost() < lowestFCost)
                {
                    lowestTile = neighbour;
                    lowestFCost = neighbour.GetFCost();
                    lowestHCost = neighbour.GetHCost();
                }
            }
        }

        return lowestTile;
    }
    
    

    // Update is called once per frame
    void Update()
    {
    }
    
    
}
