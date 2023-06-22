using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public Animator anim1;
    public ItemInfo[] endings;
    public TextMeshProUGUI[] endingUI;

    public GameObject NoEnding;
    public GameObject Ending1;
    public GameObject Ending2;
    public GameObject EndingBoth;

    public GameData data;
    public bool hasLoad;
    public int firstTime = 0;
    static public MenuManager Instance;
    public void Awake()
    {
        Instance = this;
        hasLoad = false;
    }

    public void Start()
    {
        if(GameObject.FindObjectsOfType<MenuManager>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
    public void Update()
    {
        if (hasLoad == false)
        {
            if (SceneManager.GetActiveScene().name == "Ending1")
            {
                hasLoad = true;
                anim1 = GameObject.FindWithTag("Anim").GetComponent<Animator>();
                endingUI[0] = GameObject.FindWithTag("Ending1").GetComponent<TextMeshProUGUI>();
                LoadEnding1();
                
            }
            else if (SceneManager.GetActiveScene().name == "Ending2")
            {
                hasLoad = true;
                anim1 = GameObject.FindWithTag("Anim").GetComponent<Animator>();
                endingUI[1] = GameObject.FindWithTag("Ending2").GetComponent<TextMeshProUGUI>();
                LoadEnding2();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            anim1 = null;
        }
    }
    public void EndingCollection()
    {
        if(data.finishEnding1 == false)
        {
            if(data.finishEnding2 == false)
            {
                NoEnding.SetActive(true);
                Ending1.SetActive(false);
                Ending2.SetActive(false);
                EndingBoth.SetActive(false);
            }
            else
            {
                NoEnding.SetActive(false);
                Ending1.SetActive(false);
                Ending2.SetActive(true);
                EndingBoth.SetActive(false);
            }
        }
        else
        {
            if (data.finishEnding2 == false)
            {
                NoEnding.SetActive(false);
                Ending1.SetActive(true);
                Ending2.SetActive(false);
                EndingBoth.SetActive(false);
            }
            else
            {
                NoEnding.SetActive(false);
                Ending1.SetActive(false);
                Ending2.SetActive(false);
                EndingBoth.SetActive(true);
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadEnding1()
    {
        if (SceneManager.GetActiveScene().name == "Ending1")
        {
            StartCoroutine(TextByText1(endingUI[0]));
        }
    }
    public void LoadEnding2()
    {
        if (SceneManager.GetActiveScene().name == "Ending2")
        {
            StartCoroutine(TextByText2(endingUI[1]));
        }
    }
    public void TryAgain()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    IEnumerator TextByText1(TextMeshProUGUI text)
    {
        if (SceneManager.GetActiveScene().name == "Ending1")
        {
            for (int i = 0; i < endings[0].itemDescription.Length; i++)
            {
                text.text += endings[0].itemDescription[i];
                yield return new WaitForSeconds(0.075f);
            }
            anim1.SetTrigger("Load");
            yield return null;
        }
    }
    IEnumerator TextByText2(TextMeshProUGUI text)
    {
        if (SceneManager.GetActiveScene().name == "Ending2")
        {
            for (int i = 0; i < endings[1].itemDescription.Length; i++)
            {
                text.text += endings[1].itemDescription[i];
                yield return new WaitForSeconds(0.075f);
            }
            anim1.SetTrigger("Load");
            yield return null;
        }
    }
}
