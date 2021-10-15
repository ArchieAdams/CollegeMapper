using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{

   private readonly GameObject tile;
   private int x;
   private int y;

   private int gCost = -1; //Distance to start from tile
   private int hCost = -1; //Distance to end form tile
   private int fCost = -1; //hCost + fCost

   private bool walkable;


   private readonly List<Tile> neighbours = new List<Tile>();



   public Tile(GameObject tile, int x, int y)
   {
      this.tile = tile; 
      this.x = x;
      this.y = y;
      this.tile.transform.position = new Vector2(x,y);
      this.tile.name = "Tile (" + (x+1) + "," + (y+1) + ")";
   }
   

   public GameObject GetTile()
   {
      return tile;
   }

   public int GetX()
   {
      return x;
   }
   
   public int GetY()
   {
      return y;
   }

   public string GetCoordinates()
   {
      return "(" + (x+1) + "," + (y+1) + ")";
   }
   
   public List<Tile> GetNeighbours()
   {
      return neighbours;
   }
      
   public void SetColour(Color color)
   {
      this.tile.GetComponent<SpriteRenderer>().color = color; 
   }
   
   public void SetX(int x)
   {
      this.x = x;
   }
   
   public void SetY(int y)
   {
      this.y = y;
   }
   
   public Vector2 GetVector2()
   {
      return tile.transform.position;
   }
   
   public void SetVector2(int x, int y)
   {
      this.tile.transform.position = new Vector2(x,y);
   }

   public void AddNeighbours(Tile tile)
   {
      neighbours.Add(tile);
   }
   
   
   
   
   
   
   //Pathfinding bits
   
   public int GetGCost()
   {
      return gCost;
   }
   
   public int GetHCost()
   {
      return hCost;
   }

   public int GetFCost()
   {
      return fCost;
   }
   
   public bool IsWalkable()
   {
      return walkable;
   }
   
   public void SetGCost(int gCost)
   {
      this.gCost = gCost;
   }
   
   public void SetHCost(int hCost)
   {
      this.hCost = hCost;
   }
   
   public void SetFCost(int fCost)
   {
      this.fCost = fCost;
   }
   
   public void SetWalkable(bool walkable)
   {
      this.walkable = walkable;
   }
}
   


