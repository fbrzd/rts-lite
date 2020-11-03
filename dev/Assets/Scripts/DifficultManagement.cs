using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultManagement : MonoBehaviour
{
    public float timeInterval;
    public SpawnManagement scriptSpawn;
    float timeCurrent;
    public int MAX_DIFFICULT;
    public int currentDifficult;
    public float stepDifficult;

    // Start is called before the first frame update
    void Start()
    {
        timeCurrent = 0;
        currentDifficult = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDifficult < MAX_DIFFICULT){
            timeCurrent += Time.deltaTime;
            if (timeCurrent >= timeInterval){
                DifficultUp();
                timeCurrent = 0;
            }
        }
    }
    void DifficultUp(){
        currentDifficult += 1;
        foreach (GameObject town in scriptSpawn.listTowns){ // cambia a los pueblos ya creados, ACTUALIZAR si se pueden crear nuevos pueblos
            town.GetComponent<TownController>().timeInterval *= stepDifficult; // TEST
        }
        scriptSpawn.timeInterval *= stepDifficult;
    }
}
