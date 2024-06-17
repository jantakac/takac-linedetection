using MathNet.Numerics.LinearAlgebra;

namespace LineDetection.MathImageProcessing
{
	public static class Math2DTools
	{
		//public const float Xmax = 100.0f;
		//public const float Ymax = 100.0f;

		//public const int Umax = 699;
		//public const int Vmax = 699;

		///// <summary>
		///// GetUV
		///// </summary>
		//public static Point GetUV(Matrix v)
		//{
		//	if ((v == null) || (v.Rows != 3 || v.Columns != 1))
		//		throw new ApplicationException($"Wrong vertex data input!");

		//	return new Point((int)Math.Round(v[0, 0] / Xmax * Umax), (int)Math.Round(Vmax - (v[1, 0] / Ymax * Vmax)));
		//}

		///// <summary>
		///// GetUVF
		///// </summary>
		//public static PointF GetUVF(Matrix v)
		//{
		//	if ((v == null) || (v.Rows != 3 || v.Columns != 1))
		//		throw new ApplicationException($"Wrong vertex data input!");

		//	return new PointF(v[0, 0] / Xmax * Umax, Vmax - (v[1, 0] / Ymax * Vmax));
		//}

		///// <summary>
		///// Get world coordinates X,Y from U,V
		///// </summary>
		//public static Matrix GetXY(Point p)
		//{
		//	return new Matrix(new float[,] { { p.X / (float)Umax * Xmax }, { Ymax - (p.Y / (float)Vmax * Ymax) }, { 1 } });
		//}

		///// <summary>
		///// BuildPointVector
		///// </summary>
		//public static Matrix BuildPointVector(float x, float y)
  //      {
		//	var vectorArray = new float[] { x, y, 1 };

  //          return new Matrix(vectorArray, Direction.Vertical);
  //      }

		///// <summary>
		///// BuildTranslationMatrix
		///// </summary>
		//public static Matrix BuildTranslationMatrix(float x, float y)
		//{
		//	var matrixValues = new float[,]
		//	{
		//		{ 1, 0, x },
		//		{ 0, 1, y },
		//		{ 0, 0, 1 }
		//	};

		//	return new Matrix(matrixValues);
		//}

		///// <summary>
		///// Create a rotation matrix that rotates values around point (0,0)
		///// </summary>
		//public static Matrix BuildRotationMatrix(float angle)
  //      {
		//	var matrixValues = new float[,]
		//	{
		//		{ (float)Math.Cos(angle), (float)-Math.Sin(angle), 0 },
		//		{ (float)Math.Sin(angle), (float)Math.Cos(angle), 0 },
		//		{ 0, 0, 1 }
		//	};

		//	return new Matrix(matrixValues);
		//}

		///// <summary>
		///// BuildScalingMatrix
		///// </summary>
		//public static Matrix BuildScalingMatrix(float xScale, float yScale)
  //      {
		//	var matrixValues = new float[,]
		//	{
		//		{ xScale, 0, 0 },
		//		{ 0, yScale, 0 },
		//		{ 0, 0, 1 }
		//	};

		//	return new Matrix(matrixValues);
		//}

		///// <summary>
		///// BuildSkewingMatrix
		///// </summary>
		//public static Matrix BuildSkewingMatrix(float xSkew, float ySkew)
  //      {
		//	var matrixValues = new float[,]
		//	{
		//		{ 1, (float)Math.Tan(xSkew), 0 },
		//		{ (float)Math.Tan(ySkew), 1, 0 },
		//		{ 0, 0, 1 }
		//	};

		//	return new Matrix(matrixValues);
		//}

		///// <summary>
		///// Calculates distance between two points
		///// </summary>
		//public static float CalculateDistanceBetweenPoints(Matrix point1, Matrix point2)
		//{
		//	float deltaX = Math.Abs(point1[0, 0] - point2[0, 0]);
		//	float deltaY = Math.Abs(point1[1, 0] - point2[1, 0]);

		//	return (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
		//}
	}
}
