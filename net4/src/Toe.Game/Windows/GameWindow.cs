using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK.Graphics.OpenGL;

namespace Toe.Game.Windows
{
	public partial class GameWindow : Form
	{
		private bool isFullScreen = true;

		/// <summary>
		/// Is window completely loaded.
		/// </summary>
		bool isLoaded = false;

		public GameWindow()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			isLoaded = true;
			this.ApplyScreenMode();
		}

		public bool FullScreen
		{
			get
			{
				return this.isFullScreen;
			}
			set
			{
				if (value == this.isFullScreen)
					return;
				this.isFullScreen = value;

				this.ApplyScreenMode();
			}
		}

		private void ApplyScreenMode()
		{
			if (this.isFullScreen)
			{
				this.FormBorderStyle = FormBorderStyle.None;
				this.TopMost = true;
				//DisplayDevice.Primary.ChangeResolution(width, height, 32, 60.0f);
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.FormBorderStyle = FormBorderStyle.Sizable;
				this.TopMost = false;
				//DisplayDevice.Primary.ChangeResolution(width, height, 32, 60.0f);
				this.WindowState = FormWindowState.Normal;
			}
		}

		private void RenderGameScreen(object sender, PaintEventArgs e)
		{
			if (!isLoaded)
				return;
			GL.ClearColor(Color.SkyBlue);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			int w = glControl.Width;
			int h = glControl.Height;
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, w, 0, h, -1, 1); // Верхний левый угол имеет кооординаты(0, 0)
			GL.Viewport(0, 0, w, h); // Использовать всю поверхность GLControl под рисование


			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Color3(Color.Yellow);
			GL.Begin(BeginMode.Triangles);
			GL.Vertex2(10, 20);
			GL.Vertex2(100, 20);
			GL.Vertex2(100, 50);
			GL.End();

			glControl.SwapBuffers();
		}

		private void ResizeGlControl(object sender, EventArgs e)
		{
			if (!isLoaded)
				return;
		}
	}
}
