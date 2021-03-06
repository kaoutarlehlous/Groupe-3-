using System;
using System.Collections;

namespace Sudoku
{
	/// <summary>
	/// Summary description for SudokuGenome.
	/// </summary>
	public class Sudokufitness : SudokuChromesome
	{
		protected int[,] TheArray = new int[9,9];
		public static Random TheSeed = new Random((int)DateTime.Now.Ticks);
		protected int TheMin = 0;
		protected int TheMax = 100;

		public int this [int index1, int index2]
		{
			get
			{
				return TheArray[index1, index2];
			}
		}

		public override int CompareTo(object a)
		{
			Sudokufitness Gene1 = this;
			Sudokufitness Gene2 = (Sudokufitness)a;
			return Math.Sign(Gene2.CurrentFitness  -  Gene1.CurrentFitness);
		}


		public override void SetCrossoverPoint(int crossoverPoint)
		{
			CrossoverPoint = 	crossoverPoint;
		}

		public Sudokufitness()
		{

		}


		public Sudokufitness(long length, object min, object max)
		{
			//
			// TODO: Add constructor logic here
			//
			Length = length;
			TheMin = (int)min;
			TheMax = (int)max;
			for (int i = 0; i < 9; i++)
			{
				int offset = TheSeed.Next(9);
				for (int j = 0; j < 9; j++)
				{
					TheArray[j,i] = (int)GenerateGeneValue(1, 9);
				}
			}
		}

		public override void Initialize()
		{

		}

		public override bool CanDie(float fitness)
		{
			if (CurrentFitness <= (int)(fitness * 100.0f))
			{
				return true;
			}

			return false;
		}


		public override bool CanReproduce(float fitness)
		{
			if (Sudokufitness.TheSeed.Next(100) >= (int)(fitness * 100.0f))
			{
				return true;
			}

			return false;
		}


		public override object GenerateGeneValue(object min, object max)
		{
			return TheSeed.Next((int)min, (int)max + 1);
		}

		public override void Mutate()
		{
			int MutationIndex1 = TheSeed.Next((int)9);
			int MutationIndex2 = TheSeed.Next((int)9);
			int MutationIndex3 = TheSeed.Next((int)9);

			if (TheSeed.Next(2) == 1)
			{
				TheArray[MutationIndex1, MutationIndex2] = MutationIndex3 + 1;
			}
			else
			{
				int temp = 0;
				// switch 2
				if (TheSeed.Next(2) == 1)
				{
					// switch two in a row
					temp = TheArray[MutationIndex1, MutationIndex2];
					TheArray[MutationIndex1, MutationIndex2] = TheArray[MutationIndex3, MutationIndex2];
					TheArray[MutationIndex3, MutationIndex2] = temp;

				}
				else
				{
					// switch two in a column
					temp = TheArray[MutationIndex2, MutationIndex1];
					TheArray[MutationIndex2, MutationIndex1] = TheArray[MutationIndex2, MutationIndex3];
					TheArray[MutationIndex2, MutationIndex3] = temp;
				}
			}

			/*
			int MutationIndex1 = TheSeed.Next((int)9);
			int MutationIndex2 = TheSeed.Next((int)9);
			int MutationIndex3 = TheSeed.Next((int)9);
			int temp = TheArray[MutationIndex1, MutationIndex3];
			TheArray[MutationIndex1, MutationIndex3] = TheArray[MutationIndex2, MutationIndex3]; // switch them
			TheArray[MutationIndex2, MutationIndex3] = temp; // switch them
			*/

		}


		Hashtable RowMap = new Hashtable();
		Hashtable ColumnMap = new Hashtable();
		Hashtable SquareMap = new Hashtable();


