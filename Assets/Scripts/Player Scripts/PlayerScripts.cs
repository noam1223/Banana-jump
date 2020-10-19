using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{

    private Rigidbody2D myBody;
    private Touch touch;

    public float moveSpeed = 4f;
    public float normalPush = 10f;
    public float extraPush = 14f;

    private bool initialPush;
    private int pushCount;
    private bool playerDied = false;
    public int score = 0;

    public int collectedBananasSoFar = 0;
    public int oneBananaCollected = 0;
    public int threeBananasCollected = 0;

    public string[] texts = { "Awesome!", "Great!", "Good Job", "Excellent Work", "Excellent", "Smoove" };


    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        collectedBananasSoFar = 0;
        oneBananaCollected = 0;
        threeBananasCollected = 0;
#endif
}


    // Update is called once per frame
    void FixedUpdate()
    {

        Move();
        GameManager.instance.addScore(score);

    }



    void Move()
    {

        if (playerDied)
            return;


#if UNITY_IOS || UNITY_ANDROID
        //float dirX = Input.acceleration.x * moveSpeed;
        //myBody.velocity = new Vector2(dirX, myBody.velocity.y);


        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {

                Vector3 touchPosition = Input.GetTouch(0).position;

                if (touchPosition.x > Screen.width * 0.5f)
                    myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
                else if (touchPosition.x < Screen.width * 0.5f)
                    myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            

        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            //transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            //transform.Translate(Vector3.left * Time.deltaTime * 1.5f);
        }
        else myBody.velocity = new Vector2(0f, myBody.velocity.y);
       



#else
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            //transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
        } else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            //transform.Translate(Vector3.left * Time.deltaTime * 1.5f);
        }else myBody.velocity = new Vector2(0f, myBody.velocity.y);

#endif
        //transform.position = Vector3.zero;


    }//Move Function




    private void OnTriggerEnter2D(Collider2D target)
    {

        if (playerDied)
            return;


        if (target.tag == "ExtraPush")
        {
            if (!initialPush)
            {
                initialPush = true;

                myBody.velocity = new Vector2(myBody.velocity.x, 18f);

                target.gameObject.SetActive(false);

                SoundManager.instance.JumpSoundFX();

                score += 3;

                return;
            }

            myBody.velocity = new Vector2(myBody.velocity.x, extraPush);
            target.gameObject.SetActive(false);
            pushCount++;
            SoundManager.instance.JumpSoundFX();
            score += 3;

            threeBananasCollected++;
            collectedBananasSoFar++;

            if (threeBananasCollected >= 3 || collectedBananasSoFar > 10)
            {
                threeBananasCollected = 0;
                collectedBananasSoFar = 0;
                oneBananaCollected = 0;
                GameManager.instance.ShowTextCongrats(texts[Random.Range(0, texts.Length)]);

            }


        }

        if(target.tag == "NormalPush")
        {
            myBody.velocity = new Vector2(myBody.velocity.x, normalPush);
            target.gameObject.SetActive(false);
            pushCount++;
            SoundManager.instance.JumpSoundFX();
            score++;

            oneBananaCollected++;
            collectedBananasSoFar++;

            if (oneBananaCollected >= 7 || collectedBananasSoFar > 10)
            {
                threeBananasCollected = 0;
                collectedBananasSoFar = 0;
                oneBananaCollected = 0;
                GameManager.instance.ShowTextCongrats(texts[Random.Range(0, texts.Length)]);

            }

        }

        if (pushCount == 2)
        {
            pushCount = 0;
            PlatformScript.instance.SpawnPlatforms();
        }


        if(target.tag == "FallDown" || target.tag == "Bird")
        {
            playerDied = true;

            SoundManager.instance.GameOverSoundFX();

            GameManager.instance.GameOverMenu(score);
        }
    }
}
