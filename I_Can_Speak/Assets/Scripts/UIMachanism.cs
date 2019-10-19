using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMachanism : MonoBehaviour
{
    public GameObject PresentationScreen;

    #region [ButtonEvent]
    [SerializeField]
    public static int Object_Cnt;
    [SerializeField]
    public int[] Func_index = new int[Object_Cnt];
    [SerializeField]
    public GameObject[] target_Object = new GameObject[Object_Cnt];
    #endregion

    #region [ButtonColor]
    private Color Normal_Color = new Color(1f, 1f, 1f) / 25;

    private Color Button_Now_Color;
    private Color Button_Normal_Color;
    private Color Button_Highlighted_Color;
    private Color Button_Clicked_Color;

    public float Highlight_strength = 40f;
    public float Click_strength = 50f;
    #endregion

    #region [Boolean]
    public bool CursorIsOn = false;     // 커서(레이저) 닿음
    public bool thisBtnIsStart = false; // GetDown(딸깍-)된 버튼이 이 버튼인가?
    #endregion

    private void Start()
    {
        // 버튼상태에 따른 버튼 material의 색깔
        Button_Normal_Color = this.GetComponent<Renderer>().material.color;
        Button_Highlighted_Color = Button_Normal_Color - (Normal_Color * Highlight_strength);
        Button_Clicked_Color = Button_Normal_Color - (Normal_Color * Click_strength);
    }

    private void Update()
    {
        Button_Now_Color = this.GetComponent<Renderer>().material.color;
        this.GetComponent<Renderer>().material.color = Button_Highlighted_Color;

        if (CursorIsOn)
        {
            this.GetComponent<Renderer>().material.color = Button_Clicked_Color;
            if (this.transform.GetChild(0) != null) this.transform.GetChild(0).gameObject.SetActive(true);

            if (OVRInput.GetDown(OVRInput.Button.One)) // 딸깍- 버튼누르기시작할 때
            {
                thisBtnIsStart = true;
                this.GetComponent<Renderer>().material.color = Button_Clicked_Color;
            }

            if (OVRInput.GetDown(OVRInput.Button.One)) // 꾹--
            {
                this.GetComponent<Renderer>().material.color = Button_Clicked_Color;
            }



            ///////* 버튼 이벤트 처리부분 *////////

            if (OVRInput.GetUp(OVRInput.Button.One) && thisBtnIsStart) // 뙇-뗐을때
            {
                this.GetComponent<Renderer>().material.color = Button_Normal_Color;

                ///* 씬 이동버튼 *///
                if (this.name.Contains("Button_To_"))
                {
                    SceneManager.LoadScene(this.name.Remove(0, 10));
                }



                thisBtnIsStart = false;
            }

        }

        else if (!thisBtnIsStart && !OVRInput.Get(OVRInput.Button.One))
        {
            this.GetComponent<Renderer>().material.color = Button_Normal_Color;
            if (this.transform.GetChild(0) != null) this.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

}