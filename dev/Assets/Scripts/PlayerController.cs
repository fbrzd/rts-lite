using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    public bool flagPlaying;
    public TownController scriptTown;
    public AudioSource soundFolk;
    public GameObject indicatorPlayer;
    
    Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (!SingletonManager.singletonManager.flagLAN){
            if (PlayerController.player != null){
                Destroy(indicatorPlayer);
                Destroy(this);
            } else {
                player = this;
                flagPlaying = true;
                scriptTown = gameObject.GetComponent<TownController>();
                scriptTown.id = "player";
                scriptTown.SetVisibleBar(true);
                SetIndicator();
                //SingletonManager.singletonManager.SetCamera(transform.position);
                Destroy(gameObject.GetComponent<EnemyAController>());
            }
        } else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && flagPlaying){
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