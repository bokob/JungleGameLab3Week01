using UnityEngine;

public class StateManager : MonoBehaviour
{

    public float SpearCount;
    public float ReloadingTime;


    // �̱��� ����
    public static StateManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �̵��ص� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
