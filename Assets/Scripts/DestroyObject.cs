using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timer;
    public GameObject particles;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (particles)
            {
                Instantiate(particles, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
