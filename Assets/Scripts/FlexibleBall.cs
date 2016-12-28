using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleBall : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip audioClipBounce;
    bool growing = true;
    int wobbleCount = 0;
    int maxWobbleCount = 200;
    float deltaScaleX;
    float deltaScaleY;
    float deltaScaleZ;
    float startScaleX;
    float startScaleY;
    float startScaleZ;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        startScaleX = transform.localScale.x;
        startScaleY = transform.localScale.y;
        startScaleZ = transform.localScale.z;

        deltaScaleX = Random.Range(0.05f, 0.15f);
        deltaScaleY = Random.Range(0.05f, 0.15f);
        deltaScaleZ = Random.Range(0.05f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        Wobble();
    }

    void Wobble()
    {
        if (wobbleCount == 0)
        {
            transform.localScale = new Vector3(
                    startScaleX,
                    startScaleY,
                    startScaleZ);
            return;
        }
        wobbleCount--;
        if (wobbleCount == 1)
        {
            transform.localScale = new Vector3(
                    startScaleX,
                    startScaleY,
                    startScaleZ);
            return;
        }
        
        Debug.Log(wobbleCount);
        if (transform.localScale.x > 0.0f && transform.localScale.x < 2.0f)
        {
            if (growing)
            {
                transform.localScale = new Vector3(
                    transform.localScale.x + deltaScaleX,
                    transform.localScale.y - deltaScaleY,
                    transform.localScale.z - deltaScaleZ);

            }
            else
            {
                transform.localScale = new Vector3(
                    transform.localScale.x - deltaScaleX,
                    transform.localScale.y + deltaScaleY,
                    transform.localScale.z + deltaScaleZ);
            }
        }
        if (transform.localScale.x < 0.5f)
        {
            growing = true;
            transform.localScale = new Vector3(
                transform.localScale.x + deltaScaleX,
                transform.localScale.y - deltaScaleY,
                transform.localScale.z - deltaScaleZ);
        }
        if (transform.localScale.x > 1.5f)
        {
            growing = false;
            transform.localScale = new Vector3(
                transform.localScale.x - deltaScaleX,
                transform.localScale.y + deltaScaleY,
                transform.localScale.z + deltaScaleZ);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        wobbleCount = maxWobbleCount;

        if (collision.relativeVelocity.magnitude < 1.0f)
        {
            audioSource.PlayOneShot(audioClipBounce, 0.3F);
        }
        else if (collision.relativeVelocity.magnitude < 2.0f)
        {
            audioSource.PlayOneShot(audioClipBounce, 0.5F);
        }
        else if (collision.relativeVelocity.magnitude < 3.0f)
        {
            audioSource.PlayOneShot(audioClipBounce, 0.7f);
        }
        else
        {
            audioSource.PlayOneShot(audioClipBounce, 0.9f);
        }
    }
}
