using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform touchMin, maxHeight, minHeight;
    public float moveSpeed = 10.0f, turnSpeed = 5.0f;

    private Vector2 moveTo;
    private float rotateTo;

    //Gambiarras
    public Animator touchAnim;

    //Otimizacoes
    private Vector2 defaultPosition, currentPosition, position, rotation;
    private Transform _transform;
    private StateManager state;

    void Start()
    {
        _transform = transform; //Cache o transform pra não ficar acessando com chamadas da Engine, isso demora.
        defaultPosition = _transform.position;
        state = FindObjectOfType<StateManager>();
        print(moveTo);
        print(touchMin.localPosition.x);
        moveTo = touchMin.localPosition;
    }

    void FixedUpdate()
    {
        currentPosition = _transform.localPosition;   //Atual posição do bob
        position = currentPosition;             //Vector2 auxiliar para fazer as transformacoes de posicao

        rotation = moveTo - currentPosition;    //Vector2 auxiliar para calcular a rotacao
        if (!state.gameOver)
        {
            //Pega o ponto onde o usuario esta clicando
            if (Input.GetButton("Fire1"))
                moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Se estiver dentro da area permitida, efetua o movimento
            if (moveTo.x >= touchMin.localPosition.x)
            {
                //Não mostra o touch animator
                touchAnim.SetBool("touchOk", true);

                //Movimento
                position = Vector2.MoveTowards(position, moveTo, Time.deltaTime * (moveSpeed * 10));      //position = Vector2.Lerp(position, moveTo, Time.deltaTime * moveSpeed/10); //Antigo, não sei se nescessariamente melhor, usar com speed 30~
                position.y = Mathf.Clamp(position.y, minHeight.localPosition.y, maxHeight.localPosition.y);
                position.x = currentPosition.x;
                _transform.localPosition = position;

                //Rotacao
                rotateTo = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                _transform.localRotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(0, 0, rotateTo), Time.deltaTime * turnSpeed);
            }
            //Senão reseta ao original
            else
            {
                //Mostra o touch animator
                touchAnim.SetBool("touchOk", false);

                //Movimento
                position = Vector2.MoveTowards(position, defaultPosition, Time.deltaTime * (moveSpeed));
                position.y = Mathf.Clamp(position.y, minHeight.localPosition.y, maxHeight.localPosition.y);
                position.x = currentPosition.x;
                _transform.localPosition = position;

                //Rotacao caso o clique saia da area indicada
                rotateTo = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                _transform.localRotation = Quaternion.Slerp(_transform.rotation, Quaternion.identity, Time.deltaTime * turnSpeed);
            }
        }
    }
}