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

    private readonly List<Tile> tiles = new List<Tile>();
    
    public InputField inputField;
    
    
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
                tiles.Add(tile);
                AddNeighbours();
            }
        }
        Destroy(referenceTile);

        float gridWidth = columns * tileSize;
        float gridHeight = rows * tileSize;
        transform.position = new Vector3(-(gridWidth- tileSize) / 2 , -(gridHeight-tileSize) / 2);
    }

    private void AddNeighbours()
    {
        foreach (var tile in tiles)
        {
            int x = tile.GetX();
            int y = tile.GetY();
            for (int xDifference = -1; xDifference <= 1; xDifference++)
            {
                for (int yDifference = -1; yDifference <= 1; yDifference++)
                {
                    foreach (var neighbourTile in tiles.Where(neighbourTile =>
                        neighbourTile.GetX() == x + xDifference && neighbourTile.GetY() == y + yDifference && !(xDifference==0 && yDifference == 0)))
                    {
                        tile.AddNeighbours(neighbourTile);
                    }
                }
            }
        }
    }

    public void SetGoal()
    {
        var coordinates = inputField.text;
        foreach (var tile in tiles.Where(tile => tile.GetCoordinates().Equals(coordinates)))
        {
            tile.SetColour(Color.blue);
            foreach (var neighbourTile in tile.GetNeighbours())
            {
                neighbourTile.SetColour(Color.yellow);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    
}
