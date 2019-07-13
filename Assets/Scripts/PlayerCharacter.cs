using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float movementSpeed;
    public MapLimits limits;
    public GameObject bullet;
    public Transform bulletPosM;
    public Transform bulletPosL;
    public Transform bulletPosR;
    public float shotSpeed;
    int power;
    public AudioClip bulletSound;
    AudioSource bulletAudio;
    // Start is called before the first frame update
    void Start()
    {
        power = 1;
        bulletAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
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
        if(col.gameObject.tag == "powerUp" && power < 3)
        {
            power++;
            Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "powerDown" && power > 1)
        {
            power--;
            Destroy(col.gameObject);
        }
    }
}
