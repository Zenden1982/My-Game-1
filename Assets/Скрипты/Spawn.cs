
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    Vector3 spawn;
    public GameObject[] gameObjects;
    public float spawnDistance = 50f;
    private float counter = 0f;
    GameObject player;
    private bool boosSpawning;
    private TextMeshProUGUI bossText;
    public void Start()
    {
        YouWin.winBool = false;
        boosSpawning = false;
        bossText = GameObject.Find("BossFight").GetComponent<TextMeshProUGUI>();
        bossText.enabled = false;
        player = GameObject.Find("Knight");
        InvokeRepeating("SpawnSkelet1", 0.0f,6f);

    }

    public void Update()
    {
        if (!YouWin.winBool)
        {
            if (CameraController.score == 20 && !boosSpawning)
            {
                GameObject[] allEnemys = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in allEnemys)
                {

                    Enemy enemyClass = enemy.GetComponent<Enemy>();
                    enemyClass.DeathEnemy();
                    boosSpawning = true;

                }
                StartCoroutine(BossSpawner());
                bossText.enabled=true;

            }
        }
    }

    public void SpawnSkelet1()
    {
        counter +=1f;

        if (counter % 5 == 0 && !boosSpawning)
        {
            SpawnEnemy(1);
        }
        else if (!boosSpawning)
        {
            SpawnEnemy(0);
        }
    }



    public void SpawnEnemy(int i)
    {
        Vector3 vector3;
        Vector3 playerPos = player.transform.position;
        playerPos.y = -16;
        playerPos.x=player.transform.position.x;
        if (playerPos.x + 1 * spawnDistance >= 40.5)
        {
            vector3 = Vector3.left;
        }
        else if (playerPos.x - 1 * spawnDistance <= -15.75)
        {
            vector3 = Vector3.right;
        }
        else
        {
            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
            {
                vector3 = Vector3.right;
            }
            else vector3 = Vector3.left;
        }
        spawn = playerPos + vector3 * spawnDistance;
        GameObject enemy = Instantiate(gameObjects[i]);
        enemy.transform.position = spawn;
    }

    IEnumerator BossSpawner()
    {
        yield return new WaitForSeconds(4); // ждем 3 секунды
        bossText.enabled=false;
        SpawnEnemy(2);
    }
}
