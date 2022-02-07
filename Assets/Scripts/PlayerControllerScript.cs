using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
  [SerializeField]
  private GameObject gameController;
  [SerializeField]
  private GameObjectPool fishPool;
  [SerializeField]
  private AudioClip walking, sliding;

  private Rigidbody rb;
  private Animator anim;
  private AudioSource audio;
  private bool gameOver;

   void Start() {
       rb = GetComponent<Rigidbody>();
       anim =  GetComponent<Animator>();
       audio = GetComponent<AudioSource>();
   }

    // Update is called once per frame
    void Update() {
        if(!gameOver)
            Move();
    } 

      private void Move() {
       if(Input.GetKey("w")) {
           rb.AddForce(transform.forward * 15f * Time.deltaTime , ForceMode.VelocityChange);
           anim.Play("Running");
           if(!audio.isPlaying) 
                audio.PlayOneShot(walking);
       }
       else if(Input.GetKey("s")) {
           rb.AddForce(transform.forward * 25f * Time.deltaTime, ForceMode.VelocityChange);
            anim.Play("sliding");
            if(!audio.isPlaying)
                audio.PlayOneShot(sliding);
       } else {
           anim.Play("idle");
           audio.Stop();
       }

        if(Input.GetKey("a")) {
            transform.Rotate(0, -6, 0);
        }

        if(Input.GetKey("d")) {
            transform.Rotate(0, 6, 0);
        }
    }

    public void setGameOver(bool value) => this.gameOver = value;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Fish")) {
            gameController.GetComponent<GameControllerScript>().FishCaught(other.gameObject);
        }
    }
}