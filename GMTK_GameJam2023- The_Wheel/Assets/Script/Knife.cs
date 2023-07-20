using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float knifeSpeed;

    [SerializeField] private bool isHit;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, knifeSpeed);
    }

    public void InitializeArrow(int angle, float speed)
    {
        gameObject.transform.Rotate(new Vector3(0, 0, angle));
        rb.velocity = new Vector2(0f, speed);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            rb.velocity = Vector2.zero;
            GameManager.Instance.DisplayFinishedCanvas(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wheel" && isHit == false)
        {
            AudioManager.Instance.PlayArrowHitSound();
            rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<WheelController>().PlayAnimation();
            gameObject.transform.SetParent(collision.transform);
            GameManager.Instance.UpdateTargetArrow();
            if (GameManager.Instance.currentArrows == GameManager.Instance.GetTargetArrows())
            {
                GameManager.Instance.DisplayFinishedCanvas(true);
            }
            isHit = true;
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            GameManager.Instance.DisplayFinishedCanvas(false);
        }
    }
}
