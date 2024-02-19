using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImage;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            return;

        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
