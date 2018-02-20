using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawObstacle : MonoBehaviour {
    public float timeToGetPositionToActionMode; //czas po którym przeszkoda będzie na pozycji na ktorej ma sie znajdowac w trakcie ActionMode
    public float obstacleActionModePartTime;    //czas jaki trwa kazda czesc ActionMode
    public float rotationAcceleration;          //przyspieszenie rotacji

    
    private bool movementMode;      //tryb do poruszania sie po ekranie
    private bool actionMode;        //tryb w ktorym obiekt jest faktyczna przeszkoda
    private bool breakMode;         //przerwa w obrotach kiedy obiekt nie jest przeszkoda ani sie nie porusza
    

    
    private float rotationSpeed;    //predkosc rotacji (obliczane w skrypcie)
    private Vector2 movementSpeed;  //predkosc spadania (obliczane w skrypcie)
    private int repetitionsLeft;    //zmienna okresla ile razy przeszkoda ma sie jeszcze zakrecic

    private Rigidbody2D rB;
    private EdgeCollider2D polCol;

    private bool once;


    void Awake()
    {
        rB = GetComponent<Rigidbody2D>();       //pobiera komponent do zmiennej bo bedzie on czesto uzywany

        movementMode = false;           //obiekt aktywuje sie funkcja zewnetrzna wiec na poczatku wszystkie mody sa wylaczone
        actionMode = false;             //obiekt aktywuje sie funkcja zewnetrzna wiec na poczatku wszystkie mody sa wylaczone
        polCol = GetComponent<EdgeCollider2D>();
    }

	
	void Update () {
        
        if (actionMode)    //jezeli wlaczany jest tryb w ktorym obiekt faktycznie jest przeszkoda i sie kreci
        {
            polCol.enabled = false;
            polCol.enabled = true;
            if (once)
            {
                gameObject.tag = "PlayerKillerObstacle";
                once = false;
            } 
            rotationSpeed += (rotationAcceleration * Time.deltaTime);  //zwieksza/zmniejsza predkosc rotacji
            rB.angularVelocity = rotationSpeed;     //ustawia zmieniona predkosc rotacji
        }
        else if(once)
        {
            if (movementMode)      //jezeli wlaczony jest tryb do poruszania sie po ekranie
            {
                rB.velocity = movementSpeed;    //ustawienie predkosci i kierunku ruchu
                rB.angularVelocity = 0;         //upewnienie sie ze obiekt nie obraca sie
                gameObject.tag = "InactiveObstacle";
                
            }
            else                   //inaczej upewnij sie ze obiekt nie rotuje
            {
                gameObject.tag = "InactiveObstacle";
                rB.angularVelocity = 0;
            }
            
        }
        
	}

    //funkcja zewnetrzna pozwalajaca na uaktywnienie obiektu przyjmujaca:
    //yPositionToStopOn - pozycje Y na ktorej ma zatrzymac sie przeszkoda 
    //repetitions - ile razy przeszkoda ma sie zakrecic zanim wyjdzie z ekranu
    public float StartObstacle( float yPositionToStopOn, int repetitions = 2)
    {
        
        float speed = (transform.position.y - yPositionToStopOn) / timeToGetPositionToActionMode; //obliczenie predkosci w zaleznosci od czasu
        movementSpeed = new Vector2(0, -speed); //ustawienie predkosci
        rotationSpeed = 0;                      //ustawienie poczatkowej pretkosci rotacji
        rB.velocity = movementSpeed;    //ustawienie predkosci i kierunku ruchu         ////nie bylo
        speed = (yPositionToStopOn + transform.position.y) / timeToGetPositionToActionMode; //!!!!nie bylo
        movementSpeed = new Vector2(0, -speed); //ustawienie predkosci //nie bylo 
        movementMode = false;                    //wlaczenie poruszania sie po ekranie ///!!!!!!!!! bylo true
        once = true;
        repetitionsLeft = repetitions;          //ustawienie ilosci zakrecen sie przeszkody

        //procedury odpowiedzialne za przelaczanie miedzy odpowiednimi 3 czesciami ActionMode
        StartCoroutine(Waiting(timeToGetPositionToActionMode , 0));
        StartCoroutine(Waiting(timeToGetPositionToActionMode + obstacleActionModePartTime, 1));
        StartCoroutine(Waiting(timeToGetPositionToActionMode + (obstacleActionModePartTime * 2), 2));

        return (2 * timeToGetPositionToActionMode) + (repetitions * 3 * (obstacleActionModePartTime));
    }
    IEnumerator Waiting(float time , int mode)
    {
        yield return new WaitForSeconds(time);
        switch (mode)
        {
            case 0:
                rB.velocity = new Vector2(0, 0);            //zatrzymanie ruchu obiektu
                actionMode = true;                          //wlaczenie ActionMode
                once = true;
                movementMode = false;                       //wylaczenie MovementMode
                

                //Debug.Log("PlayerKillerObstacle");
                break;
            case 1: //sprawia ze przeszkoda w polowie czasu obrotow zaczyna zwalniac
                rotationAcceleration = -rotationAcceleration; //zmiana przyspieszenia rotacji na ujemne
                break;
            case 2: //rozpoczyna cala sekwencje kolejnego obrotu
                    //albo konczy obroty i przechodzi do MovementMode
                if (repetitionsLeft > 1) //jezeli nalezy wykonac wiecej obrotow
                {
                    repetitionsLeft--;  //zmniejsza liczbe pozostalych obrotow
                    actionMode = false; //tymczasowo wylacza action mode
                    once = true;
                    StartCoroutine(Waiting(obstacleActionModePartTime, 3)); //rozpoczyna okres przerwy(kiedy przeszkoda nie kreci sie 
                                                                            //anie sie nie porusza)
                }
                else //inaczej przelacza spowrotem do trybu poruszania sie po ekranie aby wyjsc z ekranu i zniknac
                {
                    actionMode = false;
                    movementMode = true;  //wlacza tryb poruszania sie po ekranie
                    once = true;
                }
                
                break;
            case 3: //rozpoczyna przerwe
                rotationAcceleration = -rotationAcceleration; //przywraza normalne dodatnie przyscpieszenie rotacji
                actionMode = true; //przywraca ActionMode
                once = true;

                //przywraca procedury odpowiedzialne za przelaczanie miedzy odpowiednimi 3 czesciami ActionMode
                StartCoroutine(Waiting(timeToGetPositionToActionMode, 0));
                StartCoroutine(Waiting(obstacleActionModePartTime, 1)); 
                StartCoroutine(Waiting((obstacleActionModePartTime * 2), 2));
                break;
        }
    }

    //kolizja z niszczacymi scianami i niszczycielem obasacli
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "RestKiller" || coll.tag == "DestroyingLine" || coll.tag == "ObstacleDestroyingLine")
        {
            Destroy(gameObject);
        }
    }
}
