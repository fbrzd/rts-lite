﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayController : MonoBehaviour
{
    public MenuMain menu;
    public string mode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) menu.GetComponent<MenuMain>().PlayConfig(mode);
    }
}
