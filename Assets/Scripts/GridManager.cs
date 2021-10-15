using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 16; //Number of rows in the grid
    [SerializeField]
    private int columns = 9; //Number of columns in the grid
    [SerializeField]
    private int tileSize = 1; //Spacing between tiles

    private readonly TileManager tileManager = new TileManager();
    
    public InputField startInputField;
    public InputField goalInputField;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }
    
    /// <summary>
    /// Creates a grid of row x columns and saves the tiles to a list
    /// </summary>
    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Tile")); //Gets the prefab for your tile so you only have to load it once
        for (int column = 0; column < columns; column++)
        {

            tileManager.AddColumn();
            for (int row = 0; row < rows; row++)
            {
                Tile tile = new Tile(Instantiate(referenceTile, transform), column * tileSize, row * tileSize); //Creates new tile object for each coordinate 
                tileManager.AddTile(tile, column); //Adds tiles to the list of all tiles (the grid)
            }
        }
        Destroy(referenceTile); //Stops the program having to load the referenceTile and frees up memory
        AddNeighbours();
        float gridWidth = columns * tileSize;
        float gridHeight = rows * tileSize;
        transform.position = new Vector3(-(gridWidth- tileSize) / 2 , -(gridHeight-tileSize) / 2);
        
    }

    /// <summary>
    /// Loops through all tiles and adds there neighbours to a list
    /// </summary>
    private void AddNeighbours()
    {
        List<List<Tile>> grid = tileManager.GetTiles();
        foreach (var column in grid)
        {
            foreach (var tile in column)
            {
                int x = tile.GetX();
                int y = tile.GetY();
                for (int xDifference = -1; xDifference <= 1; xDifference++)
                {
                    for (int yDifference = -1; yDifference <= 1; yDifference++)
                    {
                        if (!(xDifference == 0 && yDifference == 0))
                        {
                            try
                            {
                                tile.AddNeighbours(tileManager.GetTiles()[y+yDifference][x+xDifference]);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sets important locations called by a button press
    /// </summary>
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

    /// <summary>
    /// Finds the distance between 2 points.
    /// </summary>
    /// <param name="tileA">The first tile you want to find the distance between.</param>
    /// <param name="tileB">The second tile you want to find the distance between.</param>
    /// <returns>The distance between the points as an int this number has been x10 to make it easier to interpret.</returns>
    private int CalculateDistance(Tile tileA, Tile tileB)
    {
        return (int)(Math.Sqrt(Math.Pow(tileA.GetX() - tileB.GetX(), 2) + Math.Pow(tileA.GetY() - tileB.GetY(), 2)) * 10);
    }
    
    /// <summary>
    /// Searches all seen tiles for the one containing the lowest F cost.
    /// </summary>
    /// <returns>The Tile that contains the lowest F cost.</returns>
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