		/// <summary>
		/// The Calculate Sudoku Fitness uses the uniqueness of columns, rows
		/// and 3x3 squares in the grid to determine a fitness value
		/// </summary>
		/// <returns></returns>
		private float CalculateSudokuFitness()
		{
			// set fitnesses for columns, rows, and squares initially to 0
			float fitnessColumns = 0;
			float fitnessRows = 0;
			float fitnessSquares = 0;

			// go through each column
			for (int i = 0; i < 9; i++)
			{
				// Go through each cell in a column, add it to the ColumnMap according
				// to the cell value
				ColumnMap.Clear();
				for (int j = 0; j < 9; j++)
				{
					 // check for uniqueness in row
					if (ColumnMap[TheArray[i,j]] == null)
					{
						ColumnMap[TheArray[i,j]] = 0;
					}

					 ColumnMap[TheArray[i,j]] = ((int)ColumnMap[TheArray[i,j]]) + 1;
				}

				// accumulate the column fitness based on the number of entries in the ColumnMap
				fitnessColumns += (float)(1.0f/ (10-ColumnMap.Count))/9.0f;
				//fitnessColumns += (float)Math.Exp(ColumnMap.Count*10 - 90)/9;
			}

			// go through each row next
			for (int i = 0; i < 9; i++)
			{
				// Go through each cell in a row, add it to the RowMap according
				// to the cell value
				RowMap.Clear();
				for (int j = 0; j < 9; j++)
				{
					// check for uniqueness in row
					if (RowMap[TheArray[j,i]] == null)
					{
						RowMap[TheArray[j,i]] = 0;
					}

					RowMap[TheArray[j,i]] = ((int)RowMap[TheArray[j,i]]) + 1;
				}

				// accumulate the row fitness based on the number of entries in the RowMap
				fitnessRows += (float)(1.0f/ (10-RowMap.Count))/9.0f;
			//	fitnessRows += (float)Math.Exp(RowMap.Count*10 - 90)/9;
			}

			// go through square next
			for (int l = 0; l < 3; l++)
			{
				for (int k = 0; k < 3; k++)
				{
					// Go through each cell in a 3 x 3 square, add it to the SquareMap according
					// to the cell value
					SquareMap.Clear();
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							// check for uniqueness in row
							// check for uniqueness in row
							if (SquareMap[TheArray[i + k*3,j + l*3]] == null)
							{
								SquareMap[TheArray[i+k*3,j+l*3]] = 0;
							}

							// accumulate the square fitness based on the number of entries in the SquareMap
							SquareMap[TheArray[i + k*3,j + l*3]] = ((int)SquareMap[TheArray[i + k*3,j + l*3]]) + 1;
						}
					}

					fitnessSquares += (float)(1.0f/ (10-SquareMap.Count))/9.0f;
				}

			}

			// The fitness of the entire Sudoku Grid is the product
			// of the column fitness, row fitness and 3x3 square fitness
			CurrentFitness = fitnessColumns * fitnessRows * fitnessSquares;

