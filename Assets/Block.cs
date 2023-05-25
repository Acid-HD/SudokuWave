using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<int> OriginPossibleNumbers;
    public List<int> PossibleNumbersLeft;
    public List<int> EliminatedNumbers;

    public int CurrentNumber = 0;

    public TextMeshPro textUI;

    public GameObject possibleUIBlock;

    public List<GameObject> GeneratedPossibleUIBlock;

    public float x_space, y_space;
    public float x_start, y_start;

    public SudokuMain ParentScript;

    public int GridID;
    public int RowID;
    public int ColumnID;

    private Camera camera;

    private void Awake()
    {
        OriginPossibleNumbers = new List<int>();
        PossibleNumbersLeft = new List<int>();
        EliminatedNumbers = new List<int>();
        GeneratedPossibleUIBlock = new List<GameObject>();
        camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = null;
        //PopulateNumbers(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildUI()
    {
        GeneratedPossibleUIBlock.ForEach(e =>
        {
            Destroy(e);
        });
        GeneratedPossibleUIBlock.Clear();
        if (CurrentNumber == 0)
        {
            for (int i = 0; i < PossibleNumbersLeft.Count; i++)
            {
                GameObject newBlock = Instantiate(possibleUIBlock, new Vector2(transform.position.x + x_start + (x_space * (i % 3)), transform.position.y + y_start + (y_space * (i / 3))), Quaternion.identity, transform);
                TextMeshPro newTMP = newBlock.transform.GetChild(0).GetComponent<TextMeshPro>();
                newTMP.text = PossibleNumbersLeft[i].ToString();
                InsideBlock script = newBlock.GetComponent<InsideBlock>();
                script.setScript(PossibleNumbersLeft[i], this);
                GeneratedPossibleUIBlock.Add(newBlock);
            }
        }
    }

    public void SetUp(List<int> n, SudokuMain s, int g, int c, int r)
    {
        ParentScript = s;
        PopulateNumbers(n);
        GridID = g;
        ColumnID = c;
        RowID = r;
    }

    public void PopulateNumbers(List<int> n)
    {
        OriginPossibleNumbers = new List<int>(n);
        PossibleNumbersLeft = new List<int>(n);

        BuildUI();
    }

    public void EliminateNumber(int n)
    {
        PossibleNumbersLeft.Remove(n);
        EliminatedNumbers.Add(n);

        BuildUI();
    }

    public void SelectNumber(int n)
    {
        CurrentNumber = n;
        PossibleNumbersLeft.Remove(n);
        EliminatedNumbers.Add(n);
        textUI.text = n.ToString();

        ParentScript.BlockSet(GridID, ColumnID, RowID, n);

        BuildUI();
    }

    public void RemoveNumber()
    {
        if(CurrentNumber != 0)
        {
            PossibleNumbersLeft.Add(CurrentNumber);
            EliminatedNumbers.Remove(CurrentNumber);
        }
        textUI.text = null;
        CurrentNumber = 0;

        BuildUI();
    }
}
