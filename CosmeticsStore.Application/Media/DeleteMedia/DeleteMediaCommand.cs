using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.DeleteMedia
{
    public class DeleteMediaCommand : IRequest<Unit>
    {
        public Guid MediaId { get; set; }
    }
}
