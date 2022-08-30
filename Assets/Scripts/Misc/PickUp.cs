using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public enum PickupType {
        Lollipop = 0,
        Soda = 1,
        Pizza = 2,
        Book = 3,
        TeddyBear = 4,
        CarBattery = 5,
        Everclear = 6,
        Joystick = 7,
        Keycard = 8,
        PogoStick = 9,
        Raygun = 10,
        VacuumCleaner = 11
    }

    [SerializeReference] PickupType pickupType;

    private void OnTriggerEnter2D(Collider2D other) {

        PlayerController currentPlayer = other.gameObject.GetComponent<PlayerController>();

        if (other.tag == "Player") {
            switch (pickupType) {
                case PickupType.Lollipop:
                    currentPlayer.StartJumpforceChange();
                    Debug.Log("Lollipop picked up");
                    break;
                case PickupType.Soda:
                    currentPlayer.StartSpeedChange();
                    Debug.Log("Soda picked up");
                    break;
                case PickupType.Pizza:
                    GameManager.instance.lives ++;
                    Debug.Log("Pizza picked up");
                    break;
                case PickupType.Book:
                    Debug.Log("Book picked up");
                    break;
                case PickupType.TeddyBear:
                    Debug.Log("TeddyBear picked up");
                    break;
                case PickupType.CarBattery:
                    Debug.Log("CarBattery picked up");
                    break;
                case PickupType.Everclear:
                    GameManager.instance.score += 50;
                    Debug.Log("Everclear picked up");
                    break;
                case PickupType.Joystick:
                    Debug.Log("Joystick picked up");
                    break;
                case PickupType.Keycard:
                    Debug.Log("Keycard picked up");
                    break;
                case PickupType.PogoStick:
                    Debug.Log("PogoStick picked up");
                    break;
                case PickupType.Raygun:
                    Debug.Log("Raygun picked up");
                    break;
                case PickupType.VacuumCleaner:
                    Debug.Log("VacuumCleaner picked up");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
