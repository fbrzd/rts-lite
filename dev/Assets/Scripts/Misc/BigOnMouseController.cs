using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOnMouseController : MonoBehaviour
{
    public float scaleBig;
    public float scaleNormal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        transform.localScale = new Vector3(scaleBig, scaleBig, 0);
    }
    void OnMouseExit()
    {
        transform.localScale = new Vector3(scaleNormal, scaleNormal, 0);
    }
}
