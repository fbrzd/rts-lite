using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public float speed;
    public float timeToDie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToDie -= Time.deltaTime;
        transform.Translate(0, speed*Time.deltaTime, 0);
        if (timeToDie <= 0) Destroy(gameObject);
    }
    public void SetValue(int val){
        if (val > 0){
            textScore.text = "+" + val;
            textScore.color = new Color32(0, 255, 0, 255);
        } else if (val < 0){
            textScore.text = "" + val;
            textScore.color = new Color32(255, 0, 0, 255);
        }
    }
}
