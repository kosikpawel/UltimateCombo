using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    public bool shadowMode = false;
    public GameObject Spawner;
    public bool MissileRaidMode;
    public BubbleGeneratorScript bubbleGenerator;
    public float wallTimeWithoutControl;

    void Start()
    {
        MissileRaidMode = false;
        shadowMode = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shadowMode)
        {
            if (collision.gameObject.tag == "PlayerKillerObstacle" || collision.gameObject.tag =="Missile" || collision.gameObject.tag == "Cutlery")
            {
                shadowMode = true;
                transform.tag = "PlayerOff";
                GetComponent<PlayerMovement>().OnDeathWall();
                if (MissileRaidMode)
                {
                    MissileRaidMode = false;
                    GetComponent<PlayerHPSystem>().infoGenerator.MakeInfoObject("Missile Raid Failed");
                    bubbleGenerator.SetAlternativeMode(false);
                    bubbleGenerator.GetComponent<ComboTaker>().obstacleGenerator.obstacleSpawning = true;
                }
            }
        }
        if ( collision.gameObject.tag == "PlayerKiller" )
        {
            shadowMode = true;
            transform.tag = "PlayerOff";
            GetComponent<PlayerMovement>().OnDeathWall();
        }
        if (collision.gameObject.tag == "JumpWall")
        {
            
            if (transform.position.x > 0)
            {
                GetComponent<PlayerMovement>().PlayerJumpsOnWall(2, wallTimeWithoutControl);
            }
            else
            {
                GetComponent<PlayerMovement>().PlayerJumpsOnWall(1, wallTimeWithoutControl);
            }
        }
        if (collision.gameObject.tag == "JumpWallHori")
        {
            if (transform.position.y > 0)
            {
                GetComponent<PlayerMovement>().PlayerJumpsOnWall(4, wallTimeWithoutControl);
            }
            else
            {
                GetComponent<PlayerMovement>().PlayerJumpsOnWall(3, wallTimeWithoutControl);
            }
        }
    }
}
