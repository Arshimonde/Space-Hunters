using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerCharacter : MonoBehaviour
{
    public int hp;
    public int score;
    int highScore;
    public Text scoreText;
    public Text highScoreText;
    public float movementSpeed;
    public MapLimits limits;
    public GameObject bullet;
    public Transform bulletPosM;
    public Transform bulletPosL;
    public Transform bulletPosR;
    public float shotSpeed;
    public GameObject ship;
    int power;
    public AudioClip bulletSound;
    AudioSource bulletAudio;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        power = 1;
        bulletAudio = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("highScore"))
        {
            highScore = 0;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        scoreText.text = score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();

        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    //Movements
    void Movement()
    {
        //move top
        if (Input.GetKey(KeyCode.Z) && transform.position.y <= limits.maximumY)
        {
             transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
        }
        //move down
        if (Input.GetKey(KeyCode.S) && transform.position.y >= limits.minimumY)
        {
            transform.Translate(Vector3.up * -movementSpeed * Time.deltaTime);
        }

        //move left
        if (Input.GetKey(KeyCode.Q) && transform.position.x >= limits.minimumX)
        {
            transform.Translate(Vector3.right * -movementSpeed * Time.deltaTime);
        }

        //move right
        if (Input.GetKey(KeyCode.D) && transform.position.x <= limits.maximumX)
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
    }

    //Shooting 
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletAudio.PlayOneShot(bulletSound);
            switch (power)
            {
                //1 power
                case 1:
                    {
                        createBullet(bulletPosM);
                        break;
                    }
                //2 power
                case 2:
                    {
                        createBullet(bulletPosL);
                        createBullet(bulletPosR);
                        break;
                    }
                //3 power
                case 3:
                    {
                        createBullet(bulletPosL);
                        createBullet(bulletPosM);
                        createBullet(bulletPosR);
                        break;
                    }
                //default power
                default:
                    {
                        createBullet(bulletPosM);
                        break;
                    }
            }
        }
 
    }

    void createBullet(Transform transform)
    {
        GameObject newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = Vector3.up * shotSpeed;
    }


    // Collider TRIGGER
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "powerUp" )
        {
            if( power < 3)
                power++;
            Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "powerDown" )
        {
            if (power > 1)
                power--;
            Destroy(col.gameObject);
        }

        // Enemy Collider
        if(col.gameObject.tag == "enemy")
        {
            if( hp < 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
    }
}
