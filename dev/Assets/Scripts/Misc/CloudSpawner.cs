using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float timeInterval;
    public GameObject prefabCloud;
    public int xInit;
    public int yRange;
    
    float timeCurrent;
    
    // Start is called before the first frame update
    void Start()
    {
        /*if (BoardController.mainBoard.rows == 10){
            xInit *= 2;
            yRange *= 2;
            timeInterval /= 2;
        } */
        timeCurrent = timeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timeCurrent += Time.deltaTime;
        if (timeCurrent >= timeInterval){
            int rnd_value = Random.Range(-yRange, yRange+1);
            Instantiate(prefabCloud, new Vector3(xInit, 30*rnd_value, 0), new Quaternion());
            timeCurrent = 0;
        }
    }
}
