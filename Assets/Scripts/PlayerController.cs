using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 키 눌렀을 때
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isIdle", true);
        }
    }
}
