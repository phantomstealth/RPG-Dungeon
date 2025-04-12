using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    string filePath;

    public GameObject[] prefabEnemies;
    private Transform EnemyArray;
    private Save save=null;
    public bool modeLoad;

    public List<GameObject> EnemySave = new List<GameObject>();
    public List<GameObject> objectMapSave = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        prefabEnemies = FindObjectOfType<RespawnMonsters>().Monster;
    }


    public void FindAllObjects() //Находим все объекты со скриптом EnemyMovement, Считаем их количество и сохраняем в массив EnemySave;
    {
        EnemySave.Clear();
        var EnemyObjects = FindObjectsOfType<EnemyController>();
        Debug.Log("SavedEnemies: " + EnemyObjects.Length);
        for (int i = 0; i < EnemyObjects.Length; i++)
        {
            EnemySave.Add(EnemyObjects[i].gameObject);
        }

        objectMapSave.Clear();
        GameObject objectsMap = GameObject.FindGameObjectWithTag("objectsMap");
        for (int i = 0; i < objectsMap.transform.childCount; i++)
        {
            objectMapSave.Add(objectsMap.transform.GetChild(i).gameObject);
        }
    }

    public void ClearReloadAllObjects(int countObjectsSave)//Сбрасываем массив EnemySave, уничтожаем все объекты со скриптом EnemyMovement;
    {
        EnemySave.Clear();
        var EnemyObjects = FindObjectsOfType<EnemyController>();
        for (int i = 0; i < EnemyObjects.Length; i++)
        {
            Destroy(EnemyObjects[i].gameObject);
        }
        Debug.Log("Load Objects: " + countObjectsSave);

        GameObject objectsMap = GameObject.FindGameObjectWithTag("objectsMap");
        for (int i = 0; i < objectsMap.transform.childCount; i++)
        {
            Destroy(objectsMap.transform.GetChild(i).gameObject);
        }
    }

    private GameObject GetPrefab(GameObject[] prefabArray, string namePrefab)
    {
        for (int i = 0; i < prefabArray.Length; i++)
        {
            if (prefabArray[i].name == namePrefab) return prefabArray[i];
        }
        return prefabArray[0];
    }

    public void SaveGame()
    {
        FindAllObjects();//Находим все объекты 
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);//открываем файл как новый и потоком зашифрованной информации записываем

        Save save = new Save();//новая переменная структуры Save - в ней как данные врагов, так и данные плеера

        save.SaveMapObject(objectMapSave);//вызываем процедуру сохранения врагов
        save.SaveEnemies(EnemySave);//вызываем процедуру сохранения врагов
        save.SavePlayer();// вызываем процедуру сохранения данных игрока включая название текущей сцены

        bf.Serialize(fs, save);//вносим в файл fs данные save (зашифрованные)
        fs.Close(); //сохраняем файл
        GetComponent<FrmSettings>().BtnResume();// выходим обратно в игру
    }

    public void LoadGame()
    {
        //Если файл сохранения не создан выходим из процедуры
        if (!File.Exists(filePath))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);

        save = (Save)bf.Deserialize(fs);
        fs.Close();
        modeLoad = true;
        SceneManager.LoadScene(save.plSaveData.nameScene);
    }

    public void LoadAfterSceneData()
    {
        modeLoad = false;
        //Находим объект RespawnMonsters
        EnemyArray = FindObjectOfType<RespawnMonsters>().transform;
        GameObject playerObject = GameObject.FindObjectOfType<PlayerContrloller>().gameObject;
        GameObject playerStats = GameObject.FindObjectOfType<PlayerStats>().gameObject;

        //загружаем данные игрока
        playerStats.GetComponent<PlayerStats>().currentLevel= save.plSaveData.curLevel;
        playerStats.GetComponent<MoneyManager>().currentGold= save.plSaveData.curMoney;
        playerStats.GetComponent<MoneyManager>().Refresh();
        playerObject.GetComponent<PlayerContrloller>().startPoint = null;
        playerObject.transform.position = new Vector3(save.plSaveData.Position.x, save.plSaveData.Position.y, save.plSaveData.Position.z);
        playerObject.GetComponent<PlayerContrloller>().LastMove= new Vector2(save.plSaveData.Direction.x,save.plSaveData.Direction.y);
        playerObject.GetComponent<PlayerHealthManager>().PlayerCurrentHealth = save.plSaveData.curHealth;

        //Удаляем всех врагов и объекты на карте
        ClearReloadAllObjects(save.EnemiesData.Count);

        //Создаем на карте врагов
        int i = 0;
        foreach (var enemy in save.EnemiesData)
        {
            //Пересоздаем все объекты в количестве countObjectSave, и заново заполняем массив EnemySave
            GameObject MainPrefab = GetPrefab(prefabEnemies,enemy.nameMonster);
            GameObject gobj = Instantiate(MainPrefab, new Vector3(0, 0, 0), MainPrefab.transform.rotation);
            gobj.transform.SetParent(EnemyArray.transform.parent);
            gobj.name = MainPrefab.name;
            EnemySave.Add(gobj);
            EnemySave[i].GetComponent<EnemyController>().LoadData(enemy);
            i++;
        }

        //Создаем на карте объекты
        /* i = 0;
        foreach (var objmap in save.objMapData)
        {
            //Пересоздаем все объекты в количестве countObjectSave, и заново заполняем массив EnemySave
            GameObject objectPrefab = GetPrefab(prefabEnemies, objmap.nameMapObject);
            GameObject om = Instantiate(objectPrefab, new Vector3(0, 0, 0), objectPrefab.transform.rotation);
            om.transform.SetParent(EnemyArray.transform.parent);
            om.name = objectPrefab.name;
            EnemySave.Add(om);
            EnemySave[i].GetComponent<EnemyController>().LoadData(enemy);
            i++;
        }
        */
        GetComponent<FrmSettings>().BtnResume();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Save
{
    [System.Serializable]
    public struct Vec3
    {
        public float x, y, z;
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [System.Serializable]
    public struct EnemySaveData
    {
        public Vec3 Position, Direction;
        public string nameMonster;
        public int curHealth;


        /*public EnemySaveData(Vec3 pos, Vec3 dir,string nMonster)
        {
            Position = pos;
            Direction = dir;
            nameMonster = nMonster;
        }*/
    }

    public List<EnemySaveData> EnemiesData = new List<EnemySaveData>();

    public void SaveEnemies(List<GameObject> enemies)
    {
        foreach (var go in enemies)
        {
            var em = go.GetComponent<EnemyController>();

            Vec3 pos = new Vec3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            Vec3 dir = new Vec3(em.directionMove.x, em.directionMove.y, em.directionMove.z);

            EnemySaveData esd;
            esd.Position = pos;
            esd.Direction = dir;
            esd.nameMonster = go.transform.name;
            esd.curHealth = go.GetComponent<EnemyHealthManager>().CurrentHealth;

            //EnemiesData.Add(new EnemySaveData(pos,dir, go.transform.name));
            EnemiesData.Add(esd);

        }
    }

    [System.Serializable]
    public struct ObjectMapSaveData
    {
        public Vec3 Position;
        public string nameMapObject;
    }

    public List<ObjectMapSaveData> objMapData = new List<ObjectMapSaveData>();

    public void SaveMapObject(List<GameObject> objectsMap)
    {
        foreach (var go in objectsMap)
        {
            ObjectMapSaveData omsd;
            Vec3 pos = new Vec3(go.transform.position.x, go.transform.position.y, go.transform.position.z);

            omsd.Position = pos;
            omsd.nameMapObject = go.name;

            objMapData.Add(omsd);
        }
    }

    [System.Serializable]
    public struct PlayerSaveData
    {
        public Vec3 Position, Direction;
        public int curHealth;
        public int curLevel;
        public string nameScene;
        public int curMoney;
    }

    public PlayerSaveData plSaveData;

    public void SavePlayer()
    {
        GameObject playerObject = GameObject.FindObjectOfType<PlayerContrloller>().gameObject;
        GameObject statsObject = GameObject.FindObjectOfType<PlayerStats>().gameObject;
        plSaveData.Position = new Vec3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
        plSaveData.Direction= new Vec3(playerObject.GetComponent<PlayerContrloller>().LastMove.x, playerObject.GetComponent<PlayerContrloller>().LastMove.y, 0);
        plSaveData.curHealth = playerObject.GetComponent<PlayerHealthManager>().PlayerCurrentHealth;
        plSaveData.curLevel= statsObject.GetComponent<PlayerStats>().currentLevel;
        plSaveData.nameScene =SceneManager.GetActiveScene().name;
        plSaveData.curMoney = statsObject.GetComponent<MoneyManager>().currentGold;

    }
}


