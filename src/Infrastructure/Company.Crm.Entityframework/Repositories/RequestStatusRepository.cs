using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Crm.Entityframework.Repositories;

public class RequestStatusRepository : BaseRepository<AppDbContext, RequestStatus>, IRequestStatusRepository
{
    public RequestStatusRepository(AppDbContext context) : base(context)
    {
    }
}
