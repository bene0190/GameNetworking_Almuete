using UnityEngine;
using Unity.Netcode;

public class NetworkProjectile : NetworkBehaviour
{
    [SerializeField] float speed = 12f;
    [SerializeField] float projectileLifetime = 10f;//max proj life

    private float despawnTime;//will count down to the projectile life time if it hits projectileLifetime itll KILL ya

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            despawnTime = Time.time + projectileLifetime;
        }
    }

    void Update()
    {
        if(!IsServer) { return; }
        transform.position += transform.forward * speed * Time.deltaTime;
        if(Time.time >= despawnTime)
        {
            NetworkObject.Despawn();//no need to put anything here cuz itll be on boolet
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //despawns player when hit by projectile using tag system
        if(!IsServer) { return;}
        if(other.CompareTag("Player"))//"Player" tag!!!!!!!
        {
            NetworkObject.Despawn();
        }
    }
}
