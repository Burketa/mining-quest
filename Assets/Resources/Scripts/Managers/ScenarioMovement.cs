using UnityEngine;
using System.Collections;

public class ScenarioMovement : MonoBehaviour
{
    public float speed = 1f;
    public float parallax = 2f;

    public Transform movingResetBack, movingResetFront, movingContinuous;

    //Otimizacoes
    private Vector3 defaultOffset = new Vector3(1024 * 2, 0, 0);
    private Vector2 translateBy, translateByParallax;
    private Transform mrb1, mrb2, mrf1, mrf2;

    void Start()
    {
        //Cria um clone do cenario padrao pra fazer o scroll
        Instantiate(movingResetBack.GetChild(0), new Vector2(1024, 0), Quaternion.identity, movingResetBack);
        //Cria um clone do cenario padrao pra fazer o scroll
        Instantiate(movingResetFront.GetChild(0), new Vector2(1024, 0), Quaternion.identity, movingResetFront);
            //Transform tilesClone = Instantiate(movingResetBack.GetChild(0), new Vector2(1024, 0), Quaternion.identity, movingResetBack);  //Cria um clone do cenario padrao pra fazer o scroll
            //Transform skyClone = Instantiate(movingResetFront.GetChild(0), new Vector2(1024, 0), Quaternion.identity, movingResetFront); //Cria um clone do ceu padrao pra fazer o scroll
        mrb1 = movingResetBack.GetChild(0);
        mrb2 = movingResetBack.GetChild(1);
        mrf1 = movingResetFront.GetChild(0);
        mrf2 = movingResetFront.GetChild(1);
    }

    void FixedUpdate()
    {
        translateBy.x = Mathf.RoundToInt(Time.deltaTime * -speed);
        translateByParallax.x = Mathf.RoundToInt(translateBy.x / parallax);
        
        if (mrb1.localPosition.x <= -1024)
            mrb1.localPosition += defaultOffset;
        mrb1.Translate(translateBy);

        if (mrb2.localPosition.x <= -1024)
            mrb2.localPosition += defaultOffset;
        mrb2.Translate(translateBy);

        if (mrf1.localPosition.x <= -1024)
            mrf1.localPosition += defaultOffset;
        mrf1.Translate(translateByParallax);

        if (mrf2.localPosition.x <= -1024)
            mrf2.localPosition += defaultOffset;
        mrf2.Translate(translateByParallax);

        movingContinuous.Translate(translateBy);
        //child.transform.position += new Vector3(translateBy.x, 0, 0);
    }
}
