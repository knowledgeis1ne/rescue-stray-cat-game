using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float fallTime = 0.5f, destroyTime = 2f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            PlatformManager.Instance.StartCoroutine("spawnPlatform", new Vector2(transform.position.x, transform.position.y));
            Invoke("FallPlatform", fallTime);
        }    
    }

    void FallPlatform()
    {
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
