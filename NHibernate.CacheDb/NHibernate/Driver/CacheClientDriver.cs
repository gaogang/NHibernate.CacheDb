using System;

namespace NHibernate.Driver
{
    /// <summary>
    /// Author: Werner Kolov
    /// </summary>
    public class CacheClientDriver : ReflectionBasedDriver
    {
		public CacheClientDriver() :
			base(
            "InterSystems.Data.CacheClient",
            "InterSystems.Data.CacheClient",
            "InterSystems.Data.CacheClient.CacheConnection",
            "InterSystems.Data.CacheClient.CacheCommand"
            ) 
        {
        }

		public override bool UseNamedPrefixInSql
		{
			get { return false; }
		}

		public override bool UseNamedPrefixInParameter
		{
            get { return false; }
		}

		public override string NamedPrefix
		{
			get { throw new InvalidOperationException("This method must never be called."); }
		}
	}        
}