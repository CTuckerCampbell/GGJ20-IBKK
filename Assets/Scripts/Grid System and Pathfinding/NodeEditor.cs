﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CanEditMultipleObjects()]
[CustomEditor(typeof(Node))]
public class NodeEditor : UnityEditor.Editor
{
    Color ogColor = Color.white;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        foreach (Object obj in targets)
        {
            var n = obj as Node;

            if (n == null) continue;

            var mesh = n.GetComponent<MeshRenderer>();
            
            if (n.isStairs)
            {
                n.transform.localScale = (n.direction) ? new Vector3(1, .5f, .5f) : new Vector3(.5f, .5f, 1);
           
            }

            
            if (!n.walkable)
            {
                mesh.material.SetColor("_BaseColor", Color.red);
            }
            else if (n.isStairs)
            {
                mesh.material.color = Color.blue;
                List<int> x = new List<int>();
                List<int> copy = x.Where(e => true).ToList();

            }
            else
                mesh.material.SetColor("_BaseColor", Color.grey);
                

            foreach (var neighbor in n.neighbors)
            {
                Debug.DrawLine(n.transform.position + Vector3.up, neighbor.transform.position + Vector3.up);
            }

        }
    }
    private void OnSceneGUI()
    {

    }
}
#endif