			return CurrentFitness;
		}


		public override float CalculateFitness()
		{
			CalculateSudokuFitness();
//			CurrentFitness = CalculateClosestRatioToNaturalLog();
//			CurrentFitness = CalculateClosestRatioToPi();
//			CurrentFitness = CalculateNumbersProducingProductsWithSameDigitsAsFirst();
//			CurrentFitness = CalculateClosestProductSum();
//			CurrentFitness =  CalculateClosestSumTo10();
			return CurrentFitness;
		}

		public override string ToString()
		{
			string strResult = "";
			for (int j = 0; j < Length; j++)
			{
				for (int i = 0; i < Length; i++)
				{
					strResult = strResult + ((int)TheArray[i, j]).ToString() + " ";
				}
				strResult += "\r\n";
			}

			strResult += "-->" + CurrentFitness.ToString();

			return strResult;
		}



		public override void CopyGeneInfo(SudokuChromesome dest)
		{
			Sudokufitness theGene = (Sudokufitness)dest;
			theGene.Length = Length;
			theGene.TheMin = TheMin;
			theGene.TheMax = TheMax;
		}


		public override SudokuChromesome Crossover(SudokuChromesome g)
		{
			Sudokufitness aGene1 = new Sudokufitness();
			Sudokufitness aGene2 = new Sudokufitness();
			g.CopyGeneInfo(aGene1);
			g.CopyGeneInfo(aGene2);


			Sudokufitness CrossingGene = (Sudokufitness)g;
			if (TheSeed.Next(2) == 1)
			{
				for (int j = 0; j < 9; j++)
				{
					CrossoverPoint = TheSeed.Next(8) + 1;
					for (int k = 0; k < CrossoverPoint; k++)
					{
						aGene1.TheArray[k,j] = CrossingGene.TheArray[k, j];
						//						aGene1.TheArray[8 - k,j] = TheArray[8-k, j];
						aGene2.TheArray[k ,j] = TheArray[k, j];
						//						aGene2.TheArray[8 - k ,j] = CrossingGene.TheArray[8 - k,j];
					}

					for (int k = CrossoverPoint; k < 9; k++)
					{
						aGene2.TheArray[k,j] = CrossingGene.TheArray[k, j];
						//						aGene1.TheArray[8 - k,j] = TheArray[8-k, j];
						aGene1.TheArray[k ,j] = TheArray[k, j];
						//						aGene2.TheArray[8 - k ,j] = CrossingGene.TheArray[8 - k,j];
					}
				}
			}
			else
			{
				for (int j = 0; j < 9; j++)
				{
					CrossoverPoint = TheSeed.Next(8) + 1;
					for (int k = 0; k < CrossoverPoint; k++)
					{
						aGene1.TheArray[j,k] = CrossingGene.TheArray[j, k];
						//						aGene1.TheArray[8 - k,j] = TheArray[8-k, j];
						aGene2.TheArray[j ,k] = TheArray[j, k];
						//						aGene2.TheArray[8 - k ,j] = CrossingGene.TheArray[8 - k,j];
					}

					for (int k = CrossoverPoint; k < 9; k++)
					{
						aGene2.TheArray[j,k] = CrossingGene.TheArray[j, k];
						//						aGene1.TheArray[8 - k,j] = TheArray[8-k, j];
						aGene1.TheArray[j ,k] = TheArray[j, k];
						//						aGene2.TheArray[8 - k ,j] = CrossingGene.TheArray[8 - k,j];
					}
				}
			}


			


/*
			SudokuGenome CrossingGene = (SudokuGenome)g;
			for (int i = 0; i < CrossoverPoint; i++)
			{
				for (int j = 0; j < CrossoverPoint; j++)
				{
					aGene1.TheArray[i,j] = CrossingGene.TheArray[i,j];
					aGene2.TheArray[i,j] = TheArray[i, j];
				}
			}

			for (int i = CrossoverPoint; i < 9; i++)
				for (int j = CrossoverPoint; j < 9; j++)
				{
					aGene1.TheArray[i,j] =  TheArray[i,j];
					aGene2.TheArray[i,j] = CrossingGene.TheArray[i,j];
				}

			for (int i = CrossoverPoint; i < 9; i++)
				for (int j = 0; j < CrossoverPoint; j++)
				{
					aGene1.TheArray[i,j] =  TheArray[i,j];
					aGene2.TheArray[i,j] = CrossingGene.TheArray[i,j];
				}

			for (int i = 0; i < CrossoverPoint; i++)
				for (int j = CrossoverPoint; j < 9; j++)
				{
					aGene1.TheArray[i,j] =  CrossingGene.TheArray[i,j];
					aGene2.TheArray[i,j] = TheArray[i,j];
				}
*/				

			// 50/50 chance of returning gene1 or gene2
			Sudokufitness aGene = null;
			if (TheSeed.Next(2) == 1)			
			{
				aGene = aGene1;
			}
			else
			{
				aGene = aGene2;
			}

			return aGene;
		}

	}
}
