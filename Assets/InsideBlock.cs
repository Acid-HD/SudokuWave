using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideBlock : MonoBehaviour
{
    public Block ParentScript;
    public int Number;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        ParentScript.SelectNumber(Number);
    }

    public void setScript(int n, Block s)
    {
        Number = n;
        ParentScript = s;
    }
}
