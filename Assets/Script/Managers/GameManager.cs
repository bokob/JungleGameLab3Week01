using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get { return _instance; } private set { } }

    [Header("������")]
    public bool isGameOver;
    public bool isGameClear;
    public float playTime = 0;  // �÷��� �ð�
    public int Level = 1;
    public int startTime = 5;
    public bool isStartGame = false;

    [Header("��ȯ")]
    public List<Transform> spawnTransformList = new List<Transform>();         // ������ ��ȯ ��� ����Ʈ,    0: ����, 1: ���, 2: ũ����
    public List<GameObject> spawnPrefabList = new List<GameObject>();          // ������ ����Ʈ,              0: ����, 1: ���, 2: ũ����
    public List<Coroutine> spawnIntervalCorouineList = new List<Coroutine>(3); // ������ ��ȯ �ڷ�ƾ ����Ʈ,  0: ����, 1: ���
    public bool isboss;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        GameStart();
    }

    void Update()
    {
        if (isGameOver) 
            return;

        UpdateTimer();

        if (playTime > 15 && !isboss)
            BossStart();

        // ���� ����
        if (playTime > startTime && !isStartGame)
        {
            isStartGame = true;
            GamePlaying();
        }


        if (playTime > 180)
        {
            UIManager.Instance.UpdateGameClearUI();
            return;
        }
    }

    public void UpdateTimer()
    {
        playTime += Time.deltaTime;
        UIManager.Instance.UpdateTimeText((int)playTime);
    }

    // ���� ����
    public void GameStart()
    {
        isboss = false;
        isGameOver = false;
        isGameClear = false;

        // ������ �ڷ�ƾ���� ������ ��, ���߰� ����
        StopAllCoroutines();
        for (int i = 0; i < spawnIntervalCorouineList.Count; i++)
            spawnIntervalCorouineList[i] = null;

        // ���� ��ȯ
        if (spawnIntervalCorouineList.Count == 0)
        {
            spawnIntervalCorouineList.Add(StartCoroutine(SpawnIntervalPrefabCoroutine(spawnPrefabList[0], 10.0f)));

            Debug.Log($"���� �ڷ�ƾ �߰�, ���� �ڷ�ƾ ����: {spawnIntervalCorouineList.Count}");
        }
    }

    // ���� �÷��� �� 
    public void GamePlaying()
    {
        // ���� ���� UI�� ��ȯ
        UIManager.Instance.UpdateGamePlayingUI();

        // ��� ��ȯ
        if(spawnIntervalCorouineList.Count == 1)
        {
            spawnIntervalCorouineList.Add(StartCoroutine(SpawnIntervalPrefabCoroutine(spawnPrefabList[1], 2.0f)));

            Debug.Log($"��� �ڷ�ƾ �߰�, ���� �ڷ�ƾ ����: {spawnIntervalCorouineList.Count}");
        }
    }


    // ���ӿ��� ���� ��
    public void GameOver()
    {
        UIManager.Instance.UpdateGameOverUI(); // ���� ���� UI ���̱�

        //if (enemySpawner != null) enemySpawner.SetActive(false);
        isGameOver = true;
    }

    // �÷��� ������ �Ѿ
    public void GoInGameScene()
    {
        SceneManager.LoadScene(0);
        GameStart();
    }

    // ���� ������ �Ѿ
    public void GoShopScene()
    {
        UIManager.Instance.UpdateGoShopUI();    // �ΰ��� UI ����
        SceneManager.LoadScene(1);              // ���� ������ �̵�
    }


    #region ����
    // ������ ����
    public void BossStart()
    {
        UIManager.Instance.UpdateGamePlayingUI();

        // ��� ��ȯ ����
        StopCoroutine(spawnIntervalCorouineList[1]);
        spawnIntervalCorouineList[1] = null;

        // ũ���� ��ȯ
        isboss = true;
        Instantiate(spawnPrefabList[2]);
    }

    // ���� Ŭ�����
    public void BossClear()
    {
        isGameOver = true;

        //  UI Ȱ��ȭ
        // (���� ����)
        Debug.Log("���� Ŭ����~");



        Invoke("GoShopScene", 3f);
    }
    #endregion

    // ���� �ֱ�� ��� ������ ��ȯ
    IEnumerator SpawnIntervalPrefabCoroutine(GameObject prefab, float interval)
    {
        while(true)
        {
            int randomPosX = Random.Range(-7, 7);
            Instantiate(prefab, new Vector3(randomPosX, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
}