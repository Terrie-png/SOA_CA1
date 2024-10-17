namespace SOA_CA1
{
	public static class FilterEnumExtensions
	{

		public static string ToFriendlyString(this FilterEnum filter)
		{
			return filter switch
			{
				FilterEnum.FreeEbooks => "free-ebooks",
				FilterEnum.PaidEbooks => "paid-ebooks",
				FilterEnum.AllBooks => "physical-books",
				_=> throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
			};
		}
	}
}
