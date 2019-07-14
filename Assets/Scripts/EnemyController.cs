using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float changeTimer;
    public int scoreReward;
    public GameObject powerUp;
    public GameObject powerDown;
    float maxTimer;
    public bool isDirectionSwitch;
    Rigidbody enemyRig;
    public int hp;
    public GameObject particles;
    public MapLimits limits;
    // Start is called before the first frame update
    void Start()
    {
        maxTimer = changeTimer;
        enemyRig = GetComponent<Rigidbody>();
        Debug.Log(limits.minimumX);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (transform.position.x == limits.maximumX) isDirectionSwitch = isDirectionSwitch ? false : true;
        if (transform.position.x == limits.minimumX) isDirectionSwitch = isDirectionSwitch ? false : true;
        //Map Limit
        float x = Mathf.Clamp(transform.position.x, limits.minimumX, limits.maximumX);
        float y = Mathf.Clamp(transform.position.y, limits.minimumY, limits.maximumY);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    //Move Enemy
    void Movement()
    {
        if (isDirectionSwitch)
        {
            enemyRig.velocity = new Vector3(speed * Time.deltaTime, -speed * Time.deltaTime, 0);
        }
        else
        {
            enemyRig.velocity = new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
        }
        SwitchDirection();
    }

    // switch direction
    void SwitchDirection()
    {
        changeTimer -= Time.deltaTime;
        if(changeTimer < 0)
        {
            isDirectionSwitch = isDirectionSwitch ? false : true ;
            changeTimer = maxTimer;
        }
    }

    //Enemy damage
    private void OnTriggerEnter(Collider collider)
    {
        // bullet
        if ( collider.gameObject.tag == "friendlyBullet")
        {
            Instantiate(particles, collider.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(collider.gameObject);
            hp--;
            destroyEnemy();
        }

        // Character damage
        if (collider.gameObject.tag == "playerShip")
        {
            Instantiate(particles, collider.gameObject.transform.position, gameObject.transform.rotation);
            collider.gameObject.GetComponent<PlayerCharacter>().hp--;
            hp--;
            destroyEnemy();
        }

    }

    void destroyEnemy()
    {
        if (hp < 0)
        {
            int randNum = Random.Range(0, 100);

            if (randNum >= 60 ) {
                Instantiate(powerUp, transform.position, transform.rotation);
            }

            if (randNum <= 40) {
                Instantiate(powerDown, transform.position, transform.rotation);
            }
            GameObject.FindGameObjectWithTag("playerShip").GetComponent<PlayerCharacter>().score += scoreReward;
            Destroy(gameObject);
        }
    }
}
