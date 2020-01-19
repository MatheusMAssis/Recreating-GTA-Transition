using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Playable Players")]
    public GameObject ALFONSO;
    public GameObject BARNEY;


    private Camera cameraALFONSO;
    private Camera cameraBARNEY;
    private CharacterMovement cmALFONSO;
    private CharacterMovement cmBARNEY;

    void Start()
    {
        cameraALFONSO = ALFONSO.transform.GetChild(0).GetComponent<Camera>();
        cameraBARNEY = BARNEY.transform.GetChild(0).GetComponent<Camera>();

        cmALFONSO = ALFONSO.GetComponent<CharacterMovement>();
        cmBARNEY = BARNEY.GetComponent<CharacterMovement>();
    }

    public void ChangePlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Time.timeScale = .5f;

            if (cmALFONSO.isPlayable)
            {
                cmALFONSO.isPlayable = false;
                cameraALFONSO.enabled = false;

                cmBARNEY.isPlayable = true;
                cameraBARNEY.enabled = true;

                Time.timeScale = 1f;
            }
            else
            {
                cmALFONSO.isPlayable = true;
                cameraALFONSO.enabled = true;

                cmBARNEY.isPlayable = false;
                cameraBARNEY.enabled = false;

                Time.timeScale = 1f;
            }
        }
    }


    void Update()
    {
        ChangePlayer();
    }
}
