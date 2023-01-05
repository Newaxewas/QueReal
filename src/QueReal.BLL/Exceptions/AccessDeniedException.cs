namespace QueReal.BLL.Exceptions
{
	public class AccessDeniedException : Exception
	{
		public AccessDeniedException() : base() { }
		public AccessDeniedException(string message) : base(message) { }
		
	}
}
