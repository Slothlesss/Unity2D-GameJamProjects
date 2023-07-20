using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private float steerSpeed = 0f;
    private Animator anim;
    private void Awake()
    {
        UIInteraction.OnRightRotate += RightRotate;
        UIInteraction.OnLeftRotate += LeftRotate;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RightRotate();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            LeftRotate();
        }
    }

    public void PlayAnimation()
    {
        anim.SetTrigger("isHit");
    }

    public void RightRotate()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, -steerSpeed));
    }

    public void LeftRotate()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, steerSpeed));
    }

    private void OnDestroy()
    {
        UIInteraction.OnRightRotate -= RightRotate;
        UIInteraction.OnLeftRotate -= LeftRotate;
    }


}
