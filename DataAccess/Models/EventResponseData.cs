namespace DataAccess.Models
{
    public class EventResponseData
    {
		private bool _success;

		public bool Sucess
		{
			get { return _success; }
			set { _success = value; }
		}

		private string message = "";

		public string Message
		{
			get { return message; }
			set { message = value; }
		}



	}
}