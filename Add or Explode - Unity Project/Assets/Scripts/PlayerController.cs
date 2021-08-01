using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    /*This script controls the player's movement, adds digits to the player body, updates the score
     * in HighScore.cs, generates tiles as needed, spawns squares, and spawns bullets*/
    public GameObject firstChild;
    public GameObject cameraFocus;
    public CinemachineVirtualCamera virtualCamera;
    public Tilemap tilemap;
    public Sprite[] spriteList;
    public Tile[] tiles;

    Vector2 stageSize;
    Vector2Int tileStageSize;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;
    Vector2 moveInput;
    PlayerInput playerinput;
    TextController textController;
    List<GameObject> digits = new List<GameObject>(); //List of all digits
    GameObject digitPrefab; //digits are within the player
    GameObject squarePrefab; //squares are in the world
    GameObject bulletPrefab; //kaboom
    GameObject upgradeEffect;

    const int tilemapPadding = 3;
    const int BULLET_LIMIT = 5; //If I let this number grow, the game gets ridiculously hard
    int speed = 5; //Units per second
    int speedThreshold = 50;
    int slowDown = 0;
    bool isOver = false;
    int score = 1; //uint because I want a high upperbound
    float innerRadius;
    float outerRadius;    
    public System.Random rand = new System.Random();
    System.Random tilesRand = new System.Random(0);
    List<GameObject> squareList = new List<GameObject>();
    List<GameObject> bulletList = new List<GameObject>();

    private void Awake() => playerinput = new PlayerInput();
    private void OnEnable() => playerinput.Enable();
    private void UpdateText() => textController.UpdateText(score, cameraFocus.transform.position);
    private void Update() => moveInput = playerinput.All.Move.ReadValue<Vector2>();

    private void Start()
    {
        digitPrefab = (GameObject)Resources.Load("Prefabs/Digit Prefab");
        squarePrefab = (GameObject)Resources.Load("Prefabs/Square Prefab");
        bulletPrefab = (GameObject)Resources.Load("Prefabs/Bullet Prefab");
        upgradeEffect = (GameObject)Resources.Load("Prefabs/Upgrade Particle Effect");

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        textController = GameObject.FindGameObjectWithTag("Game Canvas").GetComponent<TextController>();

        //Initalize digits
        digits.Add(gameObject); //Like a padding object so everything runs smoothly. Don't remove this line.
        digits.Add(firstChild);

        UpdateText();
        UpdateOrthographicSize();
        innerRadius = stageSize.x / 2 + 5;
        outerRadius = innerRadius + 10; //The explosion controller has this set to +5 instead of 10

        //Introductory squares and tiles
        MakeSquare(rand.Next(1, 5), new Vector2(0, 3));
        MakeSquare(rand.Next(1, 5), new Vector2(-1, 6));
        TilemapUpdate();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
        if (moveInput != Vector2.zero) { TilemapUpdate(); }

        if (!isOver) { UpdateText(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet Prefab(Clone)")
        {
            StartCoroutine(GameOver(collision.gameObject));
            return;
        }

        //----------Increment score
        int collisionValue = Convert.ToInt32(collision.gameObject.name);
        score += collisionValue;
        UpdateText();

        //----------Destroy game object
        collision.gameObject.GetComponent<SquareController>().CollisionWork("collide");
        squareList.RemoveAt(squareList.IndexOf(collision.gameObject));

        //----------Cull object then increase the threshold
        int squareCap = 40 + speed;
        if (squareList.Count >= squareCap)
        {
            //Destroy all squares not seen by the camera
            GameObject currentSquare;
            for (int c = 0; c < squareCap - 3; c++)
            {
                currentSquare = squareList[c];
                Vector3 screenPos = Camera.main.WorldToViewportPoint(currentSquare.transform.position);
                //0.2 padding so you don't see squares vanish from the edge of your vision
                if (screenPos.x < -0.2 || screenPos.x > 1.2 || screenPos.y < -0.2 || screenPos.y > 1.2)
                {
                    currentSquare.GetComponent<SquareController>().CollisionWork("cull");
                    squareList.RemoveAt(c);
                    //the list shrinks, so a decrement in the looping limit and counter is neccesary to avoid errors
                    squareCap--;
                    c--;
                }
            }
        }

        if (score > speedThreshold && speed <= 10)
        {
            slowDown += 10;
            speedThreshold += 50 + slowDown; //Distance to next upgrade increases
            speed += 1;

            SFXPlayer.Play("upgrade", 0.75f);
            GameObject upgrade = Instantiate(upgradeEffect);
            upgrade.transform.parent = transform;
            upgrade.transform.localPosition = cameraFocus.transform.localPosition;
            upgrade.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            SFXPlayer.Play($"laser_{rand.Next(0, 4)}", 0.3f);
        }

        int bulletCap = BULLET_LIMIT;
        if (bulletList.Count >= bulletCap)
        {
            //Destroy all bullets not seen by the camera
            GameObject currentBullet;
            for (int c = 0; c < bulletCap - 3; c++) //haha c++ C++
            {
                currentBullet = bulletList[c];
                Vector3 screenPos = Camera.main.WorldToViewportPoint(currentBullet.transform.position);
                if (screenPos.x < -0.2 || screenPos.x > 1.2 || screenPos.y < -0.2 || screenPos.y > 1.2)
                {
                    Destroy(currentBullet);
                    bulletList.RemoveAt(c);
                    //the list shrinks, so a decrement in the looping limit and counter is neccesary to avoid errors
                    bulletCap--;
                    c--;
                }
            }
        }

        //----------Now that objects have been freed up, create squares and bullets in spawning circle
        int toGenerate = rand.Next(7, 10);
        for (int j = 1; j <= toGenerate; j++)
        { 
            MakeSquare(rand.Next(1, 10), GetCoords(innerRadius, outerRadius)); 
        }

        //MAKE BULLET LIMIT GROW TO AN ALMOST UNBEARABLE POINT
        toGenerate = rand.Next(1, BULLET_LIMIT);
        for (int j = 1; j <= toGenerate; j++) { MakeBullet(); }

        //----------Update the digits in the player object
        IEnumerable<int> iterableOfScore = Convert.ToString(score).Select(
            x => Convert.ToInt32(Convert.ToString(x))
        );

        GameObject workingObj;
        int i = 1;
        bool doUpdate = false;
        foreach (int currentDigit in iterableOfScore)
        {
            if (i < digits.Count)
            {
                workingObj = digits[i]; //Refer the working object to an existing digit if possible
            }
            else //Else, make the object
            {
                doUpdate = true;
                workingObj = Instantiate(digitPrefab);
                workingObj.transform.parent = transform; //You are my child
                workingObj.transform.localPosition = new Vector3(digits.Count - 1, 0, 0);

                //Colliders and camera
                boxCollider.size = new Vector2(digits.Count, 1);
                boxCollider.offset += new Vector2(0.5f, 0);
                cameraFocus.transform.localPosition += new Vector3(0.5f, 0, 0);

                //Keep track of it
                digits.Add(workingObj);
            }
            workingObj.GetComponent<SpriteRenderer>().sprite = spriteList[currentDigit];
            i++;
        }
        
        if (doUpdate) { UpdateOrthographicSize(); }
    }

    IEnumerator GameOver(GameObject collision)
    {
        isOver = true;
        playerinput.Disable();

        collision.GetComponent<BulletController>().Explode();

        int radius = 15;
        float force = collision.GetComponent<Rigidbody2D>().velocity.magnitude;
        Vector2 source = collision.transform.position;

        foreach (GameObject i in digits)
        {
            Rigidbody2D obj = i.GetComponent<Rigidbody2D>();

            obj.bodyType = RigidbodyType2D.Dynamic;
            obj.AddTorque(rand.Next(-180, 180));
            obj.AddExplosionForce(force, source, radius);
        }

        Destroy(gameObject.GetComponent<BoxCollider2D>()); //Don't allow repeat collisions
        virtualCamera.m_Follow = null; //Freeze camera;
        SFXPlayer.Play("explode");
        yield return new WaitForSeconds(3); //Let animation play

        SceneManager.LoadScene("Main Scene"); //Reload this scene
    }

    private void MakeSquare(int number, Vector2 position)
    {
        GameObject newSquare = Instantiate(squarePrefab);
        newSquare.transform.position = position;
        newSquare.name = Convert.ToString(number);

        newSquare.GetComponent<SpriteRenderer>().sprite = spriteList[number];
        //Sprite 1 is half the size and sprite 9 is twice the size.
        //Using linear equations, these specific numbers are found
        newSquare.transform.localScale = ((0.1875f * number) + 0.3125f) * Vector3.one;
        squareList.Add(newSquare);
    }

    private void MakeBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab);

        //The bullet aims for a random point in a circle around the player with a radius of 4 units
        Vector2 spawning_position = GetCoords(innerRadius, outerRadius);
        newBullet.transform.position = spawning_position;
        Vector2 target = GetCoords(0.5f, 4);
        Vector2 velocity = (target - spawning_position).normalized;

        //It's 1-5 times normalized speed
        newBullet.GetComponent<Rigidbody2D>().velocity = velocity * rand.Next(2, 8);
        bulletList.Add(newBullet);
    }

    private void UpdateOrthographicSize()
    {
        float ratio = 1 / virtualCamera.m_Lens.Aspect; 
        float count = (digits.Count - 1) / 2f;
        virtualCamera.m_Lens.OrthographicSize = (count + 5 / ratio) * ratio;

        //Width and height of screen
        stageSize = new Vector2(2f * virtualCamera.m_Lens.OrthographicSize * virtualCamera.m_Lens.Aspect, 2f * virtualCamera.m_Lens.OrthographicSize);
        Vector2 temp = stageSize * 1.5f; //scale because the tilemap is scaled up by 1.5 
        tileStageSize = new Vector2Int(
            (int)Math.Ceiling(temp.x) + tilemapPadding,
            (int)Math.Ceiling(temp.y) + tilemapPadding
        );
    }

    private void TilemapUpdate()
    {
        //Must divide by 1.5f since the tilemap is scaled up by 1.5
        Vector3Int bottomLeft = Vector3Int.RoundToInt(Camera.main.ScreenToWorldPoint(Vector3Int.zero) / 1.5f);
        bottomLeft = new Vector3Int(bottomLeft.x - (tilemapPadding / 2), bottomLeft.y - (tilemapPadding / 2), 0); //z defaults to -7, idk why

        for (int y = 0; y < tileStageSize.y; y++)
        {
            for (int x = 0; x < tileStageSize.x; x++)
            {
                Vector3Int pos = bottomLeft + new Vector3Int(x, y, 0);
                if (tilemap.GetTile(pos) == null)
                {
                    tilemap.SetTile(pos, tiles[tilesRand.Next(0, 7)]);
                }
            }
        }
    }

    private Vector2 GetCoords(float min, float max)
    {
        float radians = rand.Next(1, 360) * Mathf.Deg2Rad;
        float distance = ((float)rand.NextDouble() * (max - min)) + min;
        return
        new Vector2(
            distance * Mathf.Cos(radians),
            distance * Mathf.Sin(radians)
        ) + (Vector2)cameraFocus.transform.position;
    }
}
