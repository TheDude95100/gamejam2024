using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chargementText;
    [SerializeField] private float timeLoading = 3f; // seconds
    private float startTime;
    private string startText;
    private int loadStage = 0;

    private void OnEnable() {
        startTime = Time.time;
        startText = chargementText.text;
    }
    private void FixedUpdate(){

               if (startTime + ( 0.25f * timeLoading) > Time.time) {
            loadStage = 1;
        } else if (startTime + ( 0.50f * timeLoading) > Time.time) {
            loadStage = 2;
        } else if (startTime + ( 0.75f * timeLoading) > Time.time) {
            loadStage = 3;
        } else if (startTime + ( 1.00f * timeLoading) > Time.time) {
            loadStage = 4;
        }

        switch (loadStage)
        {
            case 1:
                chargementText.text = $"{startText} .";
                break;
            case 2:
                chargementText.text = $"{startText} ..";
                break;
            case 3:
                chargementText.text = $"{startText} ...";
                break;
            case 4:
                GameManager.Scene_Level();
                break;
            default:
             break;
        }

    }

}
