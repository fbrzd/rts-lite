using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerLANController : NetworkBehaviour
{
    //public static PlayerController player;
    public bool flagPlaying;
    public TownController scriptTown;
    public AudioSource soundFolk;
    public GameObject indicatorPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.singletonManager.flagLAN){
            if (this.isLocalPlayer){
                SetIndicator();
                scriptTown.SetVisibleBar(true);
                scriptTown.id = "player";
            } else {
                Destroy(indicatorPlayer);
                scriptTown = gameObject.GetComponent<TownController>();
                Destroy(indicatorPlayer);
                //SingletonManager.singletonManager.SetCamera(transform.position);
            }
            flagPlaying = false;
            scriptTown.flagAlive = false;
            Destroy(gameObject.GetComponent<PlayerController>());
            //Destroy(gameObject.GetComponent<EnemyAController>());
        } else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer && Input.GetMouseButtonDown(0) && flagPlaying){
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (scriptTown.SendFolk(worldPosition)) soundFolk.Play();
        }
        //if (Input.GetKeyDown("space")) SingletonManager.singletonManager.SetCamera(transform.position);
    }
    void SetIndicator(){
        indicatorPlayer.GetComponent<IndicatorController>().SetParameters(scriptTown.gameObject.GetComponent<SpriteRenderer>().color, scriptTown.typeSpecialFolk);
        if (transform.localPosition[0] < 0){
            indicatorPlayer.transform.Translate(-2*indicatorPlayer.transform.localPosition[0], 0, 0);
            indicatorPlayer.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
