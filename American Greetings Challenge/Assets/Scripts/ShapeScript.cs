using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    Transform transform;
    PolygonCollider2D collider;
    Rigidbody2D body;
    SpriteRenderer renderer;
    AudioSource audioSrc;

    Touch touch;

    int consecutiveClicks;
    float clickTimer;
    [SerializeField] float doubleClickTime;

    [SerializeField] Vector2 jumpForce;
    [SerializeField] float force = 1f;
    [SerializeField] float forceDefault = 1f;
    [SerializeField] float forceMultiplier = 0.1f;

    [SerializeField] Color defaultColor = Color.white;

    [SerializeField] AudioClip touchSound;

    private void Start()
    {
        transform = GetComponent<Transform>();
        collider = GetComponent<PolygonCollider2D>();
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();

        if(ScoreSystem.scoreSystem == null)
        {
            GameObject.FindObjectOfType<ScoreSystem>();
        }
        ScoreSystem.scoreSystem.ResetMultiplier();
        force = forceDefault;
    }

    private void Update()
    {
        //Count down the click timer if the shape has been clicked on more than once.
        if(consecutiveClicks >= 1)
        {
            if (clickTimer > 0)
            {
                clickTimer -= Time.deltaTime;
            }
        }
    }
    
    //When the shape collides with another collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            GroundCollide();
        }
        else
        {
            audioSrc.pitch = Random.Range(.65f, .75f);
            audioSrc.PlayOneShot(touchSound);
        }
    }
    
    //When the shape has been clicked on, check if it is a double click, add a point, and jump the shape.
    private void OnMouseDown()
    {
        ++consecutiveClicks;
        if (clickTimer <= 0)
        {
            clickTimer = doubleClickTime;
        }
        else
        {
            DoubleClick();
        }
        ScoreSystem.scoreSystem.AddPoint();
        Jump();
    }

    //Reset the click timer, multiply the score by 2, and change the color
    void DoubleClick()
    {
        clickTimer = 0;
        ChangeColor();
        ScoreSystem.scoreSystem.SetMultiplier(ScoreSystem.scoreSystem.GetMultiplier() * 2);
    }

    //When the shape collides with the ground, reset the color and score.
    void GroundCollide()
    {
        audioSrc.pitch = Random.Range(.45f, .55f);
        audioSrc.PlayOneShot(touchSound);
        consecutiveClicks = 0;
        force = forceDefault;
        ScoreSystem.scoreSystem.ResetScore();
        ResetColor();
    }

    //Randomly change color
    void ChangeColor()
    {
        Color newColor = renderer.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        ScoreSystem.scoreSystem.SetScoreColor(newColor);
    }

    void ResetColor()
    {
        renderer.color = defaultColor;
    }

    public void ResetPosition()
    {
        transform.position = Vector2.zero;
    }

    void Jump()
    {
        audioSrc.pitch = Random.Range(1, 1.75f);
        audioSrc.PlayOneShot(touchSound);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 forcePosition = new Vector2(transform.position.x - touchPosition.x, 1);
        body.AddForce(forcePosition * force);
        force *= forceMultiplier;
    }

    private void OnDestroy()
    {
        ScoreSystem.scoreSystem.ResetScore();
    }
}
