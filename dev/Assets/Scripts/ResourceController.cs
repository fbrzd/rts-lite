using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public SpawnManagement scriptSpawn;
    int resources;

    // Start is called before the first frame update
    void Start()
    {
        resources = 40 + 10*(Random.Range(-1,2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*/void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "folk"){
            other.GetComponent<FolkController>().scriptTownFrom.score += resources;
            scriptSpawn.PullResource(gameObject);
            Destroy(other.gameObject);
        }
        //if (other.tag == "player-attack") Destroy(gameObject);
        //else if (other.tag == "limit-scene") transform.position = lastPosition;
    }*/
    /*void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)){
            PlayerController.player.scriptTown.SendFolk(transform.position);
        }
    }*/
}
