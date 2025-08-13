using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent player;
    [SerializeField] Vector3 pointToMoveTo;
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
        inventory.InventoryPanel.SetActive(!inventory.InventoryPanel.activeSelf);
        inventory.OnInventoryClose();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Debug.Log(hit.point);

                player.destination = hit.point;
            }
        }
    }
}

