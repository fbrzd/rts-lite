using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatButtonController : MonoBehaviour
{
    public SpawnManagement scr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) scr.RepeatBoard();
    }
}
