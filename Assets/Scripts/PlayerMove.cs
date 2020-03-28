using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //#####Private Variables #########
    Rigidbody rb;
    Animator animator;
    private bool holdItem = false;
    private bool grab = false;
    private bool nearItem = false;

    //#######Public Variables #########
    public float speedMovimento;
    public float speedRotation;
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = input * speedMovimento * Time.deltaTime * -1;
        if(rb.velocity != Vector3.zero)
        {
            animator.SetBool("Walk",true);
            Vector3 dir = input;
            rb.rotation = Quaternion.LookRotation(dir*-1);
        }
        else
        {
            animator.SetBool("Walk",false);
        }
        rb.angularVelocity = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(nearItem && !holdItem)
            {
                Debug.Log("Grab True");
                grab = true;
            }
            else if(holdItem)
            {
                //Joga item no chão
                Debug.Log("Drop Item");

                GameObject go = this.gameObject.transform.GetChild(1).GetChild(0).gameObject;

                go.GetComponent<Rigidbody>().isKinematic = false;
                go.GetComponent<Rigidbody>().useGravity = true;
                go.GetComponent<Collider>().enabled = true;

                this.gameObject.transform.GetChild(1).GetChild(0).transform.parent = null;
                holdItem = false;
            }
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("item"))
        {
            Debug.Log("Colidiu Item");
            nearItem = true;
            if(grab)
            {
                grab = false;
                Debug.Log("Pega Item");
                //Pega item
                GameObject go = other.gameObject;

                if(other.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody goRb = go.GetComponent<Rigidbody>();
                    goRb.isKinematic = true;
                    goRb.useGravity = false;
                }
                go.GetComponent<Collider>().enabled = false;

                go.transform.SetParent(this.gameObject.transform.GetChild(1));
                go.transform.position = this.gameObject.transform.GetChild(1).position;
                holdItem = true;
            }
        }    
    }


    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("item"))
        {
            nearItem = false;
        }
    }
}
