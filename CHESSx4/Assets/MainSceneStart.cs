using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameModel;
using GameModel.Client;
using GameModel.Messages;
using GameModel.Data;

public class MainSceneStart : MonoBehaviour {

    public IArbitrator arby;

	public Transform square_dark;
	public Transform square_light;
	public GameObject piece_bishop;
	public GameObject piece_knight;
	public GameObject piece_rook;
	public GameObject piece_king;
	public GameObject piece_queen;
	public GameObject piece_pawn;

	public float gridWidth;
	public float gridDepth;
	public Transform[, ] squares = new Transform[14, 14];
	public Dictionary<BoardPosition, GameObject> clientPiecesCollection = new Dictionary<BoardPosition, GameObject>();

	public bool moveState = false;
    public bool local = true;


	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetInt("local") == 0) local = false;
        if (local)
        {
            arby = new LocalArbitrator();
        }
        else
        {
            arby = new NetworkArbitrator(new System.Uri("ws://192.168.1.107:1337"));
        }
        
		CreateGrid ();
	}

	// Update is called once per frame
	/// <summary>
	/// Main Update
	/// </summary>
	void Update () {

        //Check for messages during update
        ModelMessage message;
        bool message_recieved = arby.TryGetLatestMessage(out message);
        if (message_recieved)
        {
            Debug.Log(message);
            //Create piece message
            if(message is CreatePieceMessage)
            {
                var actualMessage = (CreatePieceMessage)message;
                //King
                if (actualMessage.pieceType == PieceEnum.KING)
                {
                    clientPiecesCollection.Add(actualMessage.position, (Instantiate(piece_king, new Vector3((float) actualMessage.position.X, (float) actualMessage.position.Y - 1, -1), Quaternion.identity)));
                }
                //Queen
                else if (actualMessage.pieceType == PieceEnum.QUEEN)
                {
                    clientPiecesCollection.Add(actualMessage.position, (Instantiate(piece_queen, new Vector3((float)actualMessage.position.X, (float)actualMessage.position.Y - 1, -1), Quaternion.identity)));
                }
                //Rook
                else if (actualMessage.pieceType == PieceEnum.ROOK)
                {
                    clientPiecesCollection.Add(actualMessage.position, (Instantiate(piece_rook, new Vector3((float)actualMessage.position.X, (float)actualMessage.position.Y - 1, -1), Quaternion.identity)));
                }
                //Knight
                else if (actualMessage.pieceType == PieceEnum.KNIGHT)
                {
                    clientPiecesCollection.Add(actualMessage.position, (Instantiate(piece_knight, new Vector3((float)actualMessage.position.X, (float)actualMessage.position.Y - 1, -1), Quaternion.identity)));
                }
                //Bishop
                else if (actualMessage.pieceType == PieceEnum.BISHOP)
                {
                    clientPiecesCollection.Add(actualMessage.position, (Instantiate(piece_bishop, new Vector3((float)actualMessage.position.X, (float)actualMessage.position.Y - 1, -1), Quaternion.identity)));
                }
                //Pawn
                else if (actualMessage.pieceType == PieceEnum.PAWN)
                {
                    clientPiecesCollection.Add(actualMessage.position, (Instantiate(piece_pawn, new Vector3((float)actualMessage.position.X, (float)actualMessage.position.Y - 1, -1), Quaternion.identity)));
                }

                //Add Color
                var o = clientPiecesCollection[actualMessage.position];
                Renderer rend = o.GetComponent<Renderer>();
                if((int)actualMessage.owner == 0)
                {
                    rend.material.color = new Color(0, 0, 1);
                }
                else if ((int)actualMessage.owner == 1)
                {
                    rend.material.color = new Color(1, 0, 0);
                }
                else if ((int)actualMessage.owner == 2)
                {
                    rend.material.color = new Color(1.0f, 0.92f, 0.016f);
                }
                else if ((int)actualMessage.owner == 3)
                {
                    rend.material.color = new Color(0, 1, 0);
                }

                //Add Behavior
                o.AddComponent<PieceBehavior>();
                o.GetComponent<PieceBehavior>().currentPosition = actualMessage.position;
            }

            else if(message is SetTurnMessage)
            {
                var actualMessage = (SetTurnMessage)message;
                GameObject.Find("whitesquare").GetComponent<TurnBehavior>().turn = (int) actualMessage.player;
            }

            else if(message is TranslatePieceMessage)
            {
                var actualMessage = (TranslatePieceMessage)message;
                var o = clientPiecesCollection[actualMessage.src];
                try
                {
                    var d = clientPiecesCollection[actualMessage.dest];
                    clientPiecesCollection.Remove(actualMessage.dest);
                    Destroy(d);
                }
                catch
                {

                }
                o.GetComponent<PieceBehavior>().currentPosition = actualMessage.dest;
                clientPiecesCollection[actualMessage.dest] = o;
                clientPiecesCollection.Remove(actualMessage.src);
            }

            //else if(message is DestroyPieceMessage)
            //{
            //    var actualMessage = (DestroyPieceMessage)message;
            //    var o = clientPiecesCollection[actualMessage.position];
            //    Destroy(o);
            //}
        }
	}


	/// <summary>
	/// Creates the grid.
	/// </summary>
	void CreateGrid (){
		//Draws board and maps quares to array
		for (int y = 0; y < gridDepth; y=y+1) {
			for (int x = 0; x < gridWidth; x = x +1) {
				//This fat if statement creates the cut-outs. also squares[0][0] == null
				if (!(x < 3 && y < 3) && !(x >= 14-3 && y < 3) && !(x < 3 && y >= 14-3) && !(x >= 14-3 && y >= 14-3)) {
					if ((y % 2 == 0) && (x % 2 == 0)) {
						squares [x, y] = Instantiate (square_dark, new Vector3 (x, y, 0), Quaternion.identity);
					}
					if ((y % 2 == 0) && (x % 2 == 1)) {
						squares [x, y] = Instantiate (square_light, new Vector3 (x, y, 0), Quaternion.identity);
					}
					if ((y % 2 == 1) && (x % 2 == 0)) {
						squares [x, y] = Instantiate (square_light, new Vector3 (x, y, 0), Quaternion.identity);
					}
					if ((y % 2 == 1) && (x % 2 == 1)) {
						squares [x, y] = Instantiate (square_dark, new Vector3 (x, y, 0), Quaternion.identity);
					}
				}
			}
		}
	}

}
