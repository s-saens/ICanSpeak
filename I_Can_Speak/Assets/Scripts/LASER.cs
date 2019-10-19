using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASER : MonoBehaviour
{
    #region [Variables Statements]
    private LineRenderer LRend;
    private RaycastHit hit;
    private GameObject LastHitObject;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 Zero_position = new Vector3(0, 0, 0);
    public float MaxDist = 20.0f;
    private int LayMask;
    #endregion

    private void Start()
    {
        LRend = GetComponent<LineRenderer>();
        DestroyLaser();
    }

    private void Update()
    {
        startPos = transform.position;
        
        if (Physics.Raycast(startPos, transform.forward, out hit, MaxDist))
        {
            endPos = hit.point;

            if (hit.transform.tag == "Button") // 버튼에 레이저포인터 갖다대면
            {
                hit.transform.GetComponent<UIMachanism_indivisuals>().CursorIsOn = true;
                Debug.Log(hit.transform.GetComponent<UIMachanism_indivisuals>().CursorIsOn);
                LastHitObject = hit.transform.gameObject;
            }
            else if (hit.transform.tag == "Audience") // 청중에 레이저포인터 갖다대면
            {
                LastHitObject = hit.transform.gameObject;
            }
            else if (LastHitObject != null)
            {
                LastHitObject.transform.GetComponent<UIMachanism_indivisuals>().CursorIsOn = false;
            }
        }
        else
        {
            endPos = startPos + (transform.forward * MaxDist);
            if (LastHitObject != null)
            {
                LastHitObject.transform.GetComponent<UIMachanism_indivisuals>().CursorIsOn = false;
            }
        }

        RenderLaser();
    }

    public void RenderLaser()
    {
        LRend.SetPosition(0, startPos);
        LRend.SetPosition(1, endPos);
    }

    private void DestroyLaser()
    {
        LRend.SetPosition(0, Zero_position);
        LRend.SetPosition(1, Zero_position);
    }
    
}
