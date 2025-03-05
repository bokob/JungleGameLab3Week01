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
    public float playTime = 0;
    public int Level = 1;

    [Header("��ȯ")]
    public GameObject enemySpawner;
    public GameObject obstacleSpawner;
    public GameObject cloudeSpawner;

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
        if (isGameOver) return;

        UpdateTimer();

        if (playTime > 60)
            BossStart();

        if (playTime > 7) // 7�� �ʰ����� ���� ����
        {
            GamePlaying();
        }
        else if (playTime > 180)
        {
            if (UIManager.Instance.IsReadyUI)
                UIManager.Instance.UpdateGameClearUI();
            return;
        }
    }

    public void UpdateTimer()
    {
        playTime += Time.deltaTime;
        UIManager.Instance.UpdateTimeText((int)playTime);
    }

    //public void StageLevelUp()
    //{
    //    Level += 1;
    //    UIManager.Instance.UpdateLevelText(Level);
    //    //enemySpawner.GetComponent<EnemySpawn>().spawnInterval -= 1f;
    //}

    // ���� ����
    public void GameStart()
    {
        isGameOver = false;
        if (enemySpawner != null) enemySpawner.SetActive(false);
    }

    // ���� �÷��� �� 
    public void GamePlaying()
    {
        UIManager.Instance.EndGameStartUI();
        if (enemySpawner != null) enemySpawner.SetActive(true);
    }


    // ���ӿ��� ���� ��
    public void GameOver()
    {
        UIManager.Instance.UpdateGameOverUI();
        if (enemySpawner != null) enemySpawner.SetActive(false);
        isGameOver = true;
    }

    // ���� ������ �Ѿ
    public void GoShop()
    {
        UIManager.Instance.UpdateGoShopUI();
        SceneManager.LoadScene(1);
    }

    // �÷��̾����� �Ѿ
    public void GoGame()
    {
        SceneManager.LoadScene(0);
        GameStart();
    }

    // ������ ����
    public void BossStart()
    {

        enemySpawner.SetActive(false);
        UIManager.Instance.UpdateGamePlayingUI();
        // ���� ����

        //UIManager.Instance.UpdateLevelText(Level);
    }

    // ���� Ŭ�����
    public void BossClear()
    {
        isGameOver = true;
        // ���� Ŭ���� UI Ȱ��ȭ

    }
}