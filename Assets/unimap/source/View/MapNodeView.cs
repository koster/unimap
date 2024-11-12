using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNodeView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public MapNodeState state;

    public GameObject statePassed;
    public GameObject stateSelected;
    public GameObject stateForeshadowed;
    public GameObject stateActive;

    List<GameObject> states = new();

    public bool isReachable;
    public bool isNext;
    public bool isMarkedForTraversal;
    
    void Start()
    {
        states.Add(statePassed);
        states.Add(stateSelected);
        states.Add(stateForeshadowed);
        states.Add(stateActive);
        
        SwitchState(GetMyBaseState());

        Init();
    }

    protected virtual void Init()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isNext) return;
        if (isMarkedForTraversal) return;
        
        SwitchState(stateSelected);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isNext) return;
        if (isMarkedForTraversal) return;
        
        SwitchState(GetMyBaseState());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isNext) return;
        
        OnClickNode();
    }

    public virtual void OnTraverse()
    {
        isMarkedForTraversal = true;
        
        SwitchState(stateSelected);
    }

    protected virtual void OnClickNode()
    {
    }

    GameObject GetMyBaseState()
    {
        return isReachable ? stateForeshadowed : statePassed;
    }

    void SwitchState(GameObject to)
    {
        for (var i = 0; i < states.Count; i++)
            states[i].SetActive(states[i] == to);
    }

    public virtual void SetTraversible(bool canReach)
    {
        isReachable = canReach;
    }
}