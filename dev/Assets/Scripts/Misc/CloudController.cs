using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public float speed;
    public List<Sprite> listSprites;

    // Start is called before the first frame update
    void Start(){
        int rnd_value = Random.Range(0,3);
        if (rnd_value == 0) speed -= 5f;
        else if (rnd_value == 1) speed += 5f;
        else speed += 10f;

        gameObject.GetComponent<SpriteRenderer>().sprite = listSprites[Random.Range(0,listSprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed*Time.deltaTime, 0, 0);
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "limit-scene") Destroy(gameObject);
    }
}
