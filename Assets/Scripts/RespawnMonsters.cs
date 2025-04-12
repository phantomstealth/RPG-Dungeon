using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SuperTiled2Unity;

public class RespawnMonsters : MonoBehaviour
{
    public GameObject[] Monster;
    public float TimeRespawn=120f;

    public BoxCollider2D[] boxEnemyRespawn;

    private float countTimeRespawn;

    private void Awake()
    {
        SceneManager.sceneLoaded += FullSceneLoaded;
    }

    void FullSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        SaveLoadManager saveloadManager = GameObject.FindObjectOfType<SaveLoadManager>();
        if (!saveloadManager.modeLoad) return;

        saveloadManager.LoadAfterSceneData();
        Debug.Log("Load Game completed");
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.parent.name = "Enemy" + (transform.parent.childCount-1);
        if (TimeRespawn == 0) TimeRespawn = 120f;
        countTimeRespawn = TimeRespawn;
        if (boxEnemyRespawn.Length==0)
        {
            boxEnemyRespawn = new BoxCollider2D[1];
            boxEnemyRespawn[0] = FindObjectOfType<BorderCamera>().GetComponent<BoxCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (countTimeRespawn > 0f)
        {
            countTimeRespawn -= Time.deltaTime;
        }
        else
        {
            DoRespawn();
            countTimeRespawn = TimeRespawn;
        }
    }

    void DoRespawn()
    {
        int numBoxEnemy = 0;
        if (Monster.Length > 0)
        {
            int randomNumMonster = Random.Range(0, Monster.Length);
            float rwidth = Random.Range(boxEnemyRespawn[numBoxEnemy].bounds.min.x, boxEnemyRespawn[numBoxEnemy].bounds.max.x);
            float rheight = Random.Range(boxEnemyRespawn[numBoxEnemy].bounds.min.y, boxEnemyRespawn[numBoxEnemy].bounds.max.y);
            Vector3 NewPosition = new Vector3(rwidth, rheight);
            GameObject newMonster=Instantiate(Monster[randomNumMonster], NewPosition, transform.rotation);
            newMonster.transform.SetParent(transform.parent);
            newMonster.name = Monster[randomNumMonster].name;
            countMonsters();
        }
    }

    void countMonsters()
    {
        transform.parent.name="Enemy"+(transform.parent.childCount-1);
    }
}
