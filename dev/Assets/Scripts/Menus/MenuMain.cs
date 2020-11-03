using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public GameObject layerMenu;
    public ClickCounterController menuTowns;
    public ClickCounterController menuResources;
    public GameObject spawn;
    
    public List<AudioSource> listMusic;
    public AudioSource musicWin;
    public AudioSource musicLose;

    bool flagMenu;
    int indexMusic;
    bool flagEnd; // just for music end
    
    // Start is called before the first frame update
    void Start()
    {
        // shuffle
        flagEnd = false;
        flagMenu = true;
        indexMusic = 0;
        AudioSource tmp;
        int r;
        for (int i = 0; i < listMusic.Count; i++){
            r = Random.Range(0, listMusic.Count);
            tmp = listMusic[i];
            listMusic[i] = listMusic[r];
            listMusic[r] = tmp;
        }
        ResetAll();
        PlayNextMusic();
        if (SingletonManager.singletonManager != null){
            menuTowns.SumCount(SingletonManager.singletonManager.lastNPlayers-menuTowns.count);
            menuResources.SumCount(SingletonManager.singletonManager.lastNZones-menuResources.count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKeyDown("space")){
            if (flagMenu) PlayConfig();
            else {} // pause
        } else */
        if (!listMusic[indexMusic].isPlaying && !flagEnd) PlayNextMusic();
        if (Input.GetKeyDown("escape")){
            if (flagMenu) Application.Quit();
            else {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    public void PlayConfig(string mode){
        if (mode == "quick"){
            SingletonManager.singletonManager.lastNPlayers = menuTowns.count;
            SingletonManager.singletonManager.lastNZones = menuResources.count;
            SingletonManager.singletonManager.mode = "quick";
            spawn.GetComponent<SpawnManagement>().SetBoardByParameters(menuTowns.count, menuResources.count);
        } else if (mode == "infinite"){
            SingletonManager.singletonManager.mode = "infinite";
            spawn.GetComponent<SpawnManagement>().SetBoardInfinite();
        }
        spawn.SetActive(true);
        layerMenu.SetActive(false);
        flagMenu = false;
        SingletonManager.singletonManager.SetCursor("normal");
        //PlayerController.player.flagPlaying = true;
    }
    public void ResetAll(){ // re init static fields
        TownController.townCount = 0;
    }
    public void PlayNextMusic(){
        flagEnd = false;
        if (musicLose.isPlaying) musicLose.Stop();
        else if (musicWin.isPlaying) musicWin.Stop();
        if (listMusic[indexMusic].isPlaying) listMusic[indexMusic].Stop();
        indexMusic = (indexMusic + 1) % listMusic.Count;
        listMusic[indexMusic].Play();
    }
    public void PlayMusicEnd(string type){
        flagEnd = true;
        listMusic[indexMusic].Stop();
        if (type == "win") musicWin.Play();
        else musicLose.Play();
    }
}
