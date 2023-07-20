using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    public static event System.Action OnRightRotate;
    public static event System.Action OnLeftRotate;

    [SerializeField] private Button rightRotateButton;
    [SerializeField] private Button leftRotateButton;

    public void OnRightPointerDown()
    {
        InvokeRepeating("RepeatingRightRotate", 0f, 0.02f);
    }
    public void OnLeftPointerDown()
    {
        InvokeRepeating("RepeatingLeftRotate", 0f, 0.02f);
    }

    private void RepeatingRightRotate()
    {
        OnRightRotate.Invoke();
    }

    private void RepeatingLeftRotate()
    {
        OnLeftRotate.Invoke();
    }

    public void OnRightPointerUp()
    {
        CancelInvoke("RepeatingRightRotate");
    }
    public void OnLeftPointerUp()
    {
        CancelInvoke("RepeatingLeftRotate");
    }


}
