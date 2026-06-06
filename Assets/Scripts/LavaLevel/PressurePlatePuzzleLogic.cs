using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatePuzzleLogic : MonoBehaviour
{
    public GameObject pressurePlate0;
    public GameObject pressurePlate1;
    public GameObject pressurePlate2;
    public GameObject pressurePlate3;
    public GameObject pressurePlate4;
    public GameObject pressurePlate5;

    private PressurePlateCollision ppc0;
    private PressurePlateCollision ppc1;
    private PressurePlateCollision ppc2;
    private PressurePlateCollision ppc3;
    private PressurePlateCollision ppc4;
    private PressurePlateCollision ppc5;

    private Animator anim0;
    private Animator anim1;
    private Animator anim2;
    private Animator anim3;
    private Animator anim4;
    private Animator anim5;

    private Material mat0;
    private Material mat1;
    private Material mat2;
    private Material mat3;
    private Material mat4;
    private Material mat5;

    private string answerKey = "012345";
    public string sequenceKey = string.Empty;

    private bool appended0 = false;
    private bool appended1 = false;
    private bool appended2 = false;
    private bool appended3 = false;
    private bool appended4 = false;
    private bool appended5 = false;

    public GameObject rightSound;
    public GameObject wrongSound;
    private AudioSource pressurePlateAudioSourceRight;
    private AudioSource pressurePlateAudioSourceWrong;
    
    private bool hasPlayed = false;

    public GameObject swampHousePowerOrbParent;
    private Animator swampHousePowerOrbParentAnim;
    private bool puzzleSolved = false;

    void Awake()
    {
        ppc0 = pressurePlate0.GetComponent<PressurePlateCollision>();
        ppc1 = pressurePlate1.GetComponent<PressurePlateCollision>();
        ppc2 = pressurePlate2.GetComponent<PressurePlateCollision>();
        ppc3 = pressurePlate3.GetComponent<PressurePlateCollision>();
        ppc4 = pressurePlate4.GetComponent<PressurePlateCollision>();
        ppc5 = pressurePlate5.GetComponent<PressurePlateCollision>();

        anim0 = pressurePlate0.GetComponent<Animator>();
        anim1 = pressurePlate1.GetComponent<Animator>();
        anim2 = pressurePlate2.GetComponent<Animator>();
        anim3 = pressurePlate3.GetComponent<Animator>();
        anim4 = pressurePlate4.GetComponent<Animator>();
        anim5 = pressurePlate5.GetComponent<Animator>();

        mat0 = pressurePlate0.GetComponent<Renderer>().material;
        mat1 = pressurePlate1.GetComponent<Renderer>().material;
        mat2 = pressurePlate2.GetComponent<Renderer>().material;
        mat3 = pressurePlate3.GetComponent<Renderer>().material;
        mat4 = pressurePlate4.GetComponent<Renderer>().material;
        mat5 = pressurePlate5.GetComponent<Renderer>().material;

        pressurePlateAudioSourceRight = rightSound.GetComponent<AudioSource>();
        pressurePlateAudioSourceWrong = wrongSound.GetComponent<AudioSource>();

        swampHousePowerOrbParentAnim = swampHousePowerOrbParent.GetComponent<Animator>();
    }
    void Update()
    {
        if(ppc0.pressurePlateIsPressed && !appended0)
        {
            sequenceKey = sequenceKey + "0";
            appended0 = true;
        }

        if (ppc1.pressurePlateIsPressed && !appended1)
        {
            sequenceKey = sequenceKey + "1";
            appended1 = true;
        }

        if (ppc2.pressurePlateIsPressed && !appended2)
        {
            sequenceKey = sequenceKey + "2";
            appended2 = true;
        }

        if (ppc3.pressurePlateIsPressed && !appended3)
        {
            sequenceKey = sequenceKey + "3";
            appended3 = true;
        }

        if (ppc4.pressurePlateIsPressed && ! appended4)
        {
            sequenceKey = sequenceKey + "4";
            appended4 = true;
        }

        if (ppc5.pressurePlateIsPressed && !appended5)
        {
            sequenceKey = sequenceKey + "5";
            appended5 = true;
        }

        if (sequenceKey.Length == answerKey.Length && sequenceKey.Equals(answerKey))
        {
            if (!hasPlayed)
            {
                pressurePlateAudioSourceRight.Play();
                hasPlayed = true;
            }

            puzzleSolved = true;
            swampHousePowerOrbParentAnim.SetBool("PuzzleSolved", puzzleSolved);

        } else if (sequenceKey.Length == answerKey.Length)
        {
            ppc0.pressurePlateIsPressed = false;
            ppc1.pressurePlateIsPressed = false;
            ppc2.pressurePlateIsPressed = false;
            ppc3.pressurePlateIsPressed = false;
            ppc4.pressurePlateIsPressed = false;
            ppc5.pressurePlateIsPressed = false;

            anim0.SetBool("Pressed", false);
            anim1.SetBool("Pressed", false);
            anim2.SetBool("Pressed", false);
            anim3.SetBool("Pressed", false);
            anim4.SetBool("Pressed", false);
            anim5.SetBool("Pressed", false);

            appended0 = false;
            appended1 = false;
            appended2 = false;
            appended3 = false;
            appended4 = false;
            appended5 = false;

            mat0.color = new Color(255.0f, 0.0f, 0.0f);
            mat1.color = new Color(255.0f, 0.0f, 0.0f);
            mat2.color = new Color(255.0f, 0.0f, 0.0f);
            mat3.color = new Color(255.0f, 0.0f, 0.0f);
            mat4.color = new Color(255.0f, 0.0f, 0.0f);
            mat5.color = new Color(255.0f, 0.0f, 0.0f);

            sequenceKey = string.Empty;
            pressurePlateAudioSourceWrong.Play();
        }
    }
}
