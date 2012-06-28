#include "s3e.h"
#include "IwGx.h"
#include "IwGraphics.h"
#include "IwUtil.h"
#include "IwAnim.h"
#include "IwModelBlockGL.h"

// Attempt to lock to 25 frames per second
#define MS_PER_FRAME (1000 / 25)

typedef void (CIwModelBlockGLPrimBase::* SerialisePtr)();

//-----------------------------------------------------------------------------
// Main global function
//-----------------------------------------------------------------------------
int main()
{
	//IwGx can be initialised in a number of different configurations to help the linker eliminate unused code.
	//Normally, using IwGxInit() is sufficient.
	//To only include some configurations, see the documentation for IwGxInit_Base(), IwGxInit_GLRender() etc.
	IwGxInit();
	IwGraphicsInit();
	//Initialises all core functionality for IwResManager.
	IwResManagerInit();
	IwAnimInit();

	// Set screen clear colour
	IwGxSetColClear(0xff, 0xff, 0xff, 0xff);
	IwGxPrintSetColour(128, 128, 128);

	//{
	//	IwSerialiseOpen("squat.bin",false);
	//	CIwSQuat w (IW_SFIXED(1),0,0,0);
	//	w.Serialise();
	//	CIwSQuat x (0,IW_SFIXED(1),0,0);
	//	x.Serialise();
	//	CIwSQuat y (0,0,IW_SFIXED(1),0);
	//	y.Serialise();
	//	CIwSQuat z (0,0,0,IW_SFIXED(1));
	//	z.Serialise();
	//	IwSerialiseClose();
	//}

		/*CIwManagedList l;
	l.Add(new CIwTexture());
	l.Add(new CIwTexture());
	
	IwSerialiseOpen("managedlist.bin",false);
	l.Serialise();
	IwSerialiseClose();*/

	void* readptr = (void*)s3eFileRead;
	void* writeptr = (void*)s3eFileWrite;
	void* seekptr = (void*)s3eFileSeek;
	void* openptr = (void*)s3eFileOpen;
	SerialisePtr sptr = &CIwModelBlockGLPrimBase::Serialise;

	//IwSerialiseOpen("managedobject_mips.bin",false);
	//CIwTexture*t = new CIwTexture();
	//CIwImage* img = new CIwImage();|
	//img->LoadFromFile("testdata\\mip16.png");
	//t->SetImage(img);
	//t->SetMipMapping(true);
	//CIwManaged*m = t;
	//IwSerialiseManagedObject(m);
	//IwSerialiseClose();
	
	//IwGetResManager()->LoadGroup("testdata\\empty.group");
//	IwGetResManager()->LoadGroup("testdata\\texture.group");
	CIwResGroup* g =  IwGetResManager()->LoadGroup("testdata\\material.group");
	//CIwModel* geo = (CIwModel*)g->GetResNamed("model","CIwModel",0);
	
	//CIwMaterial* mat = (CIwMaterial*)g->GetResNamed("material","CIwMaterial",0);
	//CIwTexture* t = (CIwTexture*)g->GetResNamed("texture","CIwTexture",0);
	//CIwTexture* t2 = (CIwTexture*)g->GetResNamed("texture_p","CIwTexture",0);
	IwGetResManager()->DestroyGroup(g);
	
	
	while (1)
	{
		s3eDeviceYield(0);
		s3eKeyboardUpdate();
		s3ePointerUpdate();

		int64 start = s3eTimerGetMs();

		bool result = true;//ExampleUpdate();
		if  (
			(result == false) ||
			(s3eKeyboardGetState(s3eKeyEsc) & S3E_KEY_STATE_DOWN) ||
			(s3eKeyboardGetState(s3eKeyAbsBSK) & S3E_KEY_STATE_DOWN) ||
			(s3eDeviceCheckQuitRequest())
			)
			break;

		// Clear the screen
		IwGxClear(IW_GX_COLOUR_BUFFER_F | IW_GX_DEPTH_BUFFER_F);

		IwGxFlush();
		IwGxSwapBuffers();

		// Attempt frame rate
		while ((s3eTimerGetMs() - start) < MS_PER_FRAME)
		{
			int32 yield = (int32) (MS_PER_FRAME - (s3eTimerGetMs() - start));
			if (yield<0)
				break;
			s3eDeviceYield(yield);
		}
	}
	//Shuts down IwResManager and frees all memory.
	IwResManagerTerminate();

	IwGxTerminate();
	return 0;
}