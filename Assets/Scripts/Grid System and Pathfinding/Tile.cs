﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Tile : MonoBehaviour, ITurnListener
{
    private float rate = 5f;
    private float positionOffset = 0;
    private float scalingMagnitude = 0.9f;
    private Vector3 originalScale;
    private float speed = 0.5f;
    private float currentModifier = 0f;

    private TileStates currentState = TileStates.INACTIVE;

    [SerializeField] private Material ActiveMaterial;
    [SerializeField] private Material AttackMaterial;
    [SerializeField] private Material HealMaterial;
    [SerializeField] private Material ProjectMaterial;

    [SerializeField] private MeshRenderer mesh;

    public enum TileStates
    {
        INACTIVE,
        ACTIVE,
        ATTACK,
        HEAL,
        PROJECT
    }

    void Start()
    {
        originalScale = transform.localScale;
        positionOffset = (transform.position.x * transform.position.x + transform.position.z + transform.position.z) /
                         16 * Mathf.PI;
        
        InitiativeSystem.registerListener(this);
    }

    public void setState(TileStates state)
    {
        currentState = state;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(currentState == TileStates.INACTIVE)
        {
            currentModifier = Mathf.MoveTowards(currentModifier, 0, rate * Time.deltaTime);
            
            transform.localScale = originalScale * currentModifier;
        } else
        {

            float myRate = rate;
            
            if (currentState == TileStates.ATTACK)
            {
                myRate *= 2f;
                mesh.material = AttackMaterial;

            } else if (currentState == TileStates.HEAL)
            {
                myRate *= 0.5f;
                mesh.material = HealMaterial;
            } else if (currentState == TileStates.PROJECT)
            {
                mesh.material = ProjectMaterial;
            }
            else
            {
                mesh.material = ActiveMaterial;
            }
            
            float modifier = Mathf.Cos(Time.time * myRate + positionOffset) * 0.5f + 0.5f;
            modifier = (1.0f - scalingMagnitude) * modifier + scalingMagnitude;

            currentModifier = Mathf.MoveTowards(currentModifier, modifier, rate * Time.deltaTime);
            
            transform.localScale = originalScale * currentModifier;
        }
        
        
        
        
    }

    public void NextTurn(Unit unit)
    {
        // Reset State
    }
}
