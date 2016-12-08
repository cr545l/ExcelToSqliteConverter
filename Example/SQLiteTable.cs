namespace FriendList
{
	public class Sheet1
	{
		[PrimaryKey, AutoIncrement]
		public System.Int32 ID;
		public System.String Name;
		public System.Int32 Age;
		public System.String PhoneNumber;
		public System.Double Score;
	}
}

namespace SaleList
{
	public class Sheet1
	{
		[PrimaryKey, AutoIncrement]
		public System.Int32 ID;
		public System.String Name;
		public System.Int32 Price;
		public System.String ExpirationDate;
	}
}

