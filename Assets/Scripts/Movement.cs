using System.Collections;
using UnityEngine;

class Movement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gridSize = 1f;

    public static GameController gameController;

    public Vector2 boardDimentions;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    private int currentLocation = 1;




 void  Start(){

     startPosition = transform.position;
   
   }

    public void Update()
    {

    }

    //debrecated
     IEnumerator move(Transform transform)
    {
        isMoving = true;
        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
           
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }

        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
                t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
          
            yield return null;
        }
        gameController.MovementFinished();
        isMoving = false;
        yield return 0;
    }


     IEnumerator moveBy( int  numOfSquares)
    {


       int endLoc = currentLocation +numOfSquares;

       if (endLoc <= (boardDimentions.x * boardDimentions.y))
       {



           while (currentLocation < endLoc)
           {
               currentLocation++;

               Vector2 endcord = squareNumToCords(currentLocation);
               isMoving = true;

               t = 0;


               endPosition = new Vector3(startPosition.x + (gridSize * endcord.x), startPosition.y, startPosition.z - (gridSize * endcord.y));



               if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
               {
                   factor = 0.7071f;
               }
               else
               {
                   factor = 1f;
               }

               while (t < 1f)
               {
                   t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                   transform.position = Vector3.Lerp(transform.position, endPosition, t);

                   yield return null;
               }
               yield return null;
           }
       }
       gameController.MovementFinished();
        isMoving = false;
        yield return 0;
    }
     IEnumerator moveToDirectly(int squareNum)
     {
 currentLocation = squareNum;
                 Vector2 endcord = squareNumToCords(squareNum);
                 isMoving = true;

                 t = 0;

                
                 endPosition = new Vector3(startPosition.x + (gridSize * endcord.x), startPosition.y, startPosition.z - (gridSize * endcord.y));



                 if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
                 {
                     factor = 0.7071f;
                 }
                 else
                 {
                     factor = 1f;
                 }

                 while (t < 1f)
                 {
                     t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                     transform.position = Vector3.Lerp(transform.position, endPosition, t);

                     yield return null;
                 }
                 yield return null;
       
         isMoving = false;
         yield return 0;
     }
     IEnumerator moveToSteps(int squareNum)
     {


         if (squareNum > currentLocation)
         {
             StartCoroutine(moveBy(squareNum - currentLocation));
         }
         else
         {

             if (squareNum >=0)
             {



                 while (currentLocation > squareNum)
                 {
                     currentLocation--;

                     Vector2 endcord = squareNumToCords(currentLocation);
                     isMoving = true;

                     t = 0;


                     endPosition = new Vector3(startPosition.x + (gridSize * endcord.x), startPosition.y, startPosition.z - (gridSize * endcord.y));



                     if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
                     {
                         factor = 0.7071f;
                     }
                     else
                     {
                         factor = 1f;
                     }

                     while (t < 1f)
                     {
                         t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                         transform.position = Vector3.Lerp(transform.position, endPosition, t);

                         yield return null;
                     }
                     yield return null;
                 }
             }
         }
         isMoving = false;
         yield return 0;
     }
   private Vector2 squareNumToCords(int squareNum)
    {

            squareNum -= 1;

            int y = squareNum / (int)(boardDimentions.y);

            int x = squareNum % (int)(boardDimentions.x);//(squareNum + ((int)boardDimentions.x * y));

       if(y%2==1)
       {
           x =( (int)boardDimentions.x - x)-1;
       }

            return new Vector2(x, y);

    }


    public void MoveForwardBy(int distance)
    {
        if (!isMoving)
        {  
            StartCoroutine(moveBy(distance));
        }
    }

    public void MoveToSquareDirectly(int squareNum)
    {
        if (!isMoving)
        {
            StartCoroutine(moveToDirectly(squareNum));
        }
    }

    public void MoveToSquareSteps(int squareNum)
    {
        if (!isMoving)
        {
            StartCoroutine(moveToSteps(squareNum));
        }
    }

    public void MoveToAnySquareSteps(int max)
    {
        if (!isMoving)
        {
            int diceNum = Random.Range(1, max + 1);
            Debug.Log("diceNum " + diceNum);
            MoveToSquareSteps(diceNum);
        }
    }

    public void MoveToAnySquareDirectly(int max)
    {
        if (!isMoving)
        {
            int diceNum = Random.Range(1, max + 1);
            Debug.Log("diceNum " + diceNum);
            MoveToSquareDirectly(diceNum);
        }
    }
}