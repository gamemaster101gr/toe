namespace Toe.Marmalade.Util
{
	public interface IResourceResolver
	{
		CIwManaged Resolve(uint type, uint hash);
	}
}