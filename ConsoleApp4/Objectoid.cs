using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
	/// <summary>
	/// Obiectul acesta va fi plasat in mediul 3D. Sub influenta "gravitatiei", va cadea in "jos" 
	/// pana la atingerea "solului"
	/// </summary>
	public class Objectoid
	{
		private bool visibility;
		private bool isGravityBound;
		private Color colour;
		private List<Vector3> coordList;
		private Randomizer random;

		private const int GRAVITY_OFFSET = 1;

		/// <summary>
		/// Constructor. Initializarile vor fi plasate aici.
		/// </summary>
		public Objectoid(bool gravity_status)
		{
			random = new Randomizer();

			visibility = true;
			colour = random.RandomColor();

			isGravityBound = gravity_status;

			coordList = new List<Vector3>();
			int size_offset = random.RandomInt(3, 7);  //permite crearea unor obiecte cu un mic offset de dimensiune
			int height_offset = random.RandomInt(40, 60); //permite crearea unor obiecte plasate la un mic offset de inaltime
			int radial_offset = random.RandomInt(5, 15); //permite crearea unor obiecte cu un mic offset pe axa ox-oz pozitiv

			coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 1 * size_offset + radial_offset));
			coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 0 * size_offset + radial_offset));
			coordList.Add(new Vector3(1 * size_offset + radial_offset, 0 * size_offset + height_offset, 1 * size_offset + radial_offset));
			coordList.Add(new Vector3(1 * size_offset + radial_offset, 0 * size_offset + height_offset, 0 * size_offset + radial_offset));
			coordList.Add(new Vector3(1 * size_offset + radial_offset, 1 * size_offset + height_offset, 1 * size_offset + radial_offset));
			coordList.Add(new Vector3(1 * size_offset + radial_offset, 1 * size_offset + height_offset, 0 * size_offset + radial_offset));
			coordList.Add(new Vector3(0 * size_offset + radial_offset, 1 * size_offset + height_offset, 1 * size_offset + radial_offset));
			coordList.Add(new Vector3(0 * size_offset + radial_offset, 1 * size_offset + height_offset, 0 * size_offset + radial_offset));
			coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 1 * size_offset + radial_offset));
			coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 0 * size_offset + radial_offset));

		}

		public void Draw()
		{
			if (visibility)
			{
				GL.Color3(colour);
				GL.Begin(PrimitiveType.QuadStrip);
				foreach (Vector3 v in coordList)
				{
					GL.Vertex3(v);
				}
				GL.End();
			}
		}

		public void UpdatePosition(bool gravity_status)
		{
			if (visibility && gravity_status && !GroundCollisionDetection())
			{
				for (int i = 0; i < coordList.Count; i++)
				{
					coordList[i] = new Vector3(coordList[i].X, coordList[i].Y - GRAVITY_OFFSET, coordList[i].Z);
				}
			}
		}

		public bool GroundCollisionDetection()
		{
			foreach (Vector3 v in coordList)
			{
				if (v.Y <= 0) return true;
			}
			return false;
		}

		public void ToggleVisibility()
		{
			visibility = !visibility;
		}
	}
}
