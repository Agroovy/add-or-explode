using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class ExplosionController : MonoBehaviour
{
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
    List<GameObject> digits = new List<GameObject>(); //List of all digits
    GameObject digitPrefab; //digits are within the player
    GameObject squarePrefab; //squares are in the world
    GameObject bulletPrefab;

    int score;
    const int TILEMAP_PADDING = 3;
    float innerRadius;
    float outerRadius;
    bool isExploding = false;
    readonly System.Random rand = new System.Random();
    readonly System.Random tilesRand = new System.Random(0);

    private void FixedUpdate() { if (rb.velocity != Vector2.zero) { TilemapUpdate(); } }

    private void Start()
    {
        digitPrefab = (GameObject)Resources.Load("Prefabs/Digit Prefab");
        squarePrefab = (GameObject)Resources.Load("Prefabs/Square Prefab");
        bulletPrefab = (GameObject)Resources.Load("Prefabs/Bullet Prefab");

        //Setup variables
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        digits.Add(gameObject); //Like a padding object so everything runs smoothly. Don't remove this line.
        digits.Add(firstChild);

        UpdateOrthographicSize();
        innerRadius = stageSize.x / 2 + 5;
        outerRadius = innerRadius + 5;
        score = rand.Next(214748364); 
        UpdateDigits();

        TilemapUpdate();

        //----------Make the square and send the player off toward it
        int number = rand.Next(0, 9);
        Vector2 squarePosition = GetCoords(innerRadius - 3, innerRadius + 3);

        GameObject newSquare = Instantiate(squarePrefab);
        newSquare.transform.position = squarePosition;
        newSquare.name = Convert.ToString(number);
        newSquare.GetComponent<SpriteRenderer>().sprite = spriteList[number];
        newSquare.transform.localScale = ((0.1875f * number) + 0.3125f) * Vector3.one;

        float min = 0.5f;
        float max = 3.5f;
        float time = ((float)rand.NextDouble() * (max - min)) + min;
        Vector2 velocity = squarePosition - (Vector2)cameraFocus.transform.position;

        rb.velocity = velocity / time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet Prefab(Clone)")
        {
            BlowUp(collision.gameObject);
            StartCoroutine(WaitAndReset());
        }
        else
        {
            //Add score and update digits
            score += Convert.ToInt32(collision.gameObject.name);
            collision.gameObject.GetComponent<SquareController>().CollisionWork("collide");
            UpdateDigits();

            rb.velocity = Vector2.zero; //Stop

            //Spawn the bullets
            int times = rand.Next(1, 5);
            for (int i = 0; i < times; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab);
                Vector2 pos = GetCoords(innerRadius, outerRadius);
                newBullet.transform.position = pos;
                newBullet.GetComponent<Rigidbody2D>().velocity = (Vector2)cameraFocus.transform.position - pos;
            }

            SFXPlayer.Play($"laser_{rand.Next(0, 4)}", 0.3f);
        }
    }

    private void BlowUp(GameObject collision)
    {
        collision.GetComponent<BulletController>().Explode();
        SFXPlayer.Play("explode");

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
        if (!isExploding)
        {
            SFXPlayer.Play("explode");
            isExploding = true;
        }
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Title Scene"); //RESET!!!
    }

    private void UpdateDigits()
    {
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

    /*---------- These three functions are the same as in PlayerController.cs ----------*/

    private void UpdateOrthographicSize()
    {
        float ratio = 1 / virtualCamera.m_Lens.Aspect;
        float distance = (digits.Count - 1) / 2f; //Distance to the edge of the screen
        virtualCamera.m_Lens.OrthographicSize = (distance + (5 / ratio)) * ratio;

        //Width and height of screen
        stageSize = new Vector2(2f * virtualCamera.m_Lens.OrthographicSize * virtualCamera.m_Lens.Aspect,
                                2f * virtualCamera.m_Lens.OrthographicSize);
        Vector2 temp = stageSize * 1.5f; //scale because the tilemap is scaled up by 1.5 
        tileStageSize = new Vector2Int(
            (int)Math.Ceiling(temp.x) + TILEMAP_PADDING,
            (int)Math.Ceiling(temp.y) + TILEMAP_PADDING
        );
    }

    private void TilemapUpdate()
    {
        //Must divide by 1.5f since the tilemap is scaled up by 1.5
        Vector3Int bottomLeft = Vector3Int.RoundToInt(Camera.main.ScreenToWorldPoint(Vector3Int.zero) / 1.5f);
        bottomLeft = new Vector3Int(bottomLeft.x - TILEMAP_PADDING, bottomLeft.y - TILEMAP_PADDING, 0); //z defaults to -7, idk why
        
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
        //Return coordinates in a circle defined by minimum and maximum radii
        float radians = rand.Next(1, 360) * Mathf.Deg2Rad;
        float distance = ((float)rand.NextDouble() * (max - min)) + min;
        return
        new Vector2(
            distance * Mathf.Cos(radians),
            distance * Mathf.Sin(radians)
        ) + (Vector2)cameraFocus.transform.position;
    }
}