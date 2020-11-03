using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLANController : MonoBehaviour
{
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
        if (Input.GetMouseButtonDown(0)){
            SingletonManager.singletonManager.flagLAN = !SingletonManager.singletonManager.flagLAN;
            print(SingletonManager.singletonManager.flagLAN);
            SceneManager.LoadScene("TestMirrorScene");
        }
    }
}
