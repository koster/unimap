using System.Collections;
using UnityEngine;

public class MapPawn : MonoBehaviour
{
    public Transform jumper;
    
    void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public IEnumerator ShowAppear()
    {
        yield return LerpScale(Vector3.one);
    }

    public IEnumerator ShowDisappear()
    {
        yield return LerpScale(Vector3.zero);
    }

    public IEnumerator ShowMove(Transform mapNode)
    {
        while (Vector3.Distance(transform.position, mapNode.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, mapNode.position, Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetTo(Transform getNode)
    {
        transform.position = getNode.position;
    }

    IEnumerator LerpScale(Vector3 to)
    {
        for (var i = 0f; i < 1f; i += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, to, i);
            yield return new WaitForEndOfFrame();
        }
    }
}