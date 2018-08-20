using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(PlayerController))]

public class PlayerControllerCI : Editor
{
    AnimBool fadeVariablesMovement;
    AnimBool fadeVariablesAttack;
    AnimBool fadeVariablesHabilities;
    AnimBool fadeVariablesImpact;
    AnimBool fadeVariablesGO;
    AnimBool fadeVariablesMark;

    private void OnEnable()
    {
        var player = (PlayerController)target;
        if (fadeVariablesMovement != null)
            fadeVariablesMovement.valueChanged.AddListener(Repaint);
        if (fadeVariablesAttack != null)
            fadeVariablesAttack.valueChanged.AddListener(Repaint);
        if (fadeVariablesHabilities != null)
            fadeVariablesHabilities.valueChanged.AddListener(Repaint);
        if (fadeVariablesImpact != null)
            fadeVariablesImpact.valueChanged.AddListener(Repaint);
        if (fadeVariablesGO != null)
            fadeVariablesGO.valueChanged.AddListener(Repaint);
        if (fadeVariablesMark != null)
            fadeVariablesMark.valueChanged.AddListener(Repaint);
    }

    public override void OnInspectorGUI()
    {
        var player = (PlayerController)target;
        if (fadeVariablesMovement == null)
            fadeVariablesMovement = new AnimBool();
        if (fadeVariablesAttack == null)
            fadeVariablesAttack = new AnimBool();
        if (fadeVariablesHabilities == null)
            fadeVariablesHabilities = new AnimBool();
        if (fadeVariablesImpact == null)
            fadeVariablesImpact = new AnimBool();
        if (fadeVariablesGO == null)
            fadeVariablesGO = new AnimBool();
        if (fadeVariablesMark == null)
            fadeVariablesMark = new AnimBool();
        EditorGUILayout.Space();
        player.myLife = EditorGUILayout.FloatField("Life of Player", player.myLife);
        EditorGUILayout.Space();
        fadeVariablesMovement.target = EditorGUILayout.Foldout(fadeVariablesMovement.target, "Variables de Movimiento");
        if (EditorGUILayout.BeginFadeGroup(fadeVariablesMovement.faded))
        {
            player.moveSpeed = EditorGUILayout.FloatField("Velocidad del jugador", player.moveSpeed);
            player.jumpForce = EditorGUILayout.FloatField("Fuerza de salto", player.jumpForce);
            player.fallOffSpeed = EditorGUILayout.FloatField("Velocidad del caída forzada", Mathf.Abs(player.fallOffSpeed));
            player.gravity = EditorGUILayout.FloatField("Gravedad del juego", player.gravity);
            EditorGUILayout.Space();
            player.slowSpeedCharge = EditorGUILayout.FloatField(new GUIContent("Charge Velocity Slow", "Divide a la velocidad de la carga en porciones, ¿por cuánto?"), player.slowSpeedCharge);
            player.maxSpeedChargeTimer = EditorGUILayout.FloatField(new GUIContent("Max Charge Velocity", "Máximo multiplicador de carga para la velocidad"), player.maxSpeedChargeTimer);
            EditorGUILayout.Space();
        }
        fadeVariablesAttack.target = EditorGUILayout.Foldout(fadeVariablesAttack.target, "Variables de Ataques");
        if (EditorGUILayout.BeginFadeGroup(fadeVariablesAttack.faded))
        {
            player.weaponExtends = EditorGUILayout.FloatField("¿Qué rango tiene el arma?", player.weaponExtends);
            EditorGUILayout.LabelField("Ataque Normal:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            player.defaultAttackNormal = EditorGUILayout.FloatField(new GUIContent("Default Attack Normal", "Si la velocidad actual es 0, ¿cuánto empuje hace?"), player.defaultAttackNormal);
            player.impactVelocityNormal = EditorGUILayout.FloatField(new GUIContent("Impact Velocity Normal", "Si se está moviendo, ¿por cuánto se multiplica la velocidad?"), player.impactVelocityNormal);
            player.normalAttackCoolDown = EditorGUILayout.FloatField("Cooldown", player.normalAttackCoolDown);
            player.influenceOfMovementNormal = EditorGUILayout.FloatField(new GUIContent("Influence of Movement", "La velocidad del jugador influye en la velocidad de impacto ¿por cuánto se divide la velocidad del jugador para influir en el impacto?"), player.influenceOfMovementNormal);
            EditorGUILayout.LabelField("Ataque Arriba:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            player.defaultAttackUp = EditorGUILayout.FloatField(new GUIContent("Default Attack Up", "Si la velocidad actual es 0, ¿cuánto empuje hace?"), player.defaultAttackUp);
            player.impactVelocityUp = EditorGUILayout.FloatField(new GUIContent("Impact Velocity Up", "Si se está moviendo, ¿por cuánto se multiplica la velocidad?"), player.impactVelocityUp);
            player.upAttackCoolDown = EditorGUILayout.FloatField("Cooldown", player.upAttackCoolDown);
            player.influenceOfMovementUp = EditorGUILayout.FloatField(new GUIContent("Influence of Movement", "La velocidad del jugador influye en la velocidad de impacto ¿por cuánto se divide la velocidad del jugador para influir en el impacto?"), player.influenceOfMovementUp);
            EditorGUILayout.LabelField("Ataque Abajo:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            player.defaultAttackDown = EditorGUILayout.FloatField(new GUIContent("Default Attack Down", "Si la velocidad actual es 0, ¿cuánto empuje hace?"), player.defaultAttackDown);
            player.impactVelocityDown = EditorGUILayout.FloatField(new GUIContent("Impact Velocity Down", "Si se está moviendo, ¿por cuánto se multiplica la velocidad?"), player.impactVelocityDown);
            player.downAttackCoolDown = EditorGUILayout.FloatField("Cooldown", player.downAttackCoolDown);
            player.influenceOfMovementDown = EditorGUILayout.FloatField(new GUIContent("Influence of Movement", "La velocidad del jugador influye en la velocidad de impacto ¿por cuánto se divide la velocidad del jugador para influir en el impacto?"), player.influenceOfMovementDown);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            EditorGUILayout.TextField(new GUIContent("Impact Attack Velocity", "Se calcula internamente en base al Impact Velocity Normal/Up/Down multiplicado por el movimiento del jugador en el eje que golpee"), "Internal Calculation");
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndFadeGroup();
        fadeVariablesHabilities.target = EditorGUILayout.Foldout(fadeVariablesHabilities.target, "Variables de Habilidades");
        if (EditorGUILayout.BeginFadeGroup(fadeVariablesHabilities.faded))
        {
            EditorGUILayout.LabelField("Dash:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            player.dashSpeed = EditorGUILayout.FloatField("Velocidad del Dash", player.dashSpeed);
            player.dashDistance = EditorGUILayout.FloatField("Cuánta distancia recorre", player.dashDistance);
            player.dashCoolDown = EditorGUILayout.FloatField("Cooldown del Dash", player.dashCoolDown);
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFadeGroup();
        fadeVariablesImpact.target = EditorGUILayout.Foldout(fadeVariablesImpact.target, "Variables de Impacto");
        if (EditorGUILayout.BeginFadeGroup(fadeVariablesImpact.faded))
        {
            EditorGUILayout.LabelField("Stun:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            EditorGUILayout.TextField(new GUIContent("Receive Impact Velocity", "Se calcula internamente en base al Impact Attack Velocity dividido el Max Impact Velocity Stun"), "Internal Calculation");
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            player.maxStunVelocityLimit = EditorGUILayout.FloatField(new GUIContent("Max Velocity Stun Limit", "¿Cuál es el limite de la velocidad del jugador stuneado?"), player.maxStunVelocityLimit);
            player.maxImpactToInfinitStun = EditorGUILayout.FloatField(new GUIContent("Max Impact Velocity Stun", "¿A partir de qué velocidad el jugador no parará de volar hasta que choque?"), player.maxImpactToInfinitStun);
            player.residualStunImpact = EditorGUILayout.FloatField(new GUIContent("Residual Velocity Stun", "Si le pegan estando stuneado, se multiplicará el impacto nuevo por una porción de la velocidad actual, ¿qué porción?"), player.residualStunImpact);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("No Stun:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            player.hitHeadReject = EditorGUILayout.FloatField(new GUIContent("Hit Roof Rejection", "Si chocás el techo, ¿a qué velocidad te devuelve?"), Mathf.Abs(player.hitHeadReject));
            player.maxNoStunVelocityLimit = EditorGUILayout.FloatField(new GUIContent("Max Velocity Limit", "¿Cuál es el limite de la velocidad del jugador NO stuneado?"), player.maxNoStunVelocityLimit);
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFadeGroup();
        fadeVariablesGO.target = EditorGUILayout.Foldout(fadeVariablesGO.target, "Variables Referencias");
        if (EditorGUILayout.BeginFadeGroup(fadeVariablesGO.faded))
        {
            EditorGUILayout.LabelField("Ataques:", EditorStyles.boldLabel);
            player.attackColliders = (Collider)EditorGUILayout.ObjectField(player.attackColliders, typeof(Collider), true);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Particulas:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hit");
            player.hitParticles = (ParticleSystem)EditorGUILayout.ObjectField(player.hitParticles, typeof(ParticleSystem), true);
            EditorGUILayout.LabelField("Marked");
            player.PS_Marked = (ParticleSystem)EditorGUILayout.ObjectField(player.PS_Marked, typeof(ParticleSystem), true);
            EditorGUILayout.LabelField("Stunned");
            player.PS_Stunned = (ParticleSystem)EditorGUILayout.ObjectField(player.PS_Stunned, typeof(ParticleSystem), true);
            EditorGUILayout.LabelField("Charged");
            player.PS_Charged = (ParticleSystem)EditorGUILayout.ObjectField(player.PS_Charged, typeof(ParticleSystem), true);
            EditorGUILayout.LabelField("Fall off");
            player.PS_Fall = (ParticleSystem)EditorGUILayout.ObjectField(player.PS_Fall, typeof(ParticleSystem), true);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Vida HUD:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            player.myLifeUI = (LifeUI)EditorGUILayout.ObjectField(player.myLifeUI, typeof(LifeUI), true);
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("¿Para qué sirve este script?", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Este script es el que maneja todo el personaje con todas sus variables. Si querés cambiar cualquier cosa del personaje, va a estar acá."
            , MessageType.Info);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Nota extra:", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Si cambiás alguna variable de los personajes, ");
        EditorGUILayout.LabelField("por lo pronto no se actualizan el resto, por lo que tenes que: ");
        EditorGUILayout.LabelField("1- Click derecho en el editor del script del personaje que modificaste");
        EditorGUILayout.LabelField("2- Copy Component");
        EditorGUILayout.LabelField("3- En el resto de los personajes, click derecho en sus respectivos editores");
        EditorGUILayout.LabelField("4- Paste Component Values");
        Repaint();
    }
}
