using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions.Base
{
    public abstract class DomainException : Exception
    {
        public virtual string Title { get; } = "Domain Error";

        protected DomainException(string message) : base(message) { }
    }
}
