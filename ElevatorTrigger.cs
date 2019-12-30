using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    public bool canTrigger;
    enum MyDirection { Up, Down}
    [SerializeField]MyDirection myDirection;
    // Start is called before the first frame update
    public Elevator_Controller eController;
    void Start()
    {
        eController = this.GetComponentInParent<Elevator_Controller>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canTrigger = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canTrigger = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E) && canTrigger)
        {
            Debug.Log("Button pressed for " + myDirection);
            //eController.currentPosition = eController.transform.position;

            switch (myDirection)
            {
                case MyDirection.Up :
                    eController.ElevatorDirection = true;
                    eController.MoveElevator();
                    Debug.Log("Elevator Triggered to go up.");
                    break;
                case MyDirection.Down:
                    eController.ElevatorDirection = false;
                    eController.MoveElevator();
                    Debug.Log("Elevator Triggered to go down.");
                    break;
            }
        }
    }
}
