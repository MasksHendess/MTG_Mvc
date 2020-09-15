using MTG_Mvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Repositories
{
    public interface IcardRepositoryInterface
    {
        bool checkIfCardExsists(card card);
        card GetCard(card Card);
    }
}
