using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManagement : MonoBehaviour
{
    public float timeInterval;
    public GameObject prefabZone;
    public GameObject prefabTown;
    public TextMeshProUGUI textBig;
    public MenuMain menu;
    public GameObject endFrame;
    public GameObject endTable;
    
    public List<Vector3> listPositions;
    public List<GameObject> listZones;
    public List<GameObject> listTowns;
    public List<GameObject> listTownsPlaying;
    public int ROWS;
    public int COLS;
    public int MAX_ZONES = 3;
    public int MAX_TOWNS = 2;

    float timeCurrent; // time para spawn de recursos
    float timeCurrentPeace; // time de espera entre "rondas"
    public float timePeaceInterval;
    bool flagPeaceMode;
    int currentRoundInfiniteMode;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (listZones.Count < MAX_ZONES && timeCurrent < timeInterval) timeCurrent += Time.deltaTime;
        if (timeCurrent >= timeInterval && listPositions.Count > 0) SpawnZone();
        if (flagPeaceMode){
            if (timeCurrentPeace < timePeaceInterval) timeCurrentPeace += Time.deltaTime;
            else NextRoundInfiniteMode();
        }
    }
    void SpawnTown(int focus){
        Vector3 pos = listPositions[Random.Range(0, listPositions.Count)];
        GameObject o = Instantiate(prefabTown, pos, new Quaternion());
        o.GetComponent<TownController>().scriptSpawn = this;
        o.GetComponent<EnemyAController>().focusAttack = focus;
        listTowns.Add(o);
        listTownsPlaying.Add(o);
        listPositions.Remove(pos);
    }
    void SpawnZone(){
        Vector3 pos = listPositions[Random.Range(0, listPositions.Count)];
        GameObject o = Instantiate(prefabZone, pos, new Quaternion());
        o.GetComponent<ZoneController>().scriptSpawn = this;
        int rnd_value = Random.Range(0, 100);
        if (rnd_value >= 60) o.GetComponent<ZoneController>().ChangeType("resource1");
        else if (rnd_value >= 40) o.GetComponent<ZoneController>().ChangeType("resource2");
        else if (rnd_value >= 25) o.GetComponent<ZoneController>().ChangeType("folk-speed");
        else if (rnd_value >= 10) o.GetComponent<ZoneController>().ChangeType("folk-more");
        else o.GetComponent<ZoneController>().ChangeType("attack-all");
        
        listZones.Add(o);
        listPositions.Remove(pos);
        timeCurrent = 0;
    }
    public void PullZone(GameObject zone){
        listPositions.Add(zone.transform.position);
        listZones.Remove(zone);
        Destroy(zone);
    }
    public void PullTown(GameObject town){
        //listPositions.Add(town.transform.position);
        listTownsPlaying.Remove(town);
        endTable.GetComponent<EndTableController>().AddNewRow(town.GetComponent<TownController>());
        if (listTownsPlaying.Count == 1){
            listTownsPlaying[0].GetComponent<BoxCollider2D>().enabled = false;
            var winTown = listTownsPlaying[0].GetComponent<TownController>();
            winTown.statRank = 1;
            endTable.GetComponent<EndTableController>().AddNewRow(winTown);
        }
    }
    void CleanTownsInfinite(){
        foreach (GameObject town in listTowns){
            if (!town.GetComponent<TownController>().flagAlive){
                listPositions.Add(town.transform.position);
                Destroy(town);
            }
        }
        listTowns = new List<GameObject>(){PlayerController.player.gameObject};
    }
    void GenPositions(){
        /*int height_tile = 32; // -10 on both edges for well placed sprites
        int width_tile = 32;//SingletonManager.singletonManager.MAX_X;//height * cam.aspect - 12; // -8 on both edges for well placed sprites
        
        SingletonManager.singletonManager.MIN_X = 0;
        SingletonManager.singletonManager.MIN_Y = 0;
        SingletonManager.singletonManager.MAX_X = COLS*width_tile;
        SingletonManager.singletonManager.MAX_Y = ROWS*height_tile;
        
        for (int i = 0; i < ROWS; i++){
            for (int j = 0; j < COLS; j++){
                float y = (i+0.5f) * height_tile + Random.Range(-1f, 1f);
                float x = (j+0.5f) * width_tile + Random.Range(-1f, 1f);
                listPositions.Add(new Vector3(x, y, 0));
            }
        }*/
        
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize - 28; // -10 on both edges for well placed sprites
        float width = height * cam.aspect - 12; // -8 on both edges for well placed sprites
        for (int i=-(int)ROWS/2; i<ROWS/2+1; i++){
            for (int j=-(int)COLS/2; j<COLS/2+1; j++){
                float x = j*width/COLS + Random.Range(-8f, 8f);
                float y = i*height/ROWS;
                listPositions.Add(new Vector3(x, y, 0));
            }
        }
    }
    public void CheckEnd(){ // only singleplayer
        if (textBig.text == ""){
            if (!PlayerController.player.scriptTown.flagAlive){
                textBig.text = "Lose...";
                menu.PlayMusicEnd("lose");
                PlayerController.player.flagPlaying = false;
            }
            else if (listTownsPlaying.Count == 1){
                if (SingletonManager.singletonManager.mode == "infinite"){
                    flagPeaceMode = true;
                } else {
                    textBig.text = "Win!";
                    menu.PlayMusicEnd("win");
                    PlayerController.player.flagPlaying = false;
                }
            }
            else if (listTownsPlaying.Count == 0){
                textBig.text = "Draw...";
                menu.PlayMusicEnd("lose");
                PlayerController.player.flagPlaying = false;
            }
            
            if (!PlayerController.player.flagPlaying){
                endFrame.SetActive(true);
                endTable.GetComponent<EndTableController>().SetVisible(true);
            }
        }
    }
    public void SetBoardByParameters(int countPlayers, int countZones){
        MAX_TOWNS = countPlayers;
        MAX_ZONES = countZones;

        // rows,cols
        //ROWS = 4;
        //COLS = 4;

        //if (countPlayers <= 4) Camera.main.orthographicSize = 64;
        //else if (countPlayers <= 4) camera.orthographicSize = 72;
        /* else if (countPlayers <= 6)*/ Camera.main.orthographicSize = 72;

        // start
        GenPositions();
        while (listZones.Count < MAX_ZONES) SpawnZone();
        while (listTowns.Count < MAX_TOWNS) SpawnTown(-1);
        //PlayerController.player.flagPlaying = true;
    }
    public void SetBoardInfinite(){
        flagPeaceMode = false;
        GenPositions();
        currentRoundInfiniteMode = 0;
        NextRoundInfiniteMode();
    }
    void NextRoundInfiniteMode(){
        currentRoundInfiniteMode += 1;
        MAX_TOWNS = 1 + (currentRoundInfiniteMode+1)/2;
        MAX_ZONES = 3 + (currentRoundInfiniteMode)/3;
        if (currentRoundInfiniteMode > 1) CleanTownsInfinite();
        
        while (listZones.Count < MAX_ZONES) SpawnZone();
        while (listTowns.Count < MAX_TOWNS) SpawnTown(0);
        timeCurrentPeace = 0;
        flagPeaceMode = false;
    }
    public void RepeatBoard(){
        TownController.townCount = 0;
        // clean towns & zones
        foreach (GameObject town in listTowns) Destroy(town);
        foreach (GameObject zone in listZones) Destroy(zone);
        listTowns = new List<GameObject>();
        listZones = new List<GameObject>();
        listPositions = new List<Vector3>();
        listTownsPlaying = new List<GameObject>();
        PlayerController.player = null;
        endTable.GetComponent<EndTableController>().ClearRows();
        endTable.GetComponent<EndTableController>().SetVisible(false);
        menu.PlayNextMusic();
        textBig.text = "";
        SetBoardByParameters(MAX_TOWNS, MAX_ZONES);
        endFrame.SetActive(false);
    }
}
