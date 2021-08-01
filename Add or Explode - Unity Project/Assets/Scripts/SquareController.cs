using UnityEngine;

public class SquareController : MonoBehaviour
{
    GameObject collisionPX;
    GameObject cullingPX; //Empty particle effect

    private void Start()
    {
        collisionPX = (GameObject)Resources.Load("Prefabs/Collision Particle Effect");
        cullingPX = (GameObject)Resources.Load("Prefabs/Culling Particle Effect");
    }

    public void CollisionWork(string particleEffect)
    {
        GameObject effect = particleEffect == "collide" ? collisionPX : cullingPX;
        GameObject particleObject = Instantiate(effect);
        particleObject.transform.position = transform.position;
        particleObject.GetComponent<ParticleSystem>().Play();

        Destroy(gameObject);
    }
}
