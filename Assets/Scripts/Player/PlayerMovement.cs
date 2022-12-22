using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public float rotateSpeedMovement = 0.075f;
    float rotateVelocity;

   
  
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameState == GameState.OVERWORLD)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                //checking if the raycast shot hits something on navmesh
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    //MOVEMENT
                    //move player to the raycast point
                    agent.SetDestination(hit.point);

                    //ROTATION
                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                }

            }
        }
        
    }



}
