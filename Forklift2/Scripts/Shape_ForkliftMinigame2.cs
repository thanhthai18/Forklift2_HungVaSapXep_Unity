using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape_ForkliftMinigame2 : MonoBehaviour
{
    public bool isCorrect = false;
    public Vector3 startPos;
    public bool isFlop;
    public ForkliftObj_ForkliftMinigame2 forkliftObj;
    public Vector2 targetPos;
    public GameObject shineFXPrefab;



    private void Start()
    {
        startPos = transform.position;
        isFlop = true;
        forkliftObj = GameController_ForkliftMinigame2.instance.forkliftObj;
    }

    private void Update()
    {
        if (isFlop)
        {
            transform.position += new Vector3(0, -9.8f, 0) * Time.deltaTime * 0.5f;
            if (transform.position.y < -12)
            {
                transform.position = startPos;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Path"))
        {
            if (gameObject.name.Remove(gameObject.name.IndexOf("("), 7) + "_sample(Clone)" == collision.gameObject.name)
            {

                isCorrect = true;
                targetPos = collision.transform.position;
                if (forkliftObj.currentShape != null)
                {
                    if (forkliftObj.currentShape.isCorrect)
                    {
                        GameController_ForkliftMinigame2.instance.CheckIcon(collision.gameObject);
                        GameController_ForkliftMinigame2.instance.circleTime.gameObject.SetActive(false);
                        GameController_ForkliftMinigame2.instance.StopTimeCoroutine();
                        GameController_ForkliftMinigame2.instance.countShape++;
                        if (GameController_ForkliftMinigame2.instance.countShape == 6)
                        {
                            GameController_ForkliftMinigame2.instance.Win();
                        }
                        GetComponent<Collider2D>().enabled = false;
                        forkliftObj.currentShape.transform.position = forkliftObj.currentShape.targetPos;
                        forkliftObj.currentShape.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1);
                        var tmpFX = Instantiate(shineFXPrefab, forkliftObj.currentShape.transform.position, Quaternion.identity);
                        tmpFX.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
                        {
                            Destroy(tmpFX);
                            if (GameController_ForkliftMinigame2.instance.listCheckSame.Count != 0)
                            {
                                GameController_ForkliftMinigame2.instance.SpawnObj();
                            }
                        });
                        forkliftObj.currentShape.transform.parent = null;
                        forkliftObj.currentShape = null;
                    }
                }
            }
            else
            {
                isCorrect = false;
            }
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            isFlop = false;
            forkliftObj.currentShape = this;
            gameObject.transform.position = collision.transform.GetChild(0).position;
            gameObject.transform.parent = collision.gameObject.transform;
            GameController_ForkliftMinigame2.instance.SetCircleTime();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Path"))
        {
            isCorrect = false;
        }
    }

}
