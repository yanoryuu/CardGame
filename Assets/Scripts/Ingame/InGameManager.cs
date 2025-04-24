using R3;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private ReactiveProperty<int> currentTurn = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> CurrentTurn => currentTurn;

    public void NextTurn()
    {
        currentTurn.Value++;
    }
    
    private static InGameManager instance;
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InGameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("InGameManager");
                    instance = obj.AddComponent<InGameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
