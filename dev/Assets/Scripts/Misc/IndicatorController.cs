using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorController : MonoBehaviour
{
    public TextMeshProUGUI textYOU;
    public float timeToDie;
    public GameObject folk_GO;
    public Sprite spriteGiant;
    public Sprite spriteFly;
    public Sprite spriteWagon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0) Destroy(gameObject);
    }
    public void SetParameters(Color32 col, string specialFolk){
        gameObject.GetComponent<SpriteRenderer>().color = col;
        textYOU.color = col;

        if (specialFolk == "giant") folk_GO.GetComponent<SpriteRenderer>().sprite = spriteGiant;
        else if (specialFolk == "fly") folk_GO.GetComponent<SpriteRenderer>().sprite = spriteFly;
        else if (specialFolk == "wagon") folk_GO.GetComponent<SpriteRenderer>().sprite = spriteWagon;
        folk_GO.GetComponent<SpriteRenderer>().color = col;
    }
}
