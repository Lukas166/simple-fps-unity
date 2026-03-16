using UnityEngine;

public class TargetObjek : MonoBehaviour
{
    // Fungsi ini dipanggil dari skrip senjatamu (Player) saat laser mengenai objek ini
    public void KenaTembak()
    {
        // 1. Lapor ke GameManager bahwa objek ini hancur
        GameManager.instance.TargetDestroyed();

        // 2. Hancurkan objek (Sphere) ini dari game
        Destroy(gameObject);
    }
}
