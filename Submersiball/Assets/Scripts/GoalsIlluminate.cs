using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsIlluminate : MonoBehaviour
{
    MeshRenderer meshRend;
    Material goalMat; 
    Color emissionColor;
    [SerializeField][Range(1,2)] int teamNum = 1;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        goalMat = meshRend.material;
        if (teamNum == 1) { emissionColor = GameManager.current.team1Color; }
        else { emissionColor = GameManager.current.team2Color; }


        GameEvents.current.onStartMatch += IlluminateGoal;
        IlluminateGoal();
    }
    private void OnDestroy()
    {
        GameEvents.current.onStartMatch -= IlluminateGoal;
    }

    void IlluminateGoal()
    {
        if (teamNum == 1) { emissionColor = GameManager.current.team1Color; }
        else { emissionColor = GameManager.current.team2Color; }

        goalMat.EnableKeyword("_EMISSION");
        goalMat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

        goalMat.SetColor("_EmissionColor", emissionColor * 3); 
        RendererExtensions.UpdateGIMaterials(meshRend);

        // Inform Unity's GI system to recalculate GI based on the new emission map.
        DynamicGI.SetEmissive(meshRend, emissionColor * 3);
        DynamicGI.UpdateEnvironment();
    }
}
