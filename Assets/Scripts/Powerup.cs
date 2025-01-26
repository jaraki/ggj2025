
using UnityEngine;

public enum PowerupType
{
    Oxygen,
    Power,
}
public class Powerup : MonoBehaviour
{
    public float ReplenishAmount;
    public PowerupType Type;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            switch(Type)
            {
                case PowerupType.Oxygen:
                    Game.Instance.Oxygen += ReplenishAmount;
                    break;
                case PowerupType.Power:
                    Game.Instance.Power += ReplenishAmount;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
