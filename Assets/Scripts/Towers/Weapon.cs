using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator Anim;
    [SerializeField] private float timeToReloadWeapon = 0.1f;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject HorizontalPart;
    [SerializeField] private GameObject VerticalPart;
    private Tower Owner;
    private AudioSource audioSource;

    private void Start()
    {
        Owner = GetComponentInParent<Tower>();
        Owner.OnShoot += OnShoot;
        audioSource = GetComponent<AudioSource>();
    }

    void OnShoot()
    {
        if (Anim != null)
        {
            Anim.SetTrigger("Shoot");
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }


    void RotateWeapon()
    {
        if (Owner.Targets.Count == 0 || Owner.Targets.All(x => x == null))
        {
            return;
        }

        var target = Owner.Targets.First(x => x != null);
        float angle = Vector3.SignedAngle(HorizontalPart.transform.position, target.transform.position, Vector3.up);

        var lookPos = target.transform.position - HorizontalPart.transform.position;
        lookPos.y = 0;
        HorizontalPart.transform.rotation = Quaternion.LookRotation(lookPos);
        VerticalPart.transform.LookAt(target.transform);
    }

    void Update()
    {
        RotateWeapon();
    }
}