using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SudokuMain : MonoBehaviour
{
    public int height = 9;
    public int width = 9;
    public int grid = 9;

    public List<int> Numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public GameObject BlockPrefab;

    public List<GameObject> GeneratedBlocks;

    public float x_space, y_space;
    public float x_start, y_start;

    private void Awake()
    {
        GeneratedBlocks = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMap()
    {
        for(int i = 1; i <= height; i++)
        {
            for(int j = 1; j <= width; j++)
            {
                GameObject newObj = Instantiate(BlockPrefab, new Vector2(x_start + (x_space * i), y_start + (y_space * j)), Quaternion.identity, transform);
                Block script = newObj.GetComponent<Block>();
                script.SetUp(Numbers, this, getGridID(j, i), i,j);
                GeneratedBlocks.Add(newObj);
            }
        }
    }

    int getGridID(int r, int c)
    {
        return (((r - 1) / 3) * 3) + (((c - 1) / 3) + 1);
    }

    public void BlockSet(int g, int c, int r, int n)
    {
        List<GameObject> filtered = GeneratedBlocks.Where(b => 
        getScript(b).ColumnID == c || getScript(b).RowID == r || getScript(b).GridID == g
        || fallOnPositiveDiagonal(getScript(b).ColumnID, getScript(b).RowID, c, r)
        || fallOnNegativeDiagonal(getScript(b).ColumnID, getScript(b).RowID, c, r)).ToList();
        
        foreach(GameObject obj in filtered)
        {
            obj.GetComponent<Block>().EliminateNumber(n);
        }
    }

    bool fallOnPositiveDiagonal(int c, int r, int hitc, int hitr)
    {
        if(hitc == hitr)
        {
            for (int i = 1, j = 9; i <= 9 && j > 0; i++, j--)
            {
                if (c == i && r == i)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    bool fallOnNegativeDiagonal(int c, int r, int hitc, int hitr)
    {
        if(hitc + hitr == 10)
        {
            for (int i = 1, j = 9; i <= 9 && j > 0; i++, j--)
            {
                if (c == i && r == j)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    Block getScript(GameObject obj)
    {
        return obj.GetComponent<Block>();
    }
}
