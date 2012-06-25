using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Graphics;
using Toe.Marmalade.ResManager;

namespace Toe.Game.Windows
{
	public partial class GameWindow : Form
	{
		private readonly IwResManager resManager;

		private bool isFullScreen = true;

		/// <summary>
		/// Is window completely loaded.
		/// </summary>
		bool isLoaded = false;

		private CIwResGroup @group;

		public GameWindow(IwResManager resManager)
		{
			this.resManager = resManager;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			isLoaded = true;

			try
			{
				@group = this.resManager.LoadGroup(@"Z:\MyWork\toe.git\net4\src\Toe.Marmalade.Tests\TestData\bike.group.bin", false);
			}
			catch(Exception ex)
			{
				throw;
			}
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

			if (@group != null)
			{
				var models = @group.GetListNamed("CIwModel");
				if (models != null)
				{
					for (int i = 0; i < models.Size; ++i)
					{
						((CIwModel)models[i]).Render();
						break;
					}
				}
			}

			////GL.Color3(Color.Yellow);
			////GL.Begin(BeginMode.Triangles);
			////GL.Vertex2(10, 20);
			////GL.Vertex2(100, 20);
			////GL.Vertex2(100, 50);
			////GL.End();

			glControl.SwapBuffers();
		}

		private void ResizeGlControl(object sender, EventArgs e)
		{
			if (!isLoaded)
				return;
		}
	}
}
