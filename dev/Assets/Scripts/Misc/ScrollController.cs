using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    //public GameObject myCamera;

    public Vector3 dir;
    bool flagOn;

    // Start is called before the first frame update
    void Start()
    {
        flagOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flagOn){
            Camera cam = Camera.main;
            Vector3 new_pos = cam.transform.position + dir*Time.deltaTime;
            bool flag_y = SingletonManager.singletonManager.MIN_Y + cam.orthographicSize/2 < new_pos.y && new_pos.y < SingletonManager.singletonManager.MAX_Y - cam.orthographicSize/2;
            bool flag_x = SingletonManager.singletonManager.MIN_X + cam.orthographicSize*cam.aspect/2 < new_pos.x && new_pos.x < SingletonManager.singletonManager.MAX_X - cam.orthographicSize*cam.aspect/2;
            //print(cam.orthographicSize);
            //print(cam.orthographicSize*cam.aspect);
            //print("position: "+cam.transform.position);
            if (flag_x && flag_y){
                cam.transform.position = new_pos;
            }
            //Vector3 new_pos = dir*Time.deltaTime
            //cam.orthographicSize +
            //myCamera.GetComponent<Transform>().position += dir*Time.deltaTime;//, dir[1]*Time.deltaTime, 0);
        }
    }
    void OnMouseEnter()
    {
        flagOn = true;
    }
    void OnMouseExit()
    {
        flagOn = false;
    }
}
