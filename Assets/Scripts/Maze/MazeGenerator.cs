using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int Width;
    public int Depth;
    public MazeCell Cell;
    public MazeCell[,] Cells;
    public GameObject OxygenCanisterPrefab;
    public float PowerupSpawnRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cells = new MazeCell[Width, Depth];
        for(var x = 0; x < Width; x++)
        {
            for(var z = 0; z < Depth; z++)
            {
                Cells[x, z] = Instantiate(Cell, new Vector3(x, 0, z), Quaternion.identity, transform);
            }
        }
        GenerateMaze(null, Cells[0, 0]);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);
        GeneratePowerup(currentCell);
        MazeCell nextCell;
        do
        {
            nextCell = PickNextUnvisitedCell(currentCell);
            if(nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private void GeneratePowerup(MazeCell currentCell)
    {
        if(currentCell == null)
        {
            return;
        }
        if(Random.Range(0f,1f) < PowerupSpawnRate)
        {
            Instantiate(OxygenCanisterPrefab, new Vector3(currentCell.x, 0, currentCell.z), Quaternion.identity);
        }
    }

    private MazeCell PickNextUnvisitedCell(MazeCell cell)
    {
        var unvisitedCells = GetUnvisitedCells(cell);
        if(unvisitedCells.Count == 0)
        {
            return null;
        }
        return unvisitedCells[Random.Range(0, unvisitedCells.Count)];
    }

    private List<MazeCell> GetUnvisitedCells(MazeCell cell)
    {
        var cells = new List<MazeCell>();
        int x = cell.x;
        int z = cell.z;
        int left = Mathf.Min(x + 1, Width - 1);
        if(left != x && !Cells[left, z].IsVisited)
        {
            cells.Add(Cells[left, z]);
        }
        int right = Mathf.Max(x - 1, 0);
        if (right != x && !Cells[right, z].IsVisited)
        {
            cells.Add(Cells[right, z]);
        }
        int forward = Mathf.Min(z + 1, Depth - 1);
        if (forward != z && !Cells[x, forward].IsVisited)
        {
            cells.Add(Cells[x, forward]);
        }
        int backward = Mathf.Max(z - 1, 0);
        if (backward != z && !Cells[x, backward].IsVisited)
        {
            cells.Add(Cells[x, backward]);
        }
        return cells;
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if(previousCell == null)
        {
            return;
        }

        if (previousCell.x > currentCell.x)
        {
            previousCell.LeftWall.SetActive(false);
            currentCell.RightWall.SetActive(false);
            return;
        }
        if (previousCell.x < currentCell.x)
        {
            previousCell.RightWall.SetActive(false);
            currentCell.LeftWall.SetActive(false);
            return;
        }
        if (previousCell.z > currentCell.z)
        {
            previousCell.BackWall.SetActive(false);
            currentCell.FrontWall.SetActive(false);
            return;
        }
        if (previousCell.z < currentCell.z)
        {
            previousCell.FrontWall.SetActive(false);
            currentCell.BackWall.SetActive(false);
            return;
        }
    }
}
