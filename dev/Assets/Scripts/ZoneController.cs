using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ZoneController : NetworkBehaviour
{
    public SpawnManagement scriptSpawn;
    public float timestamp;
    public string type;

    // sprites
    public Sprite spriteResource1;
    public Sprite spriteResource2;
    public Sprite spriteFolkSpeed;
    public Sprite spriteFolkMore;
    public Sprite spriteFolkAttack;
    public Sprite spriteAttackAll;

    // Start is called before the first frame update
    void Start()
    {
        timestamp = Time.time;
        // check for LAN
        if (!SingletonManager.singletonManager.flagLAN){
            gameObject.GetComponent<NetworkIdentity>().enabled = false;
            gameObject.GetComponent<NetworkTransform>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "folk" && other.GetComponent<FolkController>().timestamp > timestamp){
            //other.GetComponent<FolkController>().scriptTownFrom.score += resources;
            ZoneEffect(other.GetComponent<FolkController>());
            scriptSpawn.PullZone(gameObject);
            Destroy(other.gameObject);
        }
    }
    public void ZoneEffect(FolkController scr){
        if (type == "resource1"){
            int resource = 40 + 10*(Random.Range(-1,2));
            if (scr.type == "wagon") resource *= 2;
            scr.scriptTownFrom.SumScore(resource);
            scr.scriptTownFrom.statHarvest += resource;
        }
        else if (type == "resource2"){
            int resource = 50 + 15*(Random.Range(-1,2));
            if (scr.type == "wagon") resource *= 2;
            scr.scriptTownFrom.SumScore(resource);
            scr.scriptTownFrom.statHarvest += resource;
        }
        else if (type == "resource3"){
            int resource = 60 + 15*(Random.Range(-1,2));
            if (scr.type == "wagon") resource *= 2;
            scr.scriptTownFrom.SumScore(resource);
            scr.scriptTownFrom.statHarvest += resource;
        }
        //no +recursos, volcan [no list]
        else if (type == "attack-all"){ //daño a todos (menos tu), torre [no list]
            int damage = -(30 + 5*Random.Range(-1, 2));
            foreach (GameObject town in scriptSpawn.listTowns){
                if (scr.scriptTownFrom.gameObject != town){
                    town.GetComponent<TownController>().SumScore(damage, scr.scriptTownFrom);
                    scr.scriptTownFrom.statDamage += damage;
                }
            }
        }//cambiar posicion, terreno [no list]
        //unidad extra, aldea (*)
        else if (type == "folk-more")
            scr.scriptTownFrom.max_stackfolks += 1;
        else if (type == "folk-speed")
            scr.scriptTownFrom.speedFolk *= 1.5f; // unidad +rapida, animales
        //unidad +daño, armas
        //recoger +recurso, herramientas
        else if (type == "bartime") scr.scriptTownFrom.timeInterval *= 0.75f; // bartime +rapida, magia?
    }
    public void ChangeType(string newType){ // pass sprite too ?
        type = newType;
        if (type == "resource1") gameObject.GetComponent<SpriteRenderer>().sprite = spriteResource1;
        if (type == "resource2") gameObject.GetComponent<SpriteRenderer>().sprite = spriteResource2;
        if (type == "folk-speed") gameObject.GetComponent<SpriteRenderer>().sprite = spriteFolkSpeed;
        if (type == "folk-more") gameObject.GetComponent<SpriteRenderer>().sprite = spriteFolkMore;
        if (type == "folk-attack") gameObject.GetComponent<SpriteRenderer>().sprite = spriteFolkAttack;
        if (type == "attack-all") gameObject.GetComponent<SpriteRenderer>().sprite = spriteAttackAll;
    }
    void OnMouseEnter()
    {
        SingletonManager.singletonManager.SetCursor("harvest");
    }
    void OnMouseExit()
    {
        SingletonManager.singletonManager.SetCursor("normal");
    }
}
