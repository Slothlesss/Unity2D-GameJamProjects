using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject finishedCanvas;
    [SerializeField] private TextMeshProUGUI targetArrowsText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button restartButton;

    public static GameManager Instance;

    [SerializeField] private int targetArrows;
    public int currentArrows = 0;
    public bool isEnded = false;

    private Animator targetArrowsTextAnimator;
    [SerializeField] private bool isLastLevel;

    private void Awake()
    {
        Instance = this;
        nextButton.onClick.AddListener(() =>
        {
            LoadNextScene();
        });
        restartButton.onClick.AddListener(() =>
        {
            ReloadCurrentScene();
        });
    }

    private void Start()
    {
        targetArrowsTextAnimator = targetArrowsText.GetComponent<Animator>();
        targetArrowsText.text = (targetArrows - currentArrows).ToString();
    }

    public int GetTargetArrows()
    {
        return targetArrows;
    }

    public void UpdateTargetArrow()
    {
        targetArrowsTextAnimator.SetTrigger("isChange");
        currentArrows += 1;
        targetArrowsText.text = (targetArrows - currentArrows).ToString();
    }
    public void DisplayFinishedCanvas(bool isWin)
    {

        isEnded = true;
        finishedCanvas.SetActive(true);
        if (isWin == false)
        {
            resultText.text = "You Failed";
            nextButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
        }
        else if (isLastLevel == true)
        {
            resultText.text = "You have passed all the levels. Thank you for playing my game <3";
            resultText.fontSize = 100;
            nextButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }
        else
        {
            resultText.text = "Congratulation ^^";
            nextButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);

        }


    }
    public void ReloadCurrentScene()
    {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx);
    }
    public void LoadNextScene()
    {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx + 1);
    }

}
