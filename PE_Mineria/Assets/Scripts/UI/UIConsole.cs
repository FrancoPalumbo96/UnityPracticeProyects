using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIConsole : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _consoleButtonText;

    [SerializeField] private GameObject _consolePanel;
    [SerializeField] private TextMeshProUGUI _consoleText;
    [SerializeField] private TMP_InputField _consoleInputField;
    
    private bool _isConsoleOpened;
    
    public void ConsoleButtonPressed()
    {
        if (_isConsoleOpened)
        {
            _consolePanel.SetActive(false);
            _isConsoleOpened = false;
            _consoleButtonText.text = "Open Console";
            _consoleInputField.text = "";
            DeleteItems();
        }
        else
        {
            _consolePanel.SetActive(true);
            _isConsoleOpened = true;
            _consoleButtonText.text = "Close Console";
           
            SendConsoleText("---Console Commands---");
            SendConsoleText("> info inventory (shows resources in inventory)");
            SendConsoleText("> info machinery (shows owned machinery)");
            SendConsoleText("> info buy (shows machinery and prices)");
            SendConsoleText("> buy [id] (Buys machinery)");
            _consoleInputField.ActivateInputField();
        }
    }

    private void Update()
    {
        if(!_isConsoleOpened) return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitConsoleText();
            _consoleInputField.ActivateInputField();
        }
    }

    private void SubmitConsoleText()
    {
        if(_consoleInputField.text.Length == 0) 
            return;

        EnterConsoleCode(_consoleInputField.text);
        _consoleInputField.text = "";
    }

    private void EnterConsoleCode(string code)
    {
        string[] codeFragments = code.Split(' ');

        if (codeFragments.Length != 2)
        {
            return;
        }

        switch (codeFragments[0].ToLower())
        {
            case "info":
                InformationCommand(codeFragments[1].ToLower());
                break;
            case "buy":
                BuyCommand(codeFragments[1].ToLower());
                break;
            default:
                SendConsoleText("No command Found!");
                break;
        }
    }

    private void InformationCommand(string secondCommand)
    {
        switch (secondCommand)
        {
            case "inventory":
                var inventory = GameManager.Instance.Inventory.ResourceInventory;
                foreach (var tuple in inventory)
                {
                    string text = tuple.ToString();
                    SendConsoleText(text);   
                }

                if (inventory.Count == 0)
                {
                    SendConsoleText("Inventory Empty");   
                }
                
                break;
            
            case "machinery":
                List<WorkingMachine> ownedMachines = GameManager.Instance.Inventory.MachineryInventory.Inventory;

                if (ownedMachines.Count == 0)
                {
                    SendConsoleText("No Machinery Found");   
                    break;
                }
                
                string helpText = "ID|Name|ResourceColor|Cost|Workers|WorkForce";
                
                SendConsoleText(helpText);
                
                foreach (WorkingMachine workingMachine in ownedMachines)
                {
                    SendConsoleText(workingMachine.ToString());
                }
                break;
            
            case "buy":
                
                List<Machine> machinesInShop = GameManager.Instance.Shop.MachinesInShop;
                
                bool machinesToBuy = false;

                foreach (Machine machine in machinesInShop)
                {
                    if (machine.InStock)
                        machinesToBuy = true;
                }
                
                if (machinesInShop.Count == 0 || !machinesToBuy)
                {
                    SendConsoleText("No Machinery on Sale");   
                    break;
                }
                
                string help = "ID|Name|ResourceColor|Cost|Workers|WorkForce";
                
                SendConsoleText(help);
                
                foreach (Machine machine in machinesInShop)
                {
                    if (!machine.InStock) continue;
                    SendConsoleText(machine.ToString());
                }
                break;
            default:
                SendConsoleText("No command Found!");
                break;
        }
        
    }
    
    private void BuyCommand(string secondCommand)
    {
        List<Machine> machinesInShop = GameManager.Instance.Shop.MachinesInShop;
        
        foreach (Machine machine in machinesInShop)
        {
            if (!machine.InStock) continue;
            
            if (machine.ID.ToString() == secondCommand)
            {
                var purchaseDone = GameManager.Instance.Shop.PurchaseMachine(machine.ID);
                if (!purchaseDone.purchased)
                {
                    SendConsoleText("Purchase Failed");
                    SendConsoleText(purchaseDone.failReason);
                    return;
                }
                GameManager.Instance.Inventory.MachineryInventory.PurchaseMachine(machine);
                SendConsoleText("Machine Purchased");
                return;
            }
        }
        
        SendConsoleText("No Machinery Found");
    }

    private void SendConsoleText(string text)
    {
        GameObject textGO = Instantiate(_consoleText.gameObject, _consoleText.transform.parent);
        TextMeshProUGUI tmpTextGO = textGO.GetComponent<TextMeshProUGUI>();
        tmpTextGO.text = text;
    }
    
    private void DeleteItems()
    {
        Transform gridTrans = _consoleText.transform.parent;

        int totalItems = gridTrans.childCount;
        
        for (int i = totalItems - 1; i > 0; i--)
        {
            Destroy(gridTrans.GetChild(i).gameObject);
        }
    }
}
