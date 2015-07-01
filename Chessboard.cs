using UnityEngine;
using System.Collections;

public class Chessboard : MonoBehaviour {

	public int m_iSize = 10;

	GameObject[,] m_Grid;
	int[,] m_AliveNeighbours;

	// Use this for initialization
	void Start () {

		m_Grid = new GameObject[m_iSize,m_iSize];
		m_AliveNeighbours = new int[m_iSize,m_iSize];

		for (int i = 0; i < m_iSize; i++)
			for (int j = 0; j < m_iSize; j++) {
				GameObject kachel = GameObject.CreatePrimitive (PrimitiveType.Quad);
				m_Grid[i,j] = kachel;
				kachel.name = "Kachel( " + i + ", " + j + ")";
				kachel.transform.position = new Vector3 (i, j, 0);
				kachel.transform.parent = this.transform;
				
				if (Random.value < 0.5f)
					kachel.GetComponent<Renderer>().material.color = Color.black;
		}
		Camera.main.transform.position = new Vector3 (m_iSize/2, m_iSize/2, -10);
		Camera.main.orthographicSize = m_iSize/2;
		transform.position = new Vector3 (0.5f, 0.5f, 0);

		//KillAll ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			int iIndexX = (int)mouseWorldPos.x;
			int iIndexY = (int)mouseWorldPos.y;
			if (iIndexX >= 0 && iIndexY < m_iSize)
				Toggle (iIndexX, iIndexY);
		}

		if (Input.GetKeyDown (KeyCode.K) == true)
			KillAll ();
		if (Input.GetKeyDown (KeyCode.Space) == false)
			return;

		for (int iCol = 0; iCol < m_iSize; iCol++)
			for (int iRow = 0; iRow < m_iSize; iRow++) {
			int iNumAlive = GetAliveNeighbours(iCol, iRow);
			m_AliveNeighbours [iCol, iRow] = iNumAlive;
		}

		for (int iCol = 0; iCol < m_iSize; iCol++)
			for (int iRow = 0; iRow < m_iSize; iRow++) {
			int iNumAlive = GetAliveNeighbours(iCol, iRow);
			// if kachel alive
			if (GetAlive (iCol, iRow)){
				if (iNumAlive < 2 || iNumAlive > 3)
					SetAlive (iCol,iRow,false);
			// if kachel dead
			}else
				if (iNumAlive == 3)
					SetAlive (iCol,iRow,true);
			}
		}
	void Toggle(int _iCol, int _iRow)
	{
		bool bAlive = GetAlive (_iCol, _iRow);
		SetAlive (_iCol, _iRow, !bAlive);
	}
	bool GetAlive(int _iCol,int _iRow){
		return (m_Grid[_iCol,_iRow].GetComponent<Renderer>().material.color == Color.black); 
	}
	void SetAlive(int _iCol,int _iRow, bool _bAlive)
		{
		if(_bAlive)
			m_Grid[_iCol,_iRow].GetComponent<Renderer>().material.color = Color.black;
		else
			m_Grid[_iCol,_iRow].GetComponent<Renderer>().material.color = Color.white;
		}
	void KillAll()
	{
		for (int iCol = 0; iCol < m_iSize; iCol++)
			for (int iRow = 0; iRow < m_iSize; iRow++) {
			SetAlive (iCol,iRow,false);
		}
	}
	int GetAliveNeighbours(int _iColumn, int _iRow){
		int AliveNeighbours = 0;

		for (int iCol = _iColumn-1; iCol <= _iColumn+1; iCol++)
			for (int iRow = _iRow-1; iRow <= _iRow+1; iRow++) {
				if (iCol >= 0 && iCol < m_iSize && iRow >= 0 && iRow < m_iSize &&
			    	GetAlive(iCol,iRow))
					AliveNeighbours++;
		}

				if (GetAlive(_iColumn,_iRow))
			AliveNeighbours--;

		return AliveNeighbours;
		}
}
