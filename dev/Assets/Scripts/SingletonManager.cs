using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager singletonManager;
    public int MIN_X;
    public int MAX_X;
    public int MIN_Y;
    public int MAX_Y;

    public int lastNPlayers;
    public int lastNZones;

    public string mode;
    public Sprite spriteCursorNormal;
    public Sprite spriteCursorAttack;
    public Sprite spriteCursorHarvest;
    public Sprite spriteCursorUnknow;

    public bool flagLAN;
    
    void Start()
    {
        if (SingletonManager.singletonManager != null) Destroy(gameObject);
        else {
            singletonManager = this;
            flagLAN = false;
            SetCursor("normal");
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCursor(string mode){
        if (mode == "normal") Cursor.SetCursor(spriteCursorNormal.texture, Vector2.zero, CursorMode.Auto);
        else if (mode == "attack") Cursor.SetCursor(spriteCursorAttack.texture, Vector2.zero, CursorMode.Auto);
        else if (mode == "harvest") Cursor.SetCursor(spriteCursorHarvest.texture, Vector2.zero, CursorMode.Auto);
        else if (mode == "unknow") Cursor.SetCursor(spriteCursorUnknow.texture, Vector2.zero, CursorMode.Auto);
    }
    /*public void SetCamera(Vector3 vec){
        Camera.main.transform.position = new Vector3(vec.x, vec.y, -10);
        bool flag_y =  && new_pos.y < SingletonManager.singletonManager.MAX_Y - cam.orthographicSize/2;
        bool flag_x = SingletonManager.singletonManager.MIN_X + cam.orthographicSize*cam.aspect/2 < new_pos.x && new_pos.x < SingletonManager.singletonManager.MAX_X - cam.orthographicSize*cam.aspect/2;
        if (SingletonManager.singletonManager.MIN_Y + cam.orthographicSize/2 >= new_pos.y){
            
        }
        
        if (!flag_x || !flag_y){
            cam.transform.position = new_pos;
        }
    }*/
}
