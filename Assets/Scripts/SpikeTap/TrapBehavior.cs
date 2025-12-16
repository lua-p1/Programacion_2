using UnityEngine;
public class TrapBehavior : MonoBehaviour
{
    [SerializeField] private float _damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.player != null)
        {
            var life = GameManager.instance.player.GetComponent<ThirdPersonInputs>().GetPlayerComponentLife;
            if (life != null)
            {
                life.TakeDamage(_damage);
            }
            else
            {
                Debug.LogError("No se encontro el componente PlayerHealth en el jugador");
            }
        }
    }
    public void OnRutine()
    {
        AudioManager.Instance.PlaySFX("electricSparks", transform.position);
    }
}
