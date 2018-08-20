using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInput))]
public class PlayerInputCI : Editor
{
    public Controller controllerCI;

    public enum Controller
    {
        J,
        K
    }

    private void OnEnable()
    {
        var playerInp = (PlayerInput)target;
    }

    public override void OnInspectorGUI()
    {
        var playerInp = (PlayerInput)target;
        EditorGUILayout.Space();
        playerInp.controller = (PlayerInput.Controller)EditorGUILayout.EnumPopup("¿Qué control usa?", playerInp.controller);
        playerInp.id = EditorGUILayout.IntField("¿Qué ID tiene el jugador?", playerInp.id);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("¿Para qué sirve este script?", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Este script se encarga de tomar el tipo de control que usará el jugador: Joystick (J) o Teclado (K). " +
               "También se ocupa de asignarle una ID para que cada jugador maneje a un solo personaje", MessageType.Info);
        Repaint();
    }

}
