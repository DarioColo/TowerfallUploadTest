using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public Controller controller;
    public int id;

    PlayerController player;
    public string horizontalMove;
    public string verticalMove;
    public string jumpButton;
    public string normalAttack;
    public string downAttack;
    public string upAttack;
    public string hability1Button;
    public string hability2Button;
    public string hability3Button;

    public enum Controller
    {
        J,
        K
    }

    private void Start()
    {
        player = GetComponent<PlayerController>();
        SetPlayerInput();
    }

    void Update()
    {
        if (!GameManager.Instance.startingGame && !GameManager.Instance.finishedGame)
        {
            if (player && player.myLife > 0)
            {
                if (Input.GetButtonDown(jumpButton))
                    player.Jump();
                if (Input.GetButtonDown(hability1Button))
                    player.Dash();
                if (Input.GetButtonDown(hability2Button))
                    player.FallOff();
                if (Input.GetButtonDown(normalAttack))
                    player.AttackNormal("Pressed");
                if (Input.GetButtonUp(normalAttack))
                    player.AttackNormal("Realese");
                if (Input.GetButtonDown(downAttack))
                    player.AttackDown("Pressed");
                if (Input.GetButtonUp(downAttack))
                    player.AttackDown("Realese");
                if (Input.GetButtonDown(upAttack))
                    player.AttackUp("Pressed");
                if (Input.GetButtonUp(upAttack))
                    player.AttackUp("Realese");
            }
        }
    }

    public float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis(controller.ToString() + "_MainHorizontal_P" + id);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis(controller.ToString() + "_MainVertical_P" + id);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public void SetPlayerInput()
    {
        horizontalMove = controller.ToString() + "_MainHorizontal_P" + id;
        verticalMove = controller.ToString() + "_MainVertical_P" + id;
        jumpButton = controller.ToString() + "_JumpButton_P" + id;
        normalAttack = controller.ToString() + "_NormalAttack_P" + id;
        downAttack = controller.ToString() + "_DownAttack_P" + id;
        upAttack = controller.ToString() + "_UpAttack_P" + id;
        hability1Button = controller.ToString() + "_Hability1_P" + id;
        hability2Button = controller.ToString() + "_Hability2_P" + id;
        hability3Button = controller.ToString() + "_Hability3_P" + id;
    }
}
