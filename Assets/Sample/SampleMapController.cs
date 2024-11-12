using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleMapController : MonoBehaviour
{
    static int startionNodeId;

    public Transform cameraHand;
    
    public MapSkin skin;
    public MapView mapView;

    public Transform preset;

    public MapPawn pawn;
    
    MapState mapState;

    IEnumerator Start()
    {
        mapState = MapLoader.LoadPreset(preset);
        mapView.Init(mapState, skin);

        mapView.SetPassedPoint(startionNodeId);
        pawn.SetTo(mapView.GetNode(startionNodeId).transform);
        cameraHand.transform.position = pawn.transform.position;
        
        yield return pawn.ShowAppear();
    }

    public void OnClick(SampleMapNode node)
    {
        StartCoroutine(MoveInto(node.state.id));
    }
    
    bool isMoving;
    
    public IEnumerator MoveInto(int nodeId)
    {
        if (isMoving)
            yield break;

        isMoving = true;
        startionNodeId = nodeId;
        
        var mapNode = mapView.GetNode(nodeId);
        
        yield return pawn.ShowMove(mapNode);
        
        yield return new WaitForSeconds(1f);

        yield return pawn.ShowDisappear();
        
        // load your level, store node index
        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        cameraHand.position = Vector3.Lerp(cameraHand.position, pawn.transform.position, 0.1f);
    }
}
