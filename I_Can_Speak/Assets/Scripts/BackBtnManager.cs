using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackBtnManager : MonoBehaviour
{
    public GameObject Last_DeActivatedGroup, Now_ActivatedGroup;
    
    private void Update()
    {
        if (Last_DeActivatedGroup != null && Now_ActivatedGroup != null)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.Back)) // 뒤로가기 버튼 -> 상위 메뉴(Last)로 가기
            {
                MatchCollection Last__ = Regex.Matches(Last_DeActivatedGroup.name, "_");
                MatchCollection Now__ = Regex.Matches(Now_ActivatedGroup.name, "_");

                if (Last_DeActivatedGroup != null && Now_ActivatedGroup != null)
                {
                    if (Last__.Count < Now__.Count) // _ 의 개수가 적을수록 상위 메뉴다. 즉 Last의 _ 개수가 적으면 실행
                    {
                        Last_DeActivatedGroup.SetActive(true);
                        Now_ActivatedGroup.SetActive(false);
                    }
                }
            }
        }
    }
}
