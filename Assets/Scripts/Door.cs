using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    void Start()
    {

    }

    //Update is called once per frame

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        animator.SetBool("isOpenning", true);
    }

    private void OnTriggerExit(Collider other) {
        animator.SetBool("isOpenning", false);
    }
}