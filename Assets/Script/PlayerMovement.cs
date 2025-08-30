using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent player;
    [SerializeField] InventorySystem inventory;
    [SerializeField] InputActionReference openInventory;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<InventorySystem>();
        openInventory.action.performed += OpeningInventory;
    }

    private void OpeningInventory(InputAction.CallbackContext obj)
    {
        inventory.parentInventoryContener.SetActive(!inventory.parentInventoryContener.activeSelf);
        inventory.OnInventoryClose();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                
                Debug.Log(hit.collider.gameObject.name);
                player.SetDestination(hit.point);
            }
        }
    }
}

