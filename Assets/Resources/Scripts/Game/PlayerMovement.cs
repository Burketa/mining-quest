using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform touchMin, maxHeight, minHeight;
    public float moveSpeed = 10.0f, turnSpeed = 5.0f;

    private Vector2 moveTo;
    private float rotateTo;

    //Otimizacoes
    private Vector2 currentPosition, position, rotation;
    private Transform _transform;

    void Start()
    {
        _transform = transform; //Cache o transform pra não ficar acessando com chamadas da Engine, isso demora.
    }

    void Update()
    {
        currentPosition = _transform.localPosition;   //Atual posição do bob
        position = currentPosition;             //Vector2 auxiliar para fazer as transformacoes de posicao

        rotation = moveTo - currentPosition;    //Vector2 auxiliar para calcular a rotacao

        if (Input.GetButton("Fire1"))
            moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (moveTo.x >= touchMin.localPosition.x)
        {
            //Movimento
            position = Vector2.MoveTowards(position, moveTo, Time.deltaTime * (moveSpeed * 10));      //position = Vector2.Lerp(position, moveTo, Time.deltaTime * moveSpeed/10); //Antigo, não sei se nescessariamente melhor, usar com speed 30~
            position.y = Mathf.Clamp(position.y, minHeight.localPosition.y, maxHeight.localPosition.y);
            position.x = currentPosition.x;
            _transform.localPosition = position;

            //Rotacao
            rotateTo = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            _transform.localRotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(0, 0, rotateTo), Time.deltaTime * turnSpeed);
        }
    }
}