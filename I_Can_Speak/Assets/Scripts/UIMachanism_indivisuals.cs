using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMachanism_indivisuals : MonoBehaviour
{
    #region [ButtonEvent]
    public string[] Function_name; // 1.Activate, 2.DeActivate, 3.MoveTo~
    public GameObject[] target_Object;
    #endregion

    #region [for BackButton]
    private GameObject Last_DeActivatedGroup;
    private GameObject Now_ActivatedGroup;
    #endregion

    #region [ButtonColor]
    private Color Normal_Color = new Color(1f, 1f, 1f) / 25;

    private Color Button_Normal_Color;
    private Color Button_Highlighted_Color;
    private Color Button_Clicked_Color;

    private float Highlight_strength = 10f;
    private float Click_strength = 5f;
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

        if (CursorIsOn)
        {
            this.GetComponent<Renderer>().material.color = Button_Highlighted_Color;
            if (this.transform.GetChild(0) != null)
            {
                Debug.Log("s");
                this.transform.GetChild(0).gameObject.SetActive(true); // 자식오브젝트 활성화
            }

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

                for (int i = 0; i < Function_name.Length; ++i)
                {
                    switch (Function_name[i])
                    {
                        case "Activate":        //Activate
                            target_Object[i].SetActive(true);
                            Now_ActivatedGroup = target_Object[i];
                            break;

                        case "Deactivate":      //DeActivate
                            target_Object[i].SetActive(false);
                            Last_DeActivatedGroup = target_Object[i];
                            break;

                        case "Move":            //MoveScene
                            SceneManager.LoadScene(this.name.Remove(0, 10));
                            break;
                    }
                }
                thisBtnIsStart = false;
            }

        }
        else if (!thisBtnIsStart && !OVRInput.Get(OVRInput.Button.One))
        {
            this.GetComponent<Renderer>().material.color = Button_Normal_Color;
            if (this.transform.GetChild(0) != null) this.transform.GetChild(0).gameObject.SetActive(false);
        }


        if (OVRInput.GetDown(OVRInput.RawButton.Back)) // 뒤로가기 버튼 -> 상위 메뉴(Last)로 가기
        {
            MatchCollection Last__ = Regex.Matches(Last_DeActivatedGroup.name, "_");
            MatchCollection Now__ = Regex.Matches(Now_ActivatedGroup.name, "_");

            if (Last_DeActivatedGroup != null && Now_ActivatedGroup != null)
                if (Last__.Count < Now__.Count) // _ 의 개수가 적을수록 상위 메뉴다. 즉 Last의 _ 개수가 적으면 실행
                {
                    Last_DeActivatedGroup.SetActive(true);
                    Now_ActivatedGroup.SetActive(false);
                }
        }

    }

}