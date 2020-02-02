using System.Collections.Generic;
using UnityEngine;

class PatrolState : State
{
    [SerializeField] private List<Node> destinations;

    private Unit myUnit;
    private Path myPath;
    private int targetIndex;
    private int pathIndex;

    private Node prevNode;

    public override void EnterState (StateMachine parent)
    {
        myUnit = parent.GetComponent<Unit> ();
        if (myUnit == null)
        {
            Debug.LogWarning ("Patrol assigned to state machine without unit.");
        }

        myUnit.transform.position = destinations[0].transform.position + Vector3.up;
        destinations[0].occupyingUnit = myUnit;
        prevNode = null;

        targetIndex = 1;
    }

    public override void UpdateState (StateMachine parent)
    {
        if (!myUnit.CurrentTurn)
        {
            return;
        }

        if (myPath == null)
        {
            myPath = Astar.CalculatePath (destinations[targetIndex],
                destinations[(targetIndex + 1) % destinations.Count], LevelStateManager.Instance.generatedGrid);
            pathIndex = 0;
        }

        Vector3 nextDestinationVector = myPath.nodes[pathIndex].transform.position + Vector3.up;

        if (Vector3.Distance (parent.transform.position, nextDestinationVector) < 0.001)
        {
            if (++pathIndex == myPath.nodes.Count)
            {
                myPath = null;
                targetIndex = (targetIndex + 1) % destinations.Count;
            }
            InitiativeSystem.nextTurn ();
            return;
        }

        parent.transform.position = Vector3.MoveTowards (parent.transform.position, nextDestinationVector,
            myUnit.speed * Time.deltaTime);
        myPath.nodes[pathIndex].occupyingUnit = myUnit;

        if (prevNode != null)
            prevNode.occupyingUnit = null;

        prevNode = myPath.nodes[pathIndex];

    }
}