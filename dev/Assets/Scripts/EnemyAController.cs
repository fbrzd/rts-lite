using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAController : MonoBehaviour
{
    public TownController scriptTown;
    float timeCurrent;
    float timeDelay;
    public int focusAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        //focusAttack = -1; // no focus
        if (SingletonManager.singletonManager.flagLAN) Destroy(this);
        else {
            scriptTown = gameObject.GetComponent<TownController>();
            timeDelay = Random.Range(scriptTown.timeInterval/4, scriptTown.timeInterval*2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }
    void Test(){
        if (scriptTown.currentStackFolks > 0){
            if (timeCurrent < timeDelay) timeCurrent += Time.deltaTime; // wait delay
            else {
                if (scriptTown.score >= 80) scriptTown.AttackRandom(focusAttack); // secure
                else if (scriptTown.score >= 50){ // random (harvest, attack)
                    if (Random.Range(0, 2) == 0 && scriptTown.HarvestRandom()){ }
                    else scriptTown.AttackRandom(focusAttack);
                } else if (scriptTown.HarvestRandom()){ } // danger
                else scriptTown.AttackRandom(focusAttack);
                timeCurrent = 0;
                timeDelay = Random.Range(scriptTown.timeInterval/4, scriptTown.timeInterval*2);
            }
        }
    }
    void OnMouseEnter()
    {
        SingletonManager.singletonManager.SetCursor("attack");
    }
    void OnMouseExit()
    {
        SingletonManager.singletonManager.SetCursor("normal");
    }
}
