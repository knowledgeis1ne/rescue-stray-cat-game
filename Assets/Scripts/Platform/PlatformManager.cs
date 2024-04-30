using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;
    [SerializeField] GameObject platform;
    [SerializeField] float posX1, posY;
    [SerializeField] float spawnTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        Instantiate(platform, new Vector2(posX1, posY), platform.transform.rotation);
    }

    IEnumerator spawnPlatform(Vector2 spawnPos)
    {
        yield return new WaitForSeconds(spawnTime);
        Instantiate(platform, spawnPos, platform.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
