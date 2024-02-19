using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    public GameObject pointerEffect;
    private Touch touch;
    private Vector3 tPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            touch = Input.GetTouch(0);

            if (Input.touchCount == 1)
            {
                tPosition = Camera.main.ScreenToViewportPoint(touch.position);
                CreateEffect();
            }
        }
    }

    //화면 터치 시 터치 효과
    void CreateEffect()
    {
        tPosition.z = 0;
        Instantiate(pointerEffect, tPosition, Quaternion.identity);

        Destroy(pointerEffect, 0.3f);
    }
}
