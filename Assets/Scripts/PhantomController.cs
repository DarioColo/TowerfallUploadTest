using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomController : Player
{

    public Vector3 moveVector;
    private IMove _iMove;
    private Dictionary<string, IMove> myMoves = new Dictionary<string, IMove>();
    public bool invulnerable;
    public float currentInvul;

    void Start()
    {
        SetMovements();
    }

    void Update()
    {
        Move();
        //transform.position += transform.forward * Time.deltaTime * 6;
        if(invulnerable)
            Invulnerable();
    }

    public void Move()
    {
        foreach (var m in myMoves.Values)
            m.Update();
        myMoves["PhantomMovement"].Move();
    }

    private void SetMovements()
    {
        myMoves.Add(typeof(PhantomMovement).ToString(), new PhantomMovement(this));
    }

    void Invulnerable()
    {
        //Falta animacion
        currentInvul += Time.deltaTime;
        if(currentInvul > 3f)
        {
            invulnerable = false;
            currentInvul = 0;
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(Random.Range(-35f, 35f), Random.Range(16f, 60f), 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Hitbox"))
        {
            if (!CheckParently(other.transform))
            {
                PlayerController target = TargetScript(other.transform);
                if(target)
                    target.GetComponent<PlayerController>().stunnedByGhost = true;
                invulnerable = true;
            }
        }
    }

    public bool CheckParently(Transform t)
    {
        if (t.parent == null)
            return false;
        if (t.parent == transform)
            return true;
        return CheckParently(t.parent);
    }

    public PlayerController TargetScript(Transform t)
    {
        if (t.GetComponent<PlayerController>())
            return t.GetComponent<PlayerController>();
        if (t.parent == null)
            return null;
        return TargetScript(t.parent);
    }
}
