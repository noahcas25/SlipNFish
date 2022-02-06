using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{

  [SerializeField]
  private GameObjectPool fishPool;

  private Rigidbody rb;
  private Animator anim;

   void Start() {
       rb = GetComponent<Rigidbody>();
       anim =  GetComponent<Animator>();
   }

    // Update is called once per frame
    void Update() {
        Move();
    } 

      private void Move() {
       if(Input.GetKey("w")) {
           rb.AddForce(transform.forward * 15f * Time.deltaTime , ForceMode.VelocityChange);
           anim.Play("Running");
       }
       else if(Input.GetKey("s")) {
           rb.AddForce(transform.forward * 25f * Time.deltaTime, ForceMode.VelocityChange);
            anim.Play("sliding");
       } else {
           anim.Play("idle");
       }

        if(Input.GetKey("a")) {
            transform.Rotate(0, -3, 0);
       }

        if(Input.GetKey("d")) {
            transform.Rotate(0, 3, 0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Fish")) 
           fishPool.ReturnToPool(other.gameObject);
    }
}
