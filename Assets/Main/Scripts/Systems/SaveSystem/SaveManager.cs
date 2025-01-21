using System.Collections;
using UnityEngine;

public class SaveManager : IGameSystem
{
    public static SaveManager Instance;
    private ISaveSystem saveSystem;
    public SaveData MyData;
    [SerializeField] private SaveData myDefaultData;

    public SaveData MyDefaultData => myDefaultData;

    private void Awake()
    {
        if (SaveManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        saveSystem = new JsonSaveSystem(); //есть еще бинарная система для шифрования файла сохранения
        MyData = saveSystem.Load();
    }

    //сохранение в файл данных из SaveData
    public void Save()
    {
        saveSystem.Save(MyData);
    }

    //загрузка из файла данных в SaveData
    public void Load()
    {
        MyData = saveSystem.Load();
        Save();
    }

    //сброс SaveData 
    public void SetDefaultSaveData()
    {
        saveSystem.Save(MyDefaultData);
        Load();
    }

    public override void Activate()
    {
        this.gameObject.SetActive(true);
    }
}