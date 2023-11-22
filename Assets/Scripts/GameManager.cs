using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int playerScore = 0;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject[] ballPositions;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //set balls
        SetBalls(BallColors.White, 0);
        SetBalls(BallColors.Red, 1);
        SetBalls(BallColors.Yellow, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBalls(BallColors color, int pos) 
    {
       GameObject ball = Instantiate(ballPrefab, ballPositions[pos].transform.position, Quaternion.identity);
       Ball b = ball.GetComponent<Ball>();
        b.SetColorAndPoint(color);
    }
}
