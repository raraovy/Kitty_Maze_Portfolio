using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KittyBubble : MonoBehaviour
{
    GameObject kitty;
    Camera cam;
    public TextMeshProUGUI kittyText;

    private void OnEnable()
    {
        cam = Camera.main;
        LookCamera();
        BubbleText();
    }

    private void LookCamera()
    {
        kitty = GameObject.FindWithTag("Kitty");
        transform.position = new Vector3(kitty.transform.position.x, kitty.transform.position.y + 2.3f, kitty.transform.position.z);
        transform.LookAt(cam.transform);
    }

    private void BubbleText()
    {
        string text_1 = "심심해요!";
        string text_2 = "배고파요!";
        string text_3 = "언제 와요?";

        int ran = Random.Range(1, 4);

        if (ran == 1)
        {
            kittyText.text = text_1;
        }
        else if (ran == 2)
        {
            kittyText.text = text_2;
        }
        else if (ran == 3)
        {
            kittyText.text = text_3;
        }
    }
}
