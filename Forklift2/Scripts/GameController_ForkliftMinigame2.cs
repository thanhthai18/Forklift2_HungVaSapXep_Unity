using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_ForkliftMinigame2 : MonoBehaviour
{
    public static GameController_ForkliftMinigame2 instance;

    public bool isWin, isLose, isBegin;
    public Camera mainCamera;
    public List<Transform> listPosTarget = new List<Transform>();
    public List<Transform> listPosSpawn = new List<Transform>();
    public ForkliftObj_ForkliftMinigame2 forkliftObj;
    public Image panelUI;
    public float startSizeCamera;
    public List<Shape_ForkliftMinigame2> listShapePrefab = new List<Shape_ForkliftMinigame2>();
    public List<GameObject> listSamplePrefab = new List<GameObject>();
    public List<int> listCheckSame = new List<int>();
    public int allShape, countShape;
    public Image circleTime;
    public Text txtTime;
    public int time;
    public Coroutine timeCoroutine;
    public List<Image> listIcon = new List<Image>();
    public GameObject tutorial1, tutorial2;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    private void Start()
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(false);
        panelUI.gameObject.SetActive(false);
        startSizeCamera = mainCamera.orthographicSize;
        SetSizeCamera();
        mainCamera.orthographicSize *= 2.0f / 5;
        SetListCheck();
        allShape = 6;
        countShape = 0;
        Intro();
    }

    void SetSizeCamera()
    {
        float f1, f2;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }

    void Intro()
    {
        mainCamera.DOOrthoSize(startSizeCamera, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            isBegin = true;
            panelUI.gameObject.SetActive(true);
            SetUpMap();

            Tutorial();
        });
    }

    public void Begin()
    {
        SpawnObj();
    }
    void Tutorial()
    {
        tutorial1.SetActive(true);
        tutorial2.SetActive(true);
        Tutorial1();
        Tutorial2();
    }

    void Tutorial1()
    {
        tutorial1.transform.DOMoveX(5.8f, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            tutorial1.transform.DOMoveX(-5.13f, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (tutorial1.activeSelf)
                {
                    Tutorial1();
                }
            });
        });
    }

    void Tutorial2()
    {
        tutorial2.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 2).SetLoops(-1);
    }

    void SetListCheck()
    {
        int[] a = { 0, 1, 2, 3, 4, 5 };
        listCheckSame.AddRange(a);
    }

    void SetUpMap()
    {
        for(int i = 0; i < listPosTarget.Count; i++)
        {
            Instantiate(listSamplePrefab[i], listPosTarget[i].position, Quaternion.identity);
        }
    }
    
    public void SpawnObj()
    {
        int ran = Random.Range(0, listCheckSame.Count);
        Instantiate(listShapePrefab[listCheckSame[ran]], listPosSpawn[Random.Range(0, listPosSpawn.Count)].position, Quaternion.identity);
        listCheckSame.RemoveAt(ran);
    }

    public void SetCircleTime()
    {
        time = 10;
        txtTime.text = time.ToString();
        timeCoroutine = StartCoroutine(CountTime());
        circleTime.fillAmount = 1;
        circleTime.gameObject.SetActive(true);
    }

    public void StopTimeCoroutine()
    {
        StopCoroutine(timeCoroutine);
    }

    public IEnumerator CountTime()
    {
        while(time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            txtTime.text = time.ToString();
            circleTime.fillAmount = time * 0.1f / 1;
            if(time == 0)
            {
                Lose();
            }
            
        }
    }

    public void CheckIcon(GameObject checkObj)
    {
        listIcon.ForEach(s =>
        {
            if (s.name == checkObj.name)
            {
                s.transform.GetChild(0).gameObject.SetActive(false);
                s.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1);
            }
        });
    }

    public void Win()
    {
        isWin = true;
        Debug.Log("Win");
        forkliftObj.transform.DOMoveX(forkliftObj.transform.position.x + 20, 2);
    }

    public void Lose()
    {
        isLose = true;
        StopAllCoroutines();
        Debug.Log("Thua");

    }
}
