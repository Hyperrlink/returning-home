using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool triggered = false;

    public Animator anim;

    void Start()
    {

        anim = gameObject.GetComponent<Animator>();

    }

    void Update()
    {

        anim.SetBool("Triggered", !triggered);

    }
}
