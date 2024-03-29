using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int playerScore;
    public int PlayerScore { get; set; }

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject[] ballPositions;

    [SerializeField] private GameObject cueBall;
    [SerializeField] private GameObject ballLine;

    [SerializeField] private float xInput;
    [SerializeField] private float force;

    [SerializeField] private GameObject camera;
    [SerializeField] private TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        camera = Camera.main.gameObject;
        CameraBehindBall();

        UpdateScoreText();

        //set balls
        //SetBalls(BallColors.White, 0);
        SetBalls(BallColors.Red, 1);
        SetBalls(BallColors.Yellow, 2);
        SetBalls(BallColors.Green, 3);
        SetBalls(BallColors.Brown, 4);
        SetBalls(BallColors.Blue, 5);
        SetBalls(BallColors.Pink, 6);
        SetBalls(BallColors.Black, 7);
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"Player Score: {PlayerScore}";
    }

    // Update is called once per frame
    void Update()
    {
        RotateBall();

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ShootBall();
        }

        if (Input.GetKeyDown(KeyCode.B)) 
        {
            StopBall();
        }
    }

    void SetBalls(BallColors color, int pos) 
    {
       GameObject ball = Instantiate(ballPrefab, ballPositions[pos].transform.position, Quaternion.identity);
       Ball b = ball.GetComponent<Ball>();
        b.SetColorAndPoint(color);
    }

    void RotateBall() 
    {
        xInput = Input.GetAxis("Horizontal");
        cueBall.transform.Rotate(new Vector3(0f,xInput/5,0f));
    }

    void ShootBall() 
    {
        camera.transform.parent = null;
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        rd.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        ballLine.SetActive(false);
    }

    void CameraBehindBall() 
    {
        camera.transform.parent = cueBall.transform;
        camera.transform.position = cueBall.transform.position + new Vector3(0f, 15f, -10f);
    }

    void StopBall() 
    {
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        rd.velocity = Vector3.zero;
        rd.angularVelocity = Vector3.zero;

        cueBall.transform.eulerAngles = Vector3.zero;
        CameraBehindBall();
        camera.transform.eulerAngles = new Vector3(40f, 0f, 0f);
        ballLine.SetActive(true);
    }
}
