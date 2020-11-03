using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RowTableController : MonoBehaviour
{
    public TextMeshProUGUI textRank;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textHarvest;
    public TextMeshProUGUI textFolks;
    public TextMeshProUGUI textKills;
    public GameObject town_GO;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetData(TownController town){
        textRank.text = ""+town.statRank;
        textName.text = ""+town.id;
        textDamage.text = ""+(-town.statDamage);
        textHarvest.text = ""+town.statHarvest;
        textFolks.text = ""+town.statFolks;
        textKills.text = ""+town.statKills;
        town_GO.GetComponent<SpriteRenderer>().color = town.gameObject.GetComponent<SpriteRenderer>().color;
    }
}
