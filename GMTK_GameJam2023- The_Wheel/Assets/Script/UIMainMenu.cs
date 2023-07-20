using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIdx + 1);
        });
    }

}
