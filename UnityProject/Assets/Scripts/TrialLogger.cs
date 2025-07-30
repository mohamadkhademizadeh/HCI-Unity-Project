using UnityEngine;
using System.IO;
using System.Text;

public class TrialLogger : MonoBehaviour
{
    [SerializeField] private string fileName = "data.csv";
    [SerializeField] private string participantId = "P001";
    [SerializeField] private string orderCode = "A";

    private string FilePath => Path.Combine(Application.persistentDataPath, fileName);

    void Awake() {
        if (!File.Exists(FilePath)) {
            File.WriteAllText(FilePath,
                "participant_id,condition,trial,task_id,target_id,hit,selection_time_ms,errors,order\n",
                Encoding.UTF8);
        }
        Debug.Log($"Logging to: {FilePath}");
    }

    public void Log(string condition, int trial, string taskId, string targetId, bool hit, int selectionTimeMs, int errors)
    {
        string line = string.Join(",",
            participantId, condition, trial, taskId, targetId,
            hit ? "1" : "0", selectionTimeMs.ToString(), errors.ToString(), orderCode);
        File.AppendAllText(FilePath, line + "\n", Encoding.UTF8);
    }
}
