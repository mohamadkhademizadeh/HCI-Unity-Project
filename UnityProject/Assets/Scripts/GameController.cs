using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    public SelectionTechnique gaze;
    public SelectionTechnique joystick;
    public TrialLogger logger;
    public List<Target> targets;
    public string currentCondition = "gaze";
    public float interTrialInterval = 0.5f;

    private int trialIndex = 0;
    private Target activeTarget;
    private float startTime;
    private int errors;

    void Start() {
        SetCondition(currentCondition);
        StartCoroutine(RunBlock(20));
    }

    public void SetCondition(string cond) {
        currentCondition = cond.ToLower();
        gaze?.EnableTechnique(currentCondition == "gaze");
        joystick?.EnableTechnique(currentCondition == "joystick");
    }

    IEnumerator RunBlock(int nTrials) {
        var sequence = targets.OrderBy(_ => Random.value).ToList();
        for (int i = 0; i < nTrials; i++) {
            trialIndex = i + 1;
            activeTarget = sequence[i % sequence.Count];
            foreach (var t in targets) t.SetHighlighted(false);
            activeTarget.SetHighlighted(true);
            startTime = Time.time;
            errors = 0;

            bool selected = false;
            while (!selected) {
                var tech = currentCondition == "gaze" ? gaze : joystick;
                var aimed = tech.GetAimedTarget();
                bool trigger = tech.SelectionTriggered();

                if (trigger) {
                    bool hit = aimed == activeTarget;
                    if (!hit) errors += 1;
                    activeTarget.Feedback(hit);
                    if (hit) {
                        int ms = Mathf.RoundToInt((Time.time - startTime) * 1000f);
                        logger.Log(currentCondition, trialIndex, "T01", activeTarget.TargetId, true, ms, errors);
                        selected = true;
                        yield return new WaitForSeconds(interTrialInterval);
                    }
                }
                yield return null;
            }
        }
        Debug.Log("Block finished.");
    }
}
