using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManagerScript : MonoBehaviour
{
    [Header("Playable Players")]
    public GameObject ALFONSO;
    public GameObject BARNEY;

    private CharacterMovement cmALFONSO;
    private CharacterMovement cmBARNEY;

    [Header("UI")]
    public GameObject changeBox;

    [Header("Cameras")]
    public Camera cameraALFONSO;
    public Camera cameraBARNEY;
    public Camera transitionCamera;

    public GameObject actualPlayerGO;
    public Camera actualPlayerCAM;
    public CharacterMovement actualPlayerCM;

    private bool changing = false;
    private Sequence transitionSequence;




    void Start()
    {
        cmALFONSO = ALFONSO.GetComponent<CharacterMovement>();
        cmBARNEY = BARNEY.GetComponent<CharacterMovement>();

        actualPlayerGO = ALFONSO;
        actualPlayerCAM = cameraALFONSO;
        actualPlayerCM = cmALFONSO;
    }

    Sequence MoveCamera(Vector3 final)
    {
        Sequence s = DOTween.Sequence();

        // going up
        s.Append(transitionCamera.transform.DOMoveY(10f, 2f));
        s.Append(transitionCamera.transform.DOMoveY(20f, 3f));

        // changing x and z
        s.Append(transitionCamera.transform.DOMove(final, 1f));

        // going down
        s.Append(transitionCamera.transform.DOMoveY(10f, 3f));
        s.Append(transitionCamera.transform.DOMoveY(5f, 2f));

        return s;
    }

    public void ChangeAlfonso()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && actualPlayerGO == ALFONSO)
        {
            changing = true;

            Vector3 initial = new Vector3(ALFONSO.transform.position.x, 5f, ALFONSO.transform.position.z);
            Vector3 final = new Vector3(BARNEY.transform.position.x, 20f, BARNEY.transform.position.z);

            transitionCamera.gameObject.transform.position = initial;
            transitionCamera.enabled = true;

            actualPlayerCAM.enabled = false;
            actualPlayerCM.isPlayable = false;


            transitionSequence = MoveCamera(final);
            transitionSequence.Play();

            actualPlayerGO = BARNEY;
            actualPlayerCAM = cameraBARNEY;
            actualPlayerCM = cmBARNEY;
        }
    }

    public void ChangeBarney()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && actualPlayerGO == BARNEY)
        {
            changing = true;

            Vector3 initial = new Vector3(BARNEY.transform.position.x, 5f, BARNEY.transform.position.z);
            Vector3 final = new Vector3(ALFONSO.transform.position.x, 20f, ALFONSO.transform.position.z);

            transitionCamera.gameObject.transform.position = initial;
            transitionCamera.enabled = true;

            actualPlayerCAM.enabled = false;
            actualPlayerCM.isPlayable = false;


            transitionSequence = MoveCamera(final);
            transitionSequence.Play();

            actualPlayerGO = ALFONSO;
            actualPlayerCAM = cameraALFONSO;
            actualPlayerCM = cmALFONSO;
        }
    }

    public void ChangeScreen()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !changing)
        {
            Time.timeScale = .5f;

            changeBox.SetActive(true);

            ChangeAlfonso();
            ChangeBarney();
        }

        if (changing)
        {
            Time.timeScale = 1f;

            changeBox.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1f;

            changeBox.SetActive(false);
        }

        if (transitionSequence != null)
        {
            if (!transitionSequence.IsActive())
            {
                changing = false;
                transitionCamera.enabled = false;

                actualPlayerCAM.enabled = true;
                actualPlayerCM.isPlayable = true;
            }
        }
    }

    void Update()
    {
        ChangeScreen();
    }
}
