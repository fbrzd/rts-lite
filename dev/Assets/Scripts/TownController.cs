using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class TownController : NetworkBehaviour
{
    public bool flagAlive;
    public bool flagSpecialFolk;
    public SpawnManagement scriptSpawn;
    public GameObject prefabFolk;
    public TextMeshProUGUI textScore;
    public Sprite spriteDefeat;
    public GameObject barTime;

    public GameObject prefabScore;

    // stats
    public int score;
    public float speedFolk;
    public float timeInterval;
    public int attackFolk;
    public float bonusResoruce;
    public AudioSource soundDefeat;

    float timeCurrent;
    public static int townCount;
    public string id; // name
    public int color;
    bool flagVisibleBar;
    const int FOLKS_TO_SPECIAL = 3;

    // stack folks
    public TextMeshProUGUI textFolks;
    public int max_stackfolks;
    public int currentStackFolks;
    public string typeSpecialFolk;

    public float timeIntervalAutosum;
    float timecurrentAutomSum;
    float timestamp;

    // for stats
    public int statDamage;
    public int statHarvest;
    public int statFolks;
    public int statKills;
    public int statRank;

    bool flagLAN;

    // Start is called before the first frame update
    void Start()
    {
        // check for LAN
        if (!SingletonManager.singletonManager.flagLAN){
            gameObject.GetComponent<NetworkIdentity>().enabled = false;
            gameObject.GetComponent<NetworkTransform>().enabled = false;
        }
        flagAlive = true;
        timestamp = Time.time;
        flagSpecialFolk = false;
        typeSpecialFolk = new string[]{"giant","fly","wagon"}[Random.Range(0,3)];
        flagVisibleBar = false;
        timeCurrent = 0;
        max_stackfolks = 3;
        score = 100;
        currentStackFolks = 0;
        id = "town " + townCount;
        SetColor(townCount);
        townCount += 1;

        statDamage = 0;
        statHarvest = 0;
        statFolks = 0;
        statKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timecurrentAutomSum += Time.deltaTime;
        if (timecurrentAutomSum >= timeIntervalAutosum){
            SumScore(5);
            timecurrentAutomSum = 0;
        }
        if (currentStackFolks < max_stackfolks){
            timeCurrent += Time.deltaTime;
            //flagReady = false;
            if (timeCurrent >= timeInterval) AddStackFolk();
        }
        ShowInfo();
    }
    void AddStackFolk(){
        currentStackFolks += 1;
        flagSpecialFolk = currentStackFolks >= FOLKS_TO_SPECIAL;
        timeCurrent = 0;
    }
    void ShowInfo(){
        textScore.text = ""+score + " / " + currentStackFolks;
        if (flagVisibleBar){
            if (currentStackFolks < max_stackfolks) barTime.transform.localScale = new Vector3(24*timeCurrent/timeInterval, 3, 0);
            else barTime.transform.localScale = new Vector3(24, 3, 0);
            if (!flagSpecialFolk) barTime.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
            else barTime.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
        }
    }
    public bool SendFolk(Vector3 positionDest){
        if (flagAlive && score > 5 && currentStackFolks > 0){
            Vector3 dir = (positionDest - transform.position).normalized;
            GameObject b = Instantiate(prefabFolk, transform.position, new Quaternion());
            score -= 5;
            b.GetComponent<FolkController>().initDirection = dir;
            b.GetComponent<FolkController>().speed = speedFolk;
            b.GetComponent<FolkController>().scriptTownFrom = this;
            b.GetComponent<FolkController>().SetColor(color);
            if (currentStackFolks >= FOLKS_TO_SPECIAL){
                b.GetComponent<FolkController>().ChangeType(typeSpecialFolk);
                currentStackFolks -= FOLKS_TO_SPECIAL;
            } else {
                b.GetComponent<FolkController>().ChangeType("normal");
                currentStackFolks -= 1;
            }
            flagSpecialFolk = false;
            statFolks += 1;
            return true;
        } else return false;
    }
    public bool HarvestRandom(){
        if (scriptSpawn.listZones.Count > 0){
            Vector3 pos = scriptSpawn.listZones[Random.Range(0, scriptSpawn.listZones.Count)].transform.position;
            return SendFolk(pos);
        } else return false;
    }
    public bool AttackRandom(int focus){
        if (focus >= 0){
            GameObject town = scriptSpawn.listTownsPlaying[focus];
            return SendFolk(town.transform.position);
        } else {
            while (scriptSpawn.listTownsPlaying.Count > 1){
                GameObject town = scriptSpawn.listTownsPlaying[Random.Range(0, scriptSpawn.listTownsPlaying.Count)];
                if (town.GetComponent<TownController>().id != id && town.GetComponent<TownController>().flagAlive)
                    return SendFolk(town.transform.position);
            }
        }
        return false;
    }
    void Defeat(){
        flagAlive = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteDefeat;
        score = 0;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        statRank = scriptSpawn.listTownsPlaying.Count;
        soundDefeat.Play();
        scriptSpawn.PullTown(gameObject);
        scriptSpawn.CheckEnd();
    }
    void SetColor(int newColor){
        color = newColor;
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
    public void SetVisibleBar(bool flag){
        flagVisibleBar = flag;
        barTime.SetActive(flagVisibleBar);
    }
    public void SumScore(int dScore, TownController triggerTown=null){
        if (flagAlive){
            GameObject score_o = Instantiate(prefabScore, transform.position, new Quaternion());
            score_o.GetComponent<ScoreController>().SetValue(dScore);
            score += dScore;
            if (score <= 0){
                if (triggerTown != null) triggerTown.statKills += 1;
                Defeat();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "folk" && other.GetComponent<FolkController>().scriptTownFrom != this){
            var scr = other.GetComponent<FolkController>();
            if (scr.timestamp > timestamp){
                int damage = -(30 + 5*Random.Range(-1, 2));
                SumScore(damage, scr.scriptTownFrom);
                scr.scriptTownFrom.statDamage += damage;
            }
            Destroy(other.gameObject);
        }
    }
}
