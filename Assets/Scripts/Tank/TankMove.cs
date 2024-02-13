using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TankMove : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private GameObject panda;
    [SerializeField] private Transform inTank;
    private void Awake()
    {
        GameManager.onGameStarted += MovePanda;
        if(joystick==null)
        {
            joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<VariableJoystick>();
        }
        if (rigidbody==null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }
    private void OnDisable()
    {
        GameManager.onGameStarted -= MovePanda;
    }
    private void MovePanda()
    {
        panda.transform.DOMove(inTank.position, 0.5f);
        panda.transform.DORotateQuaternion(inTank.transform.rotation, 2f);
        panda.GetComponent<Animator>().SetBool("isGameStarted", true);
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.IsGameStarted())
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
                rigidbody.velocity = movement * speed * Time.fixedDeltaTime;

            }
            else
            {
                Vector3 movement = new Vector3(joystick.Horizontal, 0f, 0f);
                rigidbody.velocity = movement * speed * Time.fixedDeltaTime;
            }
        }
    }
}
