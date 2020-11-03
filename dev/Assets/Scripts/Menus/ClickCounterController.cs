using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickCounterController : MonoBehaviour
{
    public int minCount;
    public int maxCount;
    public int count;
    public TextMeshProUGUI textCount;
    public AudioSource soundSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SumCount(int value){
        //sound
        if (minCount <= count + value && count + value <= maxCount) count += value;
        textCount.text = ""+count;
        soundSelect.Play();
    }
}
