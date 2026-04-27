using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UserLogin : MonoBehaviour
{
    public TMP_Text[] slotsTexts;

    char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ".ToCharArray();

    int[] slots = new int[3];
    int selectedSlot = 0;

    Vector2 input;
    float inputDelay = 0.2f;
    float lastInputTime;

    void Start()
    {
        UpdateUI();
        Debug.Log("Slots conectados: " + slotsTexts.Length);
    }

    void Update()
    {
        HandleInput();
    }

    // INPUT SYSTEM (Send Messages)
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();

        // DEBUG (muy importante para comprobar que funciona)
        // Debug.Log("INPUT: " + input);

        Debug.Log("INPUT RAW: " + input);
    }

    public void OnConfirm()
    {
        Confirm();
    }

    void HandleInput()
    {
        if (Time.time - lastInputTime < inputDelay)
            return;

        if (input == Vector2.zero)
            return;

        // HORIZONTAL
        if (input.x > 0.5f)
        {
            selectedSlot++;
            lastInputTime = Time.time;
        }
        else if (input.x < -0.5f)
        {
            selectedSlot--;
            lastInputTime = Time.time;
        }

        selectedSlot = Mathf.Clamp(selectedSlot, 0, 2);

        // VERTICAL
        if (input.y > 0.5f)
        {
            slots[selectedSlot]++;
            Wrap();
            UpdateUI();
            lastInputTime = Time.time;
        }
        else if (input.y < -0.5f)
        {
            slots[selectedSlot]--;
            Wrap();
            UpdateUI();
            lastInputTime = Time.time;
        }
    }

    void Wrap()
    {
        if (slots[selectedSlot] < 0)
            slots[selectedSlot] = characters.Length - 1;

        if (slots[selectedSlot] >= characters.Length)
            slots[selectedSlot] = 0;
    }

    void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            if (slotsTexts[i] != null)
                slotsTexts[i].text = characters[slots[i]].ToString();
        }
    }

    void Confirm()
    {
        string username = GetUsername();

        if (string.IsNullOrWhiteSpace(username))
        {
            Debug.Log("Nombre inválido");
            return;
        }

        Debug.Log("Jugador: " + username);

        // MongoDB o cambio de escena aquí
    }

    public string GetUsername()
    {
        return "" +
        characters[slots[0]] +
        characters[slots[1]] +
        characters[slots[2]];
    }
}