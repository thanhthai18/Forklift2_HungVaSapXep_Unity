using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiaDo_ForkliftMinigame2 : MonoBehaviour
{
    public float maxPos, minPos;
    public Vector3 vecDirection;

    private void Start()
    {
        maxPos = 3.12f;
        minPos = transform.position.y;
        vecDirection = Vector3.up;
    }


    private void Update()
    {
        if (transform.parent.GetComponent<ForkliftObj_ForkliftMinigame2>().isNangGiaDo && !GameController_ForkliftMinigame2.instance.isLose && !GameController_ForkliftMinigame2.instance.isWin)
        {
            transform.Translate(vecDirection * Time.deltaTime * 5);
            if (transform.position.y > maxPos)
            {
                vecDirection = Vector3.down;
            }
            if(transform.position.y < minPos)
            {
                vecDirection = Vector3.up;
            }
        }
    }
}
