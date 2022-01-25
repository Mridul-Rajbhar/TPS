using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1f;
    public float Deathforce = 10f;

    public float chaseDistance = 20f;
}
