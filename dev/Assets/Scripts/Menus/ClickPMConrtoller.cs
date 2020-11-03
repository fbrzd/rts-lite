using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPMConrtoller : MonoBehaviour
{
    public ClickCounterController scriptCounter;
    public int mountSum;
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
        if (Input.GetMouseButtonDown(0)) scriptCounter.SumCount(mountSum);
    }
}
