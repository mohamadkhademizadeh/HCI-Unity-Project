using UnityEngine;

public abstract class SelectionTechnique : MonoBehaviour
{
    public abstract void EnableTechnique(bool enable);
    public abstract Target GetAimedTarget();
    public abstract bool SelectionTriggered();
}
