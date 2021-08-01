using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject explosionPX;
    private void Start() => explosionPX = (GameObject)Resources.Load("Prefabs/Bullet Explosion Particle Effect");

    public void Explode()
    {
        GameObject effect = Instantiate(explosionPX);
        effect.transform.position = transform.position;
        Destroy(gameObject.GetComponent<SpriteRenderer>()); //hide it but don't stop this script
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
