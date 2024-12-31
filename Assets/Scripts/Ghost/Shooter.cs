using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletCount = 12;
    public float bulletSpeed = 5f;
    public float shootInterval = 2f;

    public bool stagger;
    public bool oscillate;

    private bool isShootingCircle = true;
    public bool isShooting = false;
    AudioManager audioManager;

    private void Start()
    {
        StartCoroutine(ShootRepeatedly());
    }
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public void ShootCircle()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount);
            float angleRad = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    public IEnumerator ShootRandomly()
    {
        for (int i = 0; i < 100; i++)
        {
            float randomAngle = Random.Range(0f, 360f); // Tạo góc ngẫu nhiên
            float angleRad = randomAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed * 1.5f;
            }
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            yield return new WaitForSeconds(shootInterval / bulletCount); // Đảm bảo bắn viên đạn cách nhau
        }
    }

    public IEnumerator ShootBurst()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = Random.Range(0f, 360f); // Tạo góc ngẫu nhiên
            float angleRad = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0).normalized;

            // Instantiate viên đạn từ prefab
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Lấy Rigidbody2D của viên đạn và thiết lập vận tốc
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed; // Đặt vận tốc cho viên đạn
            }

            // Đặt hướng của viên đạn
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Chờ 0.1 giây trước khi bắn viên đạn tiếp theo
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator ShootRepeatedly()
    {
        while (true)
        {
            if (isShooting)
            {
                audioManager.PlaySFX(audioManager.staff_effect);
                if (isShootingCircle)
                {
                    ShootCircle();
                }
                else
                {

                    int randomMode = Random.Range(0, 2);

                    if (randomMode == 0)
                    {
                        StartCoroutine(ShootRandomly());
                    }
                    else
                    {
                        StartCoroutine(ShootBurst());
                    }
                }
                yield return new WaitForSeconds(2f);
                isShootingCircle = !isShootingCircle;
            }
            else
            {
                yield return null;
            }
        }
    }

}
