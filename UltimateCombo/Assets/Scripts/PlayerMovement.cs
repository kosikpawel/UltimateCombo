using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float timeDeath;
    public GameObject bubbleGenerator;
    public static bool playerWasHit;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Color mainColorCopy;
    private bool revivingIsActivated = false;
    private float speedCopy;
    private bool speedEffectsAreActivated;
    public bool control; //public jezeli uzywany jest skrypt do poruszania sie na komputerze
    private int directionPhase;
    public float glichBlokerTime;

    Vector2 move;
    void Start()
    {
        control = true;
        playerWasHit = false;
        speedCopy = speed;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        mainColorCopy = sr.color;
        move = new Vector2(0, 0);
        speedEffectsAreActivated = false;
        directionPhase = 0;
    }

    void FixedUpdate()
    {
        if (Input.touchCount == 1 && control) {

            Touch tap = Input.GetTouch(0);

            if (tap.deltaTime < 1f) {
                Vector2 direction = tap.deltaPosition;
                if (direction.x > Mathf.Abs(direction.y) && directionPhase != 1) {
                    rb.velocity = new Vector2(speed, 0f);
                    directionPhase = 1;
                    StopCoroutine("GlichBlock");
                    StartCoroutine("GlichBlock");
                }
                else if (direction.x < -Mathf.Abs(direction.y) && directionPhase != 2) {
                    rb.velocity = new Vector2(-speed, 0f);
                    directionPhase = 2;
                    StopCoroutine("GlichBlock");
                    StartCoroutine("GlichBlock");
                }
                else if (direction.y > Mathf.Abs(direction.x) && directionPhase != 3) {
                    rb.velocity = new Vector2(0f, speed);
                    directionPhase = 3;
                    StopCoroutine("GlichBlock");
                    StartCoroutine("GlichBlock");
                }
                else if (direction.y < -Mathf.Abs(direction.x) && directionPhase != 4) {
                    rb.velocity = new Vector2(0f, -speed);
                    directionPhase = 4;
                    StopCoroutine("GlichBlock");
                    StartCoroutine("GlichBlock");
                }

            }
        }
    }
    public void PlayerJumpsOnWall(int direction, float timeWithoutControl)
    {
        control = false;
        switch (direction)
        {
            case 1:
                move = new Vector2(speed, 0f);
                break;
            case 2:
                move = new Vector2(-speed, 0f);
                break;
            case 3:
                move = new Vector2(0f, speed);
                break;
            case 4:
                move = new Vector2(0f, -speed);
                break;
        }
        rb.velocity = move;
        directionPhase = 0;
        StopCoroutine("GlichBlock");
        StartCoroutine(ControlIsBack(timeWithoutControl));
    }

    public void SetSpeedStat( float statSpeed )
    {
        speed = statSpeed;
        speedCopy = statSpeed;
    }

    public void OnDeathWall()
    {
        if (revivingIsActivated)
        {
            StopCoroutine("Revive");
        }

        if ( GetComponent<PlayerHPSystem>().PlayerIsHit() )
        {
            playerWasHit = true;
            transform.position = new Vector3(0, 0, 0);
            Color x = Color.gray;
            x.a = 0.7f;
            sr.color = x;
            rb.velocity = new Vector2(0, 0);

            revivingIsActivated = true;
            StartCoroutine("Revive");
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    public void PlayerSpeedEffects(float newSpeedProcent, float timeToGetNormalSpeedBack, bool deathOnSenondSlow = false, string addToInfo = "")
    {
        if (!speedEffectsAreActivated)
        {
            speedEffectsAreActivated = true;
            bubbleGenerator.GetComponent<ComboTaker>().infoGenerator.MakeInfoObject(newSpeedProcent.ToString("0") + "% Player Speed\n" + addToInfo);
            speed = newSpeedProcent * speed * 0.01f;
            GetComponent<TemporaryPlayerMovement>().force = speed; //dodatek do testowania na komputerze
            StartCoroutine(PlayerSpeedEffectsEnd(timeToGetNormalSpeedBack));
        }
        else if (deathOnSenondSlow)
        {
            OnDeathWall();
            bubbleGenerator.GetComponent<ComboTaker>().infoGenerator.MakeInfoObject("Slow + Slow = Death");
        }
        else
        {
            bubbleGenerator.GetComponent<ComboTaker>().infoGenerator.MakeInfoObject("Slow + Slow = Have a rest");
        }
    }

    IEnumerator Revive()
    {
        yield return new WaitForSeconds(timeDeath);
        sr.color = mainColorCopy;

        GetComponent<PlayerCollision>().shadowMode = false;
        transform.tag = "Player";
        revivingIsActivated = false;
        playerWasHit = false;
    }

    IEnumerator PlayerSpeedEffectsEnd( float time )
    {
        yield return new WaitForSeconds(time);
        speed = speedCopy;
        GetComponent<TemporaryPlayerMovement>().force = speedCopy; //zeby dzialalo na komputerze
        bubbleGenerator.GetComponent<ComboTaker>().infoGenerator.MakeInfoObject("Normal Player Speed");
        speedEffectsAreActivated = false;
    }
    IEnumerator ControlIsBack(float time)
    {
        yield return new WaitForSeconds(time);
        control = true;
    }
    IEnumerator GlichBlock(float time) {
        yield return new WaitForSeconds(time);
        directionPhase = 0;
    }
}
