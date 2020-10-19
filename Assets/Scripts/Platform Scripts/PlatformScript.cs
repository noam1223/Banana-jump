using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    public static PlatformScript instance;

    [SerializeField]
    private GameObject leftPlatfrom, rightPlatform;

    [SerializeField]
    private GameManager gameManager;

    private float leftXMin = -4.4f, leftXMax = -2.8f, rightXMin = 4.4f, rightXMax = 2.8f;
    private float yTreshold = 3f;
    private float lastY;
    public int spawnCount = 8;
    private int platformSpawned;

    [SerializeField]
    private Transform platformParent;

    [SerializeField]
    private GameObject[] bird;
    public float birdY = 5f;
    private float birdXMin = -2.3f, birdXMax = 2.3f;

    private float[] xTreesPosition = { -1.51f, -0.55f, 1.81f };
    [SerializeField]
    private GameObject[] trees;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        lastY = transform.position.y;

        SpawnPlatforms();

    }

    public void SpawnPlatforms()
    {
        Vector2 temp = transform.position;
        GameObject newPlatform = null;

        for(int i = 0; i < spawnCount; i++)
        {
            temp.y = lastY;

            if((platformSpawned % 2) == 0)
            {
                int rand = Random.Range(0, 2);
                temp.x = xTreesPosition[rand];
                newPlatform = Instantiate(trees[rand], temp, Quaternion.identity);
            }
            else
            {
                temp.x = xTreesPosition[2];
                newPlatform = Instantiate(trees[2], temp, Quaternion.identity);
            }

            newPlatform.transform.parent = platformParent;

            lastY += yTreshold;
            platformSpawned++;
        }


        if (Random.Range(0, 3) == 0)
        {
            SpawnBird();
        }
    }


    void SpawnBird()
    {
        Vector2 temp = transform.position;
        temp.x = Random.Range(birdXMin, birdXMax);
        temp.y += birdY;

        int rnd = Random.Range(0, 3);

        GameObject newBird = Instantiate(bird[rnd], temp, bird[rnd].transform.rotation);
        newBird.transform.parent = platformParent;
        //gameManager.ShowWhereBird();
    }
}
