using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    private static float left;
    private static float right;
    private static float jump;
    
    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_ANDROID && !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_EDITOR //primjer za touch faze
        if (Input.touchCount <= 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("touch began");
        }

        if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("touch moved");
        }

        if (touch.phase == TouchPhase.Stationary)
        {
            Debug.Log("touch stationary");
        }

        if (touch.phase == TouchPhase.Canceled)
        {
            Debug.Log("touch canceled");
        }

        if (touch.phase == TouchPhase.Ended)
        {
            Debug.Log("touch ended");
        }
#endif
    }

    public void OnRightButtonDown()
    {
        right = 1;
    }

    public void OnRightButtonUp()
    {
        right = 0;
    }

    public void OnLeftButtonDown()
    {
        left = -1;
    }

    public void OnLeftButtonUp()
    {
        left = 0;
    }

    public void OnClickJumpButton()
    {
        jump = 1;
    }

    public static void ResetJump()
    {
        jump = 0;
    }

    public static float GetJumpDirection()
    {
        return jump;
    }

    public static float GetXDirection()
    {
        if (right != 0 && left != 0)
        {
            return 0;
        }

        if (right > 0)
        {
            return right;
        }
        
        if (left < 0)
        {
            return left;
        }

        return 0;
    }

}
