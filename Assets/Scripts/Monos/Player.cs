using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerUtility utility;

    // Start is called before the first frame update
    void Start()
    {
        utility.Initialize(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        utility.FixedTick();              
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Trigger>())
        {
            utility.curTriggerEffect = other.GetComponent<Trigger>().triggerEffect;
            utility.FSM();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Trigger>())
        {
            utility.curTriggerEffect = TriggerEffect.Default;
            utility.FSM();
        }
    }
}
