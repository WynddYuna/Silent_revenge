using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HoldToLaodLevel : MonoBehaviour
{

    public float holdDuration = 1f;
    public Image fillCircle;
    private float holdTimer = 0;
    private bool isHolding =false;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if (isHolding){
            holdTimer += Time.deltaTime;
            fillCircle.fillAmount= holdTimer/holdDuration;

            if(holdTimer>= holdDuration){


            }
        }
    }

    public void OnHold(InputAction.CallbackContext context){

        if(context.started){
            isHolding=true;

        }
        else if (context.canceled)
        {
            ResetHold();

        }
    }
    private void ResetHold(){

        isHolding=false;
        holdTimer=0;
        fillCircle.fillAmount=0;
    }
}

