using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FolkController : NetworkBehaviour
{
    public float speed;
    public Vector3 initDirection;
    public TownController scriptTownFrom;
    public float timestamp;
    public Animator anim;
    public float timeFight;
    bool flagFight;
    public string type;

    public Sprite spriteNormal;
    public Sprite spriteGiant;
    public Sprite spriteFly;
    public Sprite spriteWagon;
    
    // Start is called before the first frame update
    void Start(){
        timestamp = Time.time;
        flagFight = false;

        // check for LAN
        if (!SingletonManager.singletonManager.flagLAN){
            gameObject.GetComponent<NetworkIdentity>().enabled = false;
            gameObject.GetComponent<NetworkTransform>().enabled = false;
        }
        /*int rnd_value = Random.Range(0,3);
        if (rnd_value == 0) speed = 5f;
        else if (rnd_value == 1) speed = 10f;
        else speed = 15f;

        gameObject.GetComponent<SpriteRenderer>().sprite = listSprites[Random.Range(0,listSprites.Count)];*/
    }

    // Update is called once per frame
    void Update()
    {
        if (flagFight){
            timeFight -= Time.deltaTime;
            if (timeFight <= 0) Destroy(gameObject);
        } else transform.Translate(speed*initDirection[0]*Time.deltaTime, speed*initDirection[1]*Time.deltaTime, 0);
        if (scriptTownFrom == null) Destroy(gameObject);
    }
    public void SetColor(int color){
        Color c = new Color();
        if (color == 0) c = new Color(1f, 0f, 0f, 1f);
        if (color == 1) c = new Color(0f, 0f, 1f, 1f);
        if (color == 2) c = new Color(0f, 1f, 0f, 1f);
        if (color == 3) c = new Color(0f, 1f, 1f, 1f);
        if (color == 4) c = new Color(1f, 1f, 0f, 1f);
        if (color == 5) c = new Color(1f, 0f, 1f, 1f);
        if (color == 6) c = new Color(1f, 1f, 1f, 1f);
        if (color == 7) c = new Color(0.6f, 0.6f, 0.6f, 1f);
        gameObject.GetComponent<SpriteRenderer>().color = c;
    }
    void OnTriggerEnter2D(Collider2D other){
        if (type != "fly" && other.tag == "folk" && other.GetComponent<FolkController>().type != "fly"){
            if (other.GetComponent<FolkController>().scriptTownFrom.id != scriptTownFrom.id){
                if (other.GetComponent<FolkController>().type == "giant") Destroy(gameObject);
                else if (type != "giant"){
                    flagFight = true;
                    anim.SetBool("fight",true);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        //else if (other.tag == "town" || other.tag == "resource") Destroy(gameObject);
    }
    public void ChangeType(string newType){
        type = newType;
        if (type == "normal") gameObject.GetComponent<SpriteRenderer>().sprite = spriteNormal;
        else if (type == "giant"){
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteGiant;
            speed *= 0.75f;
        }
        else if (type == "fly"){
            speed *= 1.5f;
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteFly;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        else if (type == "wagon"){
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteWagon;
            if (initDirection[0] > 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
