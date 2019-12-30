using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Controller : MonoBehaviour
{
    Vector3 target;
    public bool Activate; 
    [SerializeField]
    private float [] Locations;
    public bool ElevatorDirection;
    public static Elevator_Controller Instance;
    public float speed = 1f;
    public Vector3 currentPosition;
    public float offset = 10.5f;
    
    public int index = 0;
    void Awake()
    {

        Instance = this;
    }                        
    private void Update()
    {
        if(Activate)
        {
            ActivateElevator();
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
    public void MoveElevator()
    {

        //check if possible to go down
        if (ElevatorDirection == false)
        {
            if (index < Locations.Length -1)
            {
                
                var targetLocation = Locations[index + 1];
                Debug.Log("Target height is " + targetLocation);
                target = new Vector3(transform.position.x, targetLocation - offset, transform.position.z);
                index++;
                Activate = true;

                Debug.Log("Moving down.");
            }
        }
        else if (ElevatorDirection == true)
        {
            if (index  > 0)
            {
                var targetLocation = Locations[index - 1];
                Debug.Log("Target height is " + targetLocation);
                target = new Vector3(this.transform.position.x, targetLocation - offset, this.transform.position.z);
                Activate = true;
                index--;
                Debug.Log("Moving up");
            }
        }
        else
        {
            //Play sound effect that elevator is not moving.
        }
    }
    
    private void ActivateElevator()
    {
        float step = speed * Time.deltaTime;
        
        if (ElevatorDirection)
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f * step, transform.position.z);
        else if(!ElevatorDirection)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1f * step, transform.position.z);
        }
        
    Debug.Log("Distance from target is " + Vector3.Distance(transform.position, target));
            //transform.position = Vector3.MoveTowards(currentPosition, target, step);
            
        if(Vector3.Distance(transform.position, target ) < 0.1f)
        {
            Activate = false; 
        }
    }
    public void SetDown()
    {
        ElevatorDirection = false;
    }
    public void SetUp()
    {
        ElevatorDirection = true;
    }
}
