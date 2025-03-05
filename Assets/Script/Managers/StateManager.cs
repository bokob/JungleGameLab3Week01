using UnityEngine;

public class StateManager : MonoBehaviour
{
    static float _relodaingUpgradeValue = 0.2f;
    static float _reloadingTime = 1;
    static float _luck = 2f;
    int _spearCount;
    int _reloadUpgradeCount = 1;
    int _myCoin;
    float _luckLevel;


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
    public void BuySpear()
    {
        _spearCount++;        
    }
    public void ReroadingUpgrade()
    {
        _reloadUpgradeCount++;
    }
    public int SpearCount()
    {
        return _spearCount;
    }
    public float ReloadingTime()
    {
        return _reloadingTime + (_relodaingUpgradeValue * _reloadUpgradeCount); // Spear.cs , isReturn �϶��� �ӵ��� �����ϵ��� �����ؾ��� 
    }
    public void Addcoin(int coin)
    {
        _myCoin += coin;
    }
    public bool UseCoin(int coin)
    {
        if (_myCoin >= coin)
        {
            _myCoin -= coin;
            //���׷��̵� ���� �Ž���
            return true;
        }
            //���׷��̵� ���� �޽���
            return false;
    }
    public float GetLuckLevel()
    {
        return _luckLevel;
    }
    public void LuckLevelUpgrade()
    {
        _luckLevel += _luck;
    }
}
