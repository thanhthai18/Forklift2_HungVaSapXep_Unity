using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftObj_ForkliftMinigame2 : MonoBehaviour
{
    public bool isLeft, isRight;
    public Camera mainCamera;
    public Vector2 mousePos;
    public bool isNangGiaDo = false;
    public bool isMoveCar = false;
    public Vector2 boundSizeCam;
    public Vector2 boundSizeMyCar;
    public Vector2 deltaDistance;
    public Vector2 lastPos;
    public Shape_ForkliftMinigame2 currentShape;
   


    private void Start()
    {
        SetUpBoundSize();
    }

    private void OnMouseDown()
    {
        isNangGiaDo = true;
        isMoveCar = false;
        if (GameController_ForkliftMinigame2.instance.tutorial1.activeSelf)
        {
            GameController_ForkliftMinigame2.instance.tutorial1.SetActive(false);
            GameController_ForkliftMinigame2.instance.tutorial1.transform.DOKill();
            GameController_ForkliftMinigame2.instance.tutorial2.SetActive(false);
            GameController_ForkliftMinigame2.instance.tutorial2.transform.DOKill();
            GameController_ForkliftMinigame2.instance.Begin();
        }
    }
    private void OnMouseUp()
    {
        //Check Shape Correct
        isNangGiaDo = false;
        
    }

    public void SetUpBoundSize()
    {
        if (GetComponent<Collider2D>() != null)
        {
            boundSizeCam = new Vector2(mainCamera.orthographicSize * Screen.width * 1.0f / Screen.height, mainCamera.orthographicSize);
            boundSizeMyCar = new Vector2(GetComponent<Collider2D>().bounds.extents.x, GetComponent<Collider2D>().bounds.extents.y);
        }
    }
    public void CheckPosMove()
    {
        isMoveCar = true;
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        deltaDistance = mousePos - (Vector2)transform.position;
    }

    public void FlipCar()
    {
        if (isMoveCar)
        {
            if (transform.position.x > lastPos.x)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            if (transform.position.x < lastPos.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        lastPos = transform.position;
    }

    public void MoveCar()
    {
        if(!GameController_ForkliftMinigame2.instance.isWin && !GameController_ForkliftMinigame2.instance.isLose)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePos.x - deltaDistance.x, transform.position.y);
            if (transform.position.x > boundSizeCam.x)
            {
                transform.position = new Vector2(boundSizeCam.x, transform.position.y);
            }
            if (transform.position.x < -boundSizeCam.x)
            {
                transform.position = new Vector2(-boundSizeCam.x, transform.position.y);
            }
        }      
    }


    private void Update()
    {
        if (!GameController_ForkliftMinigame2.instance.isLose && !GameController_ForkliftMinigame2.instance.isWin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isNangGiaDo)
                {
                    isMoveCar = true;
                    CheckPosMove();
                    if (GameController_ForkliftMinigame2.instance.tutorial1.activeSelf)
                    {
                        GameController_ForkliftMinigame2.instance.tutorial1.SetActive(false);
                        GameController_ForkliftMinigame2.instance.tutorial1.transform.DOKill();
                        GameController_ForkliftMinigame2.instance.tutorial2.SetActive(false);
                        GameController_ForkliftMinigame2.instance.tutorial2.transform.DOKill();
                        GameController_ForkliftMinigame2.instance.Begin();
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMoveCar = false;                  
            }
        }

        if (isMoveCar)
        {
            MoveCar();
        }

        
    }

    private void LateUpdate()
    {
        FlipCar();
    }
}
