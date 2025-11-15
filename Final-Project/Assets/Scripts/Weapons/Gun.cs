using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo = 15;
    public float fireRate = 0.15f;
    public float range = 100f;
    public Transform muzzle;       // where bullets come from
    public Camera cam;             // main camera (assign in inspector)

    private float nextFireTime = 0f;

    public bool IsEmpty = false;


    public ParticleSystem muzzleFlash;
    public AudioSource audioSource;
    public AudioClip shotSound;


    public void Shoot()
    {
       
        if (Time.time < nextFireTime) return;

        if (ammo <= 0)
        {
            IsEmpty = true;

           

            return;
        }
        

        nextFireTime = Time.time + fireRate;
        ammo--;


        // Muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // Sound
        if (audioSource != null && shotSound != null)
            audioSource.PlayOneShot(shotSound);

        // Raycast from screen center
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("HIT: " + hit.collider.name);

            if (hit.transform.CompareTag("Zombie"))
            {
                hit.transform.GetComponent<ZombieAI>().TakeDamage(20);
            }


            // Optional: damage
            // var enemy = hit.collider.GetComponent<Enemy>();
            // if (enemy != null) enemy.TakeDamage(10);
        }

        // Update IsEmpty after firing
      



        
        
    }
}
