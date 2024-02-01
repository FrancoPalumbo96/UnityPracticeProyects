using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIMineHandler : MonoBehaviour
{
    [SerializeField] private UIPlaceMachinery _uiPlaceMachinery;
    [SerializeField] private GameObject _factoryPrefab;
    public void OnMenuOpen(Resource mineResource, Transform mine, Action onMachineSet, MineInventory mineInventory)
    {
        Action<Machine> placeMine = (machine) =>
        {
            GameObject newMachine = Instantiate(_factoryPrefab, mine.position, Quaternion.identity, mine);
            newMachine.transform.Rotate(Vector3.up, 90);

            MiningMachine miningMachine = newMachine.GetComponent<MiningMachine>();
            miningMachine.SetMiningMachine(machine, mineResource, mineInventory);
            onMachineSet.Invoke();
        };
        
        _uiPlaceMachinery.gameObject.SetActive(true);
        _uiPlaceMachinery.GenerateMachineryItemGrid(mineResource, placeMine);
    }
}
