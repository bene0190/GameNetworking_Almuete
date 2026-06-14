using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerShooter : NetworkBehaviour
{
    [SerializeField] GameObject projectilePrefab; //place prefab for proj here
    [SerializeField] Transform firePoint; //where prefab is fired
    [SerializeField] float fireCooldown = 0.25f; //fire rate
    [SerializeField] KeyCode fireButton = KeyCode.Mouse0; 
    private float nextFireTime = 0;

    void Update()
    {
        if(!IsOwner ) { return; }
        if(Input.GetKeyDown(fireButton) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time * fireCooldown;
            RequestShootServerRpc(firePoint.position, firePoint.forward);
        }
    }

    [ServerRpc]
    private void RequestShootServerRpc(Vector3 spawnPosition, Vector3 shootDirection)
    {
        //object instantiation
        GameObject projectileInstantiate = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(shootDirection));

        //spawns objects for others players to see
        NetworkObject networkObject = projectileInstantiate.GetComponent<NetworkObject>();
        networkObject.Spawn(projectileInstantiate);
    }

}
