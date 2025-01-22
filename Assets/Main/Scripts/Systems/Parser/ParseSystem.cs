using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseSystem : IGameSystem
{
    public static ParseSystem Instance;
    [SerializeField] List<JsonParser> listParser;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public override void Activate()
    {
        StartCoroutine(LoadJson());
        
    }

    private IEnumerator LoadJson()
    {

        foreach (var item in listParser)
        {
            item.LoadJsonFromUrl();
            while (!item.IsComplete)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        Debug.Log("IsActivateComplete = true;");
        IsActivateComplete = true;
    }

}
