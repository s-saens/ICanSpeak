using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMachanism : MonoBehaviour
{

    #region
    public int Button_type = 0;
    public string[] Function_name;
    public GameObject[] target_Object = new GameObject[3];
    #endregion
    
    #region [ButtonColor]
    private Color Normal_Color = new Color(1f, 1f, 1f) / 25;

    private Color Button_Normal_Color;
    private Color Button_Highlighted_Color;
    private Color Button_Clicked_Color;

    private float Highlight_strength = 10f;
    private float Click_strength = 17f;
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

                
                if(Button_type == 0)
                {
                    for (int i = 0; i < Function_name.Length; ++i)
                    {
                        switch (Function_name[i])
                        {
                            case "Activate":        //Activate
                                Trait_Initialize();
                                target_Object[i].SetActive(true);
                                break;

                            case "Deactivate":      //DeActivate
                                Trait_Initialize();
                                target_Object[i].SetActive(false);
                                break;
                            default :
                                target_Object[i].SendMessage(Function_name[i]);
                                break;
                        }
                    }
                }
                else if(Button_type == 1)
                {
                    Trait_Initialize();
                    SceneManager.LoadScene(Function_name[0]);
                }
                else if(Button_type == 2) {
                    target_Object[0].GetComponent<Text>().text = System.Convert.ToString(System.Convert.ToInt32(target_Object[0].GetComponent<Text>().text) + System.Convert.ToInt32(Function_name[0]));
                }
                else if(Button_type == 3) {
                    target_Object[0].GetComponent<Text>().text = System.Convert.ToString(System.Convert.ToInt32(target_Object[0].GetComponent<Text>().text) - System.Convert.ToInt32(Function_name[0]));
                }
                thisBtnIsStart = false;
            }

        }
        else if (!thisBtnIsStart && !OVRInput.Get(OVRInput.Button.One))
        {
            Trait_Initialize();
        }
        

        if (OVRInput.GetDown(OVRInput.Button.Two)) // 뒤로가기 버튼 -> 이전메뉴로 가기 // 0:자신, 1:다음, 2:이전
        {
            if (target_Object.Length == 3 && target_Object[2] != null && Button_type == 0)
            {
                if(target_Object[0] != null && target_Object[2] != null)
                {
                    Trait_Initialize();
                    target_Object[2].SetActive(true);  //이전거를 활성화
                    target_Object[0].SetActive(false); //자기를 비활성화
                }
            }
        }
    }

    private void Trait_Initialize()
    {
        this.GetComponent<Renderer>().material.color = Button_Normal_Color;
        if (this.transform.GetChild(0) != null) this.transform.GetChild(0).gameObject.SetActive(false);
    }
}