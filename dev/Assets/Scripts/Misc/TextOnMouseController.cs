using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOnMouseController : MonoBehaviour
{
    public TextMeshProUGUI text_GO;
    public string value;
    
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
        text_GO.text = value;
    }
    void OnMouseExit()
    {
        text_GO.text = "";
    }
}
