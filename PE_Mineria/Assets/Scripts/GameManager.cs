using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Camera")] 
    [SerializeField] private Transform ogCameraTransform;
    
    [Header("Player Stats")]
    
    [SerializeField] private int _money = 100;

    [Range(0, 100)]
    [SerializeField] private int _toxicity = 0;
    
    [Header("Script References")] 
    [SerializeField] private CycleManager _cycleManager;
    [SerializeField] private UICycleHandler _uiCycleHandler;
    [SerializeField] private MinesHandler _minesHandler;
    
    private Inventory _inventory = new();
    private Shop _shop = new();
    
    
    public CycleManager CycleManager => _cycleManager;
    public Inventory Inventory => _inventory;
    public Shop Shop => _shop;
    
    
    
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            InitializeGame();
        } 
    }
    
    #endregion

    #region Events

    public event Action<int> MoneyChanged; 
    public event Action<int> ToxicityChanged; 
   

    #endregion

    private void InitializeGame()
    {
        _uiCycleHandler.enabled = true;
        
    }
    
    //TODO replace for Start Button
    private void Start()
    {
        Invoke("StartGame", 1);
    }
    
    private void StartGame()
    {
        _minesHandler.InitializeMines();
        if (MoneyChanged != null) MoneyChanged.Invoke(_money);
        if (ToxicityChanged != null) ToxicityChanged.Invoke(_toxicity);
    }

    public void ResetCameraPosition()
    {
        if (Camera.main == null) return;
        Transform transform1 = Camera.main.transform;
        transform1.position = ogCameraTransform.position;
        transform1.rotation = ogCameraTransform.rotation;
    }

    public void AddMoney(int money)
    {
        _money += money;
        if (MoneyChanged != null) MoneyChanged.Invoke(_money);
    }

    public bool Purchase(int cost)
    {
        if (_money < cost) return false;

        _money -= cost;
        if (MoneyChanged != null) MoneyChanged.Invoke(_money);
        return true;
    }

    public void AddToxicity(int quantity)
    {
        _toxicity += quantity;
        if (ToxicityChanged != null) ToxicityChanged.Invoke(_toxicity);
    }
}

public enum DayCycle
{
    Morning = 0,
    Midday = 1,
    Afternoon = 2,
    Night = 3
}
