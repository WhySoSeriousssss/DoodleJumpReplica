using UnityEngine;

public class ScoreTrack : MonoBehaviour {

    float score = 0;
    float scoreMultiplier = 111.11f;

    public int Score
    {
        get { return (int)score; }
    }

    private void Update()
    {
        score = scoreMultiplier * transform.position.y;
        FindObjectOfType<GameplayHUD>().SetScore((int)score);
    }
}
