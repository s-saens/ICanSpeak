using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class Presentation : MonoBehaviour
{
    public GameObject TIMERTEXT;
    public GameObject StartHelp;

    private Object[] textures;
    private int index = 0;
    public bool loaded = false;
    private Vector2 TouchPosition;

    private void Start()
    {
        textures = Resources.LoadAll("Presentation", typeof(Texture));
        foreach (var t in textures)
        {
            Debug.Log(t.name);
        }
        return;
    }

    private void Update()
    {
        TouchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad); // 터치좌표

        if (OVRInput.GetDown(OVRInput.Button.One)) // 터치클릭
        {
            if (TouchPosition.y < 0.5 && TouchPosition.y > -0.5)
            {
                if (TouchPosition.x > 0.5) Next();
                else if (TouchPosition.x < -0.5) Prev();
                else if (!loaded) FirstLoad();
            }
        }
    }

    public void Next()
    {
        if (index >= textures.Length - 1)
        {
            if (loaded == true) Debug.Log("This slide is the last one.");
            return;
        }
        else if (loaded == true)
        {
            index++;
            LOAD(index);
            return;
        }
    }

    public void Prev()
    {
        if (index <= 0)
        {
            if (loaded == true) Debug.Log("This slide is the first one.");
            return;
        }
        else if (loaded == true)
        {
            index--;
            LOAD(index);
            return;
        }
    }

    public void FirstLoad()
    {
        index = 0;
        LOAD(0);
        loaded = true;
        StartHelp.SetActive(false);
        TIMERTEXT.SetActive(true);
        TIMERTEXT.GetComponent<StopWatch>().timerActive = true;
        return;
    }

    private void LOAD(int ind)
    {
        Texture tex = (Texture)textures[ind];
        this.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        this.GetComponent<Renderer>().material.mainTexture = tex;
        return;
    }
}
