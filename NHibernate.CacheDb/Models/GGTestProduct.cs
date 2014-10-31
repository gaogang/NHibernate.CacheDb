using System;

namespace NHibernate.CacheDb.Models
{
    public class GGTestProduct
    {
        /// <summary>
        /// Id is the primary key
        /// </summary>
        public virtual int Id { get; set; }

        public virtual string Category { get; set; }

        public virtual bool IsExpired { get; set; }

        public virtual string Name { get; set; }
    }
}
