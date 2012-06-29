using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using Toe.Marmalade;
using Toe.Marmalade.Graphics;
using Toe.Marmalade.ResManager;

namespace Toe.Game.Windows
{
	/// <summary>
	/// The game window.
	/// </summary>
	public partial class GameWindow : Form
	{
		#region Constants and Fields

		private readonly IwResManager resManager;

		private readonly Timer timer = new Timer();

		private float angle = (float)Math.PI / 4;

		private CIwResGroup @group;

		private bool isFullScreen;

		/// <summary>
		/// Is window completely loaded.
		/// </summary>
		private bool isLoaded;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GameWindow"/> class.
		/// </summary>
		/// <param name="resManager">
		/// The res manager.
		/// </param>
		public GameWindow(IwResManager resManager)
		{
			this.resManager = resManager;
			this.InitializeComponent();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets a value indicating whether FullScreen.
		/// </summary>
		public bool FullScreen
		{
			get
			{
				return this.isFullScreen;
			}

			set
			{
				if (value == this.isFullScreen)
				{
					return;
				}

				this.isFullScreen = value;

				this.ApplyScreenMode();
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// The on closing.
		/// </summary>
		/// <param name="e">
		/// The e.
		/// </param>
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			this.timer.Stop();
			if (this.@group != null)
			{
				this.resManager.DestroyGroup(this.@group);
			}
		}

		/// <summary>
		/// The on load.
		/// </summary>
		/// <param name="e">
		/// The e.
		/// </param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.isLoaded = true;
			this.timer.Interval = 1000 / 30;
			this.timer.Tick += (s, a) => { this.glControl.Invalidate(); };

			try
			{
				this.@group =
					this.resManager.LoadGroup(
						@"Z:\MyWork\toe.git\net4\src\Toe.Marmalade.Tests\TestData\data-sw\scalablepipeline\bike.group.bin", false);

				// @group = this.resManager.LoadGroup(@"Z:\MyWork\toe.git\net4\src\Toe.Marmalade.Tests\TestData\data-gles1\scalablepipeline\bike.group.bin", false);
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				throw;
			}

			this.ApplyScreenMode();
			this.timer.Start();
		}

		private void ApplyScreenMode()
		{
			if (this.isFullScreen)
			{
				this.FormBorderStyle = FormBorderStyle.None;
				this.TopMost = true;

				// DisplayDevice.Primary.ChangeResolution(width, height, 32, 60.0f);
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.FormBorderStyle = FormBorderStyle.Sizable;
				this.TopMost = false;

				// DisplayDevice.Primary.ChangeResolution(width, height, 32, 60.0f);
				this.WindowState = FormWindowState.Normal;
			}
		}

		private void RenderGameScreen(object sender, PaintEventArgs e)
		{
			if (!this.isLoaded)
			{
				return;
			}

			GL.ClearColor(Color.SkyBlue);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			int w = this.glControl.Width;
			int h = this.glControl.Height;
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();

			int hh = 2000 * h / w;

			GL.Ortho(-2000, 2000, -hh, hh, -2000, 2000); // Верхний левый угол имеет кооординаты(0, 0)
			GL.Viewport(0, 0, w, h); // Использовать всю поверхность GLControl под рисование
			S3E.CheckOpenGLStatus();

			GL.Enable(EnableCap.DepthTest);

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			this.angle += (float)Math.PI / 200;
			Matrix4 m = Matrix4.CreateRotationY(this.angle);
			GL.LoadMatrix(ref m);
			S3E.CheckOpenGLStatus();
			if (this.@group != null)
			{
				var models = this.@group.GetListNamed("CIwModel");
				if (models != null)
				{
					for (int i = 0; i < models.Size; ++i)
					{
						((CIwModel)models[i]).Render();

						// break;
					}
				}
			}

			////GL.Color3(Color.Yellow);
			////GL.Begin(BeginMode.Triangles);
			////GL.Vertex2(10, 20);
			////GL.Vertex2(100, 20);
			////GL.Vertex2(100, 50);
			////GL.End();
			// 
			S3E.CheckOpenGLStatus();

			this.glControl.SwapBuffers();
		}

		private void ResizeGlControl(object sender, EventArgs e)
		{
			if (!this.isLoaded)
			{
				return;
			}
		}

		#endregion
	}
}