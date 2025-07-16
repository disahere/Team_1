using UnityEngine;

public class CarFlip : MonoBehaviour
{
    public float checkInterval = 1f;
    public float flipDelay = 2f;
    public float flipHeight = 1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("CheckIfStuck", checkInterval, checkInterval);
    }

    void CheckIfStuck()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);

        // Якщо кут між віссю "вгору" машини і віссю "вгору" світу > 75°
        // Це значить: або на даху, або на боці
        if (angle > 75f)
        {
            Invoke("FlipCar", flipDelay);
        }
    }

    void FlipCar()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);

        // Якщо досі не стоїть на колесах
        if (angle > 75f)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Вирівнюємо вгору
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

            // Піднімаємо трохи вверх
            transform.position += Vector3.up * flipHeight;
        }
    }
}