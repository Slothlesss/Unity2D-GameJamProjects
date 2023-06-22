using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Load : MonoBehaviour
{
    // Start is called before the first frame update

    public void TryAgain()
    {
        SceneManager.LoadScene("Menu");
        MenuManager.Instance.hasLoad = false;
    }
}
