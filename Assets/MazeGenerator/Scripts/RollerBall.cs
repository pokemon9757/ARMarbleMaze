﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour
{
    public GameObject ViewCamera = null;
    public AudioClip JumpSound = null;
    public List<ParticleSystem> Effects = new List<ParticleSystem>();
    public AudioClip HitSound = null;
    public AudioClip CoinSound = null;
    public List<int> layersToIgnore;
    private Rigidbody mRigidBody = null;
    private AudioSource mAudioSource = null;
    private bool mFloorTouched = false;
    public GameManager gameManager;
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
        for (int i = 0; i < layersToIgnore.Count; i++)
        {
            Physics.IgnoreLayerCollision(gameObject.layer, layersToIgnore[i], true);
        }
    }

    void FixedUpdate()
    {
        if (mRigidBody != null)
        {
            if (Input.GetButton("Horizontal"))
            {
                mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal") * 10);
            }
            if (Input.GetButton("Vertical"))
            {
                mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical") * 10);
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (mAudioSource != null && JumpSound != null)
                {
                    mAudioSource.PlayOneShot(JumpSound);
                }
                mRigidBody.AddForce(Vector3.up * 200);
            }
        }
        if (ViewCamera != null)
        {
            Vector3 direction = (Vector3.up * 2 + Vector3.back) * 2;
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + direction, Color.red);
            if (Physics.Linecast(transform.position, transform.position + direction, out hit))
            {
                ViewCamera.transform.position = hit.point;
            }
            else
            {
                ViewCamera.transform.position = transform.position + direction;
            }
            ViewCamera.transform.LookAt(transform.position);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        // Debug.Log("Collided " + coll.gameObject.name);
        if (coll.gameObject.tag.Equals("Floor"))
        {
            mFloorTouched = true;
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f)
            {
                mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
        else
        {
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f)
            {
                mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
       
    }

    void  OnCollisionStay(Collision coll)
    {
        if (coll.collider.tag == "Floor" && gameManager.foundExit)
        {
            coll.gameObject.SetActive(false);
            // gameManager.foundExit = false;
        }
        if (coll.collider.name == "Ground" && gameManager.foundExit)
        {
            // coll.gameObject.GetComponent<BoxCollider>().enabled = false;
			gameManager.FinishLevel();
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag.Equals("Floor"))
        {
            mFloorTouched = false;
        }
        if (coll.collider.name == "Ground")
        {
            // coll.gameObject.GetComponent<BoxCollider>().enabled = true;
            gameManager.FinishLevel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            if (mAudioSource != null && CoinSound != null)
            {
                mAudioSource.PlayOneShot(CoinSound);
            }

            Effects[0].Play();
            
            Destroy(other.gameObject);
        }
    }
}

