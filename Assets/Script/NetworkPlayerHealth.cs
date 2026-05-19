using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerHealth : NetworkBehaviour
{

    [SerializeField] private int MaxHealth = 100;
    //this line creates network-synced health variable
    public NetworkVariable<int> CurrentHealth = new NetworkVariable<int>(
        100/*actual health num*/,
        NetworkVariableReadPermission.Everyone/*allows netvar to be read by everyone(shares value to client and host)*/,
        NetworkVariableWritePermission.Server/*allows ONLY server to write and change it(server only is allowed to change value)*/
        );//makes it so accessible yung var to network shenanigens



    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            CurrentHealth.Value = MaxHealth;
        }
        CurrentHealth.OnValueChanged += OnHealthChange;
    }

    public override void OnNetworkDespawn()
    {
        CurrentHealth.OnValueChanged -= OnHealthChange;
    }

    private void OnHealthChange(int previousValue, int newValue)
    {
        Debug.Log($"{gameObject.name} health changed: {previousValue} -> {newValue}");
    }

    public void TakeDamage(int damageAmount)
    {
        if(!IsServer) { return;}
        CurrentHealth.Value -= damageAmount;
        CurrentHealth.Value = Mathf.Clamp(CurrentHealth.Value, 0, MaxHealth);
        if (CurrentHealth.Value <= 0)
        {
            Respawn();//use a start coroutine if you want to set respawn timer delay so hindi sobra sudden respawn
        }
    }

    private void Respawn()
    {
        CurrentHealth.Value = MaxHealth;
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawnpoint");
        int randomIndex = Random.Range(0,spawns.Length);
        Transform selectedSpawn = spawns[randomIndex].transform;
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null) 
        { 
            controller.enabled = false;
        }
        transform.position = selectedSpawn.position;
        transform.rotation = selectedSpawn.rotation;

        if (controller != null)
        {

            controller.enabled = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
