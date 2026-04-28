using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UserLogin : MonoBehaviour
{
    
    public TMP_Text[] slotsTexts;              // Letras visibles
    public RectTransform arrow;                // Flecha debajo
    public RectTransform[] slotPositions;      // Posiciones de cada letra

    char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ".ToCharArray();

    int[] slots = new int[3];
    int selectedSlot = 0;

    float horizontal;
    float vertical;

    void Start()
    {
        UpdateUI();
        UpdateArrow();
    }

    // INPUT SYSTEM (Send Messages)
    public void OnMoveHorizontal(InputValue value)
    {
        horizontal = value.Get<float>();
        HandleHorizontal();
    }

    public void OnMoveVertical(InputValue value)
    {
        vertical = value.Get<float>();
        HandleVertical();
    }

    public void OnConfirm()
    {
        Confirm();
    }

    // --- MOVIMIENTO ENTRE SLOTS (A / D) ---
    void HandleHorizontal()
    {
        if (horizontal > 0.5f)
        {
            selectedSlot++;
        }
        else if (horizontal < -0.5f)
        {
            selectedSlot--;
        }

        selectedSlot = Mathf.Clamp(selectedSlot, 0, 2);

        UpdateArrow();
    }

    // --- CAMBIO DE LETRA (W / S) ---
    void HandleVertical()
    {
        if (vertical > 0.5f)
        {
            slots[selectedSlot]++;
        }
        else if (vertical < -0.5f)
        {
            slots[selectedSlot]--;
        }

        Wrap();
        UpdateUI();
    }

    // --- ENVOLVER ALFABETO ---
    void Wrap()
    {
        if (slots[selectedSlot] < 0)
            slots[selectedSlot] = characters.Length - 1;

        if (slots[selectedSlot] >= characters.Length)
            slots[selectedSlot] = 0;
    }

    // --- ACTUALIZAR LETRAS ---
    void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            slotsTexts[i].text = characters[slots[i]].ToString();
        }
    }

    // --- MOVER FLECHA ---
    void UpdateArrow()
    {
        if (arrow != null && slotPositions[selectedSlot] != null)
        {
            Vector2 pos = arrow.anchoredPosition;
            pos.x = slotPositions[selectedSlot].anchoredPosition.x;
            arrow.anchoredPosition = pos;
        }
    }

    // --- CONFIRMAR NOMBRE ---
    void Confirm()
    {
        string username = GetUsername();
        Debug.Log("Jugador: " + username);
    }

    public string GetUsername()
    {
        return "" +
        characters[slots[0]] +
        characters[slots[1]] +
        characters[slots[2]];
    }
}
