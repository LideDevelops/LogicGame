using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CuriosOtter.LogicGame.Gates;
using TMPro;
using UnityEngine;
using Utility;

[RequireComponent(typeof(SpriteRenderer))]
public class PlaceableGate : MonoBehaviour
{
    private IGate gate;
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite GateImage;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        //TODO: remove later
        SetGate();
    }

    public void SetGate()
    {
        //TODO: Open set gate dialog
        gate = new OrGate();
        GateImage = gate.GateImage;
        renderer.sprite = GateImage;
        DrawInputPorts();
        DrawOutputPorts();
    }

    private void DrawInputPorts()
    {
        ConnectionPointSpawner.SpawnConnectionPoints(transform, renderer, gate.InputNames, true);
    }
    
    private void DrawOutputPorts()
    {
        ConnectionPointSpawner.SpawnConnectionPoints(transform, renderer, gate.OutputNames, false);
    }
    
}
