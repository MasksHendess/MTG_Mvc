using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Services
{
    public class cardService
    {
        private readonly IcardRepositoryInterface itestRepository;
        public cardService(IcardRepositoryInterface _itestRepository)
        {
            itestRepository = _itestRepository;
        }

        bool checkIfCardExsists(card card)
        {
          return  itestRepository.checkIfCardExsists(card);
        }
    }
}
