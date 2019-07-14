using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public MapLimits limits;
    public GameObject invader1;
    public GameObject invader2;
    public GameObject invader3;
    public float spawnTimer;
    float maxSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer < 0)
        {
            spawnInvader();
            spawnTimer = maxSpawnTimer;
        }
    }

    void spawnInvader()
    {
        int randomNumber = Random.Range(0, 3);
        switch (randomNumber)
        {
            case 0:
                {
                    createInvader(invader1);
                    break;
                }
            case 1:
                {
                    createInvader(invader2);
                    break;
                }
            case 2:
                {
                    createInvader(invader3);
                    break;
                }
        }
    }

    void createInvader(GameObject invader)
    {
        float randomX = Random.Range(limits.minimumX, limits.maximumX);
        float randomY = Random.Range(limits.minimumY, limits.maximumY);
        Instantiate(invader, new Vector3(randomX, randomY), invader.transform.rotation);
    }
}